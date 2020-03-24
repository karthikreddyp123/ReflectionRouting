$(document).ready(function () {
    $('.box').toggleClass("open-more");
    $(".icon").click(function () {
        $('.box').toggleClass("open-more");
        $(this).toggleClass("button");
    });
});
//for google charts
function showInChart(i, j, ctrl) {
    //to highlight the selected event.
    var list = document.getElementById("myUL").getElementsByTagName('li');
    for (p = 0; p < list.length; p++) {
        list[p].style.background = 'white';
    }
    ctrl.style.background = '#76CDD8';
    // pie chart code starts here.
    google.charts.load("current", {
        packages: ["corechart"]
    });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {
        var data = google.visualization.arrayToDataTable([
            ['Registered Students', 'Attended Students'],
            [' Registered', i],
            [' Attended', j],
        ]);
        var options = {
            title: 'Event Details',
            is3D: true,
        };
        var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
        chart.draw(data, options);
        //histogram code starts here.
        var data1 = google.visualization.arrayToDataTable([
            ['Registered Students', 'Attended Students'],
            [' Event1', 0.2],
            [' Event2', 0.6],
            [' Event3', 0.3],
        ]);
        var options1 = {
            title: 'Each Event-Student Comparison',
            legend: {
                position: 'none'
            },
        };
        var chart1 = new google.visualization.Histogram(document.getElementById('chart_div'));
        chart1.draw(data1, options1);
    }
}
