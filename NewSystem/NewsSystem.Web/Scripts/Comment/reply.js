
window.onload = function () {
    var list = $("div[name = 'replyTxt']");
    var btn = $("button[name = 'reply']");
    for (var i = 0; i < list.length; i++) {
        btn[i].index = i;
        btn[i].onclick = function () {
            if (list[this.index].style.display == "none") {
                list[this.index].style.display = "block";
                for (var j = 0; j < list.length; j++) {
                    if (j != this.index) {
                        list[j].style.display = "none";
                    }
                }
            }
            else {
                list[this.index].style.display = "none";
            }
        }

    }

}