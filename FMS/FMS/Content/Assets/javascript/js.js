function validate() {
    alert("Hey");
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    if (username == "Formget" && password == "formget#123") {
        alert("Login successfully");
        window.location = "success.html"; // Redirecting to other page.
        return false;
    }