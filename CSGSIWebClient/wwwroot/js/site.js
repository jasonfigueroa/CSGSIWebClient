$(document).ready(function () {
    const user = {};

    $('.clickable').click(function () {
        const baseUrl = "http://localhost:49424/matches/match";
        const id = $(this)[0].id.split("__")[1];
        let url = `${baseUrl}/${id}`;
        window.location.href = url;
    });

    $('#input-username').keyup(function () {
        user.username = $(this).val();
        setUser();
    });

    $('#input-password').keyup(function () {
        user.password = $(this).val();
        setUser();
    });

    function setUser() {
        let stringifiedUser = JSON.stringify(user);
        let encryptedUser = CryptoJS.AES.encrypt(stringifiedUser, "Secret Passphrase");
        localStorage.setItem("user", encryptedUser);
    }

    $('#chart-selection').change(function () {
        renderChart();
    });

    function renderChart() {
        $('#my-chart').remove();
        $('#chart-canvas').append('<canvas id="my-chart"></canvas>');
        if ($('#chart-selection').val() == 'minutes-played-per-map') {
            displayMinutesPerMap();
        } else if ($('#chart-selection').val() == 'minutes-played-per-team') {
            displayMinutesPerTeam();
        }
    }

    if (window.location.href.indexOf("http://localhost:49424/Profile") > -1 || window.location.href.indexOf("http://localhost:49424/profile") > -1) {        
        renderChart();
    }

    function displayMinutesPerTeam() {
        getMatches(function (output) {
            chartData = {
                title: "Minutes Played per Team",
                labels: [],
                // key is map_name, value is the total minutes
                data: {}
            }
            for (let i = 0; i < output.matches.length; i++) {
                const match = output.matches[i];
                const team = match.team;
                // if key not in dict
                if (!(team in chartData.data)) {
                    chartData.labels.push(team);
                    chartData.data[team] = 0;
                }
                chartData.data[team] = chartData.data[team] + match.minutes_played;
            }
            //console.log(chartData);
            let data = [];
            for (let i = 0; i < chartData.labels.length; i++) {
                data.push(chartData.data[chartData.labels[i]]);
            }
            barChart(chartData.title, chartData.labels, data);
        });
    }

    function displayMinutesPerMap() {
        getMatches(function (output) {
            chartData = {
                title: "Minutes Played per Map",
                labels: [],
                // key is map_name, value is the total minutes
                data: {}
            }
            for (let i = 0; i < output.matches.length; i++) {
                const match = output.matches[i];
                const map = match.map_name;
                // if key not in dict
                if (!(map in chartData.data)) {
                    chartData.labels.push(map);
                    chartData.data[map] = 0;
                }
                chartData.data[map] = chartData.data[map] + match.minutes_played;
            }
            //console.log(chartData);
            let data = [];
            for (let i = 0; i < chartData.labels.length; i++) {
                data.push(chartData.data[chartData.labels[i]]);
            }
            barChart(chartData.title, chartData.labels, data);
        });
    }
});