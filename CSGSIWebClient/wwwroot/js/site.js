$(document).ready(function () {
    const user = {};

    $('#my-data-table').DataTable();

    $('#my-data-table tbody').on('click', '.clickable', function () {
        const baseUrl = "http://localhost:49424/matches/match";
        //const baseUrl = "http://CSGSIStatTrakr.jasonfigueroa.io/matches/match";
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
        // change this for a switch statement later
        if ($('#chart-selection').val() == 'minutes-played-per-map') {
            displayMinutesPerMap();
        } else if ($('#chart-selection').val() == 'minutes-played-per-team') {
            displayMinutesPerTeam();
        } else if ($('#chart-selection').val() == 'kdr-by-map') {
            displayKdrByMap();
        } else if ($('#chart-selection').val() == 'kdr-by-team') {
            displayKdrByTeam();
        } else if ($('#chart-selection').val() == 'wins-by-team') {
            displayWinsByTeam();
        }
    }

    //if (window.location.href.indexOf("http://localhost:49424/Profile") > -1 || window.location.href.indexOf("http://localhost:49424/profile") > -1) {
    if (window.location.href.indexOf("http://csgsistattrakr.jasonfigueroa.io/Profile") > -1 || window.location.href.indexOf("http://csgsistattrakr.jasonfigueroa.io/profile") > -1) {
        renderChart();
    }

    function displayMinutesPerTeam() {
        getMatches(function (output) {
            let chartData = {
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
            chartMe(chartData.title, chartData.labels, data, "horizontalBar", false, true);
        });
    }

    function displayMinutesPerMap() {
        getMatches(function (output) {
            let chartData = {
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
            chartMe(chartData.title, chartData.labels, data, "bar", false, true);
        });
    }

    function displayKdrByMap() {
        getMatches(function (output) {
            let chartData = {
                title: "Kill Death Ratio by Map",
                labels: [],
                data: {}
            }
            for (let i = 0; i < output.matches.length; i++) {
                const match = output.matches[i];
                const map = match.map_name;
                // if key not in dict
                if (!(map in chartData.data)) {
                    chartData.labels.push(map);
                    chartData.data[map] = {
                        kills: 0,
                        deaths: 0
                    };
                }
                chartData.data[map].kills = chartData.data[map].kills + match.match_stats.kills;
                chartData.data[map].deaths = chartData.data[map].deaths + match.match_stats.deaths;
            }
            let data = [];
            for (let i = 0; i < chartData.labels.length; i++) {
                let kdr = 0;
                let mapObj = chartData.data[chartData.labels[i]];
                if (mapObj.deaths == 0 && mapObj.kill > 0) {
                    kdr = mapObj.kills / 1;
                } else {
                    kdr = mapObj.kills / mapObj.deaths;
                }
                data.push(Number.parseFloat(kdr).toFixed(2));
            }
            chartMe(chartData.title, chartData.labels, data, "pie", true, false);
        });
    }

    function displayKdrByTeam() {
        getMatches(function (output) {
            let chartData = {
                title: "Kill Death Ratio by Team",
                labels: [],
                data: {}
            }
            for (let i = 0; i < output.matches.length; i++) {
                const match = output.matches[i];
                const team = match.team;
                // if key not in dict
                if (!(team in chartData.data)) {
                    chartData.labels.push(team);
                    chartData.data[team] = {
                        kills: 0,
                        deaths: 0
                    };
                }
                chartData.data[team].kills = chartData.data[team].kills + match.match_stats.kills;
                chartData.data[team].deaths = chartData.data[team].deaths + match.match_stats.deaths;
            }
            let data = [];
            for (let i = 0; i < chartData.labels.length; i++) {
                let kdr = 0;
                let teamObj = chartData.data[chartData.labels[i]];
                if (teamObj.deaths == 0 && teamObj.kill > 0) {
                    kdr = teamObj.kills / 1;
                } else {
                    kdr = teamObj.kills / teamObj.deaths;
                }
                data.push(Number.parseFloat(kdr).toFixed(2));
            }
            chartMe(chartData.title, chartData.labels, data, "doughnut", true, false);
        });
    }

    function displayWinsByTeam() {
        getMatches(function (output) {
            let chartData = {
                title: "Wins by Team",
                labels: [],
                data: {}
            }
            for (let i = 0; i < output.matches.length; i++) {
                const match = output.matches[i];
                const team = match.team;
                const winningTeam = match.round_win_team;
                // if key not in dict
                if (!(team in chartData.data)) {
                    chartData.labels.push(team);
                    chartData.data[team] = {
                        wins: 0,
                        losses: 0
                    };
                }
                if (team == winningTeam) {
                    chartData.data[team].wins = chartData.data[team].wins + 1;
                } else {
                    chartData.data[team].losses = chartData.data[team].losses + 1;
                }
            }
            let data = [];
            for (let i = 0; i < chartData.labels.length; i++) {
                let teamObj = chartData.data[chartData.labels[i]];
                data.push(teamObj.wins);
            }
            chartMe(chartData.title, chartData.labels, data, "doughnut", true, false);
        });
    }

    if (window.location.href.indexOf("localhost:49424/matches") > -1 || window.location.href.indexOf("localhost:49424/Matches") > -1) {
        displayStatsTable();
    }

    function displayStatsTable() {
        getMatches(function (output) {
            var dataTable = $('#my-data-table').DataTable();
            for (let i = 0; i < output.matches.length; i++) {
                let match = output.matches[i];
                dataTable.row.add([
                    `<td><span class="hidden">${match.datetime_start}</span>${new Date(match.datetime_start * 1000).toLocaleString()}</td>`,
                    `${match.minutes_played} minutes`,
                    match.map_name in decodes.mapDecodes ? decodes.mapDecodes[match.map_name] : match.map_name,
                    match.team in decodes.teamDecodes ? decodes.teamDecodes[match.team] : match.team
                ]).node().id = `match__${match.id}`;
            }            
            dataTable.draw();
            $('tr').addClass('clickable');
        });
    }
});