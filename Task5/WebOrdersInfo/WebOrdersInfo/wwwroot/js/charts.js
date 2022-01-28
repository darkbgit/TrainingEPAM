window.onload = function () {
    createManagersChart(0, 0);
    loadTopManagers();
};

loadTopManagers = function () {
    let fromTop = document.querySelector('#topManagersFromTop');
    let takeNumber = document.querySelector('#topManagersNumber');
    $.ajax({
        type: 'GET',
        url: '/Statistics/TopManagersByOrders',
        data: {
            fromTop: fromTop.value,
            take: takeNumber.value
        },
        dataType: 'json',
        success: function (response) {
            let xLabels = [];
            let yValues = [];
            $.each(response.value,
                function(idx, el) {
                    xLabels.push(el.name);
                    yValues.push(el.count);
                });
            //createManagersChart(xLabels, yValues);
            updateTopManagers(window.topManagersChart, xLabels, yValues);
        }
    });
}

createManagersChart = function (xLabels, yValues, count) {
    var chartName = "topManagersByOrdersChart";
    var ctx = document.getElementById(chartName).getContext('2d');
    var data = {
        labels: xLabels,
        datasets: [{
            label: "Топ менеджеров по заказам",
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(255, 0, 0)',
                'rgba(0, 255, 0)',
                'rgba(0, 0, 255)',
                'rgba(192, 192, 192)',
                'rgba(255, 255, 0)',
                'rgba(255, 0, 255)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
                'rgba(255, 0, 0)',
                'rgba(0, 255, 0)',
                'rgba(0, 0, 255)',
                'rgba(192, 192, 192)',
                'rgba(255, 255, 0)',
                'rgba(255, 0, 255)'
            ],
            borderWidth: 1,
            data: yValues
        }]
    };

    var options = {
        maintainAspectRatio: false,
        scales: {
            y: {
                ticks: {
                    //min: 0,
                    beginAtZero: true
                },
                gridLines: {
                    display: true,
                    color: "rgba(255,99,164,0.2)"
                }
            },
            x: {
                //ticks: {
                //    min: 0,
                //    beginAtZero: true
                //},
                gridLines: {
                    display: false
                }
            }
        }
    };

    if (window.topManagersChart !== undefined)
        window.topManagersChart.destroy();

    window.topManagersChart = new Chart(ctx, {
        options: options,
        data: data,
        type: 'bar'
    });
};



function addData(chart, label, data) {
    chart.data.labels = label;
    chart.data.datasets[0].data = data;
    chart.update();
}

function removeData(chart) {
    chart.data.labels.pop();
    chart.data.datasets[0].data.pop();
    //chart.update();
}


function updateTopManagers(chart, label, data) {
    removeData(chart);
    addData(chart, label, data);
}