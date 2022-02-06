with OrderedClients as (
    select row_number() over (order by Name) RowNum, *
    from [CsvManagerDB].[dbo].[Clients] (nolock)
),
OrderedManagers as (
    select row_number() over (order by Name) RowNum, *
    from [CsvManagerDB].[dbo].[Managers] (nolock)
)
,
OrderedProducts as (
    select row_number() over (order by Name) RowNum, *
    from [CsvManagerDB].[dbo].[Products] (nolock)
)
select c.Id as ClientId, c.Name as ClientName, m.Id as ManagerId, m.Name as ManagegName, p.Id as ProductId, p.Name as ProductName  
from OrderedClients c
    full outer join OrderedManagers m on m.RowNum = c.RowNum
	full outer join OrderedProducts p on p.RowNum = c.RowNum