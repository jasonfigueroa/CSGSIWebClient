// const decodes = {
//     mapDecodes: {
//         de_cache: "Cache",
//         de_dust2: "Dust II",
//         de_inferno: "Inferno",
//         de_mirage: "Mirage",
//         de_cbble: "Cobblestone",
//         de_train: "Train",
//         de_overpass: "Overpass",
//         de_nuke: "Nuke",
//         de_canals: "Canals",
//         de_austria: "Austria",
//         de_shipped: "Shipped"
//     },
//     teamDecodes: {
//         T: "Terrorists",
//         CT: "Counter-Terrorists"
//     }
// };

//  Params
//  title:      string representing the title of the chart
//  label:      list of the labels to be used along the x axis
//  data:       list of the data values to be charted corresponding to each label
//  type:       the type of chart you wish to display, ex.: bar, pie, doughnut
//  legend:     boolean value representing whether or not you wish to display a legend
//  gridAxist:  boolean value representing whether or not you wish to display the grid and axis details
function chartMe(charttitle, chartlabels, chartdata, charttype, chartlegend, displaygridAndAxisDetails) {
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
        type: charttype,
        data: {
            labels: actualLabels,
            datasets: [{
                label: charttitle,
                data: chartdata,
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',  // blue
                    'rgba(255, 159, 64, 0.2)',  // orange
                    'rgba(75, 192, 192, 0.2)',  // green
                    'rgba(255, 99, 132, 0.2)',  // red
                    'rgba(153, 102, 255, 0.2)', // purple
                    'rgba(255, 206, 86, 0.2)',  // yellow
                    'rgba(54, 162, 235, 0.2)',  // blue
                    'rgba(255, 159, 64, 0.2)',  // orange
                    'rgba(75, 192, 192, 0.2)',  // green
                    'rgba(255, 99, 132, 0.2)',  // red
                    'rgba(153, 102, 255, 0.2)', // purple
                    'rgba(255, 206, 86, 0.2)'  // yellow
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',    // blue
                    'rgba(255, 159, 64, 1)',    // orange
                    'rgba(75, 192, 192, 1)',    // green
                    'rgba(255,99,132,1)',       // red
                    'rgba(153, 102, 255, 1)',   // purple
                    'rgba(255, 206, 86, 1)',    // yellow
                    'rgba(54, 162, 235, 1)',    // blue
                    'rgba(255, 159, 64, 1)',    // orange
                    'rgba(75, 192, 192, 1)',    // green
                    'rgba(255,99,132,1)',       // red
                    'rgba(153, 102, 255, 1)',   // purple
                    'rgba(255, 206, 86, 1)',    // yellow
                ],
                borderWidth: 1
            }]
        },
        options: {
            legend: { display: chartlegend },
            title: {
                display: true,
                text: charttitle
            },
            scales: {
                xAxes: [{
                    display: displaygridAndAxisDetails,
                    ticks: {
                        beginAtZero: true
                    }
                }],
                yAxes: [{
                    display: displaygridAndAxisDetails,
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
