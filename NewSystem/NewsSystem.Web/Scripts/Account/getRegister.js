function Register() {
    var registerAccount = $("#registerAccount").val();
    var newPassword = $("#registerNewPass").val();
    var password = $("#registerPass").val();
    var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;//验证手机号
    if (registerAccount.length == 0) {
        alert("手机号不能为空");
        return false;
    } else if (!myreg.test(registerAccount)) {
        alert("请输入有效的手机号码！");
        return false;
    }
    else if (password.length == 0) {
        alert("密码不能为空");
        return false;
    }
    else if (newPassword.length == 0) {
        alert("请确认密码");
        return false;
    }
    else if (password != newPassword) {
        alert("两次密码不同！/n/n请重新输入:");
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Account/Register/",
            data: { "userAccount": registerAccount, "password": password },
            success: function (data) {
                alert(data);
                if (data == "注册成功") {
                    document.location.reload();
                }
            }
        });
    }
}
