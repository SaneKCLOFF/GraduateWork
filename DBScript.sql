--CREATE DATABASE ProductsService
--GO
USE ProductsService
GO

CREATE TABLE Roles
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	Title NVARCHAR(25) NOT NULL
)

CREATE TABLE [Users]
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50) NOT NULL,
	PhoneNumber NVARCHAR(11) NOT NULL,
	Email NVARCHAR(255) NOT NULL,
	RoleId INT REFERENCES Roles(ID) ON DELETE CASCADE NOT NULL,
)

CREATE TABLE ProductCategories
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	Title NVARCHAR(100) NOT NULL
)

CREATE TABLE Products
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Image] NVARCHAR(MAX) NOT NULL,
	Cost DECIMAL(10,2) NOT NULL,
	CategoryId INT REFERENCES ProductCategories(ID) ON DELETE CASCADE NOT NULL,
)

CREATE TABLE [Orders]
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	ProductId INT REFERENCES Products(ID) ON DELETE CASCADE NOT NULL,
	UserId INT REFERENCES Users(ID) ON DELETE CASCADE NOT NULL,
	OrderStatus NVARCHAR(20) NOT NULL
)

CREATE TABLE [Services]
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL
)

CREATE TABLE [Requests]
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	UserId INT REFERENCES Users(ID) NOT NULL,
	ServiceId INT REFERENCES [Services](ID) ON DELETE CASCADE NOT NULL,
	RequestStatus NVARCHAR(20) NOT NULL
)
GO

INSERT INTO [dbo].[Roles]([Title])
     VALUES
           ('�������������'),
		   ('������������')
GO

INSERT INTO [dbo].[Users]([Login],[Password],[FirstName],[LastName],[MiddleName],[PhoneNumber],[Email],[RoleId])
     VALUES
           ('chel123','coolboy123','������','�������','����������','89274652143','svin@mail.ru',2),
		   ('admin','admin','�������','�����������','���������','89474655145','borovn@mail.ru',1)
GO

INSERT INTO [dbo].[ProductCategories]([Title])
     VALUES
          ('�����������'), --1
		  ('���'), --2
		  ('��������'), --3
		  ('���������� �������'), --4
		  ('���������������'), --5
		  ('��������'), --6
		  ('�������������� ����') --7
GO

INSERT INTO [dbo].[Products]([Title],[Description],[Image],[Cost],[CategoryId])
     VALUES
           ('�1�:������������. ������ ����','������� 1C:����������� 8 ���� ��� 3.0 �������� ���� �������� ��������, �������� � ���������� ��������, ���������� �����, ������������������� � �������� ����.'
		   ,'product_1.png',13000,1),
		   ('1�:����������� 8. �������� �� 5 �������������','����������� ������� 1�:����������� 8. �������� �� 5 ������������� �������� 1� ����������� 8 ���� � 1�:����������� 8. ���������� �������� �� 5 ������� ����.'
		   ,'product_2.png',26000,1),
		   ('1�:����������� �������������� ����������� 8 ����','����������� ��� ��������� ������ � ������������� � ��������� �������, ���� �� �������� ���������� ����������� ����������, � �������������� �����������, ��� ���������� ���� ������ �������������� �����.'
		   ,'product_3.png',43200,1),
		   ('1�:�������� � ���������� ���������� 8 ����','����������� ������� ��� ������� � ������� �����������, � ������� ���� ����� ����������� ������������ �������� ����������� � ��������� ����������� ������� �����������.'
		   ,'product_4.png',100900,2),
		   ('1�:������� 8',''
		   ,'product_5.png',3600,3),
		   ('1�:����������� 8. ����������','������������ �1�:����������� 8. ���������� �������� ���������� �������� ��� ������������� ���������� ������� ��������� ������������, ��������, ������� ������������ ������������ �����������.'
		   ,'product_6.png',28000,4),
		   ('1�:����������','��������� ��� ������������� ������������ ��������� ������ ���� � ����������.'
		   ,'product_7.png',30000,4),
		   ('1�:��������������� 8 ����','��������� �1� ��������������� 8 ���ϔ ������������� �� ��������� ����������, ������� � ������� ������������ �����������.'
		   ,'product_8.png',187000,5),
		   ('1�:����������� 8. ����������� ������������� ��������','�1�:����������� 8. ����������� ������������� ��������� (����� 1�:���) ����������� �� ������������ ����, � ������� �������� ����� ������������ ������ � ������ ��������� �1�:����������� 8 ���ϻ, ���������� �� ��������� �������� 1�:���.'
		   ,'product_9.png',300000,6),
		   ('1�:���������� ����� ������ 8','������� ����������� ������� ��� ����������� ������ �������.'
		   ,'product_10.png',17400,7)
GO

INSERT INTO [dbo].[Services]([Title],[Description])
     VALUES
           ('������������','���������� ���������, �������� �������, ������ � ��������� - ������ ������ �������������� �� ��������� ����� ����������� ��������� 1�.'),
		   ('������','������ 1� � ��� ����� �����������, ������������ �� ��, ����� 1� � �������� ����������.'),
		   ('������������','��������� 1� � ���������������� ������� �������� �� ������������� 1� ��� ����������� �� ���������� ���������.')
GO

--DROP TABLE [Requests]
--DROP TABLE [Services]
--DROP TABLE [Orders]
--DROP TABLE Products
--DROP TABLE ProductCategories
--DROP TABLE [Users]
--DROP TABLE Roles
