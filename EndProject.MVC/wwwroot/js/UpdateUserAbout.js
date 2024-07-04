$(document).ready(function () {
    var token = $('#tokenId').val();

    function reloadWithAlert() {
        Swal.fire({
            title: "DostLooK.",
            text: "Your information has been updated!",
            icon: "success"
        }).then(function () {
            location.reload(); 
        });
    }

    $('#userAboutUpdateBtn').click(function (e) {
        e.preventDefault(); 

        var userAboutId = $('#userAboutId').val();
        var userId = $('#userId').val();
        var firstName = $('#firstNameId').val();
        var lastName = $('#lastNameId').val();
        var userName = $('#userNameId').val();
        var bio = $('#bioId').val();
        var country = $('#countryId').val();
        var city = $('#cityId').val();
        var gender = $('#genderId').val(); 
        var profileImage = $('#profileImageId')[0].files[0];
        var backgroundImage = $('#backgroundImageId')[0].files[0];

        var formData = new FormData();
        formData.append('Id', userAboutId);
        formData.append('UserId', userId);
        formData.append('FirstName', firstName);
        formData.append('LastName', lastName);
        formData.append('UserName', userName);
        formData.append('Bio', bio);
        formData.append('CityName', city);
        formData.append('CountryName', country);
        formData.append('Gender', gender);
        if (profileImage) {
            formData.append('ProfileImageUrl', profileImage);
        }
        if (backgroundImage) {
            formData.append('BackgroundImageUrl', backgroundImage);
        }

        $.ajax({
            url: `https://localhost:7032/api/UserProfile/UserAboutUpdate/${userAboutId}`,
            type: 'PUT',
            data: formData,
            contentType: false,
            headers: {
                'Authorization': 'Bearer ' + token
            },
            processData: false,
            success: function (response) {
                reloadWithAlert();
            },
            error: function (xhr, status, error) {
                var errorMessage = 'Something went wrong!';
                if (xhr.responseJSON && xhr.responseJSON.Message) {
                    errorMessage = xhr.responseJSON.Message;
                } else if (xhr.responseJSON && xhr.responseJSON.errors) {
                    errorMessage = '<ul>';
                    $.each(xhr.responseJSON.errors, function (key, value) {
                        errorMessage += '<li>' + value + '</li>';
                    });
                    errorMessage += '</ul>';
                }

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: errorMessage,
                });
            }
        });
    });
});