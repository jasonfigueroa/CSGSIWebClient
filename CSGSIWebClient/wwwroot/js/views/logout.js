localStorage.removeItem("accessToken");
localStorage.removeItem("refreshToken");

window.setTimeout(function () {
    window.location.href = baseUrl;
}, 5000);