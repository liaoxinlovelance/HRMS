var width = $(window).width();
var height = $(window).height();

$('body').css('background-size', width, height);
$('#da').css('width', width);
$('#da').css('height', height);
//var kuan = $('#kuan').height();
//var kuann = $('#kuan').width();
//$('#kuan1').css('width', kuann);
//$('#kuan1').css('height', kuan);

/************************  失焦判断  **********************************/
$("input").blur(function () {
    if ($(this).is("#email")) {
        var ac = /^(\w)+(\.\w)*@(\w)+((\.\w+)+)/;
        if ($("#email").val() != "") {
            if (!(ac.test($("#email").val()))) {
                $("#spa").text("格式错误！");
                return false;
            } else if (ac) {
                $("#spa").text("");
                return true;
            }
        } else {
            $("#spa").text("");
        }
    }



    if ($(this).is("#pwd")) {
        var pwd = /^\w{5,11}$/
        if ($("#pwd").val() != "") {
            if (!(pwd.test($("#pwd").val()))) {
                $("#spa1").text("请输入6-12个字符！");
                return false;
            } else if (pwd) {
                $("#spa1").text("");
                return true;
            }
        } else {
            $("#spa1").text("");
        }
    }
})

/********************** 聚焦提示 ************************/
$("input").focus(function () {
    if ($(this).is("#email")) {
        $("#spa").css("color", "#ffffff");
        $("#tishi").text("");
    }
})
$("input").focus(function () {
    if ($(this).is("#pwd")) {
        $("#spa1").css("color", "#ffffff")
        $("#tishi").text("");

    }
})