function writeComment(newsId) {
    var content = $("#CommentContent").val();
    document.getElementById("CommentContent").innerHTML = "";
    if (content.length == 0) {
        alert("内容不能为空");
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Comment/WriteComment/",
            data: { "newsId": newsId, "commentContent": content },
            success: function (data) {
                alert(data);
                if (data == "评论成功") {
                    window.location.reload();
                    return true;
                }
                else {
                    return false;
                }

            }
        });
    }
}