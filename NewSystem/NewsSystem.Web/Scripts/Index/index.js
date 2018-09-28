function getOtherType() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/OtherNewsType/",
        data: {},
        success: function (data) {
             
        }
    });
}

function creatHtml(model) {
    
}