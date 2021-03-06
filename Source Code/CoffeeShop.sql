USE [master]
GO
/****** Object:  Database [CoffeeShop]    Script Date: 4/4/2020 10:44:21 PM ******/
CREATE DATABASE [CoffeeShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoffeeShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\CoffeeShop.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CoffeeShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\CoffeeShop_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CoffeeShop] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoffeeShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoffeeShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoffeeShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoffeeShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeeShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoffeeShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoffeeShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoffeeShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoffeeShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoffeeShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoffeeShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CoffeeShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoffeeShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoffeeShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoffeeShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoffeeShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoffeeShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoffeeShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoffeeShop] SET RECOVERY FULL 
GO
ALTER DATABASE [CoffeeShop] SET  MULTI_USER 
GO
ALTER DATABASE [CoffeeShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoffeeShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoffeeShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoffeeShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CoffeeShop] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoffeeShop', N'ON'
GO
USE [CoffeeShop]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL DEFAULT (N'TD'),
	[Password] [nvarchar](1000) NOT NULL CONSTRAINT [DF__Account__Passwor__15502E78]  DEFAULT ((1)),
	[Type] [int] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[TypeID] [int] NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL DEFAULT (getdate()),
	[DateCheckOut] [date] NULL,
	[IDTable] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Discount] [int] NULL,
	[TotalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDBill] [int] NOT NULL,
	[IDFood] [int] NOT NULL,
	[Count] [int] NOT NULL CONSTRAINT [DF__BillInFo__count__24927208]  DEFAULT ((0)),
 CONSTRAINT [PK__BillInFo__3214EC27D09EFBC8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Food]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL CONSTRAINT [DF__Food__Name__1BFD2C07]  DEFAULT (N'No name'),
	[IDCategory] [int] NOT NULL,
	[Price] [float] NOT NULL CONSTRAINT [DF__Food__Price__1CF15040]  DEFAULT ((0)),
	[StatusFood] [bit] NULL CONSTRAINT [DF_Food_StatusFood]  DEFAULT ((1)),
 CONSTRAINT [PK__Food__3214EC277DBA3700] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL CONSTRAINT [DF__FoodCatego__Name__1920BF5C]  DEFAULT (N'No name'),
 CONSTRAINT [PK__FoodCate__3214EC2716FB1E24] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL CONSTRAINT [DF__TableFood__Name__108B795B]  DEFAULT (N'No name'),
	[Status] [nvarchar](100) NOT NULL CONSTRAINT [DF__TableFood__statu__117F9D94]  DEFAULT (N'Empty'),
	[Disable] [nvarchar](100) NOT NULL CONSTRAINT [DF__TableFood__Disab__40F9A68C]  DEFAULT (N'no'),
 CONSTRAINT [PK__TableFoo__3214EC27A3142E13] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountType] FOREIGN KEY([Type])
REFERENCES [dbo].[AccountType] ([TypeID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_AccountType]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK__Bill__status__21B6055D] FOREIGN KEY([IDTable])
REFERENCES [dbo].[TableFood] ([ID])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK__Bill__status__21B6055D]
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD  CONSTRAINT [FK__BillInFo__count__25869641] FOREIGN KEY([IDBill])
REFERENCES [dbo].[Bill] ([ID])
GO
ALTER TABLE [dbo].[BillInfo] CHECK CONSTRAINT [FK__BillInFo__count__25869641]
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD  CONSTRAINT [FK__BillInFo__IDFood__267ABA7A] FOREIGN KEY([IDFood])
REFERENCES [dbo].[Food] ([ID])
GO
ALTER TABLE [dbo].[BillInfo] CHECK CONSTRAINT [FK__BillInFo__IDFood__267ABA7A]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK__Food__Price__1DE57479] FOREIGN KEY([IDCategory])
REFERENCES [dbo].[FoodCategory] ([ID])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK__Food__Price__1DE57479]
GO
/****** Object:  StoredProcedure [dbo].[ASP_GetListBillByDate]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ASP_GetListBillByDate]
@checkIn date, @checkOut date
AS
BEGIN
	SELECT  t.Name AS N'Table Name', b.DateCheckIn AS N'Day In', b.DateCheckOut AS N'Day Out', b.Discount, b.TotalPrice
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut AND b.Status = 1 AND b.IDTable = t.ID
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
as
begin
	select * from dbo.Account where UserName = @userName
end

GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetTableList]
AS SELECT ID, Name, Status FROM dbo.TableFood WHERE Disable = N'no'

GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@idTable int
AS
BEGIN 
	INSERT dbo.Bill( DateCheckIn , DateCheckOut , IDTable , Status, Discount )
	VALUES  ( GETDATE(), NULL, @idTable , 0, 0 )
END

GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS 
BEGIN
	DECLARE @isBillInfoExisted INT;
	DECLARE @foodCount INT = 1;

	SELECT @isBillInfoExisted = bi.ID, @foodCount = bi.Count 
	FROM dbo.BillInfo AS bi 
	WHERE IDBill = @idBill AND IDFood = @idFood

	IF (@isBillInfoExisted > 0)
	BEGIN 
		DECLARE @newFoodCount INT = @foodCount + @count
		IF (@newFoodCount > 0)
        	UPDATE dbo.BillInfo SET Count = @foodCount + @count WHERE IDFood = @idFood AND IDBill = @idBill
		ELSE 
		DELETE dbo.BillInfo WHERE IDBill = @idBill AND IDFood = @idFood
	END
	ELSE 
	BEGIN 
	IF (@count > 0)
		BEGIN
		INSERT dbo.BillInFo( IDBill, IDFood, Count ) VALUES ( @idBill, @idFood, @count )
		END       
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Login]
@userName nvarchar(100), @passWord nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND Password = @passWord
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchTable]
@idTable1 int, @idTable2 int
AS 
BEGIN
	DECLARE @idFirstBill INT
	DECLARE @idSecondBill int 

	DECLARE @isFirstTableEmpty INT = 1
	DECLARE @isSecondTableEmpty INT = 1

	SELECT @idFirstBill = ID FROM dbo.Bill WHERE IDTable = @idTable1 AND Status = 0 
	SELECT @idSecondBill = ID FROM dbo.Bill WHERE IDTable = @idTable2 AND Status = 0 
	IF (@idFirstBill IS NULL)
	BEGIN
		INSERT dbo.Bill ( DateCheckIn ,	DateCheckOut , IDTable , Status	)
		VALUES  ( GETDATE() , NULL, @idTable1 , 0 )
		SELECT @idFirstBill = MAX(ID) FROM dbo.Bill WHERE IDTable = @idTable1 AND Status = 0 
	
    END
	SELECT @isFirstTableEmpty = COUNT(*) FROM dbo.BillInfo WHERE IDBill = @idFirstBill


	IF (@idSecondBill IS NULL)
	BEGIN
		INSERT dbo.Bill ( DateCheckIn ,	DateCheckOut , IDTable , Status	)
		VALUES  ( GETDATE() , NULL, @idTable2 , 0 )
		SELECT @idSecondBill = MAX(ID) FROM dbo.Bill WHERE IDTable = @idTable2 AND Status = 0 
    END
    SELECT @isSecondTableEmpty = COUNT(*) FROM dbo.BillInfo WHERE IDBill = @idSecondBill


	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE IDBill = @idSecondBill
	UPDATE dbo.BillInfo SET IDBill = @idSecondBill WHERE IDBill = @idFirstBill
	UPDATE dbo.BillInfo SET IDBill = @idFirstBill WHERE ID IN (SELECT * FROM IDBillInfoTable)

	DROP TABLE IDBillInfoTable

	IF (@isFirstTableEmpty = 0)
		UPDATE dbo.TableFood SET Status = N'Empty' WHERE ID = @idTable2
	IF (@isSecondTableEmpty = 0)
		UPDATE dbo.TableFood SET Status = N'Empty' WHERE ID = @idTable1
END

GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 4/4/2020 10:44:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateAccount]
@username nvarchar(100), @displayname nvarchar(100), @password nvarchar(100), @newpassword nvarchar(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE UserName = @username AND Password = @password
	IF (@isRightPass = 1)
	BEGIN
		IF (@newpassword = NULL OR @newpassword = '')
    		UPDATE dbo.Account SET DisplayName = @displayname WHERE UserName = @username
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayname, Password = @newpassword WHERE UserName = @username
	END 
END

GO
USE [master]
GO
ALTER DATABASE [CoffeeShop] SET  READ_WRITE 
GO
