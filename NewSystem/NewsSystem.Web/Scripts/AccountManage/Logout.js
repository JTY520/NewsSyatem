function Logout() {
    var href = window.location.href;
    var msg = "您真的确定要退出吗？\n\n请确认！";
    if (confirm(msg) == true) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Admin/UserManage/Logout/",
            data: {},
            success: function (data) {
                alert(data);
            }
        });
        location.href = href;
    } else {
        return false;
    }
}