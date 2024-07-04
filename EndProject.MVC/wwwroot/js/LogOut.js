$(document).ready(function () {
    var token = $('#tokenId').val(); // Assuming you have a token for authorization

    $('#LogOutBtn').click(function (e) {
        e.preventDefault();

        // Show confirmation dialog with SweetAlert2
        Swal.fire({
            title: 'Are you sure?',
            text: 'You will be logged out.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, logout!'
        }).then((result) => {
            if (result.isConfirmed) {
                // Proceed with logout
                $.ajax({
                    url: 'https://localhost:7032/api/User/LogOut',
                    type: 'GET', // Assuming you're using GET for logout
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    success: function () {
                        // On successful logout, redirect or perform other actions as needed
                        window.location.replace('/'); // Redirect to home page
                    },
                    error: function (xhr, status, error) {
                        // Handle error if needed
                        console.error('Error logging out:', error);
                        // Optionally, display an error message to the user
                        Swal.fire(
                            'Error!',
                            'Failed to log out. Please try again.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});