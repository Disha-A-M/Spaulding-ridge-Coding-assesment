CREATE PROCEDURE  GET_CHART_DATA_BASED_ON_SITE(@YEAR INT)
AS
BEGIN

SELECT
    State,
    SUM(CASE WHEN YEAR(Order_Date) = @YEAR THEN Sales * Quantity * (1 - Discount) ELSE 0 END) AS SeedingYearSales,
    SUM(CASE WHEN YEAR(Order_Date)=2021  THEN Sales * Quantity * (1 - Discount) ELSE 0 END) AS ForecastedYearSales
FROM
    ORDERS o
    INNER JOIN PRODUCTS p ON o.Order_Id = p.Order_Id
GROUP BY
    State;

END