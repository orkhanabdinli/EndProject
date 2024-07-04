$(document).ready(function () {
    $('#changePasswordBtn').click(function (e) {
        e.preventDefault();

        var userId = $('#userId').val();
        var token = $('#tokenId').val();
        var currentPassword = $('#currentPasswordId').val();
        var newPassword = $('#newPasswordId').val();
        var confirmPassword = $('#confirmPasswordId').val();

        // Validate passwords
        if (newPassword !== confirmPassword) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'New password and confirm password do not match.'
            });
            return; 
        }
        var userChangePasswordDTO = {
            CurrentPassword: currentPassword,
            NewPassword: newPassword,
            ConfirmPassword: confirmPassword
        }

        $.ajax({
            url: `https://localhost:7032/api/User/ChangePassword/${userId}`,
            type: 'PUT',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userChangePasswordDTO),
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Password changed successfully!',
                    showConfirmButton: false,
                    timer: 1500
                }).then(function () {
                    // Example redirect after success
                    window.location.replace('/UserSettings/Index');
                });
            },
            error: function (xhr, status, error) {
                console.error('Error changing password:', error);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to change password. Please try again.'
                });
            }
        });
    });
});