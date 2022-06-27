window.onload = function () {
    var url = "Dashboard";
    enviarServidor(url, mostrarLista);
};

var venta1;
var venta2;
var venta3;

var compra1;
var compra2;
var compra3;

function mostrarLista(rpta) {
    if (rpta != null) {
        var a = JSON.parse(rpta); // Object(rpta);
        venta1 = [a[0][0].toString(), [a[0][2], a[0][3], a[0][4], a[0][5], a[0][6], a[0][7], a[0][8], a[0][9], a[0][10], a[0][11], a[0][12], a[0][13]], a[0][1]];
        venta2 = [a[1][0].toString(), [a[1][2], a[1][3], a[1][4], a[1][5], a[1][6], a[1][7], a[1][8], a[1][9], a[1][10], a[1][11], a[1][12], a[1][13]], a[1][1]];
        venta3 = [a[2][0].toString(), [a[2][2], a[2][3], a[2][4], a[2][5], a[2][6], a[2][7], a[2][8], a[2][9], a[2][10], a[2][11], a[2][12], a[2][13]], a[2][1]];

        compra1 = [a[3][0].toString(), [a[3][2], a[3][3], a[3][4], a[3][5], a[3][6], a[3][7], a[3][8], a[3][9], a[3][10], a[3][11], a[3][12], a[3][13]], a[3][1]];
        compra2 = [a[4][0].toString(), [a[4][2], a[4][3], a[4][4], a[4][5], a[4][6], a[4][7], a[4][8], a[4][9], a[4][10], a[4][11], a[4][12], a[4][13]], a[4][1]];
        compra3 = [a[5][0].toString(), [a[5][2], a[5][3], a[5][4], a[5][5], a[5][6], a[5][7], a[5][8], a[5][9], a[5][10], a[5][11], a[5][12], a[5][13]], a[5][1]];

        document.getElementById("lblV_Year1").innerHTML = a[0][0];
        document.getElementById("lblV_Total1").innerHTML = a[0][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;

        document.getElementById("lblV_Year2").innerHTML = a[1][0];
        document.getElementById("lblV_Total2").innerHTML = a[1][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;

        document.getElementById("lblV_Year3").innerHTML = a[2][0];
        document.getElementById("lblV_Total3").innerHTML = a[2][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;


        document.getElementById("lblC_Year1").innerHTML = a[3][0];
        document.getElementById("lblC_Total1").innerHTML = a[3][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;

        document.getElementById("lblC_Year2").innerHTML = a[4][0];
        document.getElementById("lblC_Total2").innerHTML = a[4][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;

        document.getElementById("lblC_Year3").innerHTML = a[5][0];
        document.getElementById("lblC_Total3").innerHTML = a[5][1].toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');;

        demoHighCharts2.init();
    }
}

var demoHighCharts2 = function () {
    var anchoBar = document.getElementById("pchart2").offsetWidth;
    var main = document.getElementById("main");
    var loader = document.getElementById("loader");
    loader.style.display = "none";
    main.style.display = "block";
    var anchoPie = document.getElementById("pie1").clientWidth;
    // Hichcharts colors
    var highColors = [bgWarning, bgPrimary, bgInfo, bgAlert,
        bgDanger, bgSuccess, bgSystem, bgDark
    ];

    // High Charts Demo
    var demoHighCharts = function () {

        // High Column Charts
        var demoHighLines = function () {

            var line1 = $('#high-line');

            if (line1.length) {

                // High Line 1
                $('#high-line').highcharts({
                    credits: false,
                    colors: highColors,
                    chart: {
                        type: 'column',
                        zoomType: 'x',
                        panning: true,
                        panKey: 'shift',
                        width: anchoBar
                        //marginRight: 50,
                        //marginTop: -5,
                    },
                    title: {
                        text: null
                    },
                    xAxis: {
                        gridLineColor: '#e5eaee',
                        lineColor: '#e5eaee',
                        tickColor: '#e5eaee',
                        categories: ['Jan', 'Feb', 'Mar', 'Apr',
                            'May', 'Jun', 'Jul', 'Aug',
                            'Sep', 'Oct', 'Nov', 'Dec'
                        ]
                    },
                    yAxis: {
                        //min: -2,
                        //tickInterval: 5,
                        gridLineColor: '#e5eaee',
                        title: {
                            text: 'Vemtas',
                            style: {
                                color: bgInfo,
                                fontWeight: '600'
                            }
                        }
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 3
                        },
                        area: {
                            fillOpacity: 0.2
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    series: [{
                        name: venta1[0],//'Google',
                        data: venta1[1] //[26.5, 23.3, 18.3, 5.7, 8.5, 11.9, 15.2, 7.0, 6, 9, 13.9, 9.6]
                    }, {
                        name: venta2[0],//'Bing',
                        data: venta2[1]//[3.9, 4.2, 14, 18, 21.5, 25.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                    }, {
                        //visible: false,
                        name: venta3[0],//'Yahoo',
                        data: venta3[1]//[1, 5, 5.7, 11.3, 20.1, 14.1, 18, 21.5, 25.2, 26.5, 8.6, 2.5]
                    }]
                });

            }

            var line2 = $('#high-line2');

            if (line2.length) {

                // High Line 1
                $('#high-line2').highcharts({
                    credits: false,
                    colors: highColors,
                    chart: {
                        type: 'column',
                        zoomType: 'x',
                        panning: true,
                        panKey: 'shift',
                        width: anchoBar
                        //marginRight: 50,
                        //marginTop: -5
                    },
                    title: {
                        text: null
                    },
                    xAxis: {
                        gridLineColor: '#e5eaee',
                        lineColor: '#e5eaee',
                        tickColor: '#e5eaee',
                        categories: ['Jan', 'Feb', 'Mar', 'Apr',
                            'May', 'Jun', 'Jul', 'Aug',
                            'Sep', 'Oct', 'Nov', 'Dec'
                        ]
                    },
                    yAxis: {
                        //min: -2,
                        //tickInterval: 5,
                        gridLineColor: '#e5eaee',
                        title: {
                            text: 'Compras',
                            style: {
                                color: bgInfo,
                                fontWeight: '600'
                            }
                        }
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 3
                        },
                        area: {
                            fillOpacity: 0.2
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    series: [{
                        name: compra1[0],//'Click',
                        data: compra1[1]//[26.5, 23.3, 18.3, 5.7, 8.5, 11.9, 15.2, 7.0, 6, 9, 13.9, 9.6]
                    }, {
                        name: compra2[0],//'Bing',
                        data: compra2[1]//[3.9, 4.2, 14, 18, 21.5, 25.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                    }, {
                        name: compra3[0],//'Yahoo',
                        data: compra3[1]//[1, 5, 5.7, 11.3, 20.1, 14.1, 18, 21.5, 25.2, 26.5, 8.6, 2.5]
                    }]
                });

            }
        }; // End High Line Charts Demo

        // Hihg Pies Demo
        var demoHighPies = function () {

            var pie1 = $('#high-pie1');

            if (pie1.length) {

                // Pie Chart1
                $('#high-pie1').highcharts({
                    credits: false,
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        margin: [20, 20, 20, 20]
                    },
                    title: {
                        text: null
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            center: ['30%', '50%'],
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true,
                            size: '100%'
                        }
                    },
                    colors: highColors,
                    legend: {
                        x: 90,
                        floating: true,
                        verticalAlign: "middle",
                        layout: "vertical",
                        itemMarginTop: 10
                    },
                    series: [{
                        type: 'pie',
                        name: 'Porcentaje',
                        data: [
                            [venta1[0], venta1[2]],
                            [venta2[0], venta2[2]],
                            [venta3[0], venta3[2]],
                        ]
                    }]
                });
            }

            var pie2 = $('#high-pie2');

            if (pie2.length) {

                // Pie Chart1
                $('#high-pie2').highcharts({
                    credits: false,
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        margin: [20, 20, 20, 20]
                    },
                    title: {
                        text: null
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            center: ['30%', '50%'],
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true,
                            size: '100%'
                        }
                    },
                    colors: highColors,
                    legend: {
                        x: 90,
                        floating: true,
                        verticalAlign: "middle",
                        layout: "vertical",
                        itemMarginTop: 10
                    },
                    series: [{
                        type: 'pie',
                        name: 'Porcentaje',
                        data: [
                            [compra1[0], compra1[2]],
                            [compra2[0], compra2[2]],
                            [compra3[0], compra3[2]],
                        ]
                    }]
                });
            }
        }; // End High Pie Demo

        // Init Chart Types
        demoHighLines();
        demoHighPies();


    }; // End High Charts Demo

    // High Charts Menu Demo
    var demoHighChartMenus = function () {

        // Create menus for charts with ".chart-legend" class
        var chartLegend = $('.chart-legend');

        if (chartLegend.length) {

            $('.chart-legend').each(function (i, ele) {
                var legendID = $(ele).data('chart-id');
                $(ele).find('a.legend-item').each(function (i, e) {
                    var This = $(e);
                    var itemID = This.data(
                        'chart-id');
                    // Find chart by menu ID and find it data
                    var legend = $(legendID).highcharts()
                        .series[itemID];
                    // get legend name to add menu buttons
                    var legendName = legend.name;
                    This.html(legendName);
                    // Set click handler
                    This.click(function (e) {
                        if (This.attr(
                                'href')) {
                            e.preventDefault();
                        }
                        if (legend.visible) {
                            legend.hide();
                            This.toggleClass(
                                'active'
                            );
                        } else {
                            legend.show();
                            This.toggleClass(
                                'active'
                            );
                        }
                    });
                });
            });
        }

        // Create custom menus for table charts
        var tableLegend = $('.table-legend');

        if (tableLegend.length) {

            $('.table-legend').each(function (i, e) {
                var legendID = $(e).data('chart-id');
                $(e).find('input.legend-switch').each(
                    function (i, e) {
                        var This = $(e);
                        var itemID = This.val();
                        // Find chart by menu ID and find it data
                        var legend = $(legendID).highcharts()
                            .series[itemID];
                        // pull legend name from chart and populate menu buttons
                        var legendName = legend.name;
                        This.html(legendName);
                        // Set checkbox if visible
                        if (legend.visible) {
                            This.attr('checked', true);
                        } else {
                            This.attr('checked', false);
                        }
                        // Set click handler
                        This.on('click', function (i, e) {
                            if (legend.visible) {
                                legend.hide();
                                This.attr(
                                    'checked',
                                    false);
                            } else {
                                legend.show();
                                This.attr(
                                    'checked',
                                    true);
                            }
                        });
                    });
            });
        }

    }; // End High Chart Menus Demo

    return {
        init: function () {
            // Init Demo Charts 
            demoHighCharts();
            demoHighChartMenus();
        }
    }
}();