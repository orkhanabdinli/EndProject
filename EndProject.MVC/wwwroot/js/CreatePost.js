$(document).ready(function () {
    var token = $('#tokenId').val();

    function reloadWithAlert() {
        Swal.fire({
            title: "DostLooK.",
            text: "Your post has been created successfully!",
            icon: "success"
        }).then(function () {
            location.reload();
        });
    }

    $('#PostBtn').click(function (e) {
        e.preventDefault();

        var userId = $('#userId').val();
        var description = $('#decriptionId').val().trim();
        var files = $('#fileInput')[0].files;

        if (description === "") {
            Swal.fire({
                icon: "warning",
                title: "Warning",
                text: "Description cannot be empty.",
            });
            return;
        }

        var formData = new FormData();
        formData.append('UserId', userId);
        formData.append('Description', description);

        if (files.length > 0) {
            for (let i = 0; i < files.length; i++) {
                formData.append('PostMedias', files[i]);
            }
        }

        $.ajax({
            url: 'https://localhost:7032/api/Post/CreatePost', 
            type: 'POST',
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
