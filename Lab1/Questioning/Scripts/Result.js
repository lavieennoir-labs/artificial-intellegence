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

function initChart(chartId, chartLabel, axisLabels, data, color) {
    $(document).ready(function () {
        var ctx = document.getElementById(chartId).getContext('2d');
        var chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: axisLabels,
                datasets: [{
                    data: data,
                    backgroundColor: color,
                    borderColor: color,
                    borderWidth: 1
                }]
            },
            options: {
                //manual aspect ratio 2/1
                maintainAspectRatio: false,
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
                            max: 5,
                            stepSize: 1
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Кількість балів"
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: "Питання #"
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
                            var label = "Питання #" + tooltipItemArr[0].xLabel;
                            return label;
                        },
                        label: function (tooltipItem, data) {
                            var num = tooltipItem.yLabel % 10;
                            var scoreText;
                            if (tooltipItem.yLabel % 100 > 4 && tooltipItem.yLabel % 100 < 20)
                                scoreText = "балів"
                            else if (num === 1)
                                scoreText = "бал";
                            else if (num > 1 && num < 5)
                                scoreText = "бали";
                            else
                                scoreText = "балів"
                            var label = Math.round(tooltipItem.yLabel * 100) / 100 + " " + scoreText;
                            return label;
                        }
                    }
                }
            }
        });
    });
};

function initChartCompare(chartId, chartLabel, axisLabels, data, color) {
    $(document).ready(function () {
        var ctx = document.getElementById(chartId).getContext('2d');
        var chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: axisLabels,
                datasets: [{
                    data: data,
                    backgroundColor: color,
                    borderColor: color,
                    borderWidth: 1
                }]
            },
            options: {
                //manual aspect ratio 2/1
                maintainAspectRatio: false,
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
                            max: 100,
                            callback: function (value, index, values) {
                                return value + "%";
                            }
                        },
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: "Рівень"
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
                            var label = Math.round(tooltipItem.yLabel * 100) / 100  + "%";
                            return label;
                        }
                    }
                }
            }
        });
    });
};