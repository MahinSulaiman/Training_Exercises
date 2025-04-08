// This function will prevent back navigation
function preventBack() {
    // Replace current history entry so the back button won't work
    history.replaceState(null, null, location.href);
    // Push a new state to ensure that there's no way to go back
    history.pushState(null, null, location.href);
}

// Prevent back navigation by listening to the 'popstate' event
window.addEventListener('popstate', function () {
    preventBack(); // Re-run the preventBack function whenever back is attempted
});

// Call preventBack as soon as the page loads
document.addEventListener('DOMContentLoaded', function () {
    preventBack();
});