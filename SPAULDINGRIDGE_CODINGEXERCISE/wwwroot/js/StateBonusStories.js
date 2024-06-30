import "./kendo-ui-license.js";
var stateBonusStories;
class StateBonusStories {
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
            url: '/api/SalesForecasting/GetSatechartSalesData',    // URL to send the request to
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
        // Initialize the Kendo Chart
        $("#chart").kendoChart({
            dataSource: {
                data: dataSource
            },
            series: [{
                type: "column", 
                field: "seedingSales", 
                name: "Seeding Year Sales",
                color:"red"
            }, {
                type: "column", 
                    field: "forecastedSaled", 
                    name: "Forecasted Year Sales",
                color:"blue"
            }],
            categoryAxis: {
                field: "state",
                labels: {
                    rotation: -45 
                }
            },
            valueAxis: {
                title: {
                    text: "Sales" // Specify the title for the value axis
                }
            },
            tooltip: {
                visible: true,
                format: "{0}" // Specify tooltip format
            }
        });
    }
}
stateBonusStories = new StateBonusStories();