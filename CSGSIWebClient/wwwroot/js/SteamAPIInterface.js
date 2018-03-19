function getSteamNameAndAvatar() {
    const user = getUser();

    $.ajax({
        url: 'https://api.jasonfigueroa.io/auth',
        type: 'POST',
        data: JSON.stringify({
            username: user.username,
            password: user.password
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
        .then(function (data) {
            $.ajax({
                url: 'https://api.jasonfigueroa.io/user/steamid',
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", `JWT ${data.access_token}`);
                }
            })
                .then(function (steamObj) {
                    const steamId = steamObj.steam_id;
                    const apiKey = "E821014121563F86283961754BAC0C1C";
                    const steamAPIUrl = `http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=${apiKey}&steamids=${steamId}`;
                    $.ajax({
                        url: steamAPIUrl,
                        type: 'GET',

                    })
                        .then(function (steamAPIResponse) {
                            handleData(steamAPIResponse);
                        });
                });
        });
}

//function getMatches(handleData) {
//    const user = getUser();

//    $.ajax({
//        url: 'https://api.jasonfigueroa.io/auth',
//        type: 'POST',
//        data: JSON.stringify({
//            username: user.username,
//            password: user.password
//        }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json"
//    })
//        .then(function (data) {
//            $.ajax({
//                url: 'https://api.jasonfigueroa.io/match/list',
//                type: 'GET',
//                beforeSend: function (xhr) {
//                    xhr.setRequestHeader("Authorization", `JWT ${data.access_token}`);
//                }
//            })
//                .then(function (matches) {
//                    handleData(matches);
//                })
//        });
//}