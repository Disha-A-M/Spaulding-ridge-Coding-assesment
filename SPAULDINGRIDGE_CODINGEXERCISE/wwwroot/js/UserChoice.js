
import "./kendo-ui-license.js";
var userChoice;
class UserChoice {
    constructor() {
        this.EventHandler();
        this.CreateDropdown();
        this.GetDataForGrid();
    }
    EventHandler() {
        $("#year").change((s) => {
            this.GetDataForGrid();
        });
        $("#export").click((s) => {
            var grid = $("#totalSalesGrid").data("kendoGrid");
            grid.bind("excelExport", function (e) {
                e.workbook.fileName = "Sales Forecast" + ".xlsx";
            });
        });
        $("#add").click((s) => {
            this.GetDataForGrid();
        })

    }
    GetDataForGrid() {
        $.ajax({
            url: '/api/SalesForecasting/GetSalesData',    
            method: 'GET',              
            dataType: 'json',           
            data: {                      
                year: $("#year").val(),
                Increment: $("#increment").val()
            },
            success: (response) => { 
                if ($("#increment").val() == 0)
                    $('#value').text('Total Sales: ' + response.map(obj => obj.totalSales).reduce((a, b) => a + b, 0).toFixed(2));
                else
                    $('#value').text('Total Sales: ' + response.map(obj => obj.percentageIncrease).reduce((a, b) => a + b, 0));
                this.CreateGrid(response);    
            },
            error:  (xhr, status, error)=> { 
                console.error(status + ': ' + error); 
            }
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
    CreateGrid(dataSource) {
        var grid = $("#totalSalesGrid").data('kendoGrid');
        if (grid) {
            grid.destroy();
            $("#totalSalesGrid").empty(); // Clear the container element
        }
          var griddata= $("#totalSalesGrid").kendoGrid({
            dataSource: {
                data: dataSource,
                schema: {
                    model: {
                        fields: {
                            state: { type: "STATE" },
                            totalsales: { type: "TOTAL_SALES" },
                            incrementsales: {type:"PERCENTAGE_INCREASE"}

                        }
                    }
                },
            },
            sortable: true,
            scrollable: true,
            columns: [
                { field: "state", title: "STATE" },
                { field: "totalSales", title: "TOTAL_SALES" },
                { field: "percentageIncrease", title:"PERCENTAGE_INCREASE"}
            ]
        }).data("kendoGrid");
        if ($("#increment").val() == 0) {
            griddata.hideColumn("percentageIncrease");
        }

    }
};
userChoice = new UserChoice();