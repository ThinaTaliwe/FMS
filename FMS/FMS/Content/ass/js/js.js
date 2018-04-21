$(document).ready(function () {


    $(".submenu > a").click(function (e) {
        e.preventDefault();
        var $li = $(this).parent("li");
        var $ul = $(this).next("ul");

        if ($li.hasClass("open")) {
            $ul.slideUp(350);
            $li.removeClass("open");
        } else {
            $(".nav > li > ul").slideUp(350);
            $(".nav > li").removeClass("open");
            $ul.slideDown(350);
            $li.addClass("open");
        }
    });

});


function validate() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    if (username == "Fleet" && password == "Fleet123") {
        alert("Login successfully");
        window.location = "Home"; // Redirecting to other page.
        return false;
    }
    else {
        alert("Login was unsuccessful, please check your username and password");
        document.getElementById("Login_Error").innerHTML = "Login was unsuccessful, please check your username and password";
    }

};