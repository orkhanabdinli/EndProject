document.addEventListener('DOMContentLoaded', function () {
    const termsCheckbox = document.getElementById('termsCheckbox');
    const registerButton = document.getElementById('registerButton');

    // Function to toggle the Register button
    function toggleRegisterButton() {
        if (termsCheckbox.checked) {
            registerButton.classList.remove('disabled');
            registerButton.style.pointerEvents = 'auto';
            registerButton.style.opacity = '1';
        } else {
            registerButton.classList.add('disabled');
            registerButton.style.pointerEvents = 'none';
            registerButton.style.opacity = '0.5';
        }
    }

    // Attach event listener to the checkbox
    termsCheckbox.addEventListener('change', toggleRegisterButton);

    // Initial check
    toggleRegisterButton();
});
