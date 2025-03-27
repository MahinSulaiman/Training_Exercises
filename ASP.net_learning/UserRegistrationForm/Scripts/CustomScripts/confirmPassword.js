$(document).ready(function () {
    // When the user types in either password field
    $('#ConfirmPassword').on('keyup', function () {
        var password = $('#Password').val();
        var confirmPassword = $('#ConfirmPassword').val();

        // Check if passwords match
        if (password !== confirmPassword) {
            $('#ConfirmPasswordError').show(); // Show error message
            $('#ConfirmPassword').addClass('is-invalid'); // Add a CSS class for invalid input
        } else {
            $('#ConfirmPasswordError').hide(); // Hide error message
            $('#ConfirmPassword').removeClass('is-invalid'); // Remove the invalid class
        }
    });

});