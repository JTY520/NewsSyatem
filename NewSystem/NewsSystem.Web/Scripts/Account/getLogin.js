
function Login() {
    var userAccount = $("#loginAccount").val();
    var password = $("#loginPass").val();
    if (userAccount.length == 0) {
        alert("用户名不能为空");
        return false;
    }
    else if (password.length == 0) {
        alert("密码不能为空");
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Account/Login/",
            data: { "userAccount": userAccount, "password": password },
            success: function (data) {
                alert(data);
                if (data == "登陆成功") {
                    document.location.reload();
                    return true;
                }
                else {
                    return false;
                }
            }
        });
    }
}
