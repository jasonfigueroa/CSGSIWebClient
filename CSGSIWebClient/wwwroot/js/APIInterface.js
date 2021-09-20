function getMatches(handleData) {
    $.ajax({
        url: `${apiUrl}/match/list`,
        type: 'GET',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("accessToken")}`);
        }
    })
    .done(function (matches) {
        handleData(matches);
    })
    .fail(function (request, status, error) {
        if (request.responseJSON.error === "token_expired") {
            $.ajax({
                url: `${apiUrl}/refresh`,
                type: 'POST',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("refreshToken")}`);
                }
            })
            .done(function (data) {
                localStorage.setItem("accessToken", data.access_token);
                $.ajax({
                    url: `${apiUrl}/match/list`,
                    type: 'GET',
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("accessToken")}`);
                    }
                })
                .done(function (matches) {
                    handleData(matches);
                });
            });
        } else {
            console.log(request);
        }
    });
}

function getMatch(id, handleData) {
    $.ajax({
        url: `${apiUrl}/match/${id}`,
        type: 'GET',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("accessToken")}`);
        }
    })
    .done(function (matches) {
        handleData(matches);
    })
    .fail(function (request, status, error) {
        if (request.responseJSON.error === "token_expired") {
            $.ajax({
                url: `${apiUrl}/refresh`,
                type: 'POST',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("refreshToken")}`);
                }
            })
            .done(function (data) {
                localStorage.setItem("accessToken", data.access_token);
                $.ajax({
                    url: `${apiUrl}/match/${id}`,
                    type: 'GET',
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("accessToken")}`);
                    }
                })
                .done(function (matches) {
                    handleData(matches);
                });
            });
        } else {
            console.log(request);
        }
    });
}

/*************************************************************/
/* May try to incorporate the following or something similar */
/*************************************************************/

function refreshAndSetToken(doneCallback) {
    return $.ajax({
        url: `${apiUrl}/refresh`,
        type: 'POST',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", `Bearer ${localStorage.getItem("refreshToken")}`);
        }
    })
    .done(function (data) {
        localStorage.setItem("accessToken", data.access_token);
    });
}

function setAccessToken(accessToken) {
    localStorage.setItem("accessToken", data.access_token);
}

function setAccessTokenMakeRequest(accessToken, jqxhr, callbackFunc) {
    localStorage.setItem("accessToken", accessToken);
    jqxhr(callbackFunc);
}
