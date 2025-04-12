window.onload = function () {
    // Add the current page to history to prevent going back
    history.pushState(null, null, location.href);
    history.back();
    history.forward();

    // After logout, you can also redirect to the login page
    window.location.replace("/User/Login");
};