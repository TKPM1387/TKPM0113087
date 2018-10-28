USE [master]
GO
/****** Object:  Database [QLHS]    Script Date: 10/29/2018 6:54:09 AM ******/
CREATE DATABASE [QLHS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLHS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\QLHS.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLHS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\QLHS_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLHS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLHS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLHS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLHS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLHS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLHS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLHS] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLHS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLHS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QLHS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLHS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLHS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLHS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLHS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLHS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLHS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLHS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLHS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLHS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLHS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLHS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLHS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLHS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLHS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLHS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLHS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLHS] SET  MULTI_USER 
GO
ALTER DATABASE [QLHS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLHS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLHS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLHS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [QLHS]
GO
/****** Object:  StoredProcedure [dbo].[addStudent]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[addStudent]
    @fidstudent varchar(20) ,
    @ffullname nvarchar(100) ,
    @fbirthday varchar(20) ,
    @fgender varchar(2) ,
    @femail varchar(100) ='',
    @fphonenumber varchar(20) ='',
    @faddress nvarchar(500)
AS
    BEGIN

	DECLARE @query NVARCHAR(max)='';
	SET @query = 
	'INSERT INTO dbo.Students
	        ( StudentID ,
	          FullName ,
	          BirthDay ,
	          Gender ,
	          Email ,
	          PhoneNumber ,
	          Address
	        )
	VALUES  ( '+@fidstudent+' , 
	          '+'N'+''''+@ffullname+''''+', 
	          '+''''+@fbirthday+''''+' , 
	          '+@fgender+' , 
	          '+''''+@femail+''''+' , 
	          '+''''+@fphonenumber+''''+' , 
	          '+'N'+''''+@faddress+''''+' 
	        )'
		--SELECT @query
		EXEC(@query)
    END

GO
/****** Object:  StoredProcedure [dbo].[getClassByLevel]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getClassByLevel] @iDLevel varchar(100)
AS
    BEGIN
        DECLARE @stridlv VARCHAR(100)= ''
        IF ( @iDLevel != '0' )
            SET @stridlv = ' AND C.ClassLevel = '+@iDLevel
        
		DECLARE @strQuery NVARCHAR(max) =''
		SET @strQuery='
		SELECT  *
        FROM    Classes C
        WHERE   1 = 1 '+ @stridlv

			EXEC sp_executesql @strQuery

		--SELECT @strQuery
    END

GO
/****** Object:  StoredProcedure [dbo].[GetClasses]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetClasses]
AS
    BEGIN
        SELECT  C.ID ,
                C.ClassName ,
                CL.LevelName
        FROM    dbo.Classes C
                JOIN dbo.ClassLevel CL ON C.ClassLevel = CL.ID
	
    END

GO
/****** Object:  StoredProcedure [dbo].[getClassLevel]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getClassLevel]
as
begin
select * from ClassLevel
end


GO
/****** Object:  StoredProcedure [dbo].[getStudentByID]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getStudentByID] @id VARCHAR(20)
AS
    BEGIN
   	
	SELECT * FROM QLHS..Students S WHERE S.StudentID = @id
    END

	UPDATE Students SET ClassLevel = 1


GO
/****** Object:  StoredProcedure [dbo].[getStudents]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getStudents]
AS
BEGIN
SELECT 
       S.StudentID ,
       S.FullName ,
       S.BirthDay ,
       S.Gender ,
       S.Email ,
       S.PhoneNumber ,
       S.Address FROM dbo.Students S
END


GO
/****** Object:  StoredProcedure [dbo].[getStudentsByClass]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getStudentsByClass]
@fclass varchar(2)
as begin

select * from students where Class =@fclass

end
GO
/****** Object:  StoredProcedure [dbo].[getSubjects]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getSubjects]
AS
    BEGIN
        SELECT  ID,S.SubjectName,S.Type
        FROM    dbo.Subject S
    END 

GO
/****** Object:  Table [dbo].[Classes]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](255) NULL,
	[ClassLevel] [int] NULL,
	[Total] [int] NULL,
	[Flag] [int] NULL,
 CONSTRAINT [PK__Classes__3214EC27BA59D8BE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClassLevel]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassLevel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [nvarchar](255) NULL,
	[MaxTotal] [int] NULL,
 CONSTRAINT [PK__ClassLev__3214EC2759373ABE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Point]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudenID] [nvarchar](255) NULL,
	[SubjectID] [int] NULL,
	[Semester] [int] NULL,
	[Test15Minutes] [float] NULL,
	[Test45Minutes] [float] NULL,
	[TestSemester] [float] NULL,
 CONSTRAINT [PK__Point__3214EC27EB479E08] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [varchar](255) NOT NULL,
	[FullName] [nvarchar](255) NULL,
	[BirthDay] [datetime] NULL,
	[Gender] [int] NULL,
	[Email] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Address] [nvarchar](500) NULL,
	[ClassLevel] [int] NULL,
	[Class] [int] NULL,
 CONSTRAINT [PK__Students__3214EC273A2AC834] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 10/29/2018 6:54:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](255) NULL,
	[Flag] [int] NULL,
	[Type] [int] NULL,
	[Level1] [int] NULL,
	[Level2] [int] NULL,
	[Level3] [int] NULL,
 CONSTRAINT [PK__Subject__3214EC27813C3D83] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (1, N'Lớp 10A1', 1, 30, NULL)
INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (2, N'Lớp 10A2', 1, 22, NULL)
INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (3, N'Lớp 10A3', 1, 31, NULL)
INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (4, N'Lớp 11B1', 2, 36, NULL)
INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (5, N'Lớp 11B3', 2, 38, NULL)
INSERT [dbo].[Classes] ([ID], [ClassName], [ClassLevel], [Total], [Flag]) VALUES (6, N'Lớp 12C4', 3, 40, NULL)
SET IDENTITY_INSERT [dbo].[Classes] OFF
SET IDENTITY_INSERT [dbo].[ClassLevel] ON 

INSERT [dbo].[ClassLevel] ([ID], [LevelName], [MaxTotal]) VALUES (1, N'Khối 10xxx', 40)
INSERT [dbo].[ClassLevel] ([ID], [LevelName], [MaxTotal]) VALUES (2, N'Khối 11yyy', 40)
INSERT [dbo].[ClassLevel] ([ID], [LevelName], [MaxTotal]) VALUES (3, N'Khối 12qqq', 40)
SET IDENTITY_INSERT [dbo].[ClassLevel] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (1, N'1800000', N'nguyen van a', CAST(N'2018-01-02 00:00:00.000' AS DateTime), 1, N'anguyenvan@gmail.com', N'0123654789', N'tphcm', 1, 1)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (2, N'123', N'nguy?n th? bê', CAST(N'1900-07-21 00:00:00.000' AS DateTime), 2, N'nono@gmail.com', N'0989955261', N'h? n?i', 1, 2)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (3, N'123', N'nguyễn thị bê', CAST(N'1900-07-21 00:00:00.000' AS DateTime), 2, N'nono@gmail.com', N'0989955261', N'h? n?i', 1, 1)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (4, N'1800003', N'họ có dấu', CAST(N'2018-10-31 00:00:00.000' AS DateTime), 2, N'duongnnb@fpt.com.vn', N'0123654789', N'đồ long đao', 1, 2)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (5, N'1800004', N'dqwqw', CAST(N'2018-10-04 00:00:00.000' AS DateTime), 2, N'duongnnb@fpt.com.vn', N'', N'42323 vt34t', 1, 1)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (6, N'1800005', N'fewr23', CAST(N'2018-10-02 00:00:00.000' AS DateTime), 2, N'duongnnb@fpt.com.vn', N'', N'432rqfqe', 1, 1)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (7, N'1800006', N'không biết', CAST(N'2018-09-30 00:00:00.000' AS DateTime), 2, N'duongn2321nb@fpt.com.vn', N'', N'chưa rõ', 1, 1)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (8, N'1800007', N'tùyyt', CAST(N'2001-01-01 00:00:00.000' AS DateTime), 1, N'6ser', N's56', N'trần văn bơ546e', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (9, N'1800008', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (10, N'1800008', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (11, N'1800010', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (12, N'1800010', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (13, N'1800011', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
INSERT [dbo].[Students] ([ID], [StudentID], [FullName], [BirthDay], [Gender], [Email], [PhoneNumber], [Address], [ClassLevel], [Class]) VALUES (14, N'1800012', N'bới người ta', CAST(N'2002-02-02 00:00:00.000' AS DateTime), 2, N'', N'', N'anh đi đi', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Students] OFF
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (1, N'Tin', 0, 1, 1, NULL, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (2, N'Toán', 0, 1, 0, 1, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (3, N'Văn', 0, 1, 1, NULL, 0)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (4, N'Sử', 0, 1, 0, 1, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (5, N'Địa', 0, 1, 1, NULL, 0)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (6, N'Hát', 0, 1, 1, 1, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (7, N'Nhảy', 0, 1, 0, 1, 0)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (8, N'Gank', 0, 1, 0, NULL, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (9, N'Múa', 0, 1, 1, NULL, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (10, N'Bay', 0, 1, 1, NULL, 1)
INSERT [dbo].[Subject] ([ID], [SubjectName], [Flag], [Type], [Level1], [Level2], [Level3]) VALUES (11, N'Lắc', 0, 1, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Subject] OFF
USE [master]
GO
ALTER DATABASE [QLHS] SET  READ_WRITE 
GO
