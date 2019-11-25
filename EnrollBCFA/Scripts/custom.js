
$(document).ready(function () {


    // Print Module
    $(".btnPrint").printPage();

    var user = $(".AccountUsername").text();
    if (user === "admin") {
        $("#btnmngaccount").css('visibility', 'visible');
        $('#useraccount').attr('readonly', 'true');
    } else if (user !== "admin") {
        $("#btnmngaccount").css('visibility', 'hidden');
        $('#useraccount').removeAttr('readonly');
    }

    $(function () {
        // get open academic year
        $.ajax({
            url: 'Schedule/GetOpenSchoolYear',
            type: "Get",
            dataType: "json",
            success: function (data) {
                $("#academicyear").val(data);
                $("#academicyearenroll").val(data);
                $("#schoolyearhome").text(data);
                $("#stAcadYear").text(data);
            }
        });

        // get count
        $.ajax({
            url: 'Bcfa/CountStudent',
            type: "Get",
            dataType: "json",
            success: function (data) {
                $("#studentenrolledcount").text(data);

                $.ajax({
                    url: 'Bcfa/CountTeacher',
                    type: "Get",
                    dataType: "json",
                    success: function (data) {
                        $("#teachercount").text(data);

                        $.ajax({
                            url: 'Bcfa/CountSection',
                            type: "Get",
                            dataType: "json",
                            success: function (data) {
                                $("#sectioncount").text(data);
                            }
                        });
                    }
                });
            }
        });
    });

    // open modal account
    $("#manage-account").click(function () {
        $("#modalManageAccount").modal({ backdrop: "static", refresh: true });
    });
    $("#changepassword").click(function () {
        $("#modalChangePassword").modal({ backdrop: "static", refresh: true });
    });
    $("#savechangespassword").click(function () {
        var id = $("#accountidcp").val();
        var user = $("#useraccountcp").val();
        if (id.length === 0 || user.length === 0) {
            alert("Account not found.");
        } else {
            var oldpassword = $("#passaccount").val();
            var oldpasswordcp = $("#oldpassword").val();
            if (oldpassword !== oldpasswordcp) {
                $("#modalAccountFailed1").modal({ backdrop: "static" });
            } else {
                var pass1 = $("#newpassword").val();
                var pass2 = $("#confirmpassword").val();

                var x = $("#newpassword").val().length;
                var y = $("#confirmpassword").val().length;
                if ( x !== 0 && y !== 0) {
                    if (x !== 1 && y !== 1) {
                        if (x !== 2 && y !== 2) {
                            if (x !== 3 && y !== 3) {
                                if (pass1 !== pass2) {
                                    $("#modalAccountFailed2").modal({ backdrop: "static" });
                                } else {
                                    $.ajax({
                                        url: 'Bcfa/UpdateAccountPassword',
                                        data: { "accountid": id, "newpassword": pass1 },
                                        type: "POST",
                                        success: function () {
                                            $.post('/Login/SetSessionPassword',
                                                { "newpassword": pass2 }, function (data) {
                                                    $('#modalChangePassword').modal('toggle');
                                                    $("#modalAccountSuccess").modal({ backdrop: "static" });
                                                });
                                            $("#savecompleteclose").click(function () {
                                                location.reload();
                                            });
                                        }
                                    });
                                }
                            } else {
                                $("#modalAccountFailed3").modal({ backdrop: "static" });
                            }
                        } else {
                            $("#modalAccountFailed3").modal({ backdrop: "static" });
                        }
                    } else {
                        $("#modalAccountFailed3").modal({ backdrop: "static" });
                    }
                } else {
                    $("#modalAccountFailed3").modal({ backdrop: "static" });
                }
            }
        }
    });

    // Manage Account
    $(function () {
        // Display all User
        $.ajax({
            url: 'ManageAccount/LoadAccount',
            type: "Get",
            dataType: "html",
            success: function (data) {
                $("#account-partial").html(data);
            }
        });

        $("#btnDeleteAccount").click(function () {
            var accountid = $("#accountidmu").val();
            var username = $("#usernamemu").val();
            if (accountid.length === 0 || username === "") {
                $("#modalDeleteAccountFailed").modal({ backdrop: "static" });
            } else {
                $("#lblaccountid").text(accountid);
                $("#lbluseraccount").text(username);
                $("#modalDeleteAccountConfirm").modal({ backdrop: "static" });
            }
        });

        $("#confirmDeleteAccount").click(function () {
            var accountid = $("#accountidmu").val();
            $.ajax({
                url: 'ManageAccount/Delete',
                data: { "accountid": accountid },
                type: "POST",
                dataType: "html",
                success: function () {
                    $("#modalDeleteAccountSuccess").modal({ backdrop: "static" });
                    $("#btnReloadAccount").click();
                }
            });
        });
        $("#btnReloadAccount").click(function () {
            $.ajax({
                url: 'ManageAccount/LoadAccount',
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#account-partial").html(data);
                }
            });
        });
        $("#btnCreateAccount").click(function () {
            $.ajax({
                url: 'ManageAccount/GetAccountID',
                type: "Get",
                dataType: "html",
                success: function (data) {
                    var datetoday = $.datepicker.formatDate('yy/mm/dd', new Date());
                    $("#accountidcr").val(data);
                    $("#accountcreatedcr").val(datetoday);
                    $("#modalCreateAccount").modal({ backdrop: "static" });
                }
            });
        });

        $("#btnCreateAccountNow").click(function () {
            var accountid = $("#accountidcr").val();
            var lastname = $("#lnamecr").val();
            var firstname = $("#fnamecr").val();
            var username = $("#usernamecr").val();
            var accountcreated = $("#accountcreatedcr").val();
            var password = $("#passwordcr").val();
            var conpassword = $("#confirmpasswordcr").val();
            var lastlogin = "";
            if (accountid.length === 0 || lastname === "" || firstname === "" || username === "" || password === "") {
                $("#modalCreateAccountFailed1").modal({ backdrop: "static" });
            } else {
                if (conpassword === password) {
                    $.ajax({
                        url: 'ManageAccount/Create',
                        type: "POST",
                        data: { "AccountID": accountid, "LastName": lastname, "FirstName": firstname, "Username": username, "LastLogin": lastlogin, "Password": password, "AccountCreated": accountcreated },
                        success: function () {
                            $('#modalCreateAccount').modal('toggle');
                            $("#modalCreateAccountSuccess").modal({ backdrop: "static" });

                            $("#completecreate").click(function () {
                                location.reload();
                            });
                        }
                    });
                } else {
                    $("#modalCreateAccountFailed2").modal({ backdrop: "static" });
                }
            }

            
        });
    });

    // account module
    $(function () {
        $("#savechangesaccount").click(function (e) {
            
            var accid = $("#accountid").val();
            var lname = $("#lnameaccount").val();
            var fname = $("#fnameaccount").val();
            var username = $("#useraccount").val();
            if (lname.length === 0 || fname.length === 0 || username.length === 0) {
                alert("All fields are required.");
            } else {
                $.ajax({
                    url: 'Bcfa/UpdateAccount',
                    data: { "lname": lname, "fname": fname, "username": username, "accountid": accid },
                    type: "POST",
                    success: function () {
                        // set session value
                        $.post('/Login/SetSessionVariable',
                            { "uservalue": username, "fvalue": fname, "lvalue": lname }, function (data) {
                                $('#modalManageAccount').modal('toggle');
                                $("#modalAccountSuccess").modal({ backdrop: "static" });
                            });
                        $("#savecompleteclose").click(function () {
                            location.reload();
                        });
                    }
                });
            }

        });
    });

    // disable some control
    //$("#subjectcode").prop("disabled", true);
    $("#descriptivetitle").prop("disabled", true);
    $("#academicyear").prop("disabled", true);
    $("#sectionadviser").prop("disabled", true);
    $("#subjectcode").prop("disabled", true);
    $("#teacherid").prop("disabled", true);

    // Display all Enrolled Student
    var nameofstudent = "";
    $.ajax({
        url: 'ES/LoadES',
        data: { "nameofstudent": nameofstudent },
        type: "Get",
        dataType: "html",
        success: function (data) {
            $("#es-partial").html(data);
        }
    });

    // Class List
    $(function () {
        $("#varlevelclass").change(function () {
            var level = $("#varlevelclass option:selected").text();
            var url = 'ClassList/Section';
            var data1 = { "level": level };
            $.post(url, data1, function (data) {
                var items = [];
                items.push("<option value=" + 0 + ">" + "- select section -" + "</option>");
                for (var i = 0; i < data.length; i++) {
                    items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                }
                $("#varsectionclass").html(items.join(' '));
            });
        });

        $("#loadclass").click(function () {
            var sectionname = $("#varsectionclass option:selected").val();
            if (sectionname === "- select section -" || sectionname === "" || sectionname === "0") {
                $("#modalClassLoadFailed").modal({ backdrop: "static" });
            } else {
                $.ajax({
                    url: 'ClassList/LoadClass',
                    data: { "sectionname": sectionname },
                    type: "Get",
                    dataType: "html",
                    success: function (data) {
                        $("#class-partial").html(data);
                    }
                });
            }
        });
    });
    // Enrolled Student
    $(function () {
        $("#btnSearchEnrolled").click(function () {
            $("#esstudentid").val("");
            $("#esname").val("");

            var nameofstudent = $("#comboboxSearch").val();
            $.ajax({
                url: 'ES/LoadES',
                data: { "nameofstudent": nameofstudent },
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#es-partial").html(data);
                }
            });
        });
        $("#btnDropStudent").click(function () {
            var studentid = $("#esstudentid").val();
            var name = $("#esname").val();
            if (studentid.length === 0 || studentid === "") {
                $("#myModalDropFail").modal({ backdrop: "static" });
            } else {
                $("#myModalDropConfirm").modal({ backdrop: "static" });
            }
        });
        $("#confirmDrop").click(function () {
            var studentid = $("#esstudentid").val();
            $.ajax({
                url: 'ES/DropES',
                data: { "studentid": studentid },
                type: "POST",
                dataType: "html",
                success: function () {
                    $("#esstudentid").val("");
                    $("#esname").val("");
                    $("#myModalDropSuccess").modal({ backdrop: "static" });
                    $("#btnSearchEnrolled").click();
                }
            });
        });
        
    });
    // Enrollment events
    $(function () {
        $("#btnSearchEnroll").click(function () {
            ClearTextboxEnroll();

            var fullname = $("#comboboxSearch").val();
            if (fullname !== null && fullname.length > 1) {
                var splitname = fullname.split(", ");
                var lastname = splitname[0];
                var firstname = splitname[1];
                var middlename = splitname[2];
                $("#nameenroll").val(fullname);
                $.ajax({
                    url: 'Enrollment/GetStudentInfo',
                    data: { "lastname": lastname, "firstname": firstname, "middlename": middlename },
                    type: "GET",
                    dataType: "json",
                    success: function (data) {
                        $("#studentidenroll").val(data.StudentID);
                        $("#typeenroll").val(data.StType);

                        // check if enrolled or not
                        var nameofstudent = $("#comboboxSearch").val();
                        $.ajax({
                            url: 'Enrollment/isEnrolled',
                            data: { "nameofstudent": nameofstudent },
                            type: "GET",
                            dataType: "json",
                            success: function (data) { // record found == enrolled
                                $("#gradelevelenroll").empty();
                                $("#sectionenroll").empty();

                                $("#statusenroll").val("Enrolled");
                                $("#enrollmentidenroll").val(data.EnrollmentID);
                                $("#gradelevelenroll").append('<option>' + data.GradeLevel + '</option>');
                                $("#sectionenroll").append('<option>' + data.Section + '</option>');
                            },
                            error: function () { // no record found == not enrolled
                                $("#gradelevelenroll").empty();
                                $("#sectionenroll").empty();
                                $("#statusenroll").val("Not Enrolled");

                                // get enrollmentID
                                var currentYear = (new Date).getFullYear();
                                var currentMonth = (new Date).getMonth();
                                $.ajax({
                                    url: 'Enrollment/GetEnrollmentID',
                                    type: "GET",
                                    dataType: "json",
                                    success: function (data) {
                                        var enrollmentID = "" + currentYear + currentMonth + data;
                                        $("#enrollmentidenroll").val(enrollmentID);
                                    }
                                });
                                
                                
                                // get level base on student type
                                var type = $("#typeenroll").val();
                                var url = 'Enrollment/GradeLevel';
                                var data1 = { "type": type };
                                $.post(url, data1, function (data) {
                                    var items = [];
                                    items.push("<option value=" + 0 + ">" + "- select level -" + "</option>");
                                    for (var i = 0; i < data.length; i++) {
                                        items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                                    }
                                    $("#gradelevelenroll").html(items.join(' '));
                                });
                                // get section 
                                $("#gradelevelenroll").change(function () {                  //same logic for 3rd dropdown list
                                    var level = $("#gradelevelenroll :selected").text();
                                    var url = 'Enrollment/Section';
                                    var data1 = { "level": level };
                                    $.post(url, data1, function (data) {
                                        var items = [];
                                        items.push("<option value=" + 0 + ">" + "- select section -" + "</option>");
                                        for (var i = 0; i < data.length; i++) {
                                            items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                                        }
                                        $("#sectionenroll").html(items.join(' '));
                                    });
                                });
                            }
                        });
                    }
                });
            }
        });
        $("#btnResetEnroll").click(function () {
            location.reload();
        });
        $("#btnEnroll").click(function (event) {
            var status = $("#statusenroll").val();
            if (status === "Not Enrolled") {
                var section = $("#sectionenroll option:selected").val();
                //alert(section);
                if (section === "" || section === "- select section -" || section === null || section === "0") {
                    $("#myModal").modal({ backdrop: "static" });

                } else if (section !== "") {
                    // render table
                    $.ajax({
                        url: 'Enrollment/LoadSched',
                        data: { "section": section },
                        type: "Get",
                        dataType: "html",
                        success: function (data) {
                            $("#TableEnrollDiv").html(data);
                            var academicyear = $("#academicyearenroll").val();
                            var enrollmentid = $("#enrollmentidenroll").val();
                            var studentid = $("#studentidenroll").val();
                            var nameofstudent = $("#nameenroll").val();
                            var studenttype = $("#typeenroll").val();
                            var status = "Enrolled";
                            var gradelevel = $("#gradelevelenroll :selected").text();
                            var section = $("#sectionenroll :selected").text();
                            var dateenrolled = $.datepicker.formatDate('yy/mm/dd', new Date());
                           
                            $.ajax({
                                url: "Enrollment/Create",
                                type: "POST",
                                data: { "AcademicYear": academicyear, "EnrollmentID": enrollmentid, "StudentID": studentid, "NameOfStudent": nameofstudent, "StudentType": studenttype, "Status": status, "GradeLevel": gradelevel, "Section": section, "DateEnrolled": dateenrolled  },
                                success: function () {
                                    $("#lblstudent").text(nameofstudent);
                                    $("#statusenroll").val("Enrolled");
                                    $("#modalEnrollSuccess").modal({ backdrop: "static" });
                                },
                                error: function () {
                                    alert("Something is wrong");
                                }
                            });
                            
                            
                        }
                    });
                }
            } else if (status === "Enrolled"){
                $("#modalAlreadyEnrolled").modal({ backdrop: "static" });
            } else if (status.length === 0) {
                $("#modalNoRecord").modal({ backdrop: "static" }); 
            }
        });
    });


        // combo box value change event
        $(function () {
            $("#vartype").change(function () {
                var type = $("#vartype :selected").text();  //if user select the tournament 
                var url = 'Schedule/GradeLevel';
                var data1 = { "type": type };
                $.post(url, data1, function (data) {    //ajax call
                    var items = [];
                    items.push("<option value=" + 0 + ">" + "- select level -" + "</option>"); //first item
                    for (var i = 0; i < data.length; i++) {
                        items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                    }
                    $("#varlevel").html(items.join(' '));
                });

                // reset controls
                $("#varlevel option").eq(0).prop('selected', true);
                $("#varsection option").eq(0).prop('selected', true);
                $("#subjectcode").empty();
                $("#descriptivetitle").val("");
                $("#sectionadviser").val("");
                $("#teacherid").empty();
                $("#teachername").empty();

            });

            $("#varlevel").change(function () {                  //same logic for 3rd dropdown list
                var level = $("#varlevel :selected").text();
                var url = 'Schedule/Section';
                var data1 = { "level": level };
                $.post(url, data1, function (data) {
                    var items = [];
                    items.push("<option value=" + 0 + ">" + "- select section -" + "</option>");
                    for (var i = 0; i < data.length; i++) {
                        items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                    }
                    $("#varsection").html(items.join(' '));
                });
                $("#sectionadviser").val("");
            });
            $("#varsection").change(function () {
                // get section adviser
                var section = $("#varsection option:selected").val();
                $.ajax({
                    url: 'Schedule/GetSectionAdviser',
                    data: { "sectionname": section },
                    type: "Get",
                    dataType: "json",
                    success: function (data) {
                        $("#sectionadviser").val(data);
                    }
                });
            });
            $("#subjectcode").change(function () {
                $("#teacherid").empty();
                // get descriptive title
                var code = $("#subjectcode option:selected").val();
                $.ajax({
                    url: 'Schedule/GetDescriptiveTitle',
                    data: { "code": code },
                    type: "Get",
                    dataType: "json",
                    success: function (data) {
                        $("#descriptivetitle").val(data);
                    }
                });

                // get subject teacher name
                var url2 = 'Schedule/Teacher';
                var data2 = { "code": code };
                $.post(url2, data2, function (data1) {
                    var items = [];
                    for (var i = 0; i < data1.length; i++) {
                        items.push("<option value=" + 0 + ">" + "- select teacher -" + "</option>");
                        items.push("<option value=" + data1[i].Value + ">" + data1[i].Text + "</option>");
                    }
                    $("#teachername").html(items.join(' '));
                });
            });

            $("#teachername").change(function () {
                var name = $("#teachername option:selected").text();
                var url = 'Schedule/TeacherID';
                var data1 = { "name": name };
                $.post(url, data1, function (data) {
                    var items = [];
                    for (var i = 0; i < data.length; i++) {
                        items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                    }
                    $("#teacherid").html(items.join(' '));
                });
            });
        });

        // button event
        $(function () {
            // load data
            $("#btnLoadDataSched").click(function (event) {
                var section = $("#varsection option:selected").val();
                var txtAdviser = $("#sectionadviser").val();
                //alert(section);
                if (section === "" || section === "- select section -" || section === null || section === "0") {
                    $("#myModal").modal({ backdrop: "static" });

                } else if (section !== "") {
                    // render table
                    $.ajax({
                        url: 'Schedule/LoadSched', //@Url.Action("LoadSched", "Schedule")
                        data: { "section": section },
                        type: "Get",
                        dataType: "html",
                        success: function (data) {
                            $("#TableSchedDiv").html(data);
                        }
                    });

                    // display subject code
                    var type = $("#vartype :selected").text();
                    var url = 'Schedule/SubjectCode';
                    var data1 = { "type": type };
                    $.post(url, data1, function (data) {
                        var items = [];
                        items.push("<option value=" + 0 + ">" + "- select subject code -" + "</option>");
                        for (var i = 0; i < data.length; i++) {
                            items.push("<option value=" + data[i].Value + ">" + data[i].Text + "</option>");
                        }
                        $("#subjectcode").html(items.join(' '));
                    });

                    // enable and empty controls
                    $("#subjectcode").prop("disabled", false);
                    $("#descriptivetitle").val("");
                    $("#teachername").empty();
                    $("#teacherid").empty();
                    $("#timefrom").val("");
                    $("#timeto").val("");

                    $("#cbmonday").prop('checked', false);
                    $("#cbtuesday").prop('checked', false);
                    $("#cbwednesday").prop('checked', false);
                    $("#cbthursday").prop('checked', false);
                    $("#cbfriday").prop('checked', false);
                    $("#cbsaturday").prop('checked', false);

                }
            });
            $("#btnCreateSched").click(function (e) {
                var thisday = "";

                var monday = $("#cbmonday").is(":checked");
                var tuesday = $("#cbtuesday").is(":checked");
                var wednesday = $("#cbwednesday").is(":checked");
                var thursday = $("#cbthursday").is(":checked");
                var friday = $("#cbfriday").is(":checked");
                var saturday = $("#cbsaturday").is(":checked");

                if (monday === true) {
                    thisday += "M";
                }
                if (tuesday === true) {
                    thisday += "T";
                }
                if (wednesday === true) {
                    thisday += "W";
                }
                if (thursday === true) {
                    thisday += "Th";
                }
                if (friday === true) {
                    thisday += "F";
                }
                if (saturday === true) {
                    thisday += "S";
                }

                var day = thisday;
                var ay = $("#academicyear").val();
                var type = $("#vartype").val();
                var level = $("#varlevel").val();
                var section = $("#varsection").val();
                var adviser = $("#sectionadviser").val();
                var subjectcode = $("#subjectcode").val();
                var descriptivetitle = $("#descriptivetitle").val();


                var time = $("#timefrom").val() + "-" + $("#timeto").val();
                var timefrom = $("#timefrom").val();
                var timeto = $("#timeto").val();
                var teacherid = $("#teacherid").val();
                var teacher = $("#teachername option:selected").text();

                if (subjectcode === "" || subjectcode === 0
                    || ay === ""
                    || type === ""
                    || level === ""
                    || section === ""
                    || descriptivetitle === ""
                    || teacher === "- select teacher -"
                    || timefrom === ""
                    || timeto === ""
                ) {
                    $("#myModalCreateError").modal({ backdrop: "static" });
                } else {
                    $("#myModalCreateConfirm").modal({ backdrop: "static" });
                }
            });

            $("#confirmCreate").click(function () {
                var thisday = "";

                var monday = $("#cbmonday").is(":checked");
                var tuesday = $("#cbtuesday").is(":checked");
                var wednesday = $("#cbwednesday").is(":checked");
                var thursday = $("#cbthursday").is(":checked");
                var friday = $("#cbfriday").is(":checked");
                var saturday = $("#cbsaturday").is(":checked");

                if (monday === true) {
                    thisday += "M";
                }
                if (tuesday === true) {
                    thisday += "T";
                }
                if (wednesday === true) {
                    thisday += "W";
                }
                if (thursday === true) {
                    thisday += "Th";
                }
                if (friday === true) {
                    thisday += "F";
                }
                if (saturday === true) {
                    thisday += "S";
                }

                var day = thisday;
                var ay = $("#academicyear").val();
                var type = $("#vartype").val();
                var level = $("#varlevel").val();
                var section = $("#varsection").val();
                var adviser = $("#sectionadviser").val();
                var subjectcode = $("#subjectcode").val();
                var descriptivetitle = $("#descriptivetitle").val();


                var time = $("#timefrom").val() + "-" + $("#timeto").val();
                var timefrom = $("#timefrom").val();
                var timeto = $("#timeto").val();
                var teacherid = $("#teacherid").val();
                var teacher = $("#teachername option:selected").text();

                $.ajax({
                    url: "Schedule/Create",
                    type: "POST",
                    data: { AcademicYear: ay, Type: type, GradeLevel: level, Section: section, Adviser: adviser, SubjectCode: subjectcode, DescriptiveTitle: descriptivetitle, Monday: monday, Tuesday: tuesday, Wednesday: wednesday, Thursday: thursday, Friday: friday, Saturday: saturday, Day: day, Time: time, TimeFrom: timefrom, TimeTo: timeto, TeacherID: teacherid, Teacher: teacher },
                    success: function () {
                        $("#myModalCreateSuccess").modal({ backdrop: "static" });
                        $("#btnLoadDataSched").click();
                    },
                    error: function () {
                        alert("Something is wrong");
                    }
                });
            });

            $("#confirmDelete").click(function () {
                var academicyear = $("#academicyear").val();
                var section = $("#varsection").val();
                var subjectcode = $("#subjectcode").val();
                var teacher = $("#teachername option:selected").text();

                $.ajax({
                    url: "Schedule/Delete",
                    type: "POST",
                    data: { "academicyear": academicyear, "section": section, "subjectcode": subjectcode, "teacher": teacher },
                    success: function () {
                        $("#myModalDeleteSuccess").modal({ backdrop: "static" });
                        $("#btnLoadDataSched").click();

                        // enable and empty controls
                        $("#subjectcode").prop("disabled", false);
                        $("#descriptivetitle").val("");
                        $("#teachername").empty();
                        $("#teacherid").empty();
                        $("#timefrom").val("");
                        $("#timeto").val("");

                        $("#cbmonday").prop('checked', false);
                        $("#cbtuesday").prop('checked', false);
                        $("#cbwednesday").prop('checked', false);
                        $("#cbthursday").prop('checked', false);
                        $("#cbfriday").prop('checked', false);
                        $("#cbsaturday").prop('checked', false);

                    },
                    error: function () {
                        alert("Something is wrong");
                    }
                });
            });
            $("#btnDeleteSched").click(function (e) {
                var academicyear = $("#academicyear").val();
                var section = $("#varsection").val();
                var subjectcode = $("#subjectcode").val();
                var teacher = $("#teachername option:selected").text();
                var timefrom = $("#timefrom").val();
                var timeto = $("#timeto").val();
                if (subjectcode === "" || subjectcode === 0
                    || academicyear === ""
                    || section === ""
                    || subjectcode === "" || subjectcode === null
                    || teacher === "- select teacher -"
                    || timefrom === ""
                    || timeto === ""
                ) {
                    $("#myModalDeleteError").modal({ backdrop: "static" });
                } else {
                    $("#myModalDelete").modal({ backdrop: "static" });
                }
            });
            $("#btnUpdateSched").click(function (e) {
                var academicyear = $("#academicyear").val();
                var section = $("#varsection").val();
                var subjectcode = $("#subjectcode").val();
                var thisday = "";

                var monday = $("#cbmonday").is(":checked");
                var tuesday = $("#cbtuesday").is(":checked");
                var wednesday = $("#cbwednesday").is(":checked");
                var thursday = $("#cbthursday").is(":checked");
                var friday = $("#cbfriday").is(":checked");
                var saturday = $("#cbsaturday").is(":checked");

                if (monday === true) {
                    thisday += "M";
                }
                if (tuesday === true) {
                    thisday += "T";
                }
                if (wednesday === true) {
                    thisday += "W";
                }
                if (thursday === true) {
                    thisday += "Th";
                }
                if (friday === true) {
                    thisday += "F";
                }
                if (saturday === true) {
                    thisday += "S";
                }
                var day = thisday;
                var teacher = $("#teachername option:selected").text();

                var time = $("#timefrom").val() + "-" + $("#timeto").val();
                var timefrom = $("#timefrom").val();
                var timeto = $("#timeto").val();
                if (subjectcode === "" || subjectcode === 0
                    || academicyear === ""
                    || section === ""
                    || subjectcode === "" || subjectcode === null
                    || teacher === "- select teacher -"
                    || day === ""
                    || time === ""
                    || timefrom === ""
                    || timeto === ""
                ) {
                    $("#myModalUpdateError").modal({ backdrop: "static" });
                } else {
                    $("#myModalUpdate").modal({ backdrop: "static" });
                }
            });
            $("#confirmUpdate").click(function (e) {
                var thisday = "";

                var monday = $("#cbmonday").is(":checked");
                var tuesday = $("#cbtuesday").is(":checked");
                var wednesday = $("#cbwednesday").is(":checked");
                var thursday = $("#cbthursday").is(":checked");
                var friday = $("#cbfriday").is(":checked");
                var saturday = $("#cbsaturday").is(":checked");

                if (monday === true) {
                    thisday += "M";
                }
                if (tuesday === true) {
                    thisday += "T";
                }
                if (wednesday === true) {
                    thisday += "W";
                }
                if (thursday === true) {
                    thisday += "Th";
                }
                if (friday === true) {
                    thisday += "F";
                }
                if (saturday === true) {
                    thisday += "S";
                }
                var day = thisday;
                var academicyear = $("#academicyear").val();
                var section = $("#varsection").val();
                var subjectcode = $("#subjectcode").val();
                var teacher = $("#teachername option:selected").text();

                var time = $("#timefrom").val() + "-" + $("#timeto").val();
                var timefrom = $("#timefrom").val();
                var timeto = $("#timeto").val();
                $.ajax({
                    url: "Schedule/Update",
                    type: "POST",
                    data: { "academicyear": academicyear, "section": section, "subjectcode": subjectcode, "monday": monday, "tuesday": tuesday, "wednesday": wednesday, "thursday": thursday, "friday": friday, "saturday": saturday, "day": day, "time": time, "timefrom": timefrom, "timeto": timeto },
                    success: function () {
                        $("#myModalUpdateSuccess").modal({ backdrop: "static" });
                        $("#btnLoadDataSched").click();
                    },
                    error: function () {
                        alert("Something is wrong");
                    }
                });

            });
            // reset button
            $("#btnResetSched").click(function () {
                location.reload();
            });

        });
    });
    function SelectSchedule(schedid) {
        // get data from selected row
        $.ajax({
            url: 'Schedule/SelectedRowSched',
            data: { "id": schedid },
            type: "GET",
            dataType: "json",
            success: function (data) {
                $("#sectionadviser").val(data.Adviser);
                $("#subjectcode").val(data.SubjectCode);
                $("#descriptivetitle").val(data.DescriptiveTitle);

                $("#teachername").empty();
                $("#teacherid").empty();
                $("#teachername").append('<option>' + data.Teacher + '</option>');
                $("#teacherid").append('<option>' + data.TeacherID + '</option>');

                $("#cbmonday").prop('checked', data.Monday);
                $("#cbtuesday").prop('checked', data.Tuesday);
                $("#cbwednesday").prop('checked', data.Wednesday);
                $("#cbthursday").prop('checked', data.Thursday);
                $("#cbfriday").prop('checked', data.Friday);
                $("#cbsaturday").prop('checked', data.Saturday);

                $("#timefrom").val(data.TimeFrom);
                $("#timeto").val(data.TimeTo);

                $("html, body").animate({ "scrollTop": "0" }, 800);
                
            }
        });

       
    }
    function ClearTextboxEnroll() {
        $("#enrollmentidenroll").val("");
        $("#studentidenroll").val("");
        $("#nameenroll").val("");
        $("#typeenroll").val("");
        $("#gradelevelenroll").empty();
        $("#sectionenroll").empty();
    }
    function SelectES(id) {
        $.ajax({
            url: 'ES/SelectedES',
            data: { "id": id },
            type: "GET",
            dataType: "json",
            success: function (data) {
                $("#esstudentid").val(data.StudentID);
                $("#esname").val(data.NameOfStudent);
            }
        });
    }
    function SelectAccount(id) {
        $.ajax({
            url: 'ManageAccount/SelectedAccount',
            data: { "id": id },
            type: "GET",
            dataType: "json",
            success: function (data) {
                $("#accountidmu").val(data.AccountID);
                $("#firstnamemu").val(data.FirstName);
                $("#lastnamemu").val(data.LastName);
                $("#lastloginmu").val(data.LastLogin);
                $("#accountcreatedmu").val(data.AccountCreated);
                $("#usernamemu").val(data.Username);
                $("#passwordmu").val(data.Password);
            }
        });
    }
    function PassDataEnroll() {
        var academicyear = $("#academicyearenroll").val();
        var enrollmentid = $("#enrollmentidenroll").val();
        var studentid = $("#studentidenroll").val();
        var nameofstudent = $("#nameenroll").val();
        var studenttype = $("#typeenroll").val();
        var gradelevel = $("#gradelevelenroll :selected").text();
        var section = $("#sectionenroll :selected").text();

        localStorage["academicyear"] = academicyear;
        localStorage["enrollmentid"] = enrollmentid;
        localStorage["studentid"] = studentid.toString();
        localStorage["nameofstudent"] = nameofstudent;
        localStorage["studenttype"] = studenttype;
        localStorage["gradelevel"] = gradelevel.toString();
        localStorage["section"] = section.toString();  

        $("#printschoolyear").val("A.Y " + localStorage["academicyear"]);
        $("#printenrollmentid").val(localStorage["enrollmentid"]);
        $("#studentidprint").val(localStorage["studentid"]);
        $("#printstudentname").val(localStorage["nameofstudent"]);
        $("#printstudenttype").val(localStorage["studenttype"]);
        $("#printstudentlevel").val(localStorage["gradelevel"]);
        $("#printstudentsection").val(localStorage["section"]);
    }

    function PassDataClassRecord() {
        //var academicyear = $("#varlevelclass").val();
        var gradelevel = $("#varlevelclass :selected").text();
        var section = $("#varsectionclass :selected").text();

        localStorage["gradelevelclass"] = gradelevel.toString();
        localStorage["sectionclass"] = section.toString();

        //$("#printschoolyear").val("A.Y " + localStorage["academicyearclass"]);
        $("#printstudentlevelcc").val(localStorage["gradelevelclass"]);
        $("#printstudentsectioncc").val(localStorage["sectionclass"]);
    }
    function SaveRequirements(id) {
        var f137 = $("#Form137"+id+"").is(":checked");
        var nsos = $("#NSO"+id+"").is(":checked");
        var goodmoral = $("#GoodMoral"+id+"").is(":checked");
        var gradecard = $("#GradeCard"+id+"").is(":checked");

        $.ajax({
            url: 'Requirements/UpdateReq',
            data: { "id": id, "f137": f137, "nso": nsos, "gm": goodmoral, "gc": gradecard },
            type: "POST",
            success: function () {
                $("#modalReqSuccess").modal({ backdrop: "normal" });
            }
        });
    }