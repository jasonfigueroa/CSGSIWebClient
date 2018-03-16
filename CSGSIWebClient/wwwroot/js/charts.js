const decodes = {
    mapDecodes: {
        de_cache: "Cache",
        de_dust2: "Dust II",
        de_inferno: "Inferno",
        de_mirage: "Mirage",
        de_cbble: "Cobblestone",
        de_train: "Train",
        de_overpass: "Overpass",
        de_nuke: "Nuke",
        de_canals: "Canals",
        de_austria: "Austria",
        de_shipped: "Shipped"
    },
    teamDecodes: {
        T: "Terrorists",
        CT: "Counter-Terrorists"
    }
};

// params:
//  title: string representing the title of the chart
//  label: list of the labels to be used along the x axis
//  data: list of the data values to be charted corresponding to each label
function barChart(charttitle, chartlabels, chartdata) {
    let decode = false;
    let actualLabels = [];
    let decodesType;
    if (chartlabels[0] in decodes.mapDecodes) {
        decode = true;
        decodesType = "mapDecodes";
    } else {
        decode = true;
        decodesType = "teamDecodes";
    }

    if (decode) {
        for (let i = 0; i < chartlabels.length; i++) {
            actualLabels.push(decodes[decodesType][chartlabels[i]]);
        }
    } else {
        actualLabels = chartlabels;
    }

    var ctx = document.getElementById("my-chart").getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: actualLabels,
            datasets: [{
                label: charttitle,
                data: chartdata,
                // background color of the bar
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                // border color of the bar
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}