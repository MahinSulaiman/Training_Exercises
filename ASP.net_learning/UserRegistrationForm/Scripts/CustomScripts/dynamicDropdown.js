$(document).ready(function () {
    // Load states and cities data from the JSON file
    $.getJSON('/Content/jsonData/states-cities.json', function (data) {
        // Populate the State dropdown
        var stateDropdown = $('#State');
        stateDropdown.empty();  // Clear existing options
        stateDropdown.append('<option value="">Select State</option>');  // Default option

        // Populate state dropdown from JSON data
        $.each(data.States, function (i, state) {
            stateDropdown.append('<option value="' + state.StateName + '">' + state.StateName + '</option>');
        });
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Error loading JSON:", textStatus, errorThrown);
    });

    // When the state is changed, populate the City dropdown based on selected state
    $('#State').change(function () {
        var selectedState = $(this).val();
        var cityDropdown = $('#City');
        cityDropdown.empty();  // Clear the City dropdown
        cityDropdown.append('<option value="">Select City</option>');  // Default option

        if (selectedState) {
            // Fetch the cities corresponding to the selected state
            $.getJSON('/Content/jsonData/states-cities.json', function (data) {
                // Find the selected state's cities
                var cities = [];
                $.each(data.States, function (i, state) {
                    if (state.StateName === selectedState) {
                        cities = state.Cities;
                    }
                });

                // Populate the City dropdown with cities for the selected state
                $.each(cities, function (i, city) {
                    cityDropdown.append('<option value="' + city + '">' + city + '</option>');
                });
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.error("Error loading cities:", textStatus, errorThrown);
            });
        }
    });
});
