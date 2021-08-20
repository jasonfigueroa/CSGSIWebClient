const urlSplit = window.location.href.split('/');
const id = urlSplit[urlSplit.length - 1];
getMatch(id, function (output) {
    const match = output;
    const win = match.team != match.round_win_team ? false : true;
    $('#match-summary').append(`
        <dt>Match Start: </dt>
        <dd>${convertFromUTC(match.datetime_start)}</dd>

        <dt>Duration: </dt>
        <dd>${match.minutes_played} minutes</dd>

        <dt>Map: </dt>
        <dd>${match.map_name in decodes.mapDecodes ? decodes.mapDecodes[match.map_name] : match.map_name}</dd>

        <dt>Team: </dt>
        <dd>${match.team in decodes.teamDecodes ? decodes.teamDecodes[match.team] : match.team}</dd>

        <dt>Win/Loss: </dt>
        <dd><span class="${win ? "text-success" : "text-danger"}">${win ? "Win" : "Loss"}</span></dd>
    `);
    const stats = match.match_stats;
    $('#match-player-stats').append(`
        <dt>KDR: </dt>
        <dd>${(stats.deaths == 0 && stats.kills > 0 ? stats.kills / 1 : stats.kills / stats.deaths).toFixed(2)}</dd>

        <dt>Kills: </dt>
        <dd>${stats.kills}</dd>

        <dt>Assists: </dt>
        <dd>${stats.assists}</dd>

        <dt>Deaths: </dt>
        <dd>${stats.deaths}</dd>

        <dt>MVPs: </dt>
        <dd>${stats.mvps}</dd>

        <dt>Score: </dt>
        <dd>${stats.score}</dd>
    `);
});