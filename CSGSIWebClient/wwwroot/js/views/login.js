const loginUser = {};

const $loginUsername = $('#input-username');
const $loginPassword = $('#input-password'); 

$loginUsername.keyup(function () {
    loginUser.username = $(this).val();
    setUser(loginUser);
});

$loginPassword.keyup(function () {
    // if ($loginUsername.val()) {
    //     loginUser.username = $loginUsername.val();
    // }
    loginUser.password = $(this).val();
    setUser(loginUser);
});