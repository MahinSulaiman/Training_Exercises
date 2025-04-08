$(document).ready(function () {
    $(".dropdown-item").click(function () {
        var categoryId = $(this).data("category-id");
        loadCourses(categoryId);
    });

    // Function to load courses using AJAX
    function loadCourses(categoryId) {
        $.ajax({
            url: '/Home/Courses',  
            type: 'GET',
            data: { categoryId: categoryId },
            dataType: 'json',
            success: function (data) {
                var targetElement = $("#course-list");

                if (data && data.length > 0) {
                    var html = '';
                    data.forEach(function (item) {
                        html += '<div class="col">';
                        html += '<div class="card h-100 shadow-sm">';
                        html += '<img src="data:image/png;base64,' + item.courseImage + '" class="card-img-top img-fixed-size" alt="Course Image" />';
                        html += '<div class="card-body">';
                        html += '<h5 class="card-title">' + item.title + '</h5>';
                        html += '<a href="/Student/Details/?courseId=' + item.courseId + '" class="btn btn-primary">View Details</a>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                    });
                    // Insert the HTML into the course list container
                    targetElement.html(html);
                } else {
                    targetElement.html('<p>No courses found.</p>');
                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Error: " + status + " - " + error);
                console.log("Response Text: " + xhr.responseText);
                alert("Error loading courses.");
            }
        });
    }


    loadCourses(null);
});
