function showLogin() {
    var login = document.getElementById('login');
    var close = document.getElementById('close');
    login.style.display = "block";
    close.style.display = "block";
}
function hide() {
    var register = document.getElementById('register');
    var login = document.getElementById('login');
    var close = document.getElementById('close');
    register.style.display = "none";
    login.style.display = "none";
    close.style.display = "none";
}
function showRegister() {
    var register = document.getElementById('register');
    var close = document.getElementById('close');
    register.style.display = "block";
    close.style.display = "block";
}

//function showPassword() {
//    if ($("#pwd").attr("type") == "password") {
//        $("#pwd").attr("type", "text")
//    }
//    else {
//        $("#pwd").attr("type", "password")
//    }
//}


