function AddType() {
    var parentId = $("#TypeParentId").val();
    var name = $("#TypeName").val();
    if (name.length == 0) {
        alert("名字不能为空");
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Admin/NewsManage/AddNewsType/",
            data: { "typeName": name, "parentId": parentId },
            success: function (data) {
                alert(data);
                if (data == "添加成功") {
                    document.location.reload();
                }
            }
        });
    }
}


function UpdateType(id) {
    var parentId = $("#UpdateNewsTypeParentId").val();
    var name = $("#UpdateNewsTypeName").val();
    if (name.length == 0) {
        alert("类型名字不能为空!");
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Admin/NewsManage/UpdateNewsType/",
            data: {"id":id,"parentId": parentId ,  "typeName": name},
            success: function (data) {
                alert(data);
                if (data == "修改成功") {
                    window.location.href = "/Admin/NewsManage/GetAllNewsType";
                }
            }
        });
    }
}

