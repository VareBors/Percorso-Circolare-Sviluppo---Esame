USE [master]
GO
/****** Object:  Database [PortalePercorsi]    Script Date: 03/01/2021 16:11:18 ******/
CREATE DATABASE [PortalePercorsi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PortalePercorsi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PortalePercorsi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PortalePercorsi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PortalePercorsi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PortalePercorsi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PortalePercorsi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PortalePercorsi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PortalePercorsi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PortalePercorsi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PortalePercorsi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PortalePercorsi] SET ARITHABORT OFF 
GO
ALTER DATABASE [PortalePercorsi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PortalePercorsi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PortalePercorsi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PortalePercorsi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PortalePercorsi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PortalePercorsi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PortalePercorsi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PortalePercorsi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PortalePercorsi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PortalePercorsi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PortalePercorsi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PortalePercorsi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PortalePercorsi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PortalePercorsi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PortalePercorsi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PortalePercorsi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PortalePercorsi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PortalePercorsi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PortalePercorsi] SET  MULTI_USER 
GO
ALTER DATABASE [PortalePercorsi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PortalePercorsi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PortalePercorsi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PortalePercorsi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PortalePercorsi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PortalePercorsi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PortalePercorsi] SET QUERY_STORE = OFF
GO
USE [PortalePercorsi]
GO
/****** Object:  UserDefinedFunction [dbo].[GetResourceId]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Varena Roberto
-- Create date: 03/01/2021
-- Description:	Funzione per generare un identificativo randomico per la tabella resources
-- =============================================
CREATE FUNCTION [dbo].[GetResourceId] ()
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @NewId INT
	DECLARE @Find BIT = 0;

	WHILE @Find = 0
	BEGIN 
		SET @NewId =  (SELECT [Value] FROM dbo.vw_RandomValue) *((9999-1000+1))+1000;
		IF (SELECT COUNT(*) FROM dbo.Resources WHERE Id = @NewId) = 0
			SET @Find = 1
		
	END

	RETURN @NewId
END
GO
/****** Object:  View [dbo].[vw_RandomValue]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_RandomValue]
AS
SELECT RAND() AS Value
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Id_Referent] [int] NOT NULL,
	[ReferenceYear] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Course] [int] NULL,
	[Id_Teacher] [int] NULL,
	[Id_Room] [int] NULL,
	[LessonNumber] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Id_Creator] [int] NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonsResources]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonsResources](
	[Id_Lesson] [int] NOT NULL,
	[Id_Student] [int] NOT NULL,
 CONSTRAINT [PK_LessonsResources] PRIMARY KEY CLUSTERED 
(
	[Id_Lesson] ASC,
	[Id_Student] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Username] [nvarchar](10) NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 03/01/2021 16:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Places] [int] NULL,
	[Bookable] [bit] NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Resources] FOREIGN KEY([Id_Referent])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Resources]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Courses] FOREIGN KEY([Id_Course])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Courses]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Resources_Creator] FOREIGN KEY([Id_Creator])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Resources_Creator]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Rooms] FOREIGN KEY([Id_Room])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Rooms]
GO
ALTER TABLE [dbo].[LessonsResources]  WITH CHECK ADD  CONSTRAINT [FK_LessonsResources_Lessons] FOREIGN KEY([Id_Lesson])
REFERENCES [dbo].[Lessons] ([Id])
GO
ALTER TABLE [dbo].[LessonsResources] CHECK CONSTRAINT [FK_LessonsResources_Lessons]
GO
ALTER TABLE [dbo].[LessonsResources]  WITH CHECK ADD  CONSTRAINT [FK_LessonsResources_Resources] FOREIGN KEY([Id_Student])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[LessonsResources] CHECK CONSTRAINT [FK_LessonsResources_Resources]
GO
USE [master]
GO
ALTER DATABASE [PortalePercorsi] SET  READ_WRITE 
GO
