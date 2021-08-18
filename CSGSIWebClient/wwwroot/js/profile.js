$(document).ready(function () {
    function getKdr(kills, deaths) {
        return deaths == 0 && kills > 0 ? kills / 1 : kills / deaths;
    }

    function displayPlayerProfile() {
        getMatches(function (output) {
            console.log(output);
            const matches = output.matches;
            if (matches.length > 0) {
                const initialMatch = matches[0];
                let totalKills = 0;
                let totalDeaths = 0;
                let highestScoreMatch = initialMatch;
                let bestKdrMatch = initialMatch;
                let lastMatch = initialMatch;
                for (let i = 0; i < matches.length; i++) {
                    const match = matches[i]
                    if (getKdr(match.match_stats.kills, match.match_stats.deaths) > getKdr(bestKdrMatch.match_stats.kills, bestKdrMatch.match_stats.deaths)) {
                        bestKdrMatch = match;
                    }
                    totalKills += match.match_stats.kills;
                    totalDeaths += match.match_stats.deaths;
                    if (highestScoreMatch.match_stats.score < match.match_stats.score) {
                        highestScoreMatch = match;
                    }
                    if (match.datetime_start > lastMatch.datetime_start) {
                        lastMatch = match;
                    }
                }

                const averageKdr = totalDeaths == 0 && totalKills > 0 ? totalKills / 1 : totalKills / totalDeaths;

                $('#profile-summary-div dl').append(`
                <dt>Average KDR: </dt>
                <dd>${averageKdr.toFixed(2)}</dd>

                <dt>Highest Match Score: </dt>
                <dd><a href="${baseUrl}/matches/match/${highestScoreMatch.id}">${highestScoreMatch.match_stats.score}</a></dd>

                <dt>Best Match KDR: </dt>
                <dd><a href="${baseUrl}/matches/match/${bestKdrMatch.id}">${getKdr(bestKdrMatch.match_stats.kills, bestKdrMatch.match_stats.deaths).toFixed(2)}</a></dd>

                <dt>Last Match: </dt>
                <dd><a href="${baseUrl}/matches/match/${lastMatch.id}">${convertFromUTC(lastMatch.datetime_start)}</a></dd>
            `);
            }            
        });
    }
    
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

    function displayMinutesPerTeam() {
        getMatches(function (output) {
            if (output.matches.length > 0) {
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
                let data = [];
                for (let i = 0; i < chartData.labels.length; i++) {
                    data.push(chartData.data[chartData.labels[i]]);
                }
                chartMe(chartData.title, chartData.labels, data, "horizontalBar", false, true);
            }            
        });
    }

    function displayMinutesPerMap() {
        getMatches(function (output) {
            if (output.matches.length > 0) {
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
                let data = [];
                for (let i = 0; i < chartData.labels.length; i++) {
                    data.push(chartData.data[chartData.labels[i]]);
                }
                chartMe(chartData.title, chartData.labels, data, "bar", false, true);
            }
        });
    }

    function displayKdrByMap() {
        getMatches(function (output) {
            if (output.matches.length > 0) {
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
            }   
        });
    }

    function displayKdrByTeam() {
        getMatches(function (output) {
            if (output.matches.length > 0) {
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
            }
        });
    }

    function displayWinsByTeam() {
        getMatches(function (output) {
            if (output.matches.length > 0) {
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
            }
        });
    }
    
    displayPlayerProfile();
    renderChart('profile');
});