function getAllCom() {
    document.getElementById("getCom").style.display = "block";
    document.getElementById("lookCom").style.display = "none";
}

function Reply(newsId, parentId) {
    var replyContent = $("textarea[name = 'CommentContent']").val();
    //var content = replyComment[i].val();
    if (replyContent.length == 0) {
                alert("回复不能为空");
                return false;
            }
            else {
                $.ajax ({
                    type: "POST",
                    dataType: "json",
                    url: "/Comment/Reply/",
                    data: { "newsId": newsId, "parentId": parentId, "replyContents": replyContent },
                    success: function (data) {
                        alert(date);
                        if (data == "回复成功") {
                            window.location.reload();
                        }
                    }
                });
            }
}