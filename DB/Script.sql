USE [master]
GO
/****** Object:  Database [UTapiaOptimissa]    Script Date: 1/26/2023 7:09:58 PM ******/
CREATE DATABASE [UTapiaOptimissa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UTapiaOptimissa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UTapiaOptimissa.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UTapiaOptimissa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UTapiaOptimissa_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UTapiaOptimissa] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UTapiaOptimissa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UTapiaOptimissa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET ARITHABORT OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [UTapiaOptimissa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UTapiaOptimissa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UTapiaOptimissa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UTapiaOptimissa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UTapiaOptimissa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET RECOVERY FULL 
GO
ALTER DATABASE [UTapiaOptimissa] SET  MULTI_USER 
GO
ALTER DATABASE [UTapiaOptimissa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UTapiaOptimissa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UTapiaOptimissa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UTapiaOptimissa] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'UTapiaOptimissa', N'ON'
GO
USE [UTapiaOptimissa]
GO
/****** Object:  StoredProcedure [dbo].[AccountAdd]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccountAdd]
@account VARCHAR(9),
@balance DECIMAL,
@owner VARCHAR(20)
AS
INSERT INTO Account (account, balance, owner)
VALUES (@account, @balance, @owner)
GO
/****** Object:  StoredProcedure [dbo].[AccountGetAll]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccountGetAll]
AS
SELECT account, balance, owner, createdAt
FROM Account
GO
/****** Object:  StoredProcedure [dbo].[AccountGetByAccount]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccountGetByAccount]
@account VARCHAR(9)
AS
SELECT idtransaction, fromaccount, toaccount, amount, sentAt FROM Transacction
WHERE fromaccount = @account OR toaccount = @account
GO
/****** Object:  StoredProcedure [dbo].[AccountGetByOwner]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccountGetByOwner] 
@owner BIGINT
AS
SELECT account, balance, owner, createdAt
FROM Account
WHERE owner = @owner
GO
/****** Object:  StoredProcedure [dbo].[Balance]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Balance]
@account VARCHAR(9)
AS
SELECT account, balance, owner, createdAt
FROM Account
Where account = @account
GO
/****** Object:  StoredProcedure [dbo].[Trans]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Trans]
@fromaccount VARCHAR (9),
@toaccount VARCHAR (9),
@amount DECIMAL
AS
BEGIN TRAN
IF(SELECT balance FROM Account WHERE account = @fromaccount) > 0
BEGIN
            UPDATE Account 
			SET balance = balance - @amount
			WHERE account = @fromaccount
				IF(SELECT balance FROM Account WHERE account = @fromaccount) >= 0
				BEGIN				
					UPDATE Account
					SET balance = balance + @amount
					WHERE account = @toaccount
					INSERT INTO Transacction ( fromaccount, toaccount, amount)
					VALUES (@fromaccount, @toaccount, @amount)		
					COMMIT TRANSACTION						
				END
				ELSE
				BEGIN
				ROLLBACK TRANSACTION
				END
END
	ELSE
		ROLLBACK TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[TransFromAccount]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TransFromAccount]
@fromaccount VARCHAR(9)
AS
SELECT idtransaction, fromaccount, toaccount, amount, sentAt
FROM Transacction
WHERE fromaccount = @fromaccount
GO
/****** Object:  StoredProcedure [dbo].[TransToAccount]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TransToAccount]
@toaccount VARCHAR(9)
AS
SELECT idtransaction, fromaccount, toaccount, amount, sentAt
FROM Transacction
WHERE toaccount = @toaccount
GO
/****** Object:  Table [dbo].[Account]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[account] [varchar](9) NOT NULL,
	[balance] [decimal](18, 0) NULL,
	[owner] [varchar](20) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transacction]    Script Date: 1/26/2023 7:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transacction](
	[idtransaction] [int] IDENTITY(1,1) NOT NULL,
	[fromaccount] [varchar](9) NULL,
	[toaccount] [varchar](9) NULL,
	[amount] [decimal](18, 0) NULL,
	[sentAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idtransaction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'112233445', CAST(0 AS Decimal(18, 0)), N'1234567g', CAST(0x0000AF9600E5C395 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'123456789', CAST(47095 AS Decimal(18, 0)), N'761233785', CAST(0x0000AF95011B8EF2 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'234432234', CAST(42678 AS Decimal(18, 0)), N'13042016', CAST(0x0000AF96002AF980 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'234567853', CAST(67834 AS Decimal(18, 0)), N'13042016', CAST(0x0000AF96002AF980 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'234567890', CAST(35569 AS Decimal(18, 0)), N'45672389', CAST(0x0000AF9600271B57 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'234634674', CAST(98054 AS Decimal(18, 0)), N'13042016', CAST(0x0000AF96002AF980 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'245743456', CAST(56785 AS Decimal(18, 0)), N'13042016', CAST(0x0000AF96002AF980 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'34567434', CAST(22466 AS Decimal(18, 0)), N'12345678', CAST(0x0000AF96002A216E AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'987654321', CAST(24500 AS Decimal(18, 0)), N'11101998', CAST(0x0000AF960004C346 AS DateTime))
INSERT [dbo].[Account] ([account], [balance], [owner], [createdAt]) VALUES (N'998877770', CAST(500 AS Decimal(18, 0)), N'98583734', CAST(0x0000AF960126898C AS DateTime))
SET IDENTITY_INSERT [dbo].[Transacction] ON 

INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (1, N'234567890', N'123456789', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600AD3C6D AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (2, N'234567890', N'123456789', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600ADBDA5 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (3, N'123456789', N'234567890', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600B1832A AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (4, N'123456789', N'234567890', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600B205DA AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (5, N'123456789', N'761233785', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600B64B9C AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (6, N'123456789', N'13042016', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600B690A0 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (7, N'234432234', N'123456789', CAST(3000 AS Decimal(18, 0)), CAST(0x0000AF9600C4CD6D AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (8, N'987654321', N'123456789', CAST(500 AS Decimal(18, 0)), CAST(0x0000AF9600EA3B93 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (9, N'112233445', N'34567434', CAST(120 AS Decimal(18, 0)), CAST(0x0000AF960103B811 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (10, N'112233445', N'34567434', CAST(1 AS Decimal(18, 0)), CAST(0x0000AF96010400FA AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (11, N'123456789', N'112233445', CAST(20000 AS Decimal(18, 0)), CAST(0x0000AF96010E4334 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (12, N'112233445', N'123456789', CAST(20002 AS Decimal(18, 0)), CAST(0x0000AF96013213B9 AS DateTime))
INSERT [dbo].[Transacction] ([idtransaction], [fromaccount], [toaccount], [amount], [sentAt]) VALUES (13, N'234432234', N'123456789', CAST(0 AS Decimal(18, 0)), CAST(0x0000AF960133C761 AS DateTime))
SET IDENTITY_INSERT [dbo].[Transacction] OFF
ALTER TABLE [dbo].[Account] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Transacction] ADD  DEFAULT (getdate()) FOR [sentAt]
GO
USE [master]
GO
ALTER DATABASE [UTapiaOptimissa] SET  READ_WRITE 
GO
