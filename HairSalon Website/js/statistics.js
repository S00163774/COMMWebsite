// Chart to Display Number of each Product Sold

var numOfProducts = document.getElementsByName("ProductOrderCount").length;
var ProductCountID = [];
var ProductCountName = [];
var ProductCountOrders = [];

for (i = 0; i < numOfProducts; i++)
{
    var ProductOrderCount = document.getElementsByName("ProductOrderCount")[i].value;
    var splitProductOrderCounts = ProductOrderCount.split(",");

    ProductCountID.push(splitProductOrderCounts[0]);
    ProductCountName.push(splitProductOrderCounts[1]);
    ProductCountOrders.push(parseInt(splitProductOrderCounts[2]));
}

Highcharts.chart('ProductOrderCountChart', {
    chart: {
        type: 'area',
        inverted: true
    },
    title: {
        text: 'Number of each Product sold'
    },
    subtitle: {
        style: {
            position: 'absolute',
            right: '0px',
            bottom: '10px'
        }
    },
    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'top',
        x: -150,
        y: 100,
        floating: true,
        borderWidth: 1,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
    },
    xAxis: {
        categories: ProductCountName
    },
    yAxis: {
        title: {
            text: 'Number of units sold'
        },
        labels: {
            formatter: function () {
                return this.value;
            }
        },
        min: 0
    },
    plotOptions: {
        area: {
            fillOpacity: 0.5
        }
    },
    series: [{
        name: 'Product Sold',
        data: ProductCountOrders
    }]
});