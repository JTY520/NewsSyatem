
function Login() {
    document.getElementById("signUp").innerHTML = "登陆中";
    $("div[name = 'onSignUp']").animate({width:"100%"},10000);
    var userAccount = $("#user").val();
    var password = $("#pass").val();
    if (userAccount.length == 0) {
        $("div[name = 'onSignUp']").stop();
        $("div[name = 'onSignUp']").animate({ width: "0%" }, 1, function () {
            document.getElementById("signUp").innerHTML = "登陆";
            alert("用户名不能为空");
        });
        return false;
    }
    else if (password.length == 0) {
        
        $("div[name = 'onSignUp']").stop();
        $("div[name = 'onSignUp']").animate({ width: "0%" }, 1, function () {
            document.getElementById("signUp").innerHTML = "登陆";
           alert("密码不能为空");
        });
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Admin/UserManage/Login/",
            data: { "userAccount": userAccount, "password": password },
            success: function (data) {
                if (data == "登陆成功") {
                    alert(data);
                    window.location.href = "/Admin/NewsManage/GetAllNews";
                }
                else {
                    $("div[name = 'onSignUp']").stop();
                    $("div[name = 'onSignUp']").animate({ width: "0%" }, 1, function () {
                        document.getElementById("signUp").innerHTML = "登陆";
                        alert(data);
                    });
                }
            }
        });
    }
}
