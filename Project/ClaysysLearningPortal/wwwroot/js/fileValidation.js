document.getElementById('CourseImage').addEventListener('change', function (e) {
    var file = e.target.files[0];
    if (file) {
        var fileExtension = file.name.split('.').pop().toLowerCase();
        if (fileExtension !== 'png') {
            alert('Please upload a PNG file.');
            e.target.value = ''; // Clear the input if file is not PNG
        }
    }
});