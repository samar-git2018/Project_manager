USE [ProjectManager]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14-09-2018 07:52:10 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 14-09-2018 07:52:10 ******/
DROP TABLE [dbo].[Task]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 14-09-2018 07:52:10 ******/
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[Parent_Task]    Script Date: 14-09-2018 07:52:10 ******/
DROP TABLE [dbo].[Parent_Task]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertTaskUpdateUser]    Script Date: 14-09-2018 07:52:10 ******/
DROP PROCEDURE [dbo].[sp_InsertTaskUpdateUser]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTaskData]    Script Date: 14-09-2018 07:52:10 ******/
DROP PROCEDURE [dbo].[sp_GetTaskData]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProjectData]    Script Date: 14-09-2018 07:52:10 ******/
DROP PROCEDURE [dbo].[sp_GetProjectData]
GO
USE [master]
GO
/****** Object:  Database [ProjectManager]    Script Date: 14-09-2018 07:52:10 ******/
DROP DATABASE [ProjectManager]
GO
/****** Object:  Database [ProjectManager]    Script Date: 14-09-2018 07:52:10 ******/
CREATE DATABASE [ProjectManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ProjectManager.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProjectManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ProjectManager_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProjectManager] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectManager] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectManager] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectManager] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjectManager', N'ON'
GO
USE [ProjectManager]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProjectData]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetProjectData]
AS
BEGIN
	SET NOCOUNT ON;

    select a.Project_ID, a.ProjectName, a.Start_Date, a.End_Date, a.Priority, a.Manager_Id, count(b.Project_ID) TaskCount, count(d.Project_ID) CompletedTaskCount
		from Project a(nolock)
		left outer join Task b(nolock)
		on a.Project_ID = b.Project_ID
		left outer join (select Project_ID,Task_ID from Task c(nolock) where Status = 'C') d
		on a.Project_ID = d.Project_ID and b.Task_ID = d.Task_ID
		group by a.Project_ID, a.ProjectName, a.Start_Date, a.End_Date, a.Manager_Id, a.Priority
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetTaskData]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTaskData]
AS
BEGIN
	SET NOCOUNT ON;

    select a.Task_ID, a.TaskName,a.Project_ID, c.ProjectName, a.Start_Date, a.End_Date, isnull(a.Priority, 0) Priority, a.Status, a.Parent_ID, b.ParentTaskName 
		from Task a(nolock)
		left outer join Parent_Task b(nolock)
		on a.Parent_ID = b.Parent_ID
		left outer join Project c(nolock)
		on a.Project_ID = c.Project_ID
END


GO
/****** Object:  StoredProcedure [dbo].[sp_InsertTaskUpdateUser]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertTaskUpdateUser]

@Parent_ID int,
@TaskName nvarchar(100),
@Start_Date datetime,
@End_Date datetime,
@Priority int,
@Status char(1),
@User_ID int,
@Project_ID int
AS
BEGIN
	SET NOCOUNT ON;

    insert into Task values (@Parent_ID, @Project_ID, @TaskName, @Start_Date, @End_Date, @Priority, @Status)

	update Users set Project_ID = @Project_ID , Task_ID = @@IDENTITY where User_ID = @User_ID;
END

GO
/****** Object:  Table [dbo].[Parent_Task]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parent_Task](
	[Parent_ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentTaskName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Parent_Task] PRIMARY KEY CLUSTERED 
(
	[Parent_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Project_ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Priority] [int] NOT NULL,
	[Manager_Id] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Project_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Task](
	[Task_ID] [int] IDENTITY(1,1) NOT NULL,
	[Parent_ID] [int] NOT NULL,
	[Project_ID] [int] NOT NULL,
	[TaskName] [nvarchar](100) NOT NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Priority] [int] NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Task_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14-09-2018 07:52:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Employee_ID] [nvarchar](50) NOT NULL,
	[Project_ID] [int] NOT NULL,
	[Task_ID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Employee_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ('') FOR [ProjectName]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((0)) FOR [Priority]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT ((0)) FOR [Parent_ID]
GO
USE [master]
GO
ALTER DATABASE [ProjectManager] SET  READ_WRITE 
GO
