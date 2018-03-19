$(document).ready(function () {
    //enable responsove charts
    window.onbeforeprint = function () {
        for (var id in Chart.instances) {
            Chart.instances[id].resize();
        }
    }
});
var ratio = 2;
var minHeight = 300;

function initChart() {
    $(document).ready(function () {
        var chartId = 'funcChart';
        var chartLabel = 'Функція належності до нечіткої множини'

        var axisLabels = [];
        var color = [];
        var data = [];

        $('.param-name').each(function (index) {
            axisLabels.push(this.value);
            color.push("rgba(48, 63, 159, 0.4)");
        });
        $('.calculated-func').each(function (index) {
            data.push(this.innerHTML);
        });


        var ctx = document.getElementById(chartId).getContext('2d');
        var chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: axisLabels,
                datasets: [{
                    data: data,
                    backgroundColor: color[0],
                    borderColor: color[0],
                    borderWidth: 1,
                    lineTension: 0
                }],
            },
            options: {
                //manual aspect ratio 2/1
                maintainAspectRatio: true,
                onResize: function (chart, size) {
                    if (size.width / ratio < minHeight)
                        size.height = minHeight;
                    else
                        size.height = size.width / ratio;
                    chart.update();
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 1.1,
                            stepSize: 0.1
                        }
                    }]
                },
                title: {
                    display: true,
                    text: chartLabel,
                    fontsize: 16
                },
                legend: {
                    display: false,
                },
                tooltips: {
                    enabled: true,
                    callbacks: {
                        title: function (tooltipItemArr, data) {
                            var label = tooltipItemArr[0].xLabel;
                            return label;
                        },
                        label: function (tooltipItem, data) {
                            var label = " " + (tooltipItem.yLabel * 100)  + " %";
                            return label;
                        }
                    }
                }
            }
        });
    });
};