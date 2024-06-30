alter PROCEDURE GET_CHART_DATA(@YEAR INT)
AS
BEGIN

WITH SeedingYearSales AS (
    SELECT
        YEAR(o.Order_Date) AS Year,
        SUM(p.Sales * p.Quantity * (1 - p.Discount)) AS SeedingYearSales
    FROM
        ORDERS o
        INNER JOIN PRODUCTS p ON o.Order_Id = p.Order_Id
    WHERE
        YEAR(o.Order_Date) = @YEAR
    GROUP BY
        YEAR(o.Order_Date)
),

ForecastedYearSales AS (
    SELECT
        YEAR(o.Order_Date) AS Year,
        SUM(p.Sales * p.Quantity * (1 - p.Discount)) AS ForecastedYearSales
    FROM
        ORDERS o
        INNER JOIN PRODUCTS p ON o.Order_Id = p.Order_Id
    WHERE
        YEAR(o.Order_Date) = 2021
    GROUP BY
        YEAR(o.Order_Date)
)

SELECT
    'Seeding Year' AS YearType,
    s.Year AS Year,
    s.SeedingYearSales AS Sales
FROM
    SeedingYearSales s

UNION ALL

SELECT
    'Forecasted Year' AS YearType,
    f.Year AS Year,
    f.ForecastedYearSales AS Sales
FROM
    ForecastedYearSales f;

END