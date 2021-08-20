const $myDataTable = $('#my-data-table');

$myDataTable.DataTable();

$myDataTable.on('click', '.clickable', function () {
    const id = $(this)[0].id.split("__")[1];
    let url = `${baseUrl}/matches/match/${id}`;
    window.location.href = url;
});

getMatches(function (output) {
    var dataTable = $('#my-data-table').DataTable();
    if (output.matches.length > 0) {
        for (let i = 0; i < output.matches.length; i++) {
            let match = output.matches[i];
            dataTable.row.add([
                `<td><span class="hidden">${match.datetime_start}</span>${convertFromUTC(match.datetime_start)}</td>`,
                `${match.minutes_played} minutes`,
                match.map_name in decodes.mapDecodes ? decodes.mapDecodes[match.map_name] : match.map_name,
                match.team in decodes.teamDecodes ? decodes.teamDecodes[match.team] : match.team
            ]).node().id = `match__${match.id}`;
        }
        dataTable.draw();
        dataTable.rows().nodes().to$().addClass('clickable');
    }            
});