const registerUser = {};

const $registerUsername = $('#input-register-username');
const $registerPassword = $('#input-register-password');

$registerUsername.keyup(function () {
    registerUser.username = $(this).val();
    setUser(registerUser);
});

$registerPassword.keyup(function () {
    // if ($registerUsername.val()) {
    //     registerUser.username = $registerUsername.val();
    // }
    registerUser.password = $(this).val();
    setUser(registerUser);
});