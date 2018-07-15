USE model;
GO

CREATE DATABASE SellingDB;
GO

USE SellingDB;
GO

CREATE TABLE customers
(
	"ID" INT NOT NULL PRIMARY KEY IDENTITY,
	"FirstName" NVARCHAR(15) NOT NULL,
	"LastName" NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE sellers
(
	"ID" INT NOT NULL PRIMARY KEY IDENTITY,
	"FirstName" NVARCHAR(15) NOT NULL,
	"LastName" NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE selling
(
	"ID" INT NOT NULL PRIMARY KEY IDENTITY,
	"Customer_ID" INT NOT NULL
		FOREIGN KEY
		REFERENCES dbo.customers(ID),
	"Seller_ID" INT NOT NULL
		FOREIGN KEY
		REFERENCES dbo.sellers(ID),
	"DealAmount" SMALLMONEY NOT NULL,
	"DealDate" DATE NULL DEFAULT GETDATE()
);
GO

INSERT INTO dbo.customers
	(FirstName,LastName)
	VALUES
	('Steven', 'Rogers'),
	('Bruce', 'Banner'),
	('Tony', 'Stark'),
	('Peter', 'Parker'),
	('Edward', 'Brock'),
	('Nicholas', 'Fury'),
	('Wade', 'Wilson');
GO

INSERT INTO dbo.sellers
	(FirstName,LastName)
	VALUES
	('Homer', 'Simpson'),
	('Marge', 'Simpson'),
	('Bart', 'Simpson'),
	('Lisa', 'Simpson'),
	('Maggie', 'Simpson');
GO

INSERT INTO dbo.selling
(
    Customer_ID,
    Seller_ID,
    DealAmount,
    DealDate
)
VALUES
	(2, 1, 200, '06-01-2018'),
	(2, 1, 350, '06-02-2018'),
	(3, 2, 550, '06-18-2018'),
	(5, 3, 370, '06-24-2018'),
	(7, 3, 210, '06-27-2018'),
	(3, 5, 100, '07-03-2018'),
	(4, 5, 280, '07-05-2018'),
	(1, 1, 800, DEFAULT),
	(2, 1, 600, DEFAULT),
	(4, 3, 220, DEFAULT);
GO
