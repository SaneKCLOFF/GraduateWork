CREATE DATABASE ProductsService
GO
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
	RoleId INT REFERENCES Roles(ID) NOT NULL,
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
	CategoryId INT REFERENCES ProductCategories(ID) NOT NULL,
)

CREATE TABLE [Orders]
(
	ID INT IDENTITY PRIMARY KEY NOT NULL,
	ProductId INT REFERENCES Products(ID) NOT NULL,
	UserId INT REFERENCES Users(ID) NOT NULL,
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
	ServiceId INT REFERENCES [Services](ID) NOT NULL,
	RequestStatus NVARCHAR(20) NOT NULL
)
GO

INSERT INTO [dbo].[Roles]([Title])
     VALUES
           ('Администратор'),
		   ('Пользователь')
GO

INSERT INTO [dbo].[Users]([Login],[Password],[FirstName],[LastName],[MiddleName],[PhoneNumber],[Email],[RoleId])
     VALUES
           ('chel123','coolboy123','Адольф','Доронин','Николаевич','89274652143','svin@mail.ru',2),
		   ('admin','admin','Алексей','Шафутинский','Андреевич','89474655145','borovn@mail.ru',1)
GO

INSERT INTO [dbo].[ProductCategories]([Title])
     VALUES
          ('Бухгалтерия'), --1
		  ('ЗУП'), --2
		  ('Торговля'), --3
		  ('Отраслевые решения'), --4
		  ('Документооборот'), --5
		  ('Лицензии'), --6
		  ('Управленческий учет') --7
GO

INSERT INTO [dbo].[Products]([Title],[Description],[Image],[Cost],[CategoryId])
     VALUES
           ('«1С:Бухгалтерия». Версия ПРОФ','Решение 1C:Бухгалтерия 8 ПРОФ ред 3.0 включает учет торговых операций, кассовых и банковских операций, заработной платы, персонифицированный и кадровый учет.'
		   ,'product_1.png',13000,1),
		   ('1С:Бухгалтерия 8. Комплект на 5 пользователей','Программный продукт 1С:Бухгалтерия 8. Комплект на 5 пользователей включает 1С Бухгалтерия 8 ПРОФ и 1С:Предприятие 8. Клиентская лицензия на 5 рабочих мест.'
		   ,'product_2.png',26000,1),
		   ('1С:Бухгалтерия некоммерческой организации 8 КОРП','Разработана для системной работы с бухгалтерским и налоговым учетами, туда же включена подготовка необходимой отчетности, в некоммерческой организации, где используют план счетов бухгалтерского учета.'
		   ,'product_3.png',43200,1),
		   ('1С:Зарплата и управление персоналом 8 КОРП','Комплексное решение для средних и крупных организаций, с помощью него проще рационально распределять персонал организации с различным количеством штатных сотрудников.'
		   ,'product_4.png',100900,2),
		   ('1С:Розница 8',''
		   ,'product_5.png',3600,3),
		   ('1С:Предприятие 8. Автосервис','Конфигурация “1С:Предприятие 8. Автосервис” является отраслевым решением для автоматизации управления работой небольших автосервисов, автомоек, станций технического обслуживания автомобилей.'
		   ,'product_6.png',28000,4),
		   ('1С:Библиотека','Программа для автоматизации деятельности библиотек любого типа и назначения.'
		   ,'product_7.png',30000,4),
		   ('1С:Документооборот 8 КОРП','Программа “1С Документооборот 8 КОРП” ориентирована на бюджетные учреждения, средние и крупные коммерческие предприятия.'
		   ,'product_8.png',187000,5),
		   ('1С:Предприятие 8. Расширенная Корпоративная Лицензия','«1С:Предприятие 8. Расширенная Корпоративная Лицензия» (далее 1С:РКЛ) оформляется на определенный срок, в течение которого можно использовать версии и релизы платформы «1С:Предприятие 8 КОРП», выпущенные до окончания действия 1С:РКЛ.'
		   ,'product_9.png',300000,6),
		   ('1С:Управление нашей фирмой 8','Готовое комплексное решение для предприятий малого бизнеса.'
		   ,'product_10.png',17400,7)
GO

INSERT INTO [dbo].[Services]([Title],[Description])
     VALUES
           ('Обслуживание','Заботливое отношение, вежливое общение, помощь и поддержка - основа нашего сотрудничества по поддержке Ваших программных продуктов 1С.'),
		   ('Запуск','Запуск 1С – это серия мероприятий, направленных на то, чтобы 1С в компании заработала.'),
		   ('Техподдержка','Поддержка 1С – профессиональное решение вопросов по использованию 1С без ограничения по количеству обращений.')
GO

