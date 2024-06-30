import "./kendo-ui-license.js";
var bonusStories;
class BonusStories {
    constructor() {
        this.EventHandler();
        this.CreateDropdown();
        this.GetChartData();
        this.CreateChart();

    }
    EventHandler() {
        $("#year").change((s) => {
            this.GetChartData();
        });
    }
    CreateDropdown() {
        $("#year").kendoDropDownList({
            dataTextField: "year",
            dataValueField: "year",
            height: "400px",
            dataSource: [{ year: "2018" }, { year: "2019" }, { year: "2020" }, { year: "2021" }],
            filter: "contains"
        });
    }
    GetChartData() {
        $.ajax({
            url: '/api/SalesForecasting/GetchartSalesData',    // URL to send the request to
            method: 'GET',               // HTTP method (GET, POST, PUT, DELETE, etc.)
            dataType: 'json',            // Expected data type from the server
            data: {                      // Data to send to the server (for POST or PUT requests)
                year: $("#year").val(),
            },
            success: (response) => {  // Callback function to handle successful response
                this.CreateChart(response);    // Handle the response data here
            },
            error: (xhr, status, error) => {  // Callback function to handle error response
                console.error(status + ': ' + error);  // Handle the error here
            }
        });
    }
    CreateChart(dataSource) {
        var chart = $("#chart").data("kendoChart");
        if (chart) {
            chart.destroy();
            $("#chart").empty(); // Clear the container element
        }
        $("#chart").kendoChart({
            dataSource: {
                data: dataSource
            },
            seriesDefaults: {
                type: "column", 
                stack: false 
            },
            series: [{
                field: "totalSales", 
                name: "Sales",
            }],
            categoryAxis: {
                field: "yearType", 
                labels: {
                    rotation: -45,
             
                }
            },
            valueAxis: {
                title: {
                    text: "Sales" 
                }
            },
            tooltip: {
                visible: true,
                format: "{0}" 
            }
        });
    };
}
bonusStories = new BonusStories();