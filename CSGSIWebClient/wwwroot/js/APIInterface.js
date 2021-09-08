function getMatches(handleData) {
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
                url: 'https://api.jasonfigueroa.io/match/list',
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", `JWT ${data.access_token}`);
                }
            })
                .then(function (matches) {
                    handleData(matches);
                })
        });
}

function getMatch(id, handleData) {
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
                url: `https://api.jasonfigueroa.io/match/${id}`,
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", `JWT ${data.access_token}`);
                }
            })
                .then(function (matches) {
                    handleData(matches);
                })
        });
}