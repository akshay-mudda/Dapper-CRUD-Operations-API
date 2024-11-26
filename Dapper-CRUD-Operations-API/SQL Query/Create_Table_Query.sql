CREATE TABLE Dapper_tbl(
Id int not null PRIMARY KEY IDENTITY,
Column1 nvarchar(100) not null,
Column2 nvarchar(100) not null,
Column3 nvarchar(100) not null,
Column4 Decimal(16,2) not null,
Column5 nvarchar(MAX) not null,
CreateDate datetime2 not null default current_timestamp
);