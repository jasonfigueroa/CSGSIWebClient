$(document).ready(function () {
    $('.clickable').click(function () {
        const baseUrl = "http://localhost:49424/matches/match";
        const id = $(this)[0].id.split("__")[1];
        let url = `${baseUrl}/${id}`;
        window.location.href = url;
    });
});