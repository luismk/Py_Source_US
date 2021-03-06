USE [master]
GO
/****** Object:  Database [DB_Pangya]    Script Date: 23/07/2020 09:58:22 ******/
CREATE DATABASE [DB_Pangya]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_Pangya', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQL\MSSQL\DATA\DB_Pangya.mdf' , SIZE = 77824KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_Pangya_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQL\MSSQL\DATA\DB_Pangya_log.ldf' , SIZE = 140288KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DB_Pangya] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Pangya].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_Pangya] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_Pangya] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_Pangya] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_Pangya] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_Pangya] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_Pangya] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_Pangya] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_Pangya] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_Pangya] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_Pangya] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_Pangya] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_Pangya] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_Pangya] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_Pangya] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_Pangya] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_Pangya] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_Pangya] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_Pangya] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_Pangya] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_Pangya] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_Pangya] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_Pangya] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_Pangya] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_Pangya] SET  MULTI_USER 
GO
ALTER DATABASE [DB_Pangya] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_Pangya] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_Pangya] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_Pangya] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_Pangya] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_Pangya] SET QUERY_STORE = OFF
GO
USE [DB_Pangya]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DB_Pangya]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_ITEM_SERIAL]    Script Date: 23/07/2020 09:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[UDF_ITEM_SERIAL] (
	@TID INT
)
RETURNS INT
-- 
AS

/********************************************************
** ITEM 의 SERIAL NO. 를 리턴함(캐디, 클럽)
********************************************************/
BEGIN 
	RETURN @TID - (DBO.UDF_PARTS_GROUP(@TID)* POWER(2, 26))
END


GO
/****** Object:  UserDefinedFunction [dbo].[UDF_PARTS_GROUP]    Script Date: 23/07/2020 09:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[UDF_PARTS_GROUP] (
	@TID INT
)
RETURNS INT
-- 
AS

BEGIN 
	RETURN (@TID & (0xfc000000)) / POWER(2, 26)
END


GO
/****** Object:  UserDefinedFunction [dbo].[UNIX_TIMESTAMP]    Script Date: 23/07/2020 09:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UNIX_TIMESTAMP] (
   @DATETIME DATETIME
)
RETURNS INTEGER
AS
BEGIN
   DECLARE @Return INTEGER

   SELECT @Return = DATEDIFF(SECOND,{TS '1970-01-01 07:00:00'}, @DATETIME)

   RETURN @Return
END
GO
/****** Object:  Table [dbo].[Achievement_Counter_Data]    Script Date: 23/07/2020 09:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement_Counter_Data](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Enable] [tinyint] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_Achievement_Counter_Data] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievement_Data]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement_Data](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ACHIEVEMENT_ENABLE] [tinyint] NOT NULL,
	[ACHIEVEMENT_TYPEID] [int] NOT NULL,
	[ACHIEVEMENT_NAME] [varchar](500) NOT NULL,
	[ACHIEVEMENT_QUEST_TYPEID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Achievement_Data]    Script Date: 23/07/2020 09:58:24 ******/
CREATE CLUSTERED INDEX [IX_Achievement_Data] ON [dbo].[Achievement_Data]
(
	[ACHIEVEMENT_QUEST_TYPEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievement_QuestItem]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement_QuestItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeID] [int] NULL,
	[Name] [varchar](255) NULL,
	[QuestTypeID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievement_QuestStuffs]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement_QuestStuffs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Enable] [tinyint] NOT NULL,
	[TypeID] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[CounterTypeID] [int] NOT NULL,
	[CounterQuantity] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Achievement_QuestStuffs_1]    Script Date: 23/07/2020 09:58:24 ******/
CREATE CLUSTERED INDEX [IX_Achievement_QuestStuffs_1] ON [dbo].[Achievement_QuestStuffs]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Daily_Quest]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Daily_Quest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuestTypeID1] [int] NOT NULL,
	[QuestTypeID2] [int] NOT NULL,
	[QuestTypeID3] [int] NOT NULL,
	[RegDate] [datetime] NULL,
	[Day] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Achievement]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Achievement](
	[UID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Valid] [tinyint] NULL,
 CONSTRAINT [PK__Pangya_A__0E4FF547FE082235] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[UID] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Achievement_Counter]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Achievement_Counter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Achievement_Quest]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Achievement_Quest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[Achievement_Index] [int] NOT NULL,
	[Achivement_Quest_TypeID] [int] NOT NULL,
	[Counter_Index] [int] NOT NULL,
	[SuccessDate] [datetime] NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK__Pangya_A__0E4FF5475F9FCA8B] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[UID] ASC,
	[Achievement_Index] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Caddie]    Script Date: 23/07/2020 09:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Caddie](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TYPEID] [int] NOT NULL,
	[EXP] [int] NOT NULL,
	[cLevel] [tinyint] NOT NULL,
	[SKIN_TYPEID] [int] NULL,
	[RentFlag] [tinyint] NULL,
	[RegDate] [datetime] NULL,
	[END_DATE] [datetime] NULL,
	[SKIN_END_DATE] [datetime] NULL,
	[TriggerPay] [tinyint] NULL,
	[VALID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Pangya_Caddie] PRIMARY KEY CLUSTERED 
(
	[CID] ASC,
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Card]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Card](
	[CARD_IDX] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[CARD_TYPEID] [int] NOT NULL,
	[QTY] [int] NOT NULL,
	[RegData] [datetime] NULL,
	[VALID] [tinyint] NULL,
 CONSTRAINT [PK_Pangya_Card] PRIMARY KEY CLUSTERED 
(
	[CARD_IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Card_Equip]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Card_Equip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[CID] [int] NOT NULL,
	[CHAR_TYPEID] [int] NULL,
	[CARD_TYPEID] [int] NULL,
	[SLOT] [int] NULL,
	[REGDATE] [datetime] NULL,
	[ENDDATE] [datetime] NULL,
	[FLAG] [tinyint] NULL,
	[VALID] [tinyint] NULL,
 CONSTRAINT [PK__Pangya_C__A690D8C0177C89E1] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Character]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Character](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TYPEID] [int] NOT NULL,
	[GIFT_FLAG] [tinyint] NULL,
	[HAIR_COLOR] [tinyint] NULL,
	[POWER] [tinyint] NULL,
	[CONTROL] [tinyint] NULL,
	[IMPACT] [tinyint] NULL,
	[SPIN] [tinyint] NULL,
	[CURVE] [tinyint] NULL,
	[CUTIN] [int] NULL,
	[AuxPart] [int] NULL,
	[AuxPart2] [int] NULL,
	[PART_TYPEID_1] [int] NULL,
	[PART_TYPEID_2] [int] NULL,
	[PART_TYPEID_3] [int] NULL,
	[PART_TYPEID_4] [int] NULL,
	[PART_TYPEID_5] [int] NULL,
	[PART_TYPEID_6] [int] NULL,
	[PART_TYPEID_7] [int] NULL,
	[PART_TYPEID_8] [int] NULL,
	[PART_TYPEID_9] [int] NULL,
	[PART_TYPEID_10] [int] NULL,
	[PART_TYPEID_11] [int] NULL,
	[PART_TYPEID_12] [int] NULL,
	[PART_TYPEID_13] [int] NULL,
	[PART_TYPEID_14] [int] NULL,
	[PART_TYPEID_15] [int] NULL,
	[PART_TYPEID_16] [int] NULL,
	[PART_TYPEID_17] [int] NULL,
	[PART_TYPEID_18] [int] NULL,
	[PART_TYPEID_19] [int] NULL,
	[PART_TYPEID_20] [int] NULL,
	[PART_TYPEID_21] [int] NULL,
	[PART_TYPEID_22] [int] NULL,
	[PART_TYPEID_23] [int] NULL,
	[PART_TYPEID_24] [int] NULL,
	[PART_IDX_1] [int] NULL,
	[PART_IDX_2] [int] NULL,
	[PART_IDX_3] [int] NULL,
	[PART_IDX_4] [int] NULL,
	[PART_IDX_5] [int] NULL,
	[PART_IDX_6] [int] NULL,
	[PART_IDX_7] [int] NULL,
	[PART_IDX_8] [int] NULL,
	[PART_IDX_9] [int] NULL,
	[PART_IDX_10] [int] NULL,
	[PART_IDX_11] [int] NULL,
	[PART_IDX_12] [int] NULL,
	[PART_IDX_13] [int] NULL,
	[PART_IDX_14] [int] NULL,
	[PART_IDX_15] [int] NULL,
	[PART_IDX_16] [int] NULL,
	[PART_IDX_17] [int] NULL,
	[PART_IDX_18] [int] NULL,
	[PART_IDX_19] [int] NULL,
	[PART_IDX_20] [int] NULL,
	[PART_IDX_21] [int] NULL,
	[PART_IDX_22] [int] NULL,
	[PART_IDX_23] [int] NULL,
	[PART_IDX_24] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Club_Info]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Club_Info](
	[ITEM_ID] [int] NOT NULL,
	[C0_SLOT] [smallint] NULL,
	[C1_SLOT] [smallint] NULL,
	[C2_SLOT] [smallint] NULL,
	[C3_SLOT] [smallint] NULL,
	[C4_SLOT] [smallint] NULL,
	[CLUB_POINT] [int] NULL,
	[CLUB_WORK_COUNT] [int] NULL,
	[CLUB_SLOT_CANCEL] [int] NULL,
	[CLUB_POINT_TOTAL_LOG] [int] NULL,
	[CLUB_UPGRADE_PANG_LOG] [int] NULL,
 CONSTRAINT [PK_Pangya_Club_Info_1] PRIMARY KEY CLUSTERED 
(
	[ITEM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Daily_Quest]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Daily_Quest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[QuestID1] [int] NULL,
	[QuestID2] [int] NULL,
	[QuestID3] [int] NULL,
	[LastAccept] [datetime] NULL,
	[LastCancel] [datetime] NULL,
	[RegDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Exception_Log]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Exception_Log](
	[ExceptionID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NULL,
	[Username] [varchar](50) NULL,
	[ExceptionMessage] [varchar](2000) NULL,
	[Server] [varchar](50) NULL,
	[CreateDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Friend]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Friend](
	[Owner] [varchar](50) NOT NULL,
	[Friend] [varchar](50) NOT NULL,
	[IsAccept] [tinyint] NOT NULL,
	[GroupName] [varchar](50) NOT NULL,
	[IsAgree] [tinyint] NOT NULL,
	[IsDeleted] [tinyint] NOT NULL,
	[Memo] [varchar](20) NOT NULL,
	[IsBlock] [tinyint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Game_Macro]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Game_Macro](
	[UID] [int] NOT NULL,
	[Macro1] [varchar](45) NULL,
	[Macro2] [varchar](45) NULL,
	[Macro3] [varchar](45) NULL,
	[Macro4] [varchar](45) NULL,
	[Macro5] [varchar](45) NULL,
	[Macro6] [varchar](45) NULL,
	[Macro7] [varchar](45) NULL,
	[Macro8] [varchar](45) NULL,
	[Macro9] [varchar](45) NULL,
	[Macro10] [varchar](45) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Guild_Emblem]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Guild_Emblem](
	[EMBLEM_IDX] [int] IDENTITY(1,1) NOT NULL,
	[GUILD_ID] [int] NOT NULL,
	[GUILD_MARK_IMG] [varchar](50) NULL,
	[GUILD_MARK_ISVALID] [tinyint] NULL,
 CONSTRAINT [PK_Pangya_Guild_Emblem] PRIMARY KEY CLUSTERED 
(
	[GUILD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Guild_Info]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Guild_Info](
	[GUILD_INDEX] [int] IDENTITY(1,1) NOT NULL,
	[GUILD_NAME] [varchar](255) NOT NULL,
	[GUILD_INTRODUCING] [varchar](255) NULL,
	[GUILD_NOTICE] [varchar](255) NULL,
	[GUILD_LEADER_UID] [int] NOT NULL,
	[GUILD_POINT] [int] NULL,
	[GUILD_PANG] [int] NULL,
	[GUILD_IMAGE] [varchar](10) NULL,
	[GUILD_IMAGE_KEY_UPLOAD] [int] NULL,
	[GUILD_CREATE_DATE] [datetime] NULL,
	[GUILD_VALID] [tinyint] NULL,
 CONSTRAINT [PK_Pangya_Guild_Info] PRIMARY KEY CLUSTERED 
(
	[GUILD_INDEX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Guild_Log]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Guild_Log](
	[UID] [int] NOT NULL,
	[GUILD_ID] [int] NOT NULL,
	[GUILD_NAME] [varchar](32) NULL,
	[GUILD_ACTION] [tinyint] NOT NULL,
	[GUILD_ACTION_DATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Guild_Member]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Guild_Member](
	[GUILD_ID] [int] NOT NULL,
	[GUILD_MEMBER_UID] [int] NOT NULL,
	[GUILD_POSITION] [tinyint] NULL,
	[GUILD_MESSAGE] [varchar](255) NULL,
	[GUILD_ENTERED_TIME] [datetime] NULL,
	[GUILD_MEMBER_STATUS] [tinyint] NULL,
 CONSTRAINT [PK_Pangya_Guild_Member] PRIMARY KEY CLUSTERED 
(
	[GUILD_ID] ASC,
	[GUILD_MEMBER_UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Item_Daily]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Item_Daily](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ItemTypeID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[ItemType] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Item_Daily_Log]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Item_Daily_Log](
	[UID] [int] NOT NULL,
	[Counter] [int] NOT NULL,
	[Item_TypeID] [int] NOT NULL,
	[Item_Quantity] [int] NOT NULL,
	[Item_TypeID_Next] [int] NOT NULL,
	[Item_Quantity_Next] [int] NOT NULL,
	[LoginCount] [int] NOT NULL,
	[RegDate] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Locker_Item]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Locker_Item](
	[INVEN_ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TypeID] [int] NULL,
	[Name] [varchar](255) NULL,
	[FROM_ID] [int] NULL,
	[Valid] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[INVEN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Mail]    Script Date: 23/07/2020 09:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Mail](
	[Mail_Index] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[Sender] [varchar](20) NULL,
	[Sender_UID] [int] NULL,
	[Receiver] [varchar](20) NULL,
	[Receiver_UID] [int] NULL,
	[Subject] [varchar](200) NULL,
	[Msg] [varchar](2500) NULL,
	[ReadDate] [smalldatetime] NULL,
	[ReceiveDate] [smalldatetime] NULL,
	[DeleteDate] [smalldatetime] NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_Pangya_Mail] PRIMARY KEY CLUSTERED 
(
	[Mail_Index] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Mail_Item]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Mail_Item](
	[Mail_Index] [int] NOT NULL,
	[TYPEID] [int] NOT NULL,
	[SETTYPEID] [int] NULL,
	[QTY] [int] NULL,
	[DAY] [smallint] NULL,
	[UCC_UNIQUE] [varchar](8) NULL,
	[ITEM_GRP] [tinyint] NULL,
	[TO_UID] [int] NULL,
	[IN_DATE] [datetime] NULL,
	[RELEASE_DATE] [datetime] NULL,
	[APPLY_ITEM_ID] [int] NULL,
 CONSTRAINT [PK_Pangya_Mail_Item] PRIMARY KEY CLUSTERED 
(
	[Mail_Index] ASC,
	[TYPEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Map_Statistics]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Map_Statistics](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[Map] [smallint] NOT NULL,
	[Drive] [int] NOT NULL,
	[Putt] [int] NOT NULL,
	[Hole] [int] NOT NULL,
	[Fairway] [int] NOT NULL,
	[Holein] [int] NOT NULL,
	[PuttIn] [int] NOT NULL,
	[TotalScore] [int] NOT NULL,
	[BestScore] [smallint] NOT NULL,
	[MaxPang] [int] NOT NULL,
	[CharTypeId] [int] NOT NULL,
	[EventScore] [tinyint] NOT NULL,
	[Assist] [tinyint] NOT NULL,
	[REGDATE] [smalldatetime] NULL,
 CONSTRAINT [PK__Pangya_M__59C8E1BEBE7D7FE0] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[Map] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Mascot]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Mascot](
	[MID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[MASCOT_TYPEID] [int] NOT NULL,
	[MESSAGE] [varchar](50) NULL,
	[DateEnd] [datetime] NULL,
	[VALID] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Member]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Member](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](16) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IDState] [tinyint] NULL,
	[FirstSet] [tinyint] NULL,
	[LastLogonTime] [datetime] NULL,
	[Logon] [tinyint] NULL,
	[Nickname] [varchar](16) NULL,
	[Sex] [tinyint] NULL,
	[IPAddress] [varchar](50) NULL,
	[LogonCount] [int] NULL,
	[Capabilities] [tinyint] NULL,
	[RegDate] [datetime] NULL,
	[AuthKey_Login] [varchar](7) NULL,
	[AuthKey_Game] [varchar](7) NULL,
	[GUILDINDEX] [int] NULL,
	[DailyLoginCount] [int] NULL,
	[Tutorial] [tinyint] NULL,
	[BirthDay] [date] NULL,
	[Event1] [tinyint] NULL,
	[Event2] [tinyint] NULL,
 CONSTRAINT [PK_pangya_member] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Memorial_Log]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Memorial_Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NULL,
	[ItemName] [varchar](255) NULL,
	[Quantity] [int] NULL,
	[DateIN] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Personal]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Personal](
	[UID] [int] NOT NULL,
	[CookieAmt] [int] NULL,
	[PangLockerAmt] [int] NULL,
	[LockerPwd] [varchar](4) NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Personal_Log]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Personal_Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](255) NOT NULL,
	[Amount] [int] NULL,
	[UID] [int] NOT NULL,
	[LockerPang] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_SelfDesign]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_SelfDesign](
	[UID] [int] NOT NULL,
	[ITEM_ID] [int] NOT NULL,
	[UCC_UNIQE] [varchar](8) NOT NULL,
	[TYPEID] [int] NOT NULL,
	[UCC_STATUS] [tinyint] NULL,
	[UCC_KEY] [varchar](50) NULL,
	[UCC_NAME] [varchar](20) NULL,
	[UCC_DRAWER] [int] NULL,
	[UCC_COPY_COUNT] [int] NULL,
	[IN_DATE] [datetime] NULL,
 CONSTRAINT [PK_Pangya_SelfDesign] PRIMARY KEY CLUSTERED 
(
	[UID] ASC,
	[ITEM_ID] ASC,
	[UCC_UNIQE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Server]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Server](
	[ServerID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Port] [int] NOT NULL,
	[MaxUser] [int] NOT NULL,
	[UsersOnline] [int] NOT NULL,
	[Property] [int] NOT NULL,
	[BlockFunc] [bigint] NOT NULL,
	[ImgNo] [tinyint] NOT NULL,
	[ImgEvent] [smallint] NOT NULL,
	[ServerType] [tinyint] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Pangya_Server] PRIMARY KEY CLUSTERED 
(
	[ServerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_String]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_String](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[str] [varchar](8000) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Transaction_Log]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Transaction_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NULL,
	[String] [varchar](4096) NULL,
	[ERROR_NUMBER] [int] NULL,
	[ERROR_SEVERITY] [int] NULL,
	[ERROR_STATE] [int] NULL,
	[ERROR_PROCEDURE] [varchar](1024) NULL,
	[ERROR_LINE] [int] NULL,
	[ERROR_MESSAGE] [varchar](1024) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Tutorial]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Tutorial](
	[UID] [int] NULL,
	[Rookie] [int] NULL,
	[Beginner] [int] NULL,
	[Advancer] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_User_Equip]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_User_Equip](
	[UID] [int] NOT NULL,
	[CADDIE] [int] NOT NULL,
	[CHARACTER_ID] [int] NOT NULL,
	[CLUB_ID] [int] NOT NULL,
	[BALL_ID] [int] NOT NULL,
	[ITEM_SLOT_1] [int] NOT NULL,
	[ITEM_SLOT_2] [int] NOT NULL,
	[ITEM_SLOT_3] [int] NOT NULL,
	[ITEM_SLOT_4] [int] NOT NULL,
	[ITEM_SLOT_5] [int] NOT NULL,
	[ITEM_SLOT_6] [int] NOT NULL,
	[ITEM_SLOT_7] [int] NOT NULL,
	[ITEM_SLOT_8] [int] NOT NULL,
	[ITEM_SLOT_9] [int] NOT NULL,
	[ITEM_SLOT_10] [int] NOT NULL,
	[Skin_1] [int] NOT NULL,
	[Skin_2] [int] NOT NULL,
	[Skin_3] [int] NOT NULL,
	[Skin_4] [int] NOT NULL,
	[Skin_5] [int] NOT NULL,
	[Skin_6] [int] NOT NULL,
	[MASCOT_ID] [int] NOT NULL,
	[POSTER_1] [int] NOT NULL,
	[POSTER_2] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_User_MatchHistory]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_User_MatchHistory](
	[UID] [int] NOT NULL,
	[UID1] [int] NOT NULL,
	[UID2] [int] NOT NULL,
	[UID3] [int] NOT NULL,
	[UID4] [int] NOT NULL,
	[UID5] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_User_Message]    Script Date: 23/07/2020 09:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_User_Message](
	[ID_MSG] [int] NOT NULL,
	[UID] [int] NOT NULL,
	[UID_FROM] [int] NOT NULL,
	[Valid] [tinyint] NOT NULL,
	[Message] [varchar](70) NOT NULL,
	[Reg_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Pangya_User_Message] PRIMARY KEY CLUSTERED 
(
	[ID_MSG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_User_Statistics]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_User_Statistics](
	[UID] [int] NOT NULL,
	[Drive] [int] NOT NULL,
	[Putt] [int] NOT NULL,
	[Playtime] [int] NOT NULL,
	[Longest] [real] NOT NULL,
	[Distance] [int] NOT NULL,
	[Pangya] [int] NOT NULL,
	[Hole] [int] NOT NULL,
	[TeamHole] [int] NOT NULL,
	[Holeinone] [int] NOT NULL,
	[OB] [int] NOT NULL,
	[Bunker] [int] NOT NULL,
	[Fairway] [int] NOT NULL,
	[Albatross] [int] NOT NULL,
	[Holein] [int] NOT NULL,
	[Pang] [int] NOT NULL,
	[Timeout] [int] NOT NULL,
	[Game_Level] [smallint] NOT NULL,
	[Game_Point] [int] NOT NULL,
	[PuttIn] [int] NOT NULL,
	[LongestPuttIn] [real] NOT NULL,
	[LongestChipIn] [real] NOT NULL,
	[NoMannerGameCount] [int] NOT NULL,
	[ShotTime] [int] NOT NULL,
	[GameCount] [int] NOT NULL,
	[DisconnectGames] [int] NOT NULL,
	[wTeamWin] [int] NOT NULL,
	[wTeamGames] [int] NOT NULL,
	[LadderPoint] [smallint] NOT NULL,
	[LadderWin] [smallint] NOT NULL,
	[LadderLose] [smallint] NOT NULL,
	[LadderDraw] [smallint] NOT NULL,
	[ComboCount] [int] NOT NULL,
	[MaxComboCount] [int] NOT NULL,
	[TotalScore] [int] NOT NULL,
	[BestScore0] [smallint] NOT NULL,
	[BestScore1] [smallint] NOT NULL,
	[BestScore2] [smallint] NOT NULL,
	[BestScore3] [smallint] NOT NULL,
	[BESTSCORE4] [smallint] NOT NULL,
	[MaxPang0] [int] NULL,
	[MaxPang1] [int] NULL,
	[MaxPang2] [int] NULL,
	[MaxPang3] [int] NULL,
	[MAXPANG4] [int] NULL,
	[SumPang] [int] NOT NULL,
	[LadderHole] [smallint] NOT NULL,
	[GameCountSeason] [int] NOT NULL,
	[SkinsPang] [bigint] NOT NULL,
	[SkinsWin] [int] NOT NULL,
	[SkinsLose] [int] NOT NULL,
	[SkinsRunHoles] [int] NOT NULL,
	[SkinsStrikePoint] [int] NOT NULL,
	[SkinsAllinCount] [int] NOT NULL,
	[EventValue] [int] NOT NULL,
	[EventFlag] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pangya_Warehouse]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pangya_Warehouse](
	[item_id] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TYPEID] [int] NOT NULL,
	[C0] [smallint] NULL,
	[C1] [smallint] NULL,
	[C2] [smallint] NULL,
	[C3] [smallint] NULL,
	[C4] [smallint] NULL,
	[RegDate] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[VALID] [tinyint] NOT NULL,
	[ItemType] [tinyint] NULL,
	[Flag] [tinyint] NULL,
 CONSTRAINT [PK_Pangya_Warehouse] PRIMARY KEY CLUSTERED 
(
	[item_id] ASC,
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TD_ROOM_DATA]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TD_ROOM_DATA](
	[IDX] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[TYPEID] [int] NOT NULL,
	[POS_X] [numeric](10, 4) NULL,
	[POS_Y] [numeric](10, 4) NULL,
	[POS_Z] [numeric](10, 4) NULL,
	[POS_R] [numeric](10, 4) NULL,
	[VALID] [tinyint] NULL,
	[GETDATE] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Pangya_Guild_Info]    Script Date: 23/07/2020 09:58:27 ******/
CREATE NONCLUSTERED INDEX [IX_Pangya_Guild_Info] ON [dbo].[Pangya_Guild_Info]
(
	[GUILD_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Achievement_Data] ADD  CONSTRAINT [DF_Achievement_Data_ACHIEVEMENT_TYPEID]  DEFAULT ((0)) FOR [ACHIEVEMENT_TYPEID]
GO
ALTER TABLE [dbo].[Daily_Quest] ADD  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Daily_Quest] ADD  DEFAULT ((0)) FOR [Day]
GO
ALTER TABLE [dbo].[Pangya_Achievement] ADD  CONSTRAINT [DF__Pangya_Ach__Type__32B6742D]  DEFAULT ((3)) FOR [Type]
GO
ALTER TABLE [dbo].[Pangya_Achievement] ADD  DEFAULT ((1)) FOR [Valid]
GO
ALTER TABLE [dbo].[Pangya_Achievement_Quest] ADD  CONSTRAINT [DF_Table_1_SuccessTimestamp]  DEFAULT ((0)) FOR [SuccessDate]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_UID]  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_TYPEID]  DEFAULT ((0)) FOR [TYPEID]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_EXP]  DEFAULT ((0)) FOR [EXP]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Table_1_Level]  DEFAULT ((0)) FOR [cLevel]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_SKIN_TYPEID]  DEFAULT ((0)) FOR [SKIN_TYPEID]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Table_1_SKIN_TYPEID]  DEFAULT ((0)) FOR [RentFlag]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_TriggerPay]  DEFAULT ((0)) FOR [TriggerPay]
GO
ALTER TABLE [dbo].[Pangya_Caddie] ADD  CONSTRAINT [DF_Pangya_Caddie_VALID]  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[Pangya_Card] ADD  CONSTRAINT [DF_Pangya_Card_RegData]  DEFAULT (getdate()) FOR [RegData]
GO
ALTER TABLE [dbo].[Pangya_Card] ADD  CONSTRAINT [DF_Pangya_Card_VALID]  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[Pangya_Card_Equip] ADD  CONSTRAINT [DF__Pangya_Card__CID__3B16B004]  DEFAULT ((0)) FOR [CID]
GO
ALTER TABLE [dbo].[Pangya_Card_Equip] ADD  CONSTRAINT [DF__Pangya_Ca__REGDA__392E6792]  DEFAULT (getdate()) FOR [REGDATE]
GO
ALTER TABLE [dbo].[Pangya_Card_Equip] ADD  CONSTRAINT [DF__Pangya_Car__FLAG__477C86E9]  DEFAULT ((0)) FOR [FLAG]
GO
ALTER TABLE [dbo].[Pangya_Card_Equip] ADD  CONSTRAINT [DF__Pangya_Ca__VALID__3A228BCB]  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_GIFT_FLAG]  DEFAULT ((0)) FOR [GIFT_FLAG]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_HAIR_COLOR]  DEFAULT ((0)) FOR [HAIR_COLOR]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_POWER]  DEFAULT ((0)) FOR [POWER]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Table_1_POWER4]  DEFAULT ((0)) FOR [CONTROL]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Table_1_POWER3]  DEFAULT ((0)) FOR [IMPACT]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Table_1_POWER2]  DEFAULT ((0)) FOR [SPIN]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Table_1_POWER1]  DEFAULT ((0)) FOR [CURVE]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_CUTIN]  DEFAULT ((0)) FOR [CUTIN]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_1]  DEFAULT ((0)) FOR [PART_TYPEID_1]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_2]  DEFAULT ((0)) FOR [PART_TYPEID_2]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_3]  DEFAULT ((0)) FOR [PART_TYPEID_3]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_4]  DEFAULT ((0)) FOR [PART_TYPEID_4]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_5]  DEFAULT ((0)) FOR [PART_TYPEID_5]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_6]  DEFAULT ((0)) FOR [PART_TYPEID_6]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_7]  DEFAULT ((0)) FOR [PART_TYPEID_7]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_8]  DEFAULT ((0)) FOR [PART_TYPEID_8]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_9]  DEFAULT ((0)) FOR [PART_TYPEID_9]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_10]  DEFAULT ((0)) FOR [PART_TYPEID_10]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_11]  DEFAULT ((0)) FOR [PART_TYPEID_11]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_12]  DEFAULT ((0)) FOR [PART_TYPEID_12]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_13]  DEFAULT ((0)) FOR [PART_TYPEID_13]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_14]  DEFAULT ((0)) FOR [PART_TYPEID_14]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_15]  DEFAULT ((0)) FOR [PART_TYPEID_15]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_16]  DEFAULT ((0)) FOR [PART_TYPEID_16]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_17]  DEFAULT ((0)) FOR [PART_TYPEID_17]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_18]  DEFAULT ((0)) FOR [PART_TYPEID_18]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_19]  DEFAULT ((0)) FOR [PART_TYPEID_19]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_20]  DEFAULT ((0)) FOR [PART_TYPEID_20]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_21]  DEFAULT ((0)) FOR [PART_TYPEID_21]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_22]  DEFAULT ((0)) FOR [PART_TYPEID_22]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_23]  DEFAULT ((0)) FOR [PART_TYPEID_23]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_TYPEID_24]  DEFAULT ((0)) FOR [PART_TYPEID_24]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_1]  DEFAULT ((0)) FOR [PART_IDX_1]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_2]  DEFAULT ((0)) FOR [PART_IDX_2]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_3]  DEFAULT ((0)) FOR [PART_IDX_3]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_4]  DEFAULT ((0)) FOR [PART_IDX_4]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_5]  DEFAULT ((0)) FOR [PART_IDX_5]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_6]  DEFAULT ((0)) FOR [PART_IDX_6]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_7]  DEFAULT ((0)) FOR [PART_IDX_7]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_8]  DEFAULT ((0)) FOR [PART_IDX_8]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_9]  DEFAULT ((0)) FOR [PART_IDX_9]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_10]  DEFAULT ((0)) FOR [PART_IDX_10]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_11]  DEFAULT ((0)) FOR [PART_IDX_11]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_12]  DEFAULT ((0)) FOR [PART_IDX_12]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_13]  DEFAULT ((0)) FOR [PART_IDX_13]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_14]  DEFAULT ((0)) FOR [PART_IDX_14]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_15]  DEFAULT ((0)) FOR [PART_IDX_15]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_16]  DEFAULT ((0)) FOR [PART_IDX_16]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_17]  DEFAULT ((0)) FOR [PART_IDX_17]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_18]  DEFAULT ((0)) FOR [PART_IDX_18]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_19]  DEFAULT ((0)) FOR [PART_IDX_19]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_20]  DEFAULT ((0)) FOR [PART_IDX_20]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_21]  DEFAULT ((0)) FOR [PART_IDX_21]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_22]  DEFAULT ((0)) FOR [PART_IDX_22]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_23]  DEFAULT ((0)) FOR [PART_IDX_23]
GO
ALTER TABLE [dbo].[Pangya_Character] ADD  CONSTRAINT [DF_Pangya_Character_PART_IDX_24]  DEFAULT ((0)) FOR [PART_IDX_24]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_C0_SLOT]  DEFAULT ((0)) FOR [C0_SLOT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Table_1_C0_SLOT1]  DEFAULT ((0)) FOR [C1_SLOT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Table_1_C0_SLOT2]  DEFAULT ((0)) FOR [C2_SLOT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Table_1_C0_SLOT3]  DEFAULT ((0)) FOR [C3_SLOT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Table_1_C0_SLOT4]  DEFAULT ((0)) FOR [C4_SLOT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_CLUB_POINT]  DEFAULT ((0)) FOR [CLUB_POINT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_CLUB_WORK_COUNT]  DEFAULT ((0)) FOR [CLUB_WORK_COUNT]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_CLUB_SLOT_CANCEL]  DEFAULT ((0)) FOR [CLUB_SLOT_CANCEL]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_CLUB_POINT_TOTAL_LOG]  DEFAULT ((0)) FOR [CLUB_POINT_TOTAL_LOG]
GO
ALTER TABLE [dbo].[Pangya_Club_Info] ADD  CONSTRAINT [DF_Pangya_Club_Info_CLUB_UPGRADE_PANG_LOG]  DEFAULT ((0)) FOR [CLUB_UPGRADE_PANG_LOG]
GO
ALTER TABLE [dbo].[Pangya_Daily_Quest] ADD  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[Pangya_Daily_Quest] ADD  DEFAULT ((0)) FOR [QuestID1]
GO
ALTER TABLE [dbo].[Pangya_Daily_Quest] ADD  DEFAULT ((0)) FOR [QuestID2]
GO
ALTER TABLE [dbo].[Pangya_Daily_Quest] ADD  DEFAULT ((0)) FOR [QuestID3]
GO
ALTER TABLE [dbo].[Pangya_Daily_Quest] ADD  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Exception_Log] ADD  CONSTRAINT [DF_Pangya_Exception_Log_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_IsAccept]  DEFAULT ((0)) FOR [IsAccept]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_GroupName]  DEFAULT ('Friend') FOR [GroupName]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_IsAgree]  DEFAULT ((0)) FOR [IsAgree]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_Memo]  DEFAULT ('Friend') FOR [Memo]
GO
ALTER TABLE [dbo].[Pangya_Friend] ADD  CONSTRAINT [DF_Pangya_Friend_IsBlock]  DEFAULT ((0)) FOR [IsBlock]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro1]  DEFAULT ('Pangya!') FOR [Macro1]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro2]  DEFAULT ('Pangya!') FOR [Macro2]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro3]  DEFAULT ('Pangya!') FOR [Macro3]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro4]  DEFAULT ('Pangya!') FOR [Macro4]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro5]  DEFAULT ('Pangya!') FOR [Macro5]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro6]  DEFAULT ('Pangya!') FOR [Macro6]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro7]  DEFAULT ('Pangya!') FOR [Macro7]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro8]  DEFAULT ('Pangya!') FOR [Macro8]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro9]  DEFAULT ('Pangya!') FOR [Macro9]
GO
ALTER TABLE [dbo].[Pangya_Game_Macro] ADD  CONSTRAINT [DF_Pangya_Game_Macro_Macro10]  DEFAULT ('Pangya!') FOR [Macro10]
GO
ALTER TABLE [dbo].[Pangya_Guild_Emblem] ADD  CONSTRAINT [DF_Pangya_Guild_Emblem_GUILD_MARK_ISVALID]  DEFAULT ((1)) FOR [GUILD_MARK_ISVALID]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_POINT]  DEFAULT ((0)) FOR [GUILD_POINT]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_PANG]  DEFAULT ((0)) FOR [GUILD_PANG]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_IMAGE]  DEFAULT ('GUILDMARK') FOR [GUILD_IMAGE]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_IMAGE_KEY_UPLOAD]  DEFAULT ((-1)) FOR [GUILD_IMAGE_KEY_UPLOAD]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_CREATE_DATE]  DEFAULT (getdate()) FOR [GUILD_CREATE_DATE]
GO
ALTER TABLE [dbo].[Pangya_Guild_Info] ADD  CONSTRAINT [DF_Pangya_Guild_Info_GUILD_VALID]  DEFAULT ((1)) FOR [GUILD_VALID]
GO
ALTER TABLE [dbo].[Pangya_Guild_Log] ADD  CONSTRAINT [DF_Pangya_Guild_Log_GUILD_ACTION_DATE]  DEFAULT (getdate()) FOR [GUILD_ACTION_DATE]
GO
ALTER TABLE [dbo].[Pangya_Guild_Member] ADD  CONSTRAINT [DF_Pangya_Guild_Member_GUILD_POSITION]  DEFAULT ((3)) FOR [GUILD_POSITION]
GO
ALTER TABLE [dbo].[Pangya_Guild_Member] ADD  CONSTRAINT [DF_Pangya_Guild_Member_GUILD_ENTERED_TIME]  DEFAULT (getdate()) FOR [GUILD_ENTERED_TIME]
GO
ALTER TABLE [dbo].[Pangya_Guild_Member] ADD  CONSTRAINT [DF_Pangya_Guild_Member_GUILD_MEMBER_STATUS]  DEFAULT ((0)) FOR [GUILD_MEMBER_STATUS]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily] ADD  CONSTRAINT [DF_Pangya_Item_Daily_ItemType]  DEFAULT ((0)) FOR [ItemType]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_Counter]  DEFAULT ((0)) FOR [Counter]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_Item_TypeID]  DEFAULT ((0)) FOR [Item_TypeID]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_Quantity]  DEFAULT ((0)) FOR [Item_Quantity]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_Item_TypeID_Next]  DEFAULT ((0)) FOR [Item_TypeID_Next]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_Item_Quantity_Next]  DEFAULT ((0)) FOR [Item_Quantity_Next]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_LoginCount]  DEFAULT ((0)) FOR [LoginCount]
GO
ALTER TABLE [dbo].[Pangya_Item_Daily_Log] ADD  CONSTRAINT [DF_Pangya_Item_Daily_Log_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Locker_Item] ADD  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[Pangya_Locker_Item] ADD  DEFAULT ((0)) FOR [TypeID]
GO
ALTER TABLE [dbo].[Pangya_Locker_Item] ADD  DEFAULT ('Name') FOR [Name]
GO
ALTER TABLE [dbo].[Pangya_Locker_Item] ADD  DEFAULT ((0)) FOR [FROM_ID]
GO
ALTER TABLE [dbo].[Pangya_Locker_Item] ADD  DEFAULT ((1)) FOR [Valid]
GO
ALTER TABLE [dbo].[Pangya_Mail] ADD  CONSTRAINT [DF_Pangya_Mail_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Mail_Item] ADD  CONSTRAINT [DF_Pangya_Mail_Item_SetTypeID]  DEFAULT ((0)) FOR [SETTYPEID]
GO
ALTER TABLE [dbo].[Pangya_Mail_Item] ADD  CONSTRAINT [DF_Pangya_Mail_Item_DAY]  DEFAULT ((0)) FOR [DAY]
GO
ALTER TABLE [dbo].[Pangya_Mail_Item] ADD  CONSTRAINT [DF_Pangya_Mail_Item_IN_DATE]  DEFAULT (getdate()) FOR [IN_DATE]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Map___Map__53584DE9]  DEFAULT ((0)) FOR [Map]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Drive__544C7222]  DEFAULT ((0)) FOR [Drive]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Map__Putt__5540965B]  DEFAULT ((0)) FOR [Putt]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Map__Hole__5634BA94]  DEFAULT ((0)) FOR [Hole]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Fairw__5728DECD]  DEFAULT ((0)) FOR [Fairway]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Holei__581D0306]  DEFAULT ((0)) FOR [Holein]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__PuttI__5911273F]  DEFAULT ((0)) FOR [PuttIn]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Total__5A054B78]  DEFAULT ((0)) FOR [TotalScore]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__BestS__5AF96FB1]  DEFAULT ((127)) FOR [BestScore]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__MaxPa__5BED93EA]  DEFAULT ((0)) FOR [MaxPang]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__CharT__5CE1B823]  DEFAULT ((0)) FOR [CharTypeId]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Event__5DD5DC5C]  DEFAULT ((0)) FOR [EventScore]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__Assis__629A9179]  DEFAULT ((0)) FOR [Assist]
GO
ALTER TABLE [dbo].[Pangya_Map_Statistics] ADD  CONSTRAINT [DF__Pangya_Ma__REGDA__5ECA0095]  DEFAULT (getdate()) FOR [REGDATE]
GO
ALTER TABLE [dbo].[Pangya_Mascot] ADD  CONSTRAINT [DF_Pangya_Mascot_MESSAGE]  DEFAULT ('PANGYA!') FOR [MESSAGE]
GO
ALTER TABLE [dbo].[Pangya_Mascot] ADD  CONSTRAINT [DF_Pangya_Mascot_VALID]  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_pangya_member_IDState]  DEFAULT ((0)) FOR [IDState]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_pangya_member_FirstSet]  DEFAULT ((0)) FOR [FirstSet]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_pangya_member_Sex]  DEFAULT ((0)) FOR [Sex]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_pangya_member_LogonCount]  DEFAULT ((0)) FOR [LogonCount]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_pangya_member_Capabilities]  DEFAULT ((0)) FOR [Capabilities]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF__Pangya_Me__RegDa__0CDAE408]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_Pangya_Member_GuildID]  DEFAULT ((0)) FOR [GUILDINDEX]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_Pangya_Member_DailyLoginCount]  DEFAULT ((0)) FOR [DailyLoginCount]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_Pangya_Member_Tutorial]  DEFAULT ((0)) FOR [Tutorial]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_Pangya_Member_Event1]  DEFAULT ((0)) FOR [Event1]
GO
ALTER TABLE [dbo].[Pangya_Member] ADD  CONSTRAINT [DF_Pangya_Member_Event2]  DEFAULT ((0)) FOR [Event2]
GO
ALTER TABLE [dbo].[Pangya_Memorial_Log] ADD  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Pangya_Memorial_Log] ADD  DEFAULT (getdate()) FOR [DateIN]
GO
ALTER TABLE [dbo].[Pangya_Personal] ADD  DEFAULT ((0)) FOR [CookieAmt]
GO
ALTER TABLE [dbo].[Pangya_Personal] ADD  DEFAULT ((0)) FOR [PangLockerAmt]
GO
ALTER TABLE [dbo].[Pangya_Personal] ADD  DEFAULT ((0)) FOR [LockerPwd]
GO
ALTER TABLE [dbo].[Pangya_Personal_Log] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[Pangya_Personal_Log] ADD  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[Pangya_Personal_Log] ADD  DEFAULT ((0)) FOR [LockerPang]
GO
ALTER TABLE [dbo].[Pangya_SelfDesign] ADD  CONSTRAINT [DF_Pangya_SelfDesign_UCC_STATUS]  DEFAULT ((0)) FOR [UCC_STATUS]
GO
ALTER TABLE [dbo].[Pangya_SelfDesign] ADD  CONSTRAINT [DF_Pangya_SelfDesign_UCC_DRAWER]  DEFAULT ((0)) FOR [UCC_DRAWER]
GO
ALTER TABLE [dbo].[Pangya_SelfDesign] ADD  CONSTRAINT [DF_Pangya_SelfDesign_UCC_COPY_COUNT]  DEFAULT ((1)) FOR [UCC_COPY_COUNT]
GO
ALTER TABLE [dbo].[Pangya_SelfDesign] ADD  CONSTRAINT [DF_Pangya_SelfDesign_IN_DATE]  DEFAULT (getdate()) FOR [IN_DATE]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_Port]  DEFAULT ((0)) FOR [Port]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_MaxUser]  DEFAULT ((1000)) FOR [MaxUser]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_UsersOnline]  DEFAULT ((0)) FOR [UsersOnline]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_Property]  DEFAULT ((2048)) FOR [Property]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_BlockFunc]  DEFAULT ((0)) FOR [BlockFunc]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_ImgNo]  DEFAULT ((0)) FOR [ImgNo]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_ImgEvent]  DEFAULT ((0)) FOR [ImgEvent]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_ServerType]  DEFAULT ((0)) FOR [ServerType]
GO
ALTER TABLE [dbo].[Pangya_Server] ADD  CONSTRAINT [DF_Pangya_Server_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_CADDIE]  DEFAULT ((0)) FOR [CADDIE]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_CHARACTER_ID]  DEFAULT ((0)) FOR [CHARACTER_ID]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_CLUB_ID]  DEFAULT ((0)) FOR [CLUB_ID]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_BALL_ID]  DEFAULT ((0)) FOR [BALL_ID]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_1]  DEFAULT ((0)) FOR [ITEM_SLOT_1]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_2]  DEFAULT ((0)) FOR [ITEM_SLOT_2]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_3]  DEFAULT ((0)) FOR [ITEM_SLOT_3]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_4]  DEFAULT ((0)) FOR [ITEM_SLOT_4]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_5]  DEFAULT ((0)) FOR [ITEM_SLOT_5]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_6]  DEFAULT ((0)) FOR [ITEM_SLOT_6]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_7]  DEFAULT ((0)) FOR [ITEM_SLOT_7]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_8]  DEFAULT ((0)) FOR [ITEM_SLOT_8]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_9]  DEFAULT ((0)) FOR [ITEM_SLOT_9]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_ITEM_SLOT_10]  DEFAULT ((0)) FOR [ITEM_SLOT_10]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_1]  DEFAULT ((0)) FOR [Skin_1]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_2]  DEFAULT ((0)) FOR [Skin_2]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_3]  DEFAULT ((0)) FOR [Skin_3]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_4]  DEFAULT ((0)) FOR [Skin_4]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_5]  DEFAULT ((0)) FOR [Skin_5]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_Skin_51]  DEFAULT ((0)) FOR [Skin_6]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  CONSTRAINT [DF_Pangya_User_Equip_MASCOT_ID]  DEFAULT ((0)) FOR [MASCOT_ID]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  DEFAULT ((0)) FOR [POSTER_1]
GO
ALTER TABLE [dbo].[Pangya_User_Equip] ADD  DEFAULT ((0)) FOR [POSTER_2]
GO
ALTER TABLE [dbo].[Pangya_User_MatchHistory] ADD  DEFAULT ((0)) FOR [UID1]
GO
ALTER TABLE [dbo].[Pangya_User_MatchHistory] ADD  DEFAULT ((0)) FOR [UID2]
GO
ALTER TABLE [dbo].[Pangya_User_MatchHistory] ADD  DEFAULT ((0)) FOR [UID3]
GO
ALTER TABLE [dbo].[Pangya_User_MatchHistory] ADD  DEFAULT ((0)) FOR [UID4]
GO
ALTER TABLE [dbo].[Pangya_User_MatchHistory] ADD  DEFAULT ((0)) FOR [UID5]
GO
ALTER TABLE [dbo].[Pangya_User_Message] ADD  CONSTRAINT [DF_Pangya_User_Message_Reg_Date]  DEFAULT (getdate()) FOR [Reg_Date]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Drive]  DEFAULT ((0)) FOR [Drive]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Putt]  DEFAULT ((0)) FOR [Putt]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Playtime]  DEFAULT ((0)) FOR [Playtime]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Longest]  DEFAULT ((0)) FOR [Longest]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Distance]  DEFAULT ((0)) FOR [Distance]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Pangya]  DEFAULT ((0)) FOR [Pangya]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Hole]  DEFAULT ((0)) FOR [Hole]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_TeamHole]  DEFAULT ((0)) FOR [TeamHole]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Holeinone]  DEFAULT ((0)) FOR [Holeinone]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_OB]  DEFAULT ((0)) FOR [OB]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Bunker]  DEFAULT ((0)) FOR [Bunker]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Fairway]  DEFAULT ((0)) FOR [Fairway]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Albatross]  DEFAULT ((0)) FOR [Albatross]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Holein]  DEFAULT ((0)) FOR [Holein]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Pang]  DEFAULT ((3000)) FOR [Pang]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Timeout]  DEFAULT ((0)) FOR [Timeout]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Game_level]  DEFAULT ((0)) FOR [Game_Level]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_Game_point]  DEFAULT ((0)) FOR [Game_Point]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_PuttIn]  DEFAULT ((0)) FOR [PuttIn]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LongestPuttIn]  DEFAULT ((0)) FOR [LongestPuttIn]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LongestChipIn]  DEFAULT ((0)) FOR [LongestChipIn]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_NoMannerGameCount]  DEFAULT ((0)) FOR [NoMannerGameCount]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_ShotTime]  DEFAULT ((0)) FOR [ShotTime]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_GameCount]  DEFAULT ((0)) FOR [GameCount]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_DisconnectGames]  DEFAULT ((0)) FOR [DisconnectGames]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_wTeamWin]  DEFAULT ((0)) FOR [wTeamWin]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_wTeamGames]  DEFAULT ((0)) FOR [wTeamGames]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LadderPoint]  DEFAULT ((1000)) FOR [LadderPoint]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LadderWin]  DEFAULT ((0)) FOR [LadderWin]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LadderLose]  DEFAULT ((0)) FOR [LadderLose]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LadderDraw]  DEFAULT ((0)) FOR [LadderDraw]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_ComboCount]  DEFAULT ((0)) FOR [ComboCount]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_MaxComboCount]  DEFAULT ((0)) FOR [MaxComboCount]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_TotalScore]  DEFAULT ((0)) FOR [TotalScore]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_BestScore0]  DEFAULT ((127)) FOR [BestScore0]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_BestScore1]  DEFAULT ((127)) FOR [BestScore1]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_BestScore2]  DEFAULT ((127)) FOR [BestScore2]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_BestScore3]  DEFAULT ((127)) FOR [BestScore3]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__Pangya_Us__BESTS__5F94D2F3]  DEFAULT ((127)) FOR [BESTSCORE4]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_MaxPang0]  DEFAULT ((0)) FOR [MaxPang0]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_MaxPang1]  DEFAULT ((0)) FOR [MaxPang1]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_MaxPang2]  DEFAULT ((0)) FOR [MaxPang2]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_MaxPang3]  DEFAULT ((0)) FOR [MaxPang3]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__Pangya_Us__MAXPA__4297D63B]  DEFAULT ((0)) FOR [MAXPANG4]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_SumPang]  DEFAULT ((0)) FOR [SumPang]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF_PangYa_User_Statistics_LadderHole]  DEFAULT ((0)) FOR [LadderHole]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__GameC__123F82FA]  DEFAULT ((0)) FOR [GameCountSeason]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__1333A733]  DEFAULT ((0)) FOR [SkinsPang]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__1427CB6C]  DEFAULT ((0)) FOR [SkinsWin]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__151BEFA5]  DEFAULT ((0)) FOR [SkinsLose]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__161013DE]  DEFAULT ((0)) FOR [SkinsRunHoles]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__17043817]  DEFAULT ((0)) FOR [SkinsStrikePoint]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Skins__17F85C50]  DEFAULT ((0)) FOR [SkinsAllinCount]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Event__18EC8089]  DEFAULT ((0)) FOR [EventValue]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] ADD  CONSTRAINT [DF__PangYa_Us__Event__19E0A4C2]  DEFAULT ((0)) FOR [EventFlag]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_UID]  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_C0]  DEFAULT ((0)) FOR [C0]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_C01]  DEFAULT ((0)) FOR [C1]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_C02]  DEFAULT ((0)) FOR [C2]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_C03]  DEFAULT ((0)) FOR [C3]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_C04]  DEFAULT ((0)) FOR [C4]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_RegDate]  DEFAULT (getdate()) FOR [RegDate]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_DateEnd]  DEFAULT (getdate()) FOR [DateEnd]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_VALID]  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_ItemType]  DEFAULT ((0)) FOR [ItemType]
GO
ALTER TABLE [dbo].[Pangya_Warehouse] ADD  CONSTRAINT [DF_Pangya_Warehouse_Flag]  DEFAULT ((0)) FOR [Flag]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [UID]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [TYPEID]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [POS_X]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [POS_Y]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [POS_Z]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((0)) FOR [POS_R]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT ((1)) FOR [VALID]
GO
ALTER TABLE [dbo].[TD_ROOM_DATA] ADD  DEFAULT (getdate()) FOR [GETDATE]
GO
ALTER TABLE [dbo].[Pangya_User_Statistics]  WITH NOCHECK ADD  CONSTRAINT [CK_PangYa_User_Statistics] CHECK NOT FOR REPLICATION (([pang]>=(0)))
GO
ALTER TABLE [dbo].[Pangya_User_Statistics] CHECK CONSTRAINT [CK_PangYa_User_Statistics]
GO
/****** Object:  StoredProcedure [dbo].[ProcAddItem]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcAddItem] 
	@UID INT,
	@IFFTYPEID INT,
	@QUANTITY INT,
	@ISUCC TINYINT,
	@ITEM_TYPE TINYINT,
	@DAY INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ITEM_GRP INT = 0
	DECLARE @ITEM_ID BIGINT
	DECLARE @UCC_KEY VARCHAR(8) = '';
	-- FOR CADDIE
	DECLARE @DATETIME DATETIME

	SET @ITEM_GRP = [dbo].UDF_PARTS_GROUP(@IFFTYPEID)

	/********************
	* 1. CHARACTER
	********************/
	IF (@ITEM_GRP = 1) BEGIN
		SET @ITEM_ID = (SELECT CID FROM [DBO].Pangya_Character WHERE UID = @UID AND TYPEID = @IFFTYPEID)
		IF (@ITEM_ID <= 0) OR (@ITEM_ID IS NULL) BEGIN
		Exec dbo.ProcFixPartsCharacter @UID, @IFFTYPEID, 0;	
		SET @ITEM_ID = (SELECT CID from dbo.Pangya_Character where UID = @UID AND TYPEID = @IFFTYPEID)				
		END
	END

	/********************
	* 2. PART
	********************/
	IF (@ITEM_GRP = 2) BEGIN -- PART ITEM
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES(@UID, @IFFTYPEID, 0)
		SET @ITEM_ID = SCOPE_IDENTITY()
		-- CHECK IF UCC
		IF @ISUCC = 1 BEGIN
			WHILE (1=1) BEGIN  
				SELECT @UCC_KEY = UPPER(LEFT(NEWID(), 8))  
				IF NOT EXISTS(SELECT 1 FROM DBO.Pangya_SelfDesign WHERE UID = @UID AND UCC_UNIQE = @UCC_KEY) BEGIN   
					BREAK  
				END   
			END
			INSERT INTO [dbo].Pangya_SelfDesign(UID, ITEM_ID, UCC_UNIQE, TYPEID) VALUES (@UID, @ITEM_ID, @UCC_KEY, @IFFTYPEID)
		END
		-- END IF UCC
	END
	
	/********************
	* 5,6. ITEM, BALL
	********************/
	IF (@ITEM_GRP = 6) OR (@ITEM_GRP = 5) BEGIN -- {NORMAL ITEM} AND {BALL}
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES(@UID, @IFFTYPEID, @QUANTITY)
		SET @ITEM_ID = SCOPE_IDENTITY()
	END

	/********************
	* 7. CADDIE
	********************/
	IF (@ITEM_GRP = 7) BEGIN -- CADDIE
		-- CHECK CADDIE
		IF @ITEM_TYPE = 2 BEGIN
			SET @DATETIME = DATEADD(DAY, 30, GETDATE())
		END ELSE BEGIN
			SET @DATETIME = GETDATE()
		END
		-- END
		INSERT INTO DBO.Pangya_Caddie(UID, TYPEID, RentFlag, END_DATE) VALUES (@UID, @IFFTYPEID, @ITEM_TYPE, @DATETIME)
		SET @ITEM_ID = SCOPE_IDENTITY()
	END

	/********************
	* 14. SKIN
	********************/
	IF (@ITEM_GRP = 14) BEGIN -- SKIN
		-- TO CHECK
		IF @ITEM_TYPE = 0 BEGIN
			SET @DATETIME = GETDATE()
		END ELSE BEGIN
			SET @DATETIME = DATEADD(DAY, ISNULL(@DAY, 1), GETDATE())
		END
		-- END CHECK
		INSERT INTO [dbo].Pangya_Warehouse(UID, TYPEID, C0, DateEnd, Flag) VALUES(@UID, @IFFTYPEID, 0, @DATETIME, @ITEM_TYPE)
		SET @ITEM_ID = SCOPE_IDENTITY()
	END

	/********************
	* 31. CARD
	********************/
	IF (@ITEM_GRP = 31) BEGIN -- CARD
		INSERT INTO DBO.Pangya_Card(UID, CARD_TYPEID, QTY, VALID) VALUES (@UID, @IFFTYPEID, @QUANTITY, 1)
		SET @ITEM_ID = SCOPE_IDENTITY()
	END
	
	/********************
	* 4. CLUB SET
	********************/
	IF (@ITEM_GRP = 4) BEGIN -- Club Set
		INSERT INTO DBO.Pangya_Warehouse(UID, TYPEID) VALUES (@UID, @IFFTYPEID)
		SET @ITEM_ID = SCOPE_IDENTITY()
		INSERT INTO DBO.Pangya_Club_Info(ITEM_ID) VALUES (@ITEM_ID)
	END
	
	/*******************
	Send Result
	*******************/
	IF (@ITEM_ID > 0) BEGIN
		SELECT	@ITEM_ID	   AS IDX, 
				@IFFTYPEID   AS iffTypeId, 
				@QUANTITY	   AS Quantity, 
				@UCC_KEY	   AS UCC_KEY, 
				@ITEM_GRP	   AS GROUPS, 
				@ITEM_TYPE   AS Flag, 
				@DATETIME	   AS END_DATE
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcAddItemTutorial]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LuisMK
-- Create date: 12/7/2560 13:15
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcAddItemTutorial] 
	@UID INT,
	@CODE INT,
	@OPT int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TYPEIDITEM INT;
	DECLARE @QNTD INT; 
	

	IF @OPT = 0 
	BEGIN
		IF @CODE = 1 begin 
		SET @TYPEIDITEM = 0x1A00000F; SET @QNTD = 3;
		end

		IF @CODE = 2 begin 
		SET @TYPEIDITEM = 0x18000007; SET @QNTD = 3;
		end

		IF @CODE = 4 begin 
		SET @TYPEIDITEM = 0x18000005; SET @QNTD = 3;
		end

		IF @CODE = 8 begin 
		SET @TYPEIDITEM = 0x18000008; SET @QNTD = 3;
		end

		IF @CODE = 16 begin 
		SET @TYPEIDITEM = 0x1A000010; SET @QNTD = 500;
		end

		IF @CODE = 32 begin 
		SET @TYPEIDITEM = 0x18000004; SET @QNTD = 3;
		end

		IF @CODE = 64 begin 
		SET @TYPEIDITEM = 0x1A000010; SET @QNTD = 500;
		end

		IF @CODE = 128 begin 
		SET @TYPEIDITEM = 0x1A000010; SET @QNTD = 1000;
		end

		IF @CODE = 256 begin 
		SET @TYPEIDITEM = 0x1A00000F; SET @QNTD = 3;
		end
	END
	
	else
	begin

	
		IF @CODE = 256 begin 
		SET @TYPEIDITEM = 0x1A00000F; SET @QNTD = 3;
		end

		IF @CODE = 512 begin 
		SET @TYPEIDITEM = 0x18000028; SET @QNTD = 1;
		end

		IF @CODE = 1024 begin 
		SET @TYPEIDITEM = 0x18000006; SET @QNTD = 1;
		end

		IF @CODE = 2048 begin 
		SET @TYPEIDITEM = 0x18000007; SET @QNTD = 5;
		end

		IF @CODE = 4096 begin 
		SET @TYPEIDITEM = 0x18000000; SET @QNTD = 4;
		end

		IF @CODE = 8192 begin 
		SET @TYPEIDITEM = 0x18000001; SET @QNTD = 4;
		end
	end

	exec dbo.ProcInsertMail @UID, '@GM', 'NICE TUTORIAL', @TYPEIDITEM, 0, @QNTD, 0;
END





GO
/****** Object:  StoredProcedure [dbo].[ProcAddRent]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcAddRent]
	@UID INT,
	@TYPEID INT,
	@DAY_IN INT = 7
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @DATEEND DATETIME = GETDATE()
	
	INSERT INTO [dbo].Pangya_Warehouse(UID, TYPEID, C0, Flag, DateEnd) VALUES(@UID, @TYPEID, 0, 0x60, DATEADD(DAY, @DAY_IN, @DATEEND) )
	
	SELECT 	ITEM_INDEX = SCOPE_IDENTITY(),
					ITEM_TYPEID = @TYPEID,
					ITEM_FLAG = 96,
					ITEM_DATE_END = DATEADD(DAY, @DAY_IN, @DATEEND)
END
GO
/****** Object:  StoredProcedure [dbo].[ProcAddUserMessage]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ProcAddUserMessage] 

@from_uid INT,
@uid_to int,
@msg_in VARCHAR(64)
AS
BEGIN
   SET NOCOUNT ON;
	INSERT INTO Pangya_User_Message(UID, UID_FROM, VALID, Message, REG_DATE)
    VALUES(@uid_to, @from_uid, 1, @msg_in, GETDATE());
    
    --# tira os 10 pangs da msg enviada
    UPDATE Pangya_User_Statistics SET pang = pang - 10 WHERE UID = @from_uid;
	end
GO
/****** Object:  StoredProcedure [dbo].[ProcAlterAchievement]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Name
-- Create date: 
-- Description:    
-- =============================================
CREATE PROCEDURE [dbo].[ProcAlterAchievement]
  @UID INT,
  @AchievementTypeID INT,
  @Quantity INT
  AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @QID INT
  DECLARE @QINDEX INT
  DECLARE @QTYPEID INT
  DECLARE @QCOUNTERTYPEID INT
  DECLARE @QCOUNTERQTY INT
  DECLARE @QACHIEVEMENTTYPEID INT

  DECLARE @CounterID INT = 0
  DECLARE @CounterQTY INT = 0
  DECLARE @CounterTypeID INT = 0

  DECLARE @LastInsertID INT = 0

  -- FETCH COUNTER THAT MATCH ACIEVEMENT TYPE ID
  ;WITH AchievementUpdate(ID, TypeID,Quantity) AS(
  SELECT A.ID,
         A.TypeID,
         A.Quantity
  FROM DBO.Pangya_Achievement_Counter A
  CROSS APPLY(
   SELECT ID
   FROM DBO.Pangya_Achievement
   WHERE UID = @UID AND TypeID = @AchievementTypeID
  ) B
  CROSS APPLY (
   SELECT TOP 1 Counter_Index
   FROM DBO.Pangya_Achievement_Quest
   WHERE Achievement_Index = B.ID AND SuccessDate IS NULL
  ) C
  WHERE A.ID = C.Counter_Index)
  
  -- UPDATE THAT FECHED
  UPDATE AchievementUpdate
  SET Quantity += @Quantity, 
	 -- HACK WAY
      @CounterTypeID = TypeID,
      @CounterID = ID,
      @CounterQTY = Quantity;
      
  IF (@@ROWCOUNT > 0)
  BEGIN
  	-- DECLARE COUNTER TABLE
    DECLARE @CounterTable TABLE (CounterID INT, CounterTypeID INT, CounterOld INT, CounterNew INT, CounterAdd INT)

    -- QUEST UPDATE
    DECLARE @QuestUpdate TABLE (ID INT IDENTITY(1,1), QuestIndex INT, QuestTypeID INT, CounterTypeID INT, CounterQTY INT, AchievementTypeID INT)

    -- QUEST TRIGGER
    DECLARE @QuestTrigger TABLE (AchievementTypeID INT, QuestTypeID INT)

    -- INSERT COUNTER UPDATE
    INSERT INTO @CounterTable(CounterID, CounterTypeID, CounterOld, CounterNew, CounterAdd) 
    VALUES(@CounterID, @CounterTypeID, @CounterQTY, @CounterQTY + @Quantity, @Quantity)

    -- INSERT INTO QUEST TO BE UPDATE
    INSERT INTO @QuestUpdate(QuestIndex, QuestTypeID, CounterTypeID, CounterQTY, AchievementTypeID)
    SELECT A.ID, A.Achivement_Quest_TypeID, B.TypeID, A.Count, C.TypeID
    FROM DBO.Pangya_Achievement_Quest A
    INNER JOIN DBO.Pangya_Achievement_Counter B
    ON B.ID = A.Counter_Index
    INNER JOIN DBO.Pangya_Achievement C
    ON C.ID = A.Achievement_Index
    WHERE A.UID = @UID AND B.Quantity >= A.Count AND A.SuccessDate IS NULL

    WHILE EXISTS (SELECT 1 FROM @QuestUpdate)
    BEGIN
	 -- GET TOP 1 FROM TEMP TABLE
	 SELECT TOP 1
	 @QID = ID,
	 @QINDEX = QuestIndex,
	 @QTYPEID = QuestTypeID,
	 @QCOUNTERTYPEID = CounterTypeID,
	 @QCOUNTERQTY = CounterQTY,
	 @QACHIEVEMENTTYPEID = AchievementTypeID
	 FROM @QuestUpdate

	 -- FIRST INSERT INTO COUNTER
	 INSERT INTO DBO.Pangya_Achievement_Counter(UID, TypeID, Quantity) VALUES (@UID, @QCOUNTERTYPEID, @QCOUNTERQTY)
	 SET @LastInsertID = SCOPE_IDENTITY()

	 -- UPDATE THE OLD ONE
	 UPDATE [DBO].Pangya_Achievement_Quest
	 SET Counter_Index = @LastInsertID,
		SuccessDate = GETDATE()
	 WHERE UID = @UID AND
		  ID = @QINDEX

	 -- INSERT INTO TEMP COUNTER
	 INSERT INTO @CounterTable(CounterID, CounterTypeID, CounterOld, CounterNew, CounterAdd)
	 VALUES(@LastInsertID, @QCOUNTERTYPEID, 0, @QCOUNTERQTY, @QCOUNTERQTY)

	 -- INSERT INTO TRIGGER
	 INSERT INTO @QuestTrigger(AchievementTypeID, QuestTypeID) VALUES (@QACHIEVEMENTTYPEID, @QTYPEID)

	 -- SELECT THE QUEST THAT'S BEEN PROGRESSED
	 DELETE FROM @QuestUpdate WHERE ID = @QID
    END
  END

  SELECT * FROM @CounterTable FOR JSON AUTO, ROOT ('Counters')
  SELECT * FROM @QuestTrigger FOR JSON AUTO, ROOT ('Quests')


   SELECT A.TypeID, A.ID, QuestData.* FROM
   (	 SELECT ID, TypeID
	 FROM DBO.Pangya_Achievement
	 WHERE UID = @UID AND TypeID = @AchievementTypeID
   ) A
   CROSS APPLY (
	 SELECT B.Achivement_Quest_TypeID, C.TypeID, C.ID, C.Quantity, ISNULL(DBO.UNIX_TIMESTAMP(B.SuccessDate), 0) AS SuccessDate
	 FROM DBO.Pangya_Achievement_Quest B
	 INNER JOIN DBO.Pangya_Achievement_Counter C
	 ON C.ID = B.Counter_Index
	 WHERE  B.Achievement_Index = A.ID
   ) QuestData
   FOR JSON AUTO, ROOT ('QuestData')

END
GO
/****** Object:  StoredProcedure [dbo].[ProcAlterDaily]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		by LuisMK
-- Create date: 20/7/2019 13:15
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcAlterDaily] 
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;
	declare @DATE_NOW date = GetDate();
	DECLARE @OPT INT = 0;	
	DECLARE @COUNT_ITEM INT;
	DECLARE @NUMBER1 INT;
	DECLARE @NUMBER2 INT;
	declare @LOGIN int;
	declare @TYPEID int;
	declare @QNTD int;
	declare @COUNTER_VAR int;
	declare @DAY_RAND int = 1 + abs(checksum(newid())) % 20;
	
	SET @COUNT_ITEM = (SELECT COUNT(*) FROM dbo.Pangya_Item_Daily WHERE ItemType = 1);
   
	SET @NUMBER1 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@COUNT_ITEM - 1)) + 1);
    
    IF @NUMBER1 <= 0 OR @NUMBER1 > @COUNT_ITEM 
	begin
		SET @NUMBER2 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@COUNT_ITEM)) + 1);
	END

	IF (SELECT UID FROM dbo.Pangya_Item_Daily_Log WHERE UID = @UID) IS NULL begin

		SET @NUMBER2 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@COUNT_ITEM)) + 1);
        
        IF @NUMBER2 <= 0 OR @NUMBER2 > @COUNT_ITEM 
		begin
			SET @NUMBER2 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@COUNT_ITEM)) + 1);
		END

		INSERT INTO dbo.Pangya_Item_Daily_Log(UID, RegDate, counter, LoginCount, Item_TypeID, Item_Quantity, Item_TypeID_Next, Item_Quantity_Next)

		VALUES(@UID, @DATE_NOW, 1, 1,

					(SELECT ItemTypeID FROM Pangya_Item_Daily WHERE ID = @NUMBER1),

					(SELECT Quantity FROM Pangya_Item_Daily WHERE ID = @NUMBER1),

					(SELECT ItemTypeID FROM Pangya_Item_Daily WHERE ID =  @NUMBER2),

					(SELECT Quantity FROM Pangya_Item_Daily WHERE ID = @NUMBER2));

		

		SET @OPT = 1;
		
	 IF DATEDIFF(DAY, (SELECT regdate FROM dbo.Pangya_Item_Daily_Log WHERE UID = @UID), GETDATE()) IS NULL 

		SET @NUMBER2 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@COUNT_ITEM)) + 1);
        
        IF @NUMBER2 <= 0 OR @NUMBER2 > @COUNT_ITEM 
		begin
			SET @NUMBER2 = FLOOR((RAND(dbo.UNIX_TIMESTAMP(DATEADD(DAY, @DAY_RAND, GETDATE())) + 1 + @UID) * (@DAY_RAND)) + 1);
		END

		UPDATE Pangya_Item_Daily_Log SET regdate = @DATE_NOW,

					 Item_TypeID = (SELECT ItemTypeID FROM Pangya_Item_Daily WHERE ID = @NUMBER1),

					 Item_Quantity = (SELECT Quantity FROM Pangya_Item_Daily WHERE ID = @NUMBER1),

					 Item_TypeID_Next = (SELECT ItemTypeID FROM Pangya_Item_Daily WHERE ID = @NUMBER2),

					 Item_Quantity_Next = (SELECT Quantity FROM Pangya_Item_Daily WHERE ID = @NUMBER2),

					 counter = counter + 1, LoginCount = 1

		WHERE UID = @UID;	

		SET @OPT = 1;
	END


	IF DATEDIFF(DAY, (SELECT regdate FROM Pangya_Item_Daily_Log WHERE UID = @UID), GETDATE()) >= 1 AND @OPT != 1 begin

		SET @COUNTER_VAR = (SELECT counter FROM Pangya_Item_Daily_Log WHERE UID = @UID);
        IF (@COUNTER_VAR + 2) % 10 = 0 
		begin

			UPDATE Pangya_Item_Daily_Log SET regdate = @DATE_NOW, LoginCount = 1,

					 Item_TypeID = Item_TypeID_Next, Item_Quantity = Item_Quantity_Next, counter = counter + 1,

					 Item_TypeID_Next = (SELECT TOP 1 ItemTypeID FROM Pangya_Item_Daily WHERE ItemType = 1),

					 Item_Quantity_Next = (SELECT TOP 1 Quantity FROM Pangya_Item_Daily WHERE ItemType = 1 )

			WHERE UID = @UID;
			END
		ELSE BEGIN

			UPDATE Pangya_Item_Daily_Log SET regdate = @DATE_NOW, LoginCount = 1,

						 Item_TypeID = Item_TypeID_Next, Item_Quantity = Item_Quantity_Next, counter = counter + 1,

						 Item_TypeID_Next = (SELECT ItemTypeID FROM Pangya_Item_Daily WHERE ID = @NUMBER1),

						 Item_Quantity_Next = (SELECT Quantity FROM Pangya_Item_Daily WHERE ID = @NUMBER1)

			WHERE UID = @UID;

		END
		SET @OPT = 1;
	END

	IF @OPT = 1 begin

		SET @TYPEID = (SELECT Item_TypeID FROM dbo.Pangya_Item_Daily_Log WHERE UID = @UID)

		SET @QNTD =   (SELECT Item_Quantity FROM dbo.Pangya_Item_Daily_Log WHERE UID = @UID)

		--# Item Attendance Reward System
		exec dbo.ProcInsertMail @UID, '@GM', 'Voce foi presentiado', @TYPEID, 0, @QNTD, 0;
	END

	
   -- # verifica se o login está 0 por causa do erro achievement q precisa refazer
    IF (SELECT LoginCount FROM Pangya_Item_Daily_Log WHERE UID = @UID) = 0 AND @OPT = 0 begin
		UPDATE Pangya_Item_Daily_Log SET LoginCount = 1 WHERE UID = @UID;
	END
    
	SET @LOGIN = @opt + (SELECT LoginCount FROM Pangya_Item_Daily_Log WHERE UID = @UID);

	SELECT counter, Item_TypeID, Item_Quantity, Item_TypeID_Next, Item_Quantity_Next, @LOGIN as LoginCount, @OPT as CODE

	FROM Pangya_Item_Daily_Log WHERE UID = @UID;

END

GO
/****** Object:  StoredProcedure [dbo].[ProcCheckUsername]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcCheckUsername] 
	@USERNAME VARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT UID, Username, Nickname FROM [dbo].Pangya_Member WHERE Username = LTRIM(RTRIM(@USERNAME))
END
GO
/****** Object:  StoredProcedure [dbo].[ProcCreateAchievement]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcCreateAchievement] 
   @UID INT
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @TypeID INT
   DECLARE @CounterTypeID INT
   DECLARE @AchivementIndex INT
   DECLARE @CounterIndex INT

   IF (OBJECT_ID('TEMPDB..#Achievement') IS NOT NULL)
	 DROP TABLE #Achievement

   CREATE TABLE #Achievement (AchievementTypeID INT, CounterTypeID INT ,QuestTypeID INT, CounterQuantity INT)

   INSERT INTO #Achievement(AchievementTypeID, CounterTypeID, QuestTypeID, CounterQuantity)
   SELECT A.ACHIEVEMENT_TYPEID, B.CounterTypeID, B.TypeID, b.CounterQuantity
   FROM [DBO].Achievement_Data A
   INNER JOIN [DBO].Achievement_QuestStuffs B
   ON B.TypeID = A.ACHIEVEMENT_QUEST_TYPEID

   -- CREATE INDEX
   CREATE CLUSTERED INDEX IDX_READ_ACH ON #Achievement (AchievementTypeID)

   WHILE EXISTS (SELECT 1 FROM #Achievement) BEGIN
		 SELECT TOP 1 @TypeID = AchievementTypeID, @CounterTypeID = CounterTypeID FROM #Achievement ORDER BY AchievementTypeID ASC

		 INSERT INTO [DBO].Pangya_Achievement(UID, TypeID) SELECT @UID, @TypeID
		 SET @AchivementIndex = SCOPE_IDENTITY()

		 -- INSERT INTO COUNTER
		 INSERT INTO [DBO].Pangya_Achievement_Counter(UID, TypeID, Quantity) VALUES (@UID, @CounterTypeID, 0)
		 SET @CounterIndex = SCOPE_IDENTITY()

		 -- INSERT INTO QUEST
		 INSERT INTO [DBO].Pangya_Achievement_Quest(UID, Achievement_Index, Achivement_Quest_TypeID, Counter_Index, Count, SuccessDate) 
		 SELECT @UID, @AchivementIndex, QuestTypeID, @CounterIndex, CounterQuantity, Null
		 FROM #Achievement WHERE AchievementTypeID = @TypeID

		 DELETE FROM #Achievement WHERE AchievementTypeID = @TypeID
   END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcDeleteRentItem]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcDeleteRentItem]
	@UID INT
AS
BEGIN
  SET NOCOUNT ON;
  
  DECLARE @CURRTIME SMALLDATETIME
  SELECT @CURRTIME = GETDATE()
  
  UPDATE [DBO].Pangya_Warehouse 
  SET VALID = 0
  WHERE UID = @UID AND
  		ItemType = 2 AND
        DATEDIFF(DAY, @CURRTIME, DateEnd) < 0 AND
        DATEDIFF(HOUR, @CURRTIME, DateEnd) < 0 AND
        VALID = 1
END
GO
/****** Object:  StoredProcedure [dbo].[ProcDelMail]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcDelMail] 
	@UID INT, @MailIndex int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN

	UPDATE	DBO.Pangya_Mail	SET DeleteDate = GETDATE() where UID = @UID AND Mail_Index = @MailIndex
		
		IF @@ERROR = 0
		BEGIN
			COMMIT TRAN
			SELECT RET = 1
		END ELSE BEGIN
			ROLLBACK TRAN
			SELECT RET = 0
		END
	END TRY
	BEGIN CATCH
	END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[ProcFixPartsCharacter]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



	CREATE PROCEDURE [dbo].[ProcFixPartsCharacter]
	@UID INT,
	@CHARTYPE INT,	
	@Hairs tinyint
	AS
	BEGIN
	SET NOCOUNT ON;
	--Insere as PART_TYPEID padrão do Character que vai ser passa por argumento.
	DECLARE @CHAR_TYPE INT;

	SET @CHAR_TYPE = (@CHARTYPE & 0Xff);
	
	
	IF(@CHAR_TYPE = 0) BEGIN --Character : Nuri	
		
		    INSERT INTO [dbo].Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8000400)
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8004400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8006400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Basic Top
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8008400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x800a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x800e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shin
			UPDATE Pangya_Character SET PART_TYPEID_9 = 0x8010400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shoes
		END
		IF(@CHAR_TYPE = 1) BEGIN
		  INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8040400); --Nome : Basic Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8042400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8044400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Basic Top
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8046400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hands
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8048400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x804c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shins
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x804e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shoes
			end
		IF(@CHAR_TYPE = 2) BEGIN
INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8080400); --Nome : Basic Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8082400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8084400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Basic Top
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8086400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Arm
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8088400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x808a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x808c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Legs
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x808e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shoes
		end
		IF(@CHAR_TYPE = 3) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x80c0400); --Nome : Basic Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x80c2400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x80c4400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Basic Tops
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x80c6400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Arms
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x80c8400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hands
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x80ca400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x80cc400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Legs
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x80ce400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shoes
			end

		IF(@CHAR_TYPE = 4) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8100400); --Nome : Basic Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8102400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8104400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Basic Top
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8106400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8108400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x810a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shoes
			end

		IF(@CHAR_TYPE = 5) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8140400); --Nome : Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8142400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8144400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Top
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8146400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Arm
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8148400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x814a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x814c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Legs
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x814e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Foot
			end

		IF(@CHAR_TYPE = 6) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8180400); --Nome : HAIR
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8182400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : FACE
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8184400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : SHIRTS
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8186400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : HANDS
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8188400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : PANTS
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x818a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : FOOT
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x818c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : LEGS
			end

		IF(@CHAR_TYPE = 7) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x81c0400); --Nome : Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x81c2400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x81c4400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Shirts
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x81c6400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Arm
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x81c8400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x81ca400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Pants
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x81cc400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Leg
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x81ce400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Foot
			UPDATE Pangya_Character SET PART_TYPEID_9 = 0x81d0400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Wrist
			end

		IF(@CHAR_TYPE = 8) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8200400); --Nome : Head
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8202400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Face
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8204400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Top
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8206400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Arm
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8208400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Hand
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x820a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Bottom
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x820c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Leg
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x820e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : Foot
			end

		IF(@CHAR_TYPE = 9) BEGIN
			INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8240400); --Nome : ¸Ó¸®
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8242400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¾ó±¼
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8244400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : »óÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8246400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÆÈ
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8248400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¼Õ
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x824a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÇÏÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x824c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ´Ù¸®
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x824e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¹ß
			end

		IF(@CHAR_TYPE = 10) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8280400); --Nome : ¸Ó¸®
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8282400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¾ó±¼
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8284400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : »óÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8286400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÆÈ
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8288400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¼Õ
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x828a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÇÏÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x828c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ´Ù¸®
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x828e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¹ß
			end

			IF(@CHAR_TYPE = 11) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x82c0400); --Nome : ¸Ó¸®
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x82c2400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¾ó±¼
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x82c4400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : »óÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x82c6400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÆÈ
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x82c8400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¼Õ
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x82ca400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÇÏÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x82cc400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ´Ù¸®
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x82ce400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¹ß
			end;

			IF(@CHAR_TYPE = 12) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8300400); --Nome : ¸Ó¸®
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8302400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¾ó±¼
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8304400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : »óÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8306400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÆÈ
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8308400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¼Õ
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x830a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÇÏÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x830c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ´Ù¸®
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x830e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¹ß
			end

			IF(@CHAR_TYPE = 14) BEGIN
		INSERT INTO Pangya_Character(UID,TYPEID,HAIR_COLOR,GIFT_FLAG, AuxPart, AuxPart2, PART_TYPEID_1) VALUES(@UID, @CHARTYPE, @Hairs, 1,0,0, 0x8380400); --Nome : ±âº»¸Ó¸®
			UPDATE Pangya_Character SET PART_TYPEID_2 = 0x8382400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¾ó±¼
			UPDATE Pangya_Character SET PART_TYPEID_3 = 0x8384400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ±âº»»óÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_4 = 0x8386400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÆÈ
			UPDATE Pangya_Character SET PART_TYPEID_5 = 0x8388400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ¼Õ
			UPDATE Pangya_Character SET PART_TYPEID_6 = 0x838a400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ÇÏÀÇ
			UPDATE Pangya_Character SET PART_TYPEID_7 = 0x838c400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ´Ù¸®
			UPDATE Pangya_Character SET PART_TYPEID_8 = 0x838e400 WHERE UID = @UID AND TYPEID = @CHARTYPE; --Nome : ½Å¹ß
			end;
	END
GO
/****** Object:  StoredProcedure [dbo].[ProcGet_UserInfo]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGet_UserInfo]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	/* BASIC USER INFO */
	SELECT
		A.UID,
		A.Username,
		A.Nickname,
		A.Sex,
		A.GUILDINDEX,
		B.GUILD_NAME,
		ISNULL( B.GUILD_IMAGE, 'guildmark' ) AS GUILD_IMAGE 
	FROM
		DBO.Pangya_Member A
	LEFT JOIN [DBO].Pangya_Guild_Info AS B ON B.GUILD_INDEX = A.GUILDINDEX 
	WHERE
		UID = @UID
	
	/* CHARACTER INFO */
	SELECT
		B.TYPEID,
		B.CID,
		B.GIFT_FLAG,
		B.HAIR_COLOR,
		B.POWER,
		B.CONTROL,
		B.IMPACT,
		B.SPIN,
		B.CURVE,
		B.CUTIN,
		B.PART_TYPEID_1,
		B.PART_TYPEID_2,
		B.PART_TYPEID_3,
		B.PART_TYPEID_4,
		B.PART_TYPEID_5,
		B.PART_TYPEID_6,
		B.PART_TYPEID_7,
		B.PART_TYPEID_8,
		B.PART_TYPEID_9,
		B.PART_TYPEID_10,
		B.PART_TYPEID_11,
		B.PART_TYPEID_12,
		B.PART_TYPEID_13,
		B.PART_TYPEID_14,
		B.PART_TYPEID_15,
		B.PART_TYPEID_16,
		B.PART_TYPEID_17,
		B.PART_TYPEID_18,
		B.PART_TYPEID_19,
		B.PART_TYPEID_20,
		B.PART_TYPEID_21,
		B.PART_TYPEID_22,
		B.PART_TYPEID_23,
		B.PART_TYPEID_24,
		B.PART_IDX_1,
		B.PART_IDX_2,
		B.PART_IDX_3,
		B.PART_IDX_4,
		B.PART_IDX_5,
		B.PART_IDX_6,
		B.PART_IDX_7,
		B.PART_IDX_8,
		B.PART_IDX_9,
		B.PART_IDX_10,
		B.PART_IDX_11,
		B.PART_IDX_12,
		B.PART_IDX_13,
		B.PART_IDX_14,
		B.PART_IDX_15,
		B.PART_IDX_16,
		B.PART_IDX_17,
		B.PART_IDX_18,
		B.PART_IDX_19,
		B.PART_IDX_20,
		B.PART_IDX_21,
		B.PART_IDX_22,
		B.PART_IDX_23,
		B.PART_IDX_24 
	FROM
		DBO.Pangya_User_Equip A
		INNER JOIN DBO.Pangya_Character B ON B.CID = A.CHARACTER_ID
	WHERE
		A.UID = @UID
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetAchievement]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetAchievement] 
	@UID INT
AS
BEGIN
   SET NOCOUNT ON;
	 
	 -- 1. GET TABLE Pangya_Achievement
	 SELECT ID, TypeID, Type
	 FROM DBO.Pangya_Achievement
	 WHERE UID = @UID AND Valid = 1
	 
	 -- 2. GET TABLE Pangya_Achievement_Counter
	 SELECT ID, TypeID, Quantity
	 FROM DBO.Pangya_Achievement_Counter
	 WHERE UID = @UID
	 
	 -- 3. GET TABLE Pangya_Achievement_Quest
	 SELECT ID, Achievement_Index, Achivement_Quest_TypeID, Counter_Index, ISNULL(DBO.UNIX_TIMESTAMP(SuccessDate), 0) AS SuccessDate, Count
	 FROM DBO.Pangya_Achievement_Quest
	 WHERE UID = @UID
	 ORDER BY Count ASC

   /*SELECT 
	 B.*
   FROM
   (
   SELECT A.TypeID AS AchTypeID,
	     A.ID AS AchID,
		QuestData.Achivement_Quest_TypeID,
		CounterData.TypeID AS CounterTypeID,
		CounterData.ID AS CounterID,
		CounterData.Quantity,
		ISNULL(DBO.UNIX_TIMESTAMP(QuestData.SuccessDate), 0) AS SuccessDate,
		QuestData.Count   
   FROM [DBO].Pangya_Achievement A
   INNER JOIN [DBO].Pangya_Achievement_Quest QuestData
   ON QuestData.Achievement_Index = A.ID
   INNER JOIN [DBO].Pangya_Achievement_Counter CounterData
   ON CounterData.ID = QuestData.Counter_Index
   WHERE A.UID = @UID
   ) B
   ORDER BY B.AchTypeID ASC, B.Count ASC --, QuestData.Count ASC FOR JSON AUTO , ROOT ('Achievement')*/
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetCaddies]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetCaddies] 
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].Pangya_Caddie SET SKIN_TYPEID = 0 , SKIN_END_DATE = NULL WHERE UID = @UID AND SKIN_END_DATE < GETDATE()

	SELECT CID
		,TYPEID
		,EXP
		,SKIN_TYPEID
		,TriggerPay
		,cLevel
		,DATEDIFF(HOUR, GETDATE(), SKIN_END_DATE) AS SKIN_HOUR_LEFT
		,DATEDIFF(DAY, GETDATE(), END_DATE) AS DAY_LEFT
		,END_DATE
		,RentFlag
		,SKIN_END_DATE
	FROM [dbo].Pangya_Caddie
	WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetCard]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetCard] @UID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT CARD_IDX
		,CARD_TYPEID
		,QTY
		,VALID
	FROM [dbo].Pangya_Card
	WHERE UID = @UID
		AND VALID = 1
		AND QTY > 0
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetCardEquip]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetCardEquip]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE	DBO.Pangya_Card_Equip
	SET			VALID = 0
	WHERE		UID = @UID
	AND			FLAG = 1
	AND			GETDATE() > ENDDATE
	
	SELECT	ID
					, CID
					, CHAR_TYPEID
					, CARD_TYPEID
					, SLOT
					, FLAG
					, REGDATE
					, ENDDATE
	FROM		Pangya_Card_Equip
	WHERE		UID = @UID
	AND			VALID = 1
	-- OR			GETDATE() BETWEEN REGDATE AND ENDDATE
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetCharacter]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetCharacter] 
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [CID]
      ,[UID]
      ,[TYPEID]
      ,[GIFT_FLAG]
      ,[HAIR_COLOR]
      ,[POWER]
      ,[CONTROL]
      ,[IMPACT]
      ,[SPIN]
      ,[CURVE]
      ,[CUTIN]
      ,[AuxPart]
      ,[AuxPart2]
      ,[PART_TYPEID_1]
      ,[PART_TYPEID_2]
      ,[PART_TYPEID_3]
      ,[PART_TYPEID_4]
      ,[PART_TYPEID_5]
      ,[PART_TYPEID_6]
      ,[PART_TYPEID_7]
      ,[PART_TYPEID_8]
      ,[PART_TYPEID_9]
      ,[PART_TYPEID_10]
      ,[PART_TYPEID_11]
      ,[PART_TYPEID_12]
      ,[PART_TYPEID_13]
      ,[PART_TYPEID_14]
      ,[PART_TYPEID_15]
      ,[PART_TYPEID_16]
      ,[PART_TYPEID_17]
      ,[PART_TYPEID_18]
      ,[PART_TYPEID_19]
      ,[PART_TYPEID_20]
      ,[PART_TYPEID_21]
      ,[PART_TYPEID_22]
      ,[PART_TYPEID_23]
      ,[PART_TYPEID_24]
      ,[PART_IDX_1]
      ,[PART_IDX_2]
      ,[PART_IDX_3]
      ,[PART_IDX_4]
      ,[PART_IDX_5]
      ,[PART_IDX_6]
      ,[PART_IDX_7]
      ,[PART_IDX_8]
      ,[PART_IDX_9]
      ,[PART_IDX_10]
      ,[PART_IDX_11]
      ,[PART_IDX_12]
      ,[PART_IDX_13]
      ,[PART_IDX_14]
      ,[PART_IDX_15]
      ,[PART_IDX_16]
      ,[PART_IDX_17]
      ,[PART_IDX_18]
      ,[PART_IDX_19]
      ,[PART_IDX_20]
      ,[PART_IDX_21]
      ,[PART_IDX_22]
      ,[PART_IDX_23]
      ,[PART_IDX_24]
  FROM [dbo].[Pangya_Character]	WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetGameServer]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ProcGetGameServer]

AS
BEGIN
	SET NOCOUNT ON;

    SELECT	ServerID,
			Name,
			IP,
			Port,
			ImgNo,
			ImgEvent, Property, MaxUser,UsersOnline, BlockFunc
	FROM
		[dbo].Pangya_Server
	WHERE
		ServerType = 1 AND Active = 1;
END


GO
/****** Object:  StoredProcedure [dbo].[ProcGetItemWarehouse]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetItemWarehouse] @UID INT
AS
BEGIN
	SET NOCOUNT ON;

	/*
		0x20 = PERIOD
		0x21 = SKIN PERIOD
		0x60 = RENT PART IS ACTIVATE
		0x62 = RENT PART END
	*/

	-- UPDATE TEMPORARY ITEM
	-- PERIOD ITEM
	UPDATE DBO.Pangya_Warehouse
	SET VALID = 0
	WHERE UID = @UID AND 
				VALID = 1 AND 
				Flag IN (0x20, 0x21) AND 
				DATEDIFF(MINUTE, GETDATE(), DateEnd) < 0

	-- RENTAL ITEM
	UPDATE DBO.Pangya_Warehouse
	SET Flag = 0x62
	WHERE UID = @UID AND 
				VALID = 1 AND 
				Flag = 0x60 AND 
				DATEDIFF(MINUTE, GETDATE(), DateEnd) < 0

	SELECT A.*
		,B.UCC_UNIQE
		,B.UCC_STATUS
		,B.UCC_COPY_COUNT
		,B.UCC_NAME
		,B.Nickname AS UCC_DRAWER
		,B.UCC_DRAWER AS UCC_DRAWER_UID
		,Q.CLUB_POINT
		,Q.CLUB_WORK_COUNT
		,Q.C0_SLOT
		,Q.C1_SLOT
		,Q.C2_SLOT
		,Q.C3_SLOT
		,Q.C4_SLOT
		,Q.CLUB_SLOT_CANCEL
		,Q.CLUB_POINT_TOTAL_LOG
		,Q.CLUB_UPGRADE_PANG_LOG 
	FROM (
		SELECT item_id AS IDX
			,TYPEID
			,C0
			,C1
			,C2
			,C3
			,C4
			,Flag
			,CASE 
				WHEN (Flag in (0x60, 0x20, 0x21) )
					THEN DATEDIFF(HOUR, GETDATE(), DateEnd)
				ELSE NULL
				END AS HOURLEFT
			,CASE 
				WHEN (Flag in (0x60, 0x20, 0x21) )
					THEN RegDate
				ELSE NULL
				END AS RegDate
			,CASE 
				WHEN (Flag in (0x60, 0x20, 0x21) )
					THEN DateEnd
				ELSE NULL
				END AS DateEnd
		FROM [dbo].Pangya_Warehouse
		WHERE UID = @UID
			AND VALID = 1
		) A
	OUTER APPLY (
		SELECT M.UID
			,S.UCC_UNIQE
			,S.UCC_STATUS
			,S.UCC_COPY_COUNT
			,S.UCC_NAME
			,S.UCC_DRAWER
			,M.Nickname
		FROM DBO.Pangya_SelfDesign S
		LEFT JOIN DBO.Pangya_Member M ON M.UID = S.UCC_DRAWER
		WHERE S.ITEM_ID = A.IDX
		) B
	OUTER APPLY (
		SELECT C0_SLOT,
			C1_SLOT,
			C2_SLOT,
			C3_SLOT,
			C4_SLOT,
			CLUB_POINT,
			CLUB_WORK_COUNT,
			CLUB_SLOT_CANCEL,
			CLUB_POINT_TOTAL_LOG,
			CLUB_UPGRADE_PANG_LOG 
		FROM [DBO].Pangya_Club_Info WHERE ITEM_ID = A.IDX 
	) Q
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetLockerItem]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetLockerItem]
	@UID 				INT,
	@PAGE 			INT = 1,
	@PAGE_TOTAL	INT = 20
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @TOTAL_PAGE DECIMAL;
	IF OBJECT_ID('TEMPDB..#TMP_INVEN') IS NOT NULL
		DROP TABLE #TMP_INVEN
		
	SELECT	A.INVEN_ID
					, TypeID
					, FROM_ID
	INTO	#TMP_INVEN
	FROM (
		SELECT * FROM DBO.Pangya_Locker_Item WHERE UID = @UID AND Valid = 1
	) A

	SET @TOTAL_PAGE = CEILING(@@ROWCOUNT/(@PAGE_TOTAL * 1.0))
	
	SELECT	@TOTAL_PAGE AS TOTAL_PAGE, A.INVEN_ID
					, A.TypeID
					, B.UCC_UNIQE
					, ISNULL(B.UCC_STATUS, 0) AS UCC_STATUS
					, B.UCC_NAME
					, B.UCC_COPY_COUNT
					, B.NICKNAME
	FROM (
			SELECT * FROM #TMP_INVEN
			ORDER BY INVEN_ID DESC OFFSET (@PAGE - 1) * @PAGE_TOTAL ROW FETCH NEXT @PAGE_TOTAL ROWS ONLY
	) A
	OUTER APPLY (
		SELECT	UCC_UNIQE
						, UCC_STATUS
						, UCC_NAME
						, UCC_COPY_COUNT
						, Y.NICKNAME
		FROM		DBO.Pangya_SelfDesign X
		LEFT JOIN DBO.Pangya_Member Y
		ON Y.UID = X.UCC_DRAWER
		
		WHERE		ITEM_ID = A.FROM_ID
	) B
	
END


GO
/****** Object:  StoredProcedure [dbo].[ProcGetMacro]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetMacro]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Macro1,
			Macro2,
			Macro3,
			Macro4,
			Macro5,
			Macro6,
			Macro7,
			Macro8,
			Macro9
	FROM 
			[dbo].Pangya_Game_Macro
	WHERE
			UID = @UID
		
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetMail]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetMail] 
	@UID INT,
	@PAGE INT,
	@TOTAL INT,
	@READ TINYINT -- 1 ONLY NOT READ , 2 ALL
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @PAGE_TOTAL INT = 0

   SET @PAGE_TOTAL = CEILING((SELECT COUNT(1)
	 FROM [dbo].Pangya_Mail
	 WHERE UID = @UID
	 AND DeleteDate IS NULL)* 1.0 / @TOTAL)
   
	  
   SELECT @PAGE_TOTAL AS PAGE_TOTAL, A.*,
		  B.*, 
		  (CASE WHEN [DBO].UDF_PARTS_GROUP(B.TYPEID) = 9 THEN 1 ELSE C.QTY END) AS Mail_Item_Count
   FROM
   (
	 SELECT 
		  Mail_Index, 
		  Sender,
		  IsRead = CASE WHEN ReadDate IS NULL THEN 0 ELSE 1 END
	 FROM DBO.Pangya_Mail
	 WHERE UID = @UID AND
		  DeleteDate IS NULL
	 ORDER BY Mail_Index DESC OFFSET (@PAGE - 1) * @TOTAL ROW FETCH NEXT @TOTAL ROWS ONLY
   ) A
   OUTER APPLY(
	 SELECT TOP 1
		  (CASE WHEN SETTYPEID > 0 THEN SETTYPEID ELSE TYPEID END) AS TYPEID,
		  (CASE WHEN [DBO].UDF_PARTS_GROUP(SETTYPEID) = 9 THEN 1 ELSE QTY END) AS QTY,
		  (CASE WHEN DAY > 0 THEN 1 ELSE 0 END) AS IsTimer,
		  DAY,
		  UCC_UNIQUE, SETTYPEID
	 FROM DBO.Pangya_Mail_Item
	 WHERE Mail_Index = A.Mail_Index AND
		  RELEASE_DATE IS NULL
   ) B
   OUTER APPLY(
	 SELECT QTY = COUNT(*)
	 FROM DBO.Pangya_Mail_Item
	 WHERE Mail_Index = A.Mail_Index AND
		  RELEASE_DATE IS NULL
   ) C
   WHERE A.IsRead BETWEEN 0 AND (CASE WHEN @READ = 1 THEN 0 ELSE 1 END)

END


GO
/****** Object:  StoredProcedure [dbo].[ProcGetMapStatistics]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetMapStatistics]
	@UID INT,
	@MAP INT,
	@ASSIST INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT *
	FROM DBO.Pangya_Map_Statistics
	WHERE UID = @UID AND Map = @MAP AND Assist = @ASSIST
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetMascot]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetMascot] 
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT MID
		,MASCOT_TYPEID
		,MESSAGE
		,DateEnd
		,DATEDIFF(DAY, GETDATE(), DateEnd) AS END_DATE_INT
	FROM [dbo].Pangya_Mascot
	WHERE UID = @UID AND DateEnd > GETDATE()
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetMatchHistory]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetMatchHistory]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @UID_R1 INT
	DECLARE @UID_R2 INT
	DECLARE @UID_R3 INT
	DECLARE @UID_R4 INT
	DECLARE @UID_R5 INT
	
	DECLARE @MATCHHISTORY TABLE (
		UID INT,
		SEX INT,
		NICKNAME VARCHAR(20),
		USERID VARCHAR(20)
	)
	
	SELECT	@UID_R1 = UID1,
					@UID_R2 = UID2,
					@UID_R3 = UID3,
					@UID_R4 = UID4,
					@UID_R5	= UID5
	FROM	DBO.Pangya_User_MatchHistory
	WHERE	UID = @UID
	
	IF (@UID_R1 > 0) BEGIN
		INSERT INTO @MATCHHISTORY(UID, SEX, NICKNAME, USERID)
		SELECT UID, SEX, NICKNAME, USERNAME FROM DBO.Pangya_Member WHERE UID = @UID_R1
	END
	
	IF (@UID_R2 > 0) BEGIN
		INSERT INTO @MATCHHISTORY(UID, SEX, NICKNAME, USERID)
		SELECT UID, SEX, NICKNAME, USERNAME FROM DBO.Pangya_Member WHERE UID = @UID_R2
	END
	
	IF (@UID_R3 > 0) BEGIN
		INSERT INTO @MATCHHISTORY(UID, SEX, NICKNAME, USERID)
		SELECT UID, SEX, NICKNAME, USERNAME FROM DBO.Pangya_Member WHERE UID = @UID_R3
	END
	
	IF (@UID_R4 > 0) BEGIN
		INSERT INTO @MATCHHISTORY(UID, SEX, NICKNAME, USERID)
		SELECT UID, SEX, NICKNAME, USERNAME FROM DBO.Pangya_Member WHERE UID = @UID_R4
	END
	
	IF (@UID_R5 > 0) BEGIN
		INSERT INTO @MATCHHISTORY(UID, SEX, NICKNAME, USERID)
		SELECT UID,	SEX, NICKNAME, USERNAME FROM DBO.Pangya_Member WHERE UID = @UID_R5
	END
	
	SELECT * FROM @MATCHHISTORY
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetMessengerServer]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetMessengerServer] 

AS
BEGIN
   SET NOCOUNT ON;

    SELECT	ServerID,
			Name,
			IP,
			Port,
			ImgNo,
			ImgEvent, Property, MaxUser,UsersOnline, BlockFunc
   FROM
	 [dbo].Pangya_Server
   WHERE
	 ServerType = 2 AND Active = 1;
END


GO
/****** Object:  StoredProcedure [dbo].[ProcGetRoomData]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetRoomData]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT IDX, TYPEID, POS_X, POS_Y, POS_Z, POS_R
	FROM DBO.TD_ROOM_DATA
	WHERE UID = @UID AND VALID = 1
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetStatistic]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetStatistic]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		Drive
		, Putt
		, Playtime
		, Longest
		, Distance
		, Pangya
		, Hole
		, TeamHole
		, Holeinone
		, OB
		, Bunker
		, Fairway
		, Albatross
		, Holein
		, Pang
		, Timeout
		, Game_Level
		, Game_Point
		, PuttIn
		, LongestPuttin
		, LongestChipIn
		, NoMannerGameCount
		, ShotTime
		, GameCount
		, DisconnectGames
		, wTeamWin
		, wTeamGames
		, LadderPoint
		, LadderWin
		, LadderLose
		, LadderDraw
		, ComboCount
		, MaxComboCount
		, TotalScore
		, BestScore0
		, BestScore1
		, BestScore2
		, BestScore3
		, BESTSCORE4
		, MaxPang0
		, MaxPang1
		, MaxPang2
		, MaxPang3
		, MaxPang4
		, SumPang
		, LadderHole
		, GameCountSeason
		, SkinsPang
		, SkinsWin
		, SkinsLose
		, SkinsRunHoles
		, SkinsStrikePoint
		, SkinsAllinCount
		, EventValue
		, EventFlag
	FROM DBO.Pangya_User_Statistics
	WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetToolbar]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetToolbar] @UID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT CADDIE
		,CHARACTER_ID
		,CLUB_ID
		,BALL_ID
		,MASCOT_ID		
		,ITEM_SLOT_1
		,ITEM_SLOT_2
		,ITEM_SLOT_3
		,ITEM_SLOT_4
		,ITEM_SLOT_5
		,ITEM_SLOT_6
		,ITEM_SLOT_7
		,ITEM_SLOT_8
		,ITEM_SLOT_9
		,ITEM_SLOT_10	
		,Skin_1
		,Skin_2
		,Skin_3
		,Skin_4
		,Skin_5
		,Skin_6
		,POSTER_1
		,POSTER_2
	FROM [dbo].Pangya_User_Equip
	WHERE UID = @UID
END

GO
/****** Object:  StoredProcedure [dbo].[ProcGetTutorial]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcGetTutorial] 
	@UID INT    
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT Rookie,Beginner,Advancer FROM Pangya_Tutorial WHERE UID = @UID;
END

GO
/****** Object:  StoredProcedure [dbo].[ProcGetUCCData]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGetUCCData] @UCC_INDEX INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT B.TYPEID
		,B.item_id
		,A.UCC_UNIQE
		,A.UCC_NAME
		,A.UCC_STATUS
		,A.UCC_COPY_COUNT
		,C.Nickname
	FROM DBO.Pangya_SelfDesign A
	INNER JOIN DBO.Pangya_Warehouse B ON B.item_id = A.ITEM_ID
	OUTER APPLY (
		SELECT Nickname
		FROM DBO.Pangya_Member
		WHERE UID = A.UCC_DRAWER
		) C
	WHERE A.ITEM_ID = @UCC_INDEX
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGetUserMessage]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ProcGetUserMessage] 

@IDUSER INT
AS
BEGIN
   SET NOCOUNT ON;
	SELECT 	a.ID_MSG,
			a.uid_from as uid,
			b.Nickname,
            a.Message,
            a.reg_date
	FROM Pangya_User_Message a, Pangya_Member b
    WHERE a.uid_from = b.uid AND a.uid = @IDUSER AND valid = 1;
    
    UPDATE Pangya_User_Message SET valid = 0 WHERE UID = @IDUSER;
	end
GO
/****** Object:  StoredProcedure [dbo].[ProcGuildGetData]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGuildGetData] 
   @GUILDID INT
   ,@PAGE INT = 1
   ,@TOTAL INT = 15
AS
BEGIN
   SET NOCOUNT ON;

   SELECT COUNT(*) AS GUILD_TOTAL_MEMBER
   FROM [dbo].Pangya_Guild_Member
   WHERE GUILD_ID = @GUILDID

   SELECT B.*
       ,C.GUILD_NAME
       ,D.Nickname AS PLAYER_NICKNAME
       ,D.Logon
   FROM (
       SELECT GUILD_ID
           ,GUILD_MEMBER_UID
           ,GUILD_POSITION
           ,GUILD_MESSAGE
       FROM [dbo].Pangya_Guild_Member
       WHERE GUILD_ID = @GUILDID
       ORDER BY GUILD_ENTERED_TIME DESC OFFSET(@PAGE - 1) * @TOTAL ROWS FETCH NEXT @TOTAL ROWS ONLY
       ) B
   INNER JOIN [dbo].Pangya_Guild_Info C ON C.GUILD_INDEX = B.GUILD_ID AND C.GUILD_VALID = 1
   INNER JOIN [dbo].Pangya_Member D ON D.UID = B.GUILD_MEMBER_UID
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGuildGetList]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGuildGetList] 
   @PAGE INT = 1
   ,@TOTAL INT = 15
   ,@SEARCH VARCHAR(255) = '%'
AS
BEGIN
   SET NOCOUNT ON;
   DECLARE @TOTAL_GUILD INT;
   
   SET @TOTAL_GUILD = (SELECT COUNT(*) FROM [dbo].Pangya_Guild_Info WHERE GUILD_NAME LIKE '%' + @SEARCH + '%' AND GUILD_VALID = 1)

   SELECT @TOTAL_GUILD AS GUILD_TOTAL, A.*
       ,B.*
       ,C.*
	  ,ISNULL(D.GUILD_MARK_IMG, 'GUILDMARK') AS GUILD_IMAGE
   FROM (
       SELECT GUILD_INDEX
           ,GUILD_NAME
           ,GUILD_INTRODUCING
           ,GUILD_LEADER_UID
           ,GUILD_POINT
           ,GUILD_PANG
           ,GUILD_CREATE_DATE
       FROM [dbo].Pangya_Guild_Info
       WHERE GUILD_NAME LIKE '%' + @SEARCH + '%' 
            AND GUILD_VALID = 1
       ORDER BY GUILD_INDEX DESC OFFSET(@PAGE - 1) * @TOTAL ROWS FETCH NEXT @TOTAL ROWS ONLY
       ) A
   OUTER APPLY (
       SELECT NICKNAME AS GUILD_LEADER_NICKNAME
       FROM [dbo].Pangya_Member
       WHERE A.GUILD_LEADER_UID = UID
       ) B
   OUTER APPLY (
       SELECT COUNT(*) AS GUILD_TOTAL_MEMBER
       FROM [dbo].Pangya_Guild_Member
       WHERE A.GUILD_INDEX = GUILD_ID
       ) C
   OUTER APPLY (
	  SELECT GUILD_MARK_IMG
	  FROM [dbo].Pangya_Guild_Emblem
	  WHERE GUILD_ID = A.GUILD_INDEX
	  ) D
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGuildGetLog]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGuildGetLog] 
    @UID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM [dbo].Pangya_Guild_Log WHERE UID = @UID
    ORDER BY GUILD_ACTION_DATE 
    DESC OFFSET 0 ROWS 
    FETCH NEXT 5 ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGuildGetPlayerData]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGuildGetPlayerData] 
   @UID INT
   ,@GUILDID INT = 0
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @GUID INT

   IF (@GUILDID <= 0)
   BEGIN
	  SELECT @GUID = GUILD_ID
	  FROM [DBO].Pangya_Guild_Member
	  WHERE GUILD_MEMBER_UID = @UID
   END ELSE
   BEGIN
       SELECT @GUID = @GUILDID
   END

   SELECT 
       ISNULL(B.GUILD_INDEX, 0) AS GUILD_INDEX 
       ,B.GUILD_NAME
       ,B.GUILD_INTRODUCING
       ,B.GUILD_NOTICE
       ,ISNULL(B.GUILD_LEADER_UID, 0) AS GUILD_LEADER_UID
       ,B.GUILD_CREATE_DATE
       ,ISNULL(C.GUILD_TOTAL_MEMBER, 0) AS GUILD_TOTAL_MEMBER
       ,D.GUILD_LEADER_NICKNAME
       ,ISNULL(E.GUILD_POSITION, 0) AS GUILD_POSITION
	  ,ISNULL(F.GUILD_MARK_IMG, 'GUILDMARK') AS GUILD_IMAGE
   FROM (
       SELECT GUILD_ID = @GUID
       ) A
   OUTER APPLY (
       SELECT GUILD_INDEX
           ,GUILD_NAME
           ,GUILD_INTRODUCING
           ,GUILD_NOTICE
           ,GUILD_LEADER_UID
           ,GUILD_CREATE_DATE
       FROM [dbo].Pangya_Guild_Info
       WHERE GUILD_INDEX = A.GUILD_ID
       AND GUILD_VALID = 1
       ) B
   OUTER APPLY (
       SELECT COUNT(*) AS GUILD_TOTAL_MEMBER
       FROM [dbo].Pangya_Guild_Member
       WHERE GUILD_ID = A.GUILD_ID
       ) C
   OUTER APPLY (
       SELECT Nickname AS GUILD_LEADER_NICKNAME
       FROM [dbo].Pangya_Member
       WHERE UID = B.GUILD_LEADER_UID
       ) D
   OUTER APPLY (
       SELECT GUILD_POSITION
       FROM [dbo].Pangya_Guild_Member
       WHERE GUILD_ID = A.GUILD_ID
           AND GUILD_MEMBER_UID = @UID
       ) E
   OUTER APPLY (
	  SELECT GUILD_MARK_IMG
	  FROM [dbo].Pangya_Guild_Emblem
	  WHERE GUILD_ID = A.GUILD_ID
	  ) F
END
GO
/****** Object:  StoredProcedure [dbo].[ProcGuildNameAvailable]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcGuildNameAvailable]
   @GUILDNAME VARCHAR(32)
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @FGUILD_NAME VARCHAR(32)
   SELECT @FGUILD_NAME = LTRIM(RTRIM(@GUILDNAME))
   
   IF EXISTS (	SELECT 1 
			FROM [dbo].Pangya_Guild_Info 
			WHERE GUILD_NAME = @FGUILD_NAME AND GUILD_VALID = 1
		   ) BEGIN
       SELECT CODE = 1
   END ELSE BEGIN
       SELECT CODE = 0
   END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcInsertDailyQuest]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcInsertDailyQuest]
	@IN_UID INT,
	@QUESTID INT,
	@DAILYQUEST INT
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [DBO].Pangya_Achievement_Quest(UID, Achievement_Index, Achivement_Quest_TypeID, Counter_Index, SuccessDate, Count)
	SELECT @IN_UID, @QUESTID, A.QuestTypeID, 0, Null, B.CounterQuantity
	FROM [DBO].Achievement_QuestItem A
	INNER JOIN [DBO].Achievement_QuestStuffs B
	ON B.TypeID = A.QuestTypeID
	WHERE A.TypeID = @DAILYQUEST
			
END
GO
/****** Object:  StoredProcedure [dbo].[ProcInsertMail]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcInsertMail] 
	@UID INT,
	@SENDER_MAIL VARCHAR(MAX),
	@SENDER_MSG_MAIL VARCHAR(MAX),
	@ITEM_TYPEID INT,
	@ITEM_SETTYPEID INT,
	@ITEM_QUANTITY INT,
	@ITEM_DAY INT	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @MAIL_INDEX INT = 0;
	DECLARE @GROUP INT = 0;
	DECLARE @UCC_IDX INT = 0;

	IF EXISTS (SELECT 1 FROM DBO.Pangya_Member WHERE UID = @UID)
	BEGIN
	-- INSERT TO MAIL
				  IF @MAIL_INDEX = 0
				  BEGIN 
				  	INSERT INTO [DBO].Pangya_Mail(UID, Sender, Msg) VALUES(@UID, @SENDER_MAIL, @SENDER_MSG_MAIL)
		SET @MAIL_INDEX = SCOPE_IDENTITY()
				  END
	IF @MAIL_INDEX > 0
	    begin
		INSERT INTO DBO.Pangya_Mail_Item(Mail_Index, TYPEID, SETTYPEID, QTY, ITEM_GRP, DAY) Values(@MAIL_INDEX, 
			ISNULL(@ITEM_TYPEID, 0), 
			ISNULL(@ITEM_SETTYPEID, 0), 
			ISNULL(@ITEM_QUANTITY, 0), 
			ISNULL(dbo.UDF_PARTS_GROUP(@ITEM_TYPEID), 0), 			
		    ISNULL(@ITEM_DAY, 0))	
	END
		
		
	END
END




GO
/****** Object:  StoredProcedure [dbo].[ProcLogMemorial]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcLogMemorial]
  @UID AS int ,
  @ItemName AS varchar(255) ,
  @Quantity AS int 
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO DBO.Pangya_Memorial_Log(UID, ItemName, Quantity) VALUES (@UID, @ItemName, @Quantity)
END
GO
/****** Object:  StoredProcedure [dbo].[ProcMailInsert]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcMailInsert] 
	@UID INT,
	@SENDER_MAIL VARCHAR(MAX),
	@SENDER_MSG_MAIL VARCHAR(MAX),
	@JSONData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @MAIL_INDEX INT = 0
		
	INSERT INTO dbo.Pangya_String(str) VALUES(@JSONData)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @TYPEID VARCHAR(20)
	DECLARE @SETTYPEID VARCHAR(20)
	DECLARE @QUANTITY VARCHAR(20)
	DECLARE @DAY VARCHAR(20)
	DECLARE	@ITEMGROUP VARCHAR(20)
	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@JSONData, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) 
	BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s  ^%s ^%s ', @TYPEID OUTPUT,  @SETTYPEID OUTPUT, @QUANTITY OUTPUT, @DAY OUTPUT, @ITEMGROUP OUTPUT 
			      
				    -- INSERT TO MAIL
				  IF @MAIL_INDEX = 0
				  begin 
				  INSERT INTO [DBO].Pangya_Mail(UID, Sender, Msg) VALUES(@UID, @SENDER_MAIL,@SENDER_MSG_MAIL)
				  SET @MAIL_INDEX = SCOPE_IDENTITY()
				 INSERT INTO DBO.Pangya_Mail_Item(Mail_Index, TYPEID, SETTYPEID, QTY, DAY, ITEM_GRP, TO_UID)VALUES(@MAIL_INDEX, @TYPEID, @SETTYPEID, @QUANTITY, CASE WHEN DBO.UDF_PARTS_GROUP(ISNULL(@TYPEID, 0)) = 16 AND ISNULL(@QUANTITY, 0) > 1 THEN ISNULL(@QUANTITY, 0) ELSE 0 END,@ITEMGROUP,@UID)	

				  end
		
		IF @MAIL_INDEX > 0
	    begin
		INSERT INTO DBO.Pangya_Mail_Item(Mail_Index, TYPEID, SETTYPEID, QTY, DAY, ITEM_GRP, TO_UID)VALUES(@MAIL_INDEX, @TYPEID, @SETTYPEID, @QUANTITY, CASE WHEN DBO.UDF_PARTS_GROUP(ISNULL(@TYPEID, 0)) = 16 AND ISNULL(@QUANTITY, 0) > 1 THEN ISNULL(@QUANTITY, 0) ELSE 0 END,@ITEMGROUP,@UID)	
		end	
		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[ProcMailItem]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcMailItem] 
	@UID INT,
	@Mail_Index INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		B.Mail_Index,
						B.TYPEID,
						B.QTY
	FROM			DBO.Pangya_Mail A
	INNER JOIN	DBO.Pangya_Mail_Item B
	ON			B.Mail_Index = A.Mail_Index AND B.RELEASE_DATE IS NULL
	WHERE		A.UID = @UID 
	AND 		A.Mail_Index = @Mail_Index 
	AND 		A.ReceiveDate IS NULL 
	AND			A.DeleteDate IS NULL

END


GO
/****** Object:  StoredProcedure [dbo].[ProcReadMail]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcReadMail] 
	@UID INT,
	@Mail_Index INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	DBO.Pangya_Mail
	SET			ReadDate = GETDATE()
	WHERE 	Mail_Index = @Mail_Index

	SELECT	A.Mail_Index,
					A.[Sender],
					A.RegDate,
					A.[Msg],
					B.TYPEID,
					B.QTY,
					CASE WHEN B.DAY > 0 THEN 1 ELSE 0 END AS IsTime,
					B.DAY, b.UCC_UNIQUE					
	FROM( SELECT Mail_Index,
					[Sender],
					RegDate,
					[Msg] 					
	 FROM DBO.Pangya_Mail WHERE UID = @UID AND Mail_Index = @Mail_Index) A
	INNER JOIN 	DBO.Pangya_Mail_Item B ON B.Mail_Index = @Mail_Index AND B.RELEASE_DATE IS NULL
END


GO
/****** Object:  StoredProcedure [dbo].[ProcSaveCaddies]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSaveCaddies]
	@UID INT,
    @JSONData NVARCHAR(MAX)
AS
BEGIN
  SET NOCOUNT ON;
  
  UPDATE [DBO].Pangya_Caddie
  SET 	SKIN_TYPEID = JSON.CaddieSkin,
  		SKIN_END_DATE = JSON.CaddieSkinEndDate,
        TriggerPay = ISNULL(JSON.CaddieAutoPay, 0)
  FROM OPENJSON(@JSONData, '$.Caddies')
  WITH (
	CaddieIndex INT,
	CaddieSkin INT,
	CaddieSkinEndDate DATETIME,
	CaddieAutoPay TINYINT
  ) AS JSON
  WHERE	UID = @UID AND
		CID = JSON.CaddieIndex
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveCard]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSaveCard]
	@UID INT,
	@JSONData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE DBO.Pangya_Card
	SET QTY = JSON.CardQty ,
			VALID = JSON.CardValid
	FROM OPENJSON(@JSONData, '$.Cards')
	WITH (
		CardIndex INT
		, CardQty INT
		, CardValid INT
	) AS JSON
	WHERE UID = @UID 
	AND		CARD_IDX = JSON.CardIndex
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveCardEquip]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSaveCardEquip]
	@UID INT,
	@JSData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE DBO.Pangya_Card_Equip
	SET	CARD_TYPEID = A.CARDTYPEID
			, ENDDATE = A.ENDDATE
			, VALID = A.VALID
	FROM OPENJSON(@JSData, '$.CARDEQUIP')
	WITH (
		UNID INT
		, CARDTYPEID INT
		, ENDDATE DATETIME
		, VALID INT
	) A
	WHERE UID = @UID
	AND		ID = A.UNID
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveCharacter]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSaveCharacter]
	@UID INT,
	@JSONData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
    
	UPDATE [DBO].Pangya_Character_Equip
	SET	
		-- UPDATE TYPEID
		PART_TYPEID_1 	= ISNULL(A.EquipTypeID0, 0),
		PART_TYPEID_2 	= ISNULL(A.EquipTypeID1, 0),
		PART_TYPEID_3 	= ISNULL(A.EquipTypeID2, 0),
		PART_TYPEID_4 	= ISNULL(A.EquipTypeID3, 0),
		PART_TYPEID_5 	= ISNULL(A.EquipTypeID4, 0),
		PART_TYPEID_6 	= ISNULL(A.EquipTypeID5, 0),
		PART_TYPEID_7 	= ISNULL(A.EquipTypeID6, 0),
		PART_TYPEID_8 	= ISNULL(A.EquipTypeID7, 0),
		PART_TYPEID_9 	= ISNULL(A.EquipTypeID8, 0),
		PART_TYPEID_10 	= ISNULL(A.EquipTypeID9, 0),
		PART_TYPEID_11 	= ISNULL(A.EquipTypeID10, 0),
		PART_TYPEID_12 	= ISNULL(A.EquipTypeID11, 0),
		PART_TYPEID_13 	= ISNULL(A.EquipTypeID12, 0),
		PART_TYPEID_14 	= ISNULL(A.EquipTypeID13, 0),
		PART_TYPEID_15 	= ISNULL(A.EquipTypeID14, 0),
		PART_TYPEID_16 	= ISNULL(A.EquipTypeID15, 0),
		PART_TYPEID_17 	= ISNULL(A.EquipTypeID16, 0),
		PART_TYPEID_18 	= ISNULL(A.EquipTypeID17, 0),
		PART_TYPEID_19 	= ISNULL(A.EquipTypeID18, 0),
		PART_TYPEID_20 	= ISNULL(A.EquipTypeID19, 0),
		PART_TYPEID_21 	= ISNULL(A.EquipTypeID20, 0),
		PART_TYPEID_22 	= ISNULL(A.EquipTypeID21, 0),
		PART_TYPEID_23 	= ISNULL(A.EquipTypeID22, 0),
		PART_TYPEID_24 	= ISNULL(A.EquipTypeID23, 0),
		-- UPDATE ITEM INDEX
		PART_IDX_1		= ISNULL(A.EquipIndex0, 0),
		PART_IDX_2 		= ISNULL(A.EquipIndex1, 0),
		PART_IDX_3 		= ISNULL(A.EquipIndex2, 0),
		PART_IDX_4 		= ISNULL(A.EquipIndex3, 0),
		PART_IDX_5 		= ISNULL(A.EquipIndex4, 0),
		PART_IDX_6 		= ISNULL(A.EquipIndex5, 0),
		PART_IDX_7 		= ISNULL(A.EquipIndex6, 0),
		PART_IDX_8 		= ISNULL(A.EquipIndex7, 0),
		PART_IDX_9 		= ISNULL(A.EquipIndex8, 0),
		PART_IDX_10 	= ISNULL(A.EquipIndex9, 0),
		PART_IDX_11 	= ISNULL(A.EquipIndex10, 0),
		PART_IDX_12 	= ISNULL(A.EquipIndex11, 0),
		PART_IDX_13 	= ISNULL(A.EquipIndex12, 0),
		PART_IDX_14 	= ISNULL(A.EquipIndex13, 0),
		PART_IDX_15 	= ISNULL(A.EquipIndex14, 0),
		PART_IDX_16 	= ISNULL(A.EquipIndex15, 0),
		PART_IDX_17 	= ISNULL(A.EquipIndex16, 0),
		PART_IDX_18 	= ISNULL(A.EquipIndex17, 0),
		PART_IDX_19 	= ISNULL(A.EquipIndex18, 0),
		PART_IDX_20 	= ISNULL(A.EquipIndex19, 0),
		PART_IDX_21 	= ISNULL(A.EquipIndex20, 0),
		PART_IDX_22 	= ISNULL(A.EquipIndex21, 0),
		PART_IDX_23 	= ISNULL(A.EquipIndex22, 0),
		PART_IDX_24 	= ISNULL(A.EquipIndex23, 0)
	FROM OPENJSON(@JSONData, '$.Char')
	WITH (
		-- CHAR INDEX
		CharIndex INT,
		-- EQUIP TYPEID
		EquipTypeID0 INT,
		EquipTypeID1 INT,
		EquipTypeID2 INT,
		EquipTypeID3 INT,
		EquipTypeID4 INT,
		EquipTypeID5 INT,
		EquipTypeID6 INT,
		EquipTypeID7 INT,
		EquipTypeID8 INT,
		EquipTypeID9 INT,
		EquipTypeID10 INT,
		EquipTypeID11 INT,
		EquipTypeID12 INT,
		EquipTypeID13 INT,
		EquipTypeID14 INT,
		EquipTypeID15 INT,
		EquipTypeID16 INT,
		EquipTypeID17 INT,
		EquipTypeID18 INT,
		EquipTypeID19 INT,
		EquipTypeID20 INT,
		EquipTypeID21 INT,
		EquipTypeID22 INT,
		EquipTypeID23 INT,
		-- EQUIP INDEX
		EquipIndex0 INT,
		EquipIndex1 INT,
		EquipIndex2 INT,
		EquipIndex3 INT,
		EquipIndex4 INT,
		EquipIndex5 INT,
		EquipIndex6 INT,
		EquipIndex7 INT,
		EquipIndex8 INT,
		EquipIndex9 INT,
		EquipIndex10 INT,
		EquipIndex11 INT,
		EquipIndex12 INT,
		EquipIndex13 INT,
		EquipIndex14 INT,
		EquipIndex15 INT,
		EquipIndex16 INT,
		EquipIndex17 INT,
		EquipIndex18 INT,
		EquipIndex19 INT,
		EquipIndex20 INT,
		EquipIndex21 INT,
		EquipIndex22 INT,
		EquipIndex23 INT,
		-- STATUS
		C0 TINYINT,
		C1 TINYINT,
		C2 TINYINT,
		C3 TINYINT,
		C4 TINYINT
	) AS A
	WHERE UID = @UID AND CHAR_IDX = A.CharIndex
	
	UPDATE DBO.Pangya_Character
	SET		POWER 	= A.C0
			, CONTROL = A.C1
			, IMPACT	= A.C2
			, SPIN		= A.C3
			, CURVE		= A.C4
	FROM OPENJSON(@JSONData, '$.Char')
	WITH (
		CharIndex INT,
		C0 TINYINT,
		C1 TINYINT,
		C2 TINYINT,
		C3 TINYINT,
		C4 TINYINT
	) A
	WHERE UID = @UID AND CID = A.CharIndex
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveExceptionLog]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveExceptionLog] 
	@UID INT,
	@USER VARCHAR(50),
	@EXCEPTIONMESSAGE VARCHAR(2000),
	@SERVER VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [DBO].Pangya_Exception_Log(UID, Username, ExceptionMessage, Server) 
    VALUES (@UID, @USER, @EXCEPTIONMESSAGE, @SERVER)
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveItem]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveItem] 
	@UID INT,
	@JSONData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	-- UPDATE TO WAREHOUSE
	UPDATE 	DBO.Pangya_Warehouse
	SET 		C0 			= A.ItemC0,
					C1			= A.ItemC1,
					C2			= A.ItemC2,
					C3			= A.ItemC3,
					C4			= A.ItemC4,
					VALID		= A.ItemValid,
					DateEnd = A.ItemEndDate,
					Flag 		= A.ItemFlag
	FROM OPENJSON(@JSONData, '$.Items') 
	WITH (
		ItemIndex 	INT, 
		ItemC0 			INT,
		ItemC1 			INT,
		ItemC2 			INT,
		ItemC3 			INT,
		ItemC4 			INT,
		ItemValid 	INT,
		ItemEndDate	DATETIME,
		ItemFlag 		INT
	) AS A
	WHERE UID = @UID
	AND		item_id = A.ItemIndex
          
	-- UPDATE UCC
	UPDATE 	DBO.Pangya_SelfDesign
	SET			UCC_STATUS 	= A.ItemUCCStatus,
					UCC_UNIQE 	= A.ItemUCCUnique
	FROM OPENJSON(@JSONData, '$.Items')
	WITH (
		ItemIndex 		INT, 
		ItemC0 				INT,
		ItemValid			TINYINT, 
		IsSelfDesign	TINYINT, 
		ItemUCCStatus	TINYINT,
		ItemUCCUnique	VARCHAR (10)
	) AS A
	WHERE UID = @UID
	AND		ITEM_ID = A.ItemIndex
	AND		A.IsSelfDesign = 1
	
	-- UPDATE CLUBSET
	UPDATE	DBO.Pangya_Club_Info
	SET			C0_SLOT 							= A.C0Slot,
					C1_SLOT 							=	A.C1Slot,
					C2_SLOT 							= A.C2Slot,
					C3_SLOT 							= A.C3Slot,
					C4_SLOT 							= A.C4Slot,
					CLUB_POINT 						= A.ClubPoint,
					CLUB_WORK_COUNT 			= A.WorkCount,
					CLUB_SLOT_CANCEL 			= A.CancelCount,
					CLUB_POINT_TOTAL_LOG 	= A.PointLog,
					CLUB_UPGRADE_PANG_LOG = A.PangLog
	FROM OPENJSON(@JSONData, '$.Items')
	WITH (
		ItemIndex 	INT,
		ClubPoint 	INT,
		WorkCount 	INT,
		PointLog 		INT,
		PangLog 		INT,
		C0Slot 			TINYINT,
		C1Slot 			TINYINT,
		C2Slot 			TINYINT,
		C3Slot 			TINYINT,
		C4Slot 			TINYINT,
		CancelCount	TINYINT,
		IsClubset 	TINYINT
	) A
	WHERE	ITEM_ID = A.ItemIndex
	AND		IsClubSet = 1	
    
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveMacro]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveMacro] @UID INT
	,@Macro1 VARCHAR(45) = 'Pangya!'
	,@Macro2 VARCHAR(45) = 'Pangya!'
	,@Macro3 VARCHAR(45) = 'Pangya!'
	,@Macro4 VARCHAR(45) = 'Pangya!'
	,@Macro5 VARCHAR(45) = 'Pangya!'
	,@Macro6 VARCHAR(45) = 'Pangya!'
	,@Macro7 VARCHAR(45) = 'Pangya!'
	,@Macro8 VARCHAR(45) = 'Pangya!'
	,@Macro9 VARCHAR(45) = 'Pangya!'
	,@Macro10 VARCHAR(45) = 'Pangya!'
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].Pangya_Game_Macro
	SET Macro1 = @Macro1
		,Macro2 = @Macro2
		,Macro3 = @Macro3
		,Macro4 = @Macro4
		,Macro5 = @Macro5
		,Macro6 = @Macro6
		,Macro7 = @Macro7
		,Macro8 = @Macro8
		,Macro9 = @Macro9
		,Macro10 = @Macro10
	WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSavePersonalLog]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSavePersonalLog]
	@UID INT,
	@PROCESS TINYINT,
	@AMOUNT BIGINT
AS
BEGIN
	SET NOCOUNT ON
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveToolbar]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveToolbar] 
	@UID INT,
	@JSONData NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM DBO.Pangya_User_Equip WHERE UID = @UID)
	BEGIN
		UPDATE DBO.Pangya_User_Equip
		SET	CHARACTER_ID = A.CharIndex,
			CADDIE = A.CaddieIndex,
			MASCOT_ID = A.MascotIndex,
			BALL_ID = A.BallTypeID,
			CLUB_ID = A.ClubIndex,
			ITEM_SLOT_1 = A.SLOT1,
			ITEM_SLOT_2 = A.SLOT2,
			ITEM_SLOT_3 = A.SLOT3,
			ITEM_SLOT_4 = A.SLOT4,
			ITEM_SLOT_5 = A.SLOT5,
			ITEM_SLOT_6 = A.SLOT6,
			ITEM_SLOT_7 = A.SLOT7,
			ITEM_SLOT_8 = A.SLOT8,
			ITEM_SLOT_9 = A.SLOT9,
			ITEM_SLOT_10 = A.SLOT10
		FROM OPENJSON (@JSONData)
		WITH (
			CharIndex INT,
			CaddieIndex INT,
			MascotIndex INT,
			BallTypeID INT,
			ClubIndex INT,
			SLOT1 INT,
			SLOT2 INT,
			SLOT3 INT,
			SLOT4 INT,
			SLOT5 INT,
			SLOT6 INT,
			SLOT7 INT,
			SLOT8 INT,
			SLOT9 INT,
			SLOT10 INT
		) A
		WHERE UID = @UID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveUCC]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveUCC]
	@UID INT,
	@UCC_ITEMID INT,
	@UCC_NAME VARCHAR(20),
	@UCC_STATUS SMALLINT,
	@UCC_DRAWER_UID INT

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE DBO.Pangya_SelfDesign SET UCC_NAME = RTRIM(LTRIM(@UCC_NAME)), UCC_STATUS = @UCC_STATUS, UCC_DRAWER = @UCC_DRAWER_UID
	WHERE UID = @UID AND ITEM_ID = @UCC_ITEMID AND UCC_STATUS NOT IN(1)

END
GO
/****** Object:  StoredProcedure [dbo].[ProcSaveUCCCopy]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        TOP
-- Create date: 
-- Description:    
-- =============================================
CREATE PROCEDURE [dbo].[ProcSaveUCCCopy]
  @UID INT,
  @TYPEID INT = 0, -- TYPE ORIGINAL
  @UCC_UNIQUE VARCHAR(10),-- ORIGINAL
  @UCC_IDX INT -- TO COPY IDX
  AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @UCC_COPY_COUNT SMALLINT

  IF EXISTS ( SELECT 1 FROM DBO.Pangya_SelfDesign WHERE UID = @UID AND UCC_UNIQE = @UCC_UNIQUE AND UCC_STATUS = 1 )
  BEGIN
    SELECT @UCC_COPY_COUNT = MAX(UCC_COPY_COUNT)
    FROM DBO.Pangya_SelfDesign
    WHERE UID = @UID AND
          UCC_UNIQE = @UCC_UNIQUE
    GROUP BY UID
    
    UPDATE DBO.Pangya_SelfDesign
    SET UCC_STATUS = 1,
        UCC_COPY_COUNT = @UCC_COPY_COUNT + 1,
        UCC_UNIQE = @UCC_UNIQUE,
        UCC_NAME = B.UCC_NAME,
        UCC_DRAWER = B.UCC_DRAWER
    FROM DBO.Pangya_SelfDesign A
         CROSS APPLY
         (
           SELECT UCC_NAME,
                  UCC_DRAWER
           FROM DBO.Pangya_SelfDesign
           WHERE UID = @UID AND
                 UCC_UNIQE = @UCC_UNIQUE AND
                 UCC_STATUS = 1
         ) B
    WHERE A.ITEM_ID = @UCC_IDX AND
          A.UCC_STATUS = 0 
          
	IF @@ROWCOUNT > 0
    BEGIN
      SELECT 1 AS Code,
             ITEM_ID,
             TYPEID,
             UCC_UNIQE,
             UCC_COPY_COUNT
      FROM DBO.Pangya_SelfDesign
      WHERE ITEM_ID = @UCC_IDX END
    ELSE
    BEGIN
      SELECT 0 AS Code 
    END
  END
  ELSE
  BEGIN
    SELECT 0 AS Code END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcSetLockerPwd]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcSetLockerPwd]
	@UID INT,
	@PWD VARCHAR(4)
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE DBO.Pangya_Personal
	SET LockerPwd = @PWD
	WHERE UID = @UID
	
	IF (@@ROWCOUNT > 0) BEGIN
		SELECT 1 AS Code
	END ELSE BEGIN
		SELECT 0 AS Code
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ProcTutorialEvent]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProcTutorialEvent] 
	@UID INT	
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EVENT_TUTORIAL INT;
	DECLARE @EVENT_TUTORIAL2 INT;
	DECLARE @MailString VARCHAR(255);
	SET @EVENT_TUTORIAL = (Select Event1 from dbo.Pangya_Member Where UID = @UID);
	SET @EVENT_TUTORIAL2 = (Select Event2 from dbo.Pangya_Member Where UID = @UID)

	if(@EVENT_TUTORIAL = 0 AND @EVENT_TUTORIAL2 = 0) 
	begin
	set @MailString = FORMATMESSAGE('^%d^%d,^%d^%d,', 0x1C000000, 1, 0x10000012, 1);
 
 	update dbo.Pangya_Member Set Event1 = 1 Where UID = @UID

	--Caddie Papel And Club Set Air Knight Lucky
	exec dbo.ProcMailInsert @UID, '@TutorialSystem', 'NICE FINISH TUTORIAL ROOKIE', @MailString;
	end

	if(@EVENT_TUTORIAL = 1 AND @EVENT_TUTORIAL2 = 0)
	begin 
	set @MailString = FORMATMESSAGE('^%d^%d,^%d^%d,', 0x18000027, 10, 0x10000012, 10000);
	
	update dbo.Pangya_Member Set Event2 = 1 Where UID = @UID;
	exec dbo.ProcMailInsert @UID, '@TutorialSystem', 'NICE FINISH TUTORIAL BEGINNER', @MailString;
	end
END

GO
/****** Object:  StoredProcedure [dbo].[ProcTutorialSet]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProcTutorialSet] 
	@UID INT,
    @TIPO INT,
	@VALOR INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TESTE INT;
	
	IF @TIPO = 0 
	BEGIN
		UPDATE Pangya_Tutorial SET Rookie = Rookie + @VALOR WHERE UID = @UID;

		exec ProcAddItemTutorial @UID, @VALOR, 0;
	END;

	ELSE IF @TIPO = 1 
	BEGIN

		UPDATE Pangya_Tutorial SET Rookie = Rookie + @VALOR WHERE UID = @UID;

		UPDATE Pangya_Member SET Tutorial = 1 WHERE UID = @UID;

		exec ProcAddItemTutorial @UID, @VALOR, 0;

		exec ProcTutorialEvent @UID;
	END;

	ELSE IF @TIPO = 256 BEGIN
		UPDATE Pangya_Tutorial	SET Beginner = Beginner + @VALOR WHERE UID = @UID;

		exec ProcAddItemTutorial @UID, @VALOR, 1

	 SET @TESTE = (SELECT Beginner FROM Pangya_Tutorial WHERE UID = @UID);


		IF @TESTE = 16128 
		BEGIN

			UPDATE Pangya_Member SET Tutorial = 2 WHERE UID = @UID;

			exec ProcTutorialEvent @UID;
		END

	END	
END

GO
/****** Object:  StoredProcedure [dbo].[ProcUpdateAuth]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcUpdateAuth]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;
    
	DECLARE @KEY_GAME VARCHAR(7)
    
	SET @KEY_GAME = LOWER(LEFT(NEWID(), 7))
    
	UPDATE DBO.Pangya_Member SET AuthKey_Game = @KEY_GAME WHERE UID = @UID
    
	SELECT @KEY_GAME AS KEY_GAME  
END
GO
/****** Object:  StoredProcedure [dbo].[ProcUpdateMail]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ProcUpdateMail]
	@UID INT,
	@MailIndex INT,
	@ItemTypeID int,
	@ItemAddedIndex int
AS
BEGIN
	SET NOCOUNT ON;

		UPDATE DBO.Pangya_Mail SET	ReceiveDate = GETDATE(),ReadDate = GETDATE()
	WHERE UID = @UID AND Mail_Index = @MailIndex

	UPDATE Pangya_Mail_Item SET	RELEASE_DATE = GETDATE(), APPLY_ITEM_ID = @ItemAddedIndex	
	WHERE Mail_Index = @MailIndex AND TYPEID = @ItemTypeID
END



GO
/****** Object:  StoredProcedure [dbo].[ProcUpdateMapStatistics]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcUpdateMapStatistics]
	@UID INT,
	@MAP SMALLINT,
	@DRIVE INT,
	@PUTT INT,
	@HOLE INT,
	@FAIRWAY INT,
	@HOLEIN INT,
	@PUTTIN INT,
	@SCORE_IN INT,
	@MAXPANG INT,
	@CHARTYPEID INT,
	@ASSIST INT, -- 1 = ENABLE , 0 = DISABLE
	@EVENTSCORE INT = 0
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @A_BESTSCORE SMALLINT
	DECLARE @A_MAXPANG INT
	
	DECLARE @NEWRECORD INT = 0
	
	IF NOT EXISTS ( SELECT 1 FROM DBO.Pangya_Map_Statistics WHERE UID = @UID AND Map = @MAP AND Assist = @ASSIST ) BEGIN
	
		INSERT INTO DBO.Pangya_Map_Statistics
			(UID, Map, Drive, Putt, Hole, Fairway, Holein, Puttin, TotalScore, BestScore, MaxPang, CharTypeID, EventScore, Assist) 
		VALUES (@UID, @MAP, @DRIVE, @PUTT, @HOLE, @FAIRWAY, @HOLEIN, @PUTTIN, @SCORE_IN, @SCORE_IN, @MAXPANG, @CHARTYPEID, @EVENTSCORE, @ASSIST)
		
		-- SET NEWRECORD = 1
		SET @NEWRECORD = 1
		
	END ELSE BEGIN
	
		-- FETCH OLD DATA
		SELECT	@A_BESTSCORE 	= BestScore,
						@A_MAXPANG 		= MaxPang
		FROM
						DBO.Pangya_Map_Statistics
		WHERE
						UID = @UID AND Map = @MAP AND Assist = @ASSIST
						
	
		-- UPDATE SCORE
		IF (@SCORE_IN <= @A_BESTSCORE) AND (@MAXPANG >= @A_MAXPANG) BEGIN
		
			UPDATE DBO.Pangya_Map_Statistics
			SET BestScore 	= @SCORE_IN,
					MaxPang 		= @MAXPANG,
					CharTypeId 	= @CHARTYPEID
			WHERE
					UID = @UID AND
					Map = @MAP AND
					Assist = @ASSIST
					
		END
		
		-- UPDATE MAP STATISTIC
		UPDATE DBO.Pangya_Map_Statistics
		SET	Drive 			+= @DRIVE,
				Putt 				+= @PUTT,
				Hole 				+= @HOLE,
				Fairway 		+= @FAIRWAY,
				Holein 			+= (@HOLE - @HOLEIN),
				PuttIn 			+= @PUTTIN,
				TotalScore 	+= @SCORE_IN,
				EventScore	+= @EventScore
		WHERE UID = @UID AND Map = @MAP AND Assist = @ASSIST
		
	END
	
	-- SELECT IF IT IS NEW RECORD
	SELECT @NEWRECORD AS ISNEWRECORD
	
END
GO
/****** Object:  StoredProcedure [dbo].[ProcUpdateNickname]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	Change Player Nickname
-- =============================================
CREATE PROCEDURE [dbo].[ProcUpdateNickname] 
	@UID int,
	@NICKNAME varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @NICK VARCHAR(20)
	SELECT @NICK = RTRIM(LTRIM(@NICKNAME))

	IF EXISTS (  SELECT 1 
			   FROM [dbo].Pangya_Member 
			   WHERE Nickname = @NICK COLLATE Latin1_General_CS_AS
			) 
	BEGIN
		SELECT Code = 2
	END ELSE BEGIN
		UPDATE [dbo].Pangya_Member SET Nickname = @NICK WHERE UID = @UID
		SELECT Code = 1
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_ADD_CARD_EQUIP]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ADD_CARD_EQUIP]
	@UID INT,
	@CID INT,
	@CHARTYPEID INT,
	@CARDTYPEID INT,
	@SLOT TINYINT,
	@FLAG TINYINT,
	@TIME TINYINT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @ID_OUT INT = 0
	DECLARE @IN_ENDDATE DATETIME = NULL
	
	-- MAP END DATE
	IF (@FLAG = 0) BEGIN
		SET @IN_ENDDATE = GETDATE()
	END ELSE BEGIN
		SET @IN_ENDDATE = DATEADD(N, @TIME, GETDATE())
	END
	
	IF EXISTS (SELECT 1 FROM DBO.Pangya_Member WHERE UID = @UID) BEGIN
	
		INSERT INTO DBO.Pangya_Card_Equip(UID, CID, CHAR_TYPEID, CARD_TYPEID, SLOT, ENDDATE, FLAG)
		VALUES (@UID, @CID, @CHARTYPEID, @CARDTYPEID, @SLOT, @IN_ENDDATE, @FLAG)
		
		SET @ID_OUT = SCOPE_IDENTITY()
		
		IF ( @ID_OUT > 0 )
		BEGIN
			SELECT	0 AS CODE
							, @ID_OUT 			AS OUT_INDEX
							, @CID 					AS CID
							, @CHARTYPEID		AS CHARTYPEID 
							, @CARDTYPEID		AS CARDTYPEID
							, @SLOT 				AS SLOT
							, GETDATE()			AS REGDATE
							, @IN_ENDDATE		AS ENDDATE
							, @FLAG					AS FLAG
		END ELSE BEGIN
			SELECT 1 AS CODE
		END
	END ELSE BEGIN
		SELECT 1 CODE
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_DAILYQUEST_ACCEPT]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_DAILYQUEST_ACCEPT]
	@UID INT,
	@QUESTSTR NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @COUNTER_ID INT = 0
	DECLARE @COUNTER_TYPEID INT = 0
	DECLARE @QUESTPROCESS INT = 0
	
	DECLARE @NEW_COUNTER TABLE (
		TypeID INT,
		ID INT
	)
	
	DECLARE @QUEST_PROCESS TABLE (
		QuestID INT,
		Valid TINYINT
	)
	
	DECLARE @QUEST_ID TABLE (
		QuestID INT
	)
	
	INSERT INTO @QUEST_PROCESS(QuestID, Valid)
	SELECT A.QuestID, 1
	FROM OPENJSON(@QUESTSTR, '$.QuestIDs')
	WITH (
		QuestID INT
	) A
	
	
	WHILE EXISTS (SELECT 1 FROM @QUEST_PROCESS WHERE Valid = 1) BEGIN
		/* GET QUEST */
		SELECT TOP 1 @QUESTPROCESS = QuestID FROM @QUEST_PROCESS WHERE Valid = 1
		/* QUEST 1 ADD COUNTER */
		IF EXISTS (SELECT 1 FROM [DBO].Pangya_Achievement WHERE UID = @UID AND ID = @QUESTPROCESS AND Type = 1) BEGIN
			/* GET COUNTER TYPEID */
			SELECT @COUNTER_TYPEID = B.CounterTypeID
			FROM [DBO].Pangya_Achievement_Quest A
			INNER JOIN [DBO].Achievement_QuestStuffs B
			ON A.Achivement_Quest_TypeID = B.TypeID
			WHERE A.Achievement_Index = @QUESTPROCESS
			
			/* INSERT COUNTER */
			INSERT INTO [DBO].Pangya_Achievement_Counter(UID, TypeID, Quantity) 
			SELECT @UID, @COUNTER_TYPEID, 0
			
			/* GET ID */
			SET @COUNTER_ID = SCOPE_IDENTITY()
			
			/* UPDATE QUEST */
			UPDATE DBO.Pangya_Achievement_Quest
			SET	Counter_Index = @COUNTER_ID
			WHERE Achievement_Index = @QUESTPROCESS
			
			/* SET QUEST TO ACTIVE */
			UPDATE [DBO].Pangya_Achievement
			SET Type = 3
			WHERE ID = @QUESTPROCESS
			
			/* INSERT INTO COUNTER UPDATE */
			INSERT INTO @NEW_COUNTER(TypeID, ID) SELECT @COUNTER_TYPEID, @COUNTER_ID
		
			/* UPDATE */
			INSERT INTO @QUEST_ID(QuestID) SELECT @QUESTPROCESS
		END
		
		/* UPDATE */
		UPDATE @QUEST_PROCESS SET Valid = 0 WHERE QuestID = @QUESTPROCESS
	END
	
	SELECT *
	FROM @NEW_COUNTER
	
	SELECT *
	FROM @QUEST_PROCESS
	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_DAILYQUEST_LOAD]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_DAILYQUEST_LOAD]
	@UID INT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @LAST_ID INT = 0
	DECLARE @QUEST_TYPEID1 INT = 0
	DECLARE @QUEST_TYPEID2 INT = 0
	DECLARE @QUEST_TYPEID3 INT = 0
	DECLARE @QUEST_TIME INT = 0
	
	DECLARE @NEW_QUEST TABLE (
		TypeID INT,
		QuestIndex INT,
		Quantity INT 
	)
	
	DECLARE @OLD_QUEST TABLE (
		TypeID INT,
		QuestIndex INT,
		Quantity INT 
	)
	
	DECLARE @QUESTS TABLE (
		ID INT,
		TypeID INT,
		Type TINYINT
	)
	
	SELECT	@QUEST_TYPEID1 = QuestTypeID1,
					@QUEST_TYPEID2 = QuestTypeID2,
					@QUEST_TYPEID3 = QuestTypeID3,
					@QUEST_TIME = ISNULL(DBO.UNIX_TIMESTAMP(RegDate), 0)
	FROM 		DBO.Daily_Quest
	WHERE 	Day = 1 -- DAY(GETDATE())
	
	/* GET ALL QUESTS */
	INSERT INTO @QUESTS(ID, TypeID, Type) SELECT ID, TypeID, Type FROM [DBO].Pangya_Achievement WHERE UID = @UID AND VALID = 1
	
	/* INSERT INTO SOON DELETE QUEST */
	INSERT INTO @OLD_QUEST(TypeID, QuestIndex, Quantity) 
	SELECT TypeID, ID, 0 FROM @QUESTS WHERE Type = 1 AND TypeID NOT IN (@QUEST_TYPEID1, @QUEST_TYPEID2, @QUEST_TYPEID3)
	
	/* CLEAR OLD QUEST */
	UPDATE A
	SET A.Valid = 0
	FROM [DBO].Pangya_Achievement A
	JOIN @OLD_QUEST B
	ON A.ID = B.QuestIndex
	
	IF NOT EXISTS (SELECT 1 FROM [DBO].Pangya_Daily_Quest WHERE UID = @UID) BEGIN
		INSERT INTO [DBO].Pangya_Daily_Quest(UID) SELECT @UID
	END
	
	/* QUEST 1 CHECK */
	IF NOT EXISTS ( SELECT 1 FROM @QUESTS WHERE TypeID = @QUEST_TYPEID1 ) BEGIN
			INSERT INTO [DBO].Pangya_Achievement( UID, TypeID, Type ) VALUES ( @UID, @QUEST_TYPEID1, 1 )
			SELECT @LAST_ID = SCOPE_IDENTITY()
			EXEC [DBO].ProcInsertDailyQuest @IN_UID = @UID, @QUESTID = @LAST_ID, @DAILYQUEST = @QUEST_TYPEID1
			INSERT INTO @NEW_QUEST VALUES (@QUEST_TYPEID1, @LAST_ID, 1)
	END
	
	/* QUEST 2 CHECK */
	IF NOT EXISTS ( SELECT 1 FROM @QUESTS WHERE TypeID = @QUEST_TYPEID2 ) BEGIN
			INSERT INTO [DBO].Pangya_Achievement( UID, TypeID, Type ) VALUES ( @UID, @QUEST_TYPEID2, 1 )
			SELECT @LAST_ID = SCOPE_IDENTITY()
			EXEC [DBO].ProcInsertDailyQuest @IN_UID = @UID, @QUESTID = @LAST_ID, @DAILYQUEST = @QUEST_TYPEID2
			INSERT INTO @NEW_QUEST VALUES (@QUEST_TYPEID2, @LAST_ID, 1)
	END
	
	/* QUEST 3 CHECK */
	IF NOT EXISTS ( SELECT 1 FROM @QUESTS WHERE TypeID = @QUEST_TYPEID3 ) BEGIN
			INSERT INTO [DBO].Pangya_Achievement( UID, TypeID, Type ) VALUES ( @UID, @QUEST_TYPEID3, 1 )
			SELECT @LAST_ID = SCOPE_IDENTITY()
			EXEC [DBO].ProcInsertDailyQuest @IN_UID = @UID, @QUESTID = @LAST_ID, @DAILYQUEST = @QUEST_TYPEID3
			INSERT INTO @NEW_QUEST VALUES (@QUEST_TYPEID3, @LAST_ID, 1)
	END
	
	/* NEW QUEST */
	SELECT * FROM	@NEW_QUEST
	
	/* DAILY DETAIL */
	SELECT 	@QUEST_TYPEID1 AS Quest1,
					@QUEST_TYPEID2 AS Quest2,
					@QUEST_TYPEID3 AS Quest3,
					@QUEST_TIME AS QuestRegDate,
					CASE WHEN (LastCancel > LastAccept) OR (LastAccept IS NULL) 
					THEN DBO.UNIX_TIMESTAMP(RegDate) 
					ELSE DBO.UNIX_TIMESTAMP(LastAccept) END AS ActivityDate
	FROM		[DBO].Pangya_Daily_Quest
	WHERE		UID = @UID
	
	/* OLD QUEST */
	SELECT * FROM	@OLD_QUEST
END
GO
/****** Object:  StoredProcedure [dbo].[USP_FIRST_CREATION]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_FIRST_CREATION]
	@UID INT,
	@CHAR_TYPEID INT,
	@HAIRCOLOUR TINYINT,
	@NICKNAME VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @CODE TINYINT
	-- NICKNAME
	DECLARE @NICK VARCHAR(20)
	SELECT @NICK = RTRIM(LTRIM(@NICKNAME))
	
	DECLARE @CLUB_INDEX INT = 0
	DECLARE @CHAR_INDEX INT = 0
	DECLARE @MASCOT_INDEX INT = 0;
	IF EXISTS (SELECT 1 FROM [dbo].Pangya_Member WHERE UID = @UID AND FirstSet = 0) BEGIN
		UPDATE [dbo].Pangya_Member SET FirstSet = 1, Nickname = @NICK WHERE UID = @UID
		
		-- Pangya Equip
		INSERT INTO [dbo].Pangya_User_Equip(UID) VALUES (@UID)
		
		-- Insert User Statistic
    INSERT INTO [DBO].Pangya_User_Statistics(UID, Pang) VALUES (@UID, 3000000)
		
  
  INSERT INTO dbo.Pangya_Tutorial(UID) values(@UID);

  INSERT INTO dbo.Pangya_User_MatchHistory(UID)VALUES(@UID);

		-- Insert Into Macro
		INSERT INTO [dbo].Pangya_Game_Macro(UID) VALUES (@UID)
		
		-- Insert Personal
		INSERT INTO [DBO].Pangya_Personal(UID, CookieAmt) VALUES (@UID, 1000000)

	
		INSERT INTO [DBO].td_room_data(UID, TYPEID, POS_X, POS_Y, POS_Z, POS_R)VALUES(@UID, 1207986208, 15.2, 0, 12.5, 152);	

	    INSERT INTO [DBO].td_room_data(UID, TYPEID, POS_X, POS_Y, POS_Z, POS_R)VALUES(@UID, 1207980061, 14.125, 1.0, 0.041, 0);


		-- Insert Default Item
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 436207622, 1); -- Lucky Necklace
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653188, 10);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653189, 10);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653184, 10);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653185, 10);		
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653190, 10);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653193, 10);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 436207633, 50);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653195, 5);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 402653194, 5);
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 467664918, 1); -- Assist	
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 335544320, 1); -- Ball		
		INSERT INTO [dbo].Pangya_Warehouse(UID,TYPEID,C0) VALUES (@UID, 268435456, 1);-- Insert Club


		SET @CLUB_INDEX = (SELECT item_id FROM DBO.Pangya_Warehouse WHERE UID = @UID AND TYPEID = 268435456)		
		INSERT INTO [dbo].[Pangya_Club_Info] (ITEM_ID) VALUES (@CLUB_INDEX);
		
		-- Insert Character
		exec dbo.ProcFixPartsCharacter @UID, @CHAR_TYPEID, @HAIRCOLOUR
		SET @CHAR_INDEX = (SELECT CID FROM DBO.Pangya_Character WHERE UID = @UID AND TYPEID = @CHAR_TYPEID)
		
		  INSERT INTO DBO.Pangya_Mascot(UID, MASCOT_TYPEID, MESSAGE, DateEnd, VALID) VALUES (@UID, 1073741826, 'PANGYA',  DATEADD(DAY, 1+1, GETDATE()), 1)

		SET @MASCOT_INDEX = (SELECT MID FROM dbo.Pangya_Mascot WHERE UID = @UID and MASCOT_TYPEID = 1073741826);

		-- Update Equip
		UPDATE DBO.Pangya_User_Equip
		SET BALL_ID = 335544320,
				CLUB_ID = @CLUB_INDEX,
				CHARACTER_ID = @CHAR_INDEX, MASCOT_ID = @MASCOT_INDEX
		WHERE UID = @UID
		
		
		SET @CODE = 1;
	END ELSE BEGIN
		SET @CODE = 5;
	END

	SELECT @CODE AS CODE

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GAME_LOGIN]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GAME_LOGIN]
	@USERID VARCHAR(20),
	@UID INT,
	@Code1 VARCHAR(10),
	@Code2 VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (  
				SELECT 1 
			   FROM 	[dbo].Pangya_Member
			   WHERE	UID = @UID 
				 AND		Username = @USERID COLLATE Latin1_General_CS_AS 
				 AND 		AuthKey_Login = @Code1 COLLATE Latin1_General_CS_AS 
				 AND 		AuthKey_Game = @Code2 COLLATE Latin1_General_CS_AS
			)
	 BEGIN
		SELECT 1 AS	Code,
					A.Username,
					A.Nickname,
					A.Sex,
					A.Capabilities,
					C.LockerPwd,
					-- A.GUILDINDEX,
					B.Game_Level,
					B.Game_Point,
					-- B.Pang,
					C.CookieAmt AS Cookie,
					C.PangLockerAmt
		FROM [dbo].Pangya_Member A
		INNER JOIN [DBO].Pangya_User_Statistics B
		ON B.UID = A.UID
		INNER JOIN [DBO].Pangya_Personal C
		ON C.UID = A.UID
		WHERE A.UID = @UID
		-- UPDATE
		UPDATE [dbo].Pangya_Member SET Logon = 1, LogonCount = LogonCount + 1, LastLogonTime = GETDATE() WHERE UID = @UID
	END ELSE BEGIN
		SELECT 0 AS Code
	END
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GAME_LOGOUT]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GAME_LOGOUT]
	@UID INT
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE [dbo].Pangya_Member SET Logon = 0 WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ACHIEVEMENT]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GET_ACHIEVEMENT] 
	@UID INT
AS
BEGIN
   SET NOCOUNT ON;

   SELECT ID,TypeID FROM [DBO].Pangya_Achievement WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_COUNTER]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GET_COUNTER] 
	@UID INT
AS
BEGIN
	SET NOCOUNT ON;

	 SELECT ID, TypeID, Quantity From [DBO].Pangya_Achievement_Counter WHERE UID = @UID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUILD_ACTION]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GUILD_ACTION] 
   @UID INT
   ,@GUILDID INT
   ,@GUILDACTION INT
   ,@GUILDVALUE INT = 0
   ,@GUILDVALUE2 INT = 0
   ,@GUILDVALUE3 VARCHAR(255) = ''
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StatCode INT = -1

    /********************
    1. Accept Player
      0 = Succesfully Accept Player
      1 = Unsuccessfully Accept Player
      2 = You are not guild master
    ********************/
   IF (@GUILDACTION = 1)
	 BEGIN
	    IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN (1,2))
	    BEGIN
		  -- BEGIN TRAN
		  BEGIN TRANSACTION
		  BEGIN TRY
			UPDATE [dbo].Pangya_Guild_Member SET GUILD_POSITION = 3 WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @GUILDVALUE AND GUILD_POSITION = 9
			IF (@@ROWCOUNT > 0)
			BEGIN
			   -- INSERT LOG 3 = ACCPET
			   INSERT INTO [DBO].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION)
			   SELECT @GUILDVALUE, GUILD_INDEX, GUILD_NAME, 3 FROM [DBO].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
			   UPDATE [DBO].Pangya_Member SET GUILDINDEX = @GUILDID WHERE UID = @GUILDVALUE
			   SET @StatCode = 0
			END ELSE BEGIN
			   SET @StatCode = 1
			END
			-- COMMIT TRAN
			COMMIT TRANSACTION
		  END TRY
		  BEGIN CATCH
			-- ROLLBACK TRAN
			ROLLBACK TRANSACTION
			SET @StatCode = 1
		  END CATCH
	    END ELSE BEGIN
		  SET @StatCode = 2
	    END
   END

   /********************
    2. Kick Player
      0 = Successfully Delete
      1 = Unsuccessfully Delete
      2 = Player is not Master
   ********************/
   IF (@GUILDACTION = 2)
   BEGIN
       IF EXISTS(SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN (1,2))
       BEGIN
	    -- BEGIN TRAN
	    BEGIN TRANSACTION
	    BEGIN TRY
		  DELETE [DBO].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @GUILDVALUE AND GUILD_POSITION IN (2,3,9)
		  IF (@@ROWCOUNT > 0)
		  BEGIN
		    UPDATE [DBO].Pangya_Member SET GUILDINDEX = 0 WHERE UID = @GUILDVALUE
		    IF (@@ROWCOUNT > 0)
		    BEGIN 
			  -- INSERT LOG 6 = KICK
			  INSERT INTO [DBO].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION)
			  SELECT @GUILDVALUE, GUILD_INDEX, GUILD_NAME, 6 FROM [DBO].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1

			  SET @StatCode = 0
		    END ELSE BEGIN
			  SET @StatCode = 1
		    END
		  END ELSE BEGIN
		    SET @StatCode = 1
		  END
		  -- COMMIT
		  COMMIT TRANSACTION
	    END TRY
	    BEGIN CATCH
		  -- ROLLBACK
		  ROLLBACK TRANSACTION
		  SET @StatCode = 1
	    END CATCH
       END ELSE BEGIN
         SET @StatCode = 2
       END
    END

    /********************
    3. Promote Player
      0 = Successfully Promote
      8 = Unsuccessfully Promote
      9 = You are not admin
      10 = Bad Value
    ********************/
    IF (@GUILDACTION = 3)
    BEGIN
       IF NOT @GUILDVALUE2 IN (2,3)
       BEGIN
         SET @StatCode = 10
       END ELSE BEGIN
         IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION = 1)
         BEGIN
		 -- BEGIN TRAN
		 BEGIN TRANSACTION
		 BEGIN TRY
		    UPDATE [dbo].Pangya_Guild_Member SET GUILD_POSITION = @GUILDVALUE2 WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @GUILDVALUE AND GUILD_POSITION IN (2,3) 
		    IF (@@ROWCOUNT > 0)
		    BEGIN
			  -- INSERT LOG 0C=Secondary Admin, 0D=As Member
			  INSERT INTO [DBO].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION)
			  SELECT @GUILDVALUE, GUILD_INDEX, GUILD_NAME, 
			  CASE WHEN @GUILDVALUE2 = 2 THEN 12 WHEN @GUILDVALUE2 = 3 THEN 13 ELSE 0 END
			  FROM [DBO].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1

			  SET @StatCode = 0
		    END ELSE BEGIN
			  SET @StatCode = 8
		    END
		    -- COMMIT
		    COMMIT TRANSACTION
		  END TRY
		  BEGIN CATCH
			-- ROLLBACK
			ROLLBACK TRANSACTION
			SET @StatCode = 8
		  END CATCH
         END ELSE BEGIN
           SET @StatCode = 9
         END
       END
    END

    /********************
    4. Change Intro
      0 = Successfully Change
      8 = Unsuccessfully Change
      9 = You are not admin
	 * This is not need to be in transaction because it's less important
    ********************/
    IF (@GUILDACTION = 4)
     BEGIN
       IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN(1,2)) AND LEN(@GUILDVALUE3) > 0
       BEGIN
         UPDATE [DBO].Pangya_Guild_Info SET GUILD_INTRODUCING = @GUILDVALUE3 WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
         IF (@@ROWCOUNT > 0)
         BEGIN
           SET @StatCode = 0
         END ELSE BEGIN
           SET @StatCode = 8
         END
       END ELSE BEGIN
         SET @StatCode = 9
       END
    END

    /********************
    5. Change Notice
      0 = Successfully Change
      8 = Unsuccessfully Change
      9 = You are not admin
	 * This is not need to be in transaction because it's less important
    ********************/
    IF (@GUILDACTION = 5)
    BEGIN
       IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN(1,2)) AND LEN(@GUILDVALUE3) > 0
       BEGIN
         UPDATE [DBO].Pangya_Guild_Info SET GUILD_NOTICE = @GUILDVALUE3 WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
         IF (@@ROWCOUNT > 0)
         BEGIN
           SET @StatCode = 0
         END ELSE BEGIN
           SET @StatCode = 8
         END
       END ELSE BEGIN
         SET @StatCode = 9
       END
    END

    /********************
    * 6. Change Player Intro
	    0 = Successfully Change
	    8 = Unsuccessfully Change
	    * This is not need to be in transaction because it's less important  
    ********************/
    IF (@GUILDACTION = 6)
    BEGIN
       IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN(1,2)) AND LEN(@GUILDVALUE3) > 0
       BEGIN
	    UPDATE DBO.Pangya_Guild_Member SET GUILD_MESSAGE = @GUILDVALUE3 WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @GUILDVALUE
         IF (@@ROWCOUNT > 0)
         BEGIN
           SET @StatCode = 0
         END ELSE BEGIN
           SET @StatCode = 8
         END
       END ELSE BEGIN
         SET @StatCode = 9
       END
    END

    /********************
    * 7. Player Leave Guild
	    0 = Successfully Change
	    8 = Unsuccessfully Change
   ********************/
    IF (@GUILDACTION = 7)
    BEGIN
       IF EXISTS (SELECT 1 FROM [DBO].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN (2,3))
       BEGIN
	    -- BEGIN TRAN
	    BEGIN TRANSACTION
	    BEGIN TRY
		  DELETE FROM [DBO].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION IN (2,3)
		  IF (@@ROWCOUNT > 0)
		  BEGIN
		    UPDATE [DBO].Pangya_Member SET GUILDINDEX = 0 WHERE UID = @UID
		    IF (@@ROWCOUNT > 0) BEGIN
			  -- INSERT LOG 6 = LEAVE
			  INSERT INTO [DBO].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION)
			  SELECT @UID, GUILD_INDEX, GUILD_NAME, 7 FROM [DBO].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
			  SET @StatCode = 0
		    END ELSE BEGIN
			  SET @StatCode = 8
		    END
		  END ELSE BEGIN
		    SET @StatCode = 8
		  END
		  -- COMMIT
		  COMMIT TRANSACTION
	    END TRY
	    BEGIN CATCH
		  -- ROLLBACK
		  ROLLBACK TRANSACTION
		  SET @StatCode = 8
	    END CATCH
       END ELSE BEGIN
         SET @StatCode = 8
       END
    END

    -- SELECT RESULT CODE
    SELECT @StatCode AS CODE 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUILD_CANCELJOIN]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================♠=====================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GUILD_CANCELJOIN] 
   @UID INT,
   @GUILDID INT
AS
BEGIN
   SET NOCOUNT ON;

   -- CODE = 10 NOT WAITING FOR ACCEPT OR PLAYER IS ALREADY A MEMBER IN GUILD
   -- CODE = 9 GUILD IS NOT EXISTING
   -- CODE = 8 PLAYER IS NOT IN GUILD
   -- CODE = 0 Successfully cancelled joined
   -- CODE = 2 UNKNOWN ERROR IN TRANSACTION

   DECLARE @StatCode INT
   DECLARE @FGNAME VARCHAR(32) -- GUILD NAME

   IF NOT EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1) BEGIN
	 SET @StatCode = 9
   END ELSE BEGIN
	 IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID) BEGIN
	    -- GET GUILD NAME
	    SELECT @FGNAME = GUILD_NAME FROM [dbo].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
	    -- BEGIN TRAN
	    BEGIN TRANSACTION
	    BEGIN TRY
		  -- DELETE INTO GUILD MEMBER
		  DELETE [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION = 9
		  IF @@ROWCOUNT > 0 BEGIN
			-- INSERT TO LOG
			INSERT INTO [dbo].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION) VALUES(@UID, @GUILDID, @FGNAME, 2)
			-- SELECT ALL
			SET @StatCode = 0
		  END ELSE BEGIN
			SET @StatCode = 10
		  END
		  -- COMMIT
		  COMMIT TRANSACTION
	    END TRY
	    BEGIN CATCH
		  -- ROLLBACK
		  ROLLBACK TRANSACTION
		  SET @StatCode = 2
	    END CATCH
	 END ELSE BEGIN
	    SET @StatCode = 8
	 END
   END

   -- SELECT CODE RESULT
   SELECT @StatCode AS CODE 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUILD_CREATE]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GUILD_CREATE] 
   @UID INT,
   @GUILDNAME VARCHAR(32),
   @GUILDINTRO VARCHAR(255)
AS
BEGIN
   SET NOCOUNT ON;
   -- CODE 10 = PLAYER IS IN GUILD
   -- CODE 0 = SUCCESSFULLY CREATED GUILD
   -- CODE 9 = GUILD NAME IS ALREADY EXISTED
   -- CODE 2 = TRANSACTION ERROR

   DECLARE @GUILDINDEX INT
   DECLARE @GUILDINDEX_OUTPUT INT
   DECLARE @StatCode TINYINT

   SET @GUILDNAME = LTRIM(RTRIM(@GUILDNAME))

   IF EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Info WHERE GUILD_NAME = @GUILDNAME AND GUILD_VALID = 1) BEGIN
    SET @StatCode = 9
   END ELSE BEGIN
       IF NOT EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_MEMBER_UID = @UID) BEGIN
         -- START TRAN
         BEGIN TRANSACTION
         BEGIN TRY
           -- INSERT INTO GUILD LIST
           INSERT INTO [dbo].Pangya_Guild_Info(GUILD_NAME, GUILD_INTRODUCING, GUILD_LEADER_UID) VALUES(@GUILDNAME, @GUILDINTRO, @UID)
           -- GET GUILD INDEX
           SET @GUILDINDEX_OUTPUT = SCOPE_IDENTITY()
           -- UPDATE TO PROFILE
           UPDATE [dbo].Pangya_Member SET GUILDINDEX = @GUILDINDEX_OUTPUT WHERE UID = @UID
           -- INSERT INTO GUILD MEMBER
           INSERT INTO [dbo].Pangya_Guild_Member(GUILD_ID, GUILD_MEMBER_UID, GUILD_POSITION) VALUES(@GUILDINDEX_OUTPUT, @UID, 1 /* 1 = Admin */)
		   -- INSERT EMBLEM
		   INSERT INTO [dbo].Pangya_Guild_Emblem(GUILD_ID, GUILD_MARK_IMG) VALUES (@GUILDINDEX_OUTPUT, 'GUILDMARK')
           -- INSERT INTO LOG
           INSERT INTO [dbo].Pangya_Guild_Log(UID, GUILD_ID, GUILD_NAME, GUILD_ACTION) VALUES (@UID, @GUILDINDEX_OUTPUT, @GUILDNAME, 8)
           -- OUTPUT
           SET @StatCode = 0
           -- COMMIT TRAN
           COMMIT TRANSACTION
         END TRY
         BEGIN CATCH
           -- ROLLBACK
           ROLLBACK TRANSACTION
           SET @StatCode = 2
         END CATCH
       END ELSE BEGIN
         -- CODE = 10 KNOWN AS PLAYER IS ALREADY IN THE GUILD
         SET @StatCode = 10
       END
   END

   -- SELECT RESULT
   SELECT @StatCode AS CODE

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUILD_EMBLEM]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GUILD_EMBLEM] 
	@UID INT,
	@GUILDID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @GuildEmblem VARCHAR(20)

	-- 1 = Succesfully
	-- 2 = Unsuccesfully

	 IF EXISTS ( SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_ID = @GUILDID AND GUILD_MEMBER_UID = @UID AND GUILD_POSITION = 1 )
	 BEGIN

	    WHILE (1=1) BEGIN  
		  SELECT @GuildEmblem = LOWER(LEFT(NEWID(), 8))  
		  IF NOT EXISTS(SELECT 1 FROM DBO.Pangya_Guild_Emblem WHERE GUILD_ID = @GUILDID AND GUILD_MARK_IMG = @GuildEmblem) BEGIN   
			BREAK  
		  END   
	    END

	    UPDATE [dbo].Pangya_Guild_Emblem SET GUILD_MARK_IMG = @GuildEmblem WHERE GUILD_ID = @GUILDID
	    IF (@@ROWCOUNT > 0)
	    BEGIN
		  SELECT CODE = 1, EMBLEM_IDX, GUILD_MARK_IMG FROM [DBO].Pangya_Guild_Emblem WHERE GUILD_ID = @GUILDID  
	    END ELSE BEGIN
		  SELECT CODE = 2
	    END

	 END ELSE BEGIN
	    SELECT CODE = 2  
	 END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUILD_JOIN]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_GUILD_JOIN]
    @UID INT
   ,@GUILDID INT
    ,@INTRO VARCHAR(255)
AS
BEGIN
   SET NOCOUNT ON;

   -- CODE 9 = GUILD NOT FOUND
   -- CODE 8 = PLAYER IS IN GUILD
   -- CODE 0 = SUCCESSFULLY JOINED TO GUILD
   -- CODE 10 = WAIT 24
   -- CODE 2 = TRAN ERROR
   DECLARE @FGNAME VARCHAR(32) -- GUILD NAME
   DECLARE @StatCode TINYINT

   IF EXISTS (SELECT TOP 1 1 FROM [DBO].Pangya_Guild_Log WHERE UID = @UID AND GUILD_ACTION IN (7,9) AND GUILD_ACTION_DATE >= DATEADD(HOUR, -24, GETDATE()))
   BEGIN
    SET @StatCode = 10   
   END ELSE IF NOT EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1)
   BEGIN
    SET @StatCode = 9
   END ELSE
   BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].Pangya_Guild_Member WHERE GUILD_MEMBER_UID = @UID )
    BEGIN
       -- GET GUILD DATA
       SELECT @FGNAME = GUILD_NAME FROM [dbo].Pangya_Guild_Info WHERE GUILD_INDEX = @GUILDID AND GUILD_VALID = 1
       -- BEGIN TRAN
       BEGIN TRANSACTION
       BEGIN TRY
         -- INSERT INTO GUILD MEMBER
         INSERT INTO [dbo].Pangya_Guild_Member (GUILD_ID ,GUILD_MEMBER_UID ,GUILD_POSITION ,GUILD_MESSAGE) VALUES (@GUILDID ,@UID ,9 ,@INTRO)
         -- INSERT TO LOG
         INSERT INTO [dbo].Pangya_Guild_Log (UID, GUILD_ID ,GUILD_NAME ,GUILD_ACTION) VALUES (@UID, @GUILDID, @FGNAME, 1)
	    -- SUCESS
	    SET @StatCode = 0
         -- COMMIT
         COMMIT TRANSACTION
       END TRY
       BEGIN CATCH
         -- ROLLBACK
         ROLLBACK TRANSACTION
         SET @StatCode = 2
       END CATCH
    END ELSE
    BEGIN
       SET @StatCode = 8
    END
   END

   -- SELECT RESULT
   SELECT @StatCode AS CODE
END
GO
/****** Object:  StoredProcedure [dbo].[USP_INVEN_POP]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INVEN_POP]
	@UID INT,
	@INV_ID INT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @ITEM_FROM_ID INT = 0
	DECLARE @SUC_COUNT TINYINT = 0
	DECLARE @ERROR INT;
	IF EXISTS (
							SELECT	1
							FROM 		DBO.Pangya_Locker_Item 
							WHERE 	UID = @UID 
							AND 		INVEN_ID = @INV_ID 
							AND 		VALID = 1
	) BEGIN
	
		SELECT 	@ITEM_FROM_ID = FROM_ID
		FROM 		DBO.Pangya_Locker_Item
		WHERE 	UID = @UID
		AND 		INVEN_ID = @INV_ID
		AND 		VALID = 1
		
		IF (@ITEM_FROM_ID IS NOT NULL) AND (@ITEM_FROM_ID > 0) BEGIN
		
			BEGIN TRANSACTION
			BEGIN TRY
			
				UPDATE 	DBO.Pangya_Warehouse 
				SET 		VALID = 1
				WHERE 	UID = @UID
				AND			ITEM_ID = @ITEM_FROM_ID
				AND			VALID = 0
				
				IF (@@ROWCOUNT > 0) 
					SET @SUC_COUNT = @SUC_COUNT + 1
				
				UPDATE	DBO.Pangya_Locker_Item
				SET			VALID = 0
				WHERE		UID = @UID
				AND			INVEN_ID = @INV_ID
				AND			VALID = 1
				
				IF (@@ROWCOUNT > 0) 
					SET @SUC_COUNT = @SUC_COUNT + 1
				
				IF (@@ERROR = 0) AND (@SUC_COUNT = 2) BEGIN
					COMMIT TRANSACTION
					
					-- SET SUC
					SET @ERROR = 0
					-- RETURN ITEM DATA
					SELECT	@ERROR AS ERROR, A.ITEM_ID
										, A.TYPEID
										, A.C0
										, A.C1
										, A.C2
										, A.C3
										, A.C4
										-- , A.RegDate
										, A.DateEnd
										-- , A.ITEMTYPE
										, A.FLAG
										, B.UCC_UNIQE
										, B.UCC_STATUS
										, B.UCC_NAME
										, B.UCC_DRAWER_UID
										, B.UCC_DRAWER_NICKNAME
										, B.UCC_COPY_COUNT
					FROM			DBO.Pangya_Warehouse A
					OUTER APPLY (
						SELECT 	X.UCC_UNIQE
										, X.UCC_STATUS
										, X.UCC_NAME
										, X.UCC_DRAWER AS UCC_DRAWER_UID
										, X.UCC_COPY_COUNT
										, Y.NICKNAME AS UCC_DRAWER_NICKNAME
						FROM		DBO.Pangya_SelfDesign X
						LEFT JOIN DBO.Pangya_Member Y
						ON Y.UID = X.UCC_DRAWER
						WHERE		X.ITEM_ID = A.ITEM_ID
					) B
					WHERE			A.UID = @UID
					AND				A.item_id = @ITEM_FROM_ID
					AND				A.VALID = 1
					
					
				END ELSE BEGIN
					ROLLBACK TRANSACTION
					SET @ERROR = 1
					SELECT @ERROR AS ERROR
				END
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
			SET @ERROR = 1
					SELECT @ERROR AS ERROR
			END CATCH
		END ELSE BEGIN
			SET @ERROR = 1
					SELECT @ERROR AS ERROR
		END
	END ELSE BEGIN
		SET @ERROR = 1
					SELECT @ERROR AS ERROR
	END
	
END


GO
/****** Object:  StoredProcedure [dbo].[USP_INVEN_PUSH]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INVEN_PUSH]
	@UID INT,
	@TYPEID INT,
	@NAME VARCHAR(255),
	@FROM_ID INT
AS
BEGIN
	SET NOCOUNT ON
	
	BEGIN TRANSACTION
	BEGIN TRY
	
		INSERT INTO DBO.Pangya_Locker_Item(UID, TypeID, Name, FROM_ID) 
		VALUES (@UID, @TYPEID, @NAME, @FROM_ID)
		
		UPDATE 	DBO.Pangya_Warehouse
		SET			VALID = 0
		WHERE 	item_id = @FROM_ID
		AND			VALID = 1
		
		IF (@@ROWCOUNT > 0) BEGIN
			COMMIT TRANSACTION
			SELECT 0 AS CODE
		END ELSE BEGIN
			ROLLBACK TRANSACTION
			SELECT 1 AS CODE
		END
		
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 1 AS CODE
	END CATCH
	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LOGIN_SERVER]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_LOGIN_SERVER]
  @User VARCHAR(20),
  @Pwd VARCHAR(20),
  @IPAddress VARCHAR(20),
  @Auth1 VARCHAR(7),
  @Auth2 VARCHAR(7)
  AS
BEGIN
  SET NOCOUNT ON;

  IF EXISTS(SELECT 1 FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS)
  BEGIN
    IF EXISTS(SELECT 1 FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS AND Password = @Pwd COLLATE Latin1_General_CS_AS)
    BEGIN
      SELECT 1 AS CODE, UID, Nickname, FirstSet, IDState, Logon FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS
	 -- UPDATE KEY TO USE IN GAME SERVER
      UPDATE [dbo].Pangya_Member SET AuthKey_Login = @Auth1, AuthKey_Game = @Auth2, IPAddress = @IPAddress 
	 WHERE Username = @User COLLATE Latin1_General_CS_AS 
    END ELSE
    BEGIN
      -- CASE OF PASSWORD ERROR
      SELECT CODE = 6;
    END
  END ELSE
  BEGIN
    -- CASE OF USERNAME NOT FOUND
    SELECT CODE = 5;
  END

END
GO
/****** Object:  StoredProcedure [dbo].[USP_LOGIN_SERVER_US]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[USP_LOGIN_SERVER_US]
  @User VARCHAR(20),
  @Pwd VARCHAR(150),
  @IPAddress VARCHAR(20),
  @Auth1 VARCHAR(7),
  @Auth2 VARCHAR(7)
  AS
BEGIN
  SET NOCOUNT ON;

  IF EXISTS(SELECT 1 FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS)
  BEGIN
    IF EXISTS(SELECT 1 FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS AND SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5', Password)), 3, 32) = @Pwd)
    BEGIN
      SELECT 1 AS CODE, UID, Nickname, FirstSet, IDState, Logon FROM [dbo].Pangya_Member WHERE Username = @User COLLATE Latin1_General_CS_AS
	 -- UPDATE KEY TO USE IN GAME SERVER
      UPDATE [dbo].Pangya_Member SET AuthKey_Login = @Auth1, AuthKey_Game = @Auth2, IPAddress = @IPAddress 
	 WHERE Username = @User COLLATE Latin1_General_CS_AS 
    END ELSE
    BEGIN
      -- CASE OF PASSWORD ERROR
      SELECT CODE = 6;
    END
  END ELSE
  BEGIN
    -- CASE OF USERNAME NOT FOUND
    SELECT CODE = 5;
  END

END

GO
/****** Object:  StoredProcedure [dbo].[USP_MAIL_UPDATE]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_MAIL_UPDATE] 
	@UID INT,
	@ITEMSTR VARCHAR(5000)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @MailIndex VARCHAR(20)
	DECLARE @ItemTypeID VARCHAR(20)
	DECLARE @ItemAddedIndex VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s  ^%s ', @MailIndex OUTPUT, @ItemTypeID OUTPUT, @ItemAddedIndex OUTPUT
		-- UPDATE ITEM		
	UPDATE DBO.Pangya_Mail SET	ReceiveDate = GETDATE(),ReadDate = GETDATE()
	WHERE UID = @UID AND Mail_Index = @MailIndex

	UPDATE Pangya_Mail_Item SET	RELEASE_DATE = GETDATE(), APPLY_ITEM_ID = @ItemAddedIndex	
	WHERE Mail_Index = @MailIndex AND TYPEID = @ItemTypeID

		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_MESSENGER_1PLAYER_GUILD]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MESSENGER_1PLAYER_GUILD]
  @UID INT,
  @GUILDID INT
  AS
BEGIN
  SET NOCOUNT ON;

  -- This procedure uses for get only one player information

  SELECT A.GUILD_MEMBER_UID,
         A.GUILD_ID,
         B.Username,
         B.Nickname,
         C.Game_Level,
         B.Logon
  FROM (
         SELECT GUILD_ID,
                GUILD_MEMBER_UID
         FROM [DBO].Pangya_Guild_Member
         WHERE GUILD_ID = @GUILDID AND
               GUILD_MEMBER_UID = @UID AND
               GUILD_POSITION IN (1, 2, 3)
       ) A
       INNER JOIN [DBO].Pangya_Member B ON B.UID = A.GUILD_MEMBER_UID
       INNER JOIN [DBO].Pangya_User_Statistics C ON C.UID = A.GUILD_MEMBER_UID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MESSENGER_LOGIN]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MESSENGER_LOGIN]
	@UID INT,
    @NICKNAME VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
    
    -- Login
    -- Nickname
    -- UID
    -- GUILD ID
    
   	DECLARE @USERNAME VARCHAR(50)
    DECLARE @USER_NICKNAME VARCHAR(50)
    DECLARE @USER_UID INT
    DECLARE @GUILD_ID INT
    
	SELECT 	@USERNAME 		= Username,
    		@USER_NICKNAME 	= Nickname,
            @USER_UID 		= UID,
            @GUILD_ID 		= GUILDINDEX
    FROM [DBO].Pangya_Member
    WHERE UID = @UID AND Nickname = @Nickname
    
    IF (@USER_UID > 0)
    BEGIN
    	SELECT Code = 0, @USERNAME AS Username, @USER_NICKNAME AS Nickname, @USER_UID AS UID, @GUILD_ID AS GUILD_ID
        
        /* GET FRIEND LIST */
        SELECT TOP 50
               B.Nickname,
               C.Game_Level,
               A.Memo,
               B.UID,
               A.IsAccept,
               B.Sex,
               A.IsAgree,
               B.Logon,
               A.IsBlock,
			   0 AS Status,
               1 AS Position
        FROM (
               SELECT DISTINCT Friend,
                      Owner,
                      IsAccept,
                      -- GroupName,
                      IsAgree,
                      Memo,
                      ISNULL(IsBlock, 'N') AS IsBlock
               FROM [DBO].Pangya_Friend
               WHERE Owner = @USERNAME AND
                     IsDeleted = 0
             ) A
             INNER JOIN DBO.Pangya_Member B ON B.Username = A.Friend
             INNER JOIN DBO.Pangya_User_Statistics C ON C.UID = B.UID
		/* END GET FRIEND LIST */
        
        /* GET GUILD PLAYER LIST */
        SELECT
        	B.Nickname,
            C.Game_Level,
            Memo = '',
            A.GUILD_MEMBER_UID AS UID,
            IsAccept = 0,
            B.Sex,
            IsAgree = 0,
            B.Logon,
            IsBlock = 0,
            0 AS Status,
            2 AS Position
        FROM (
        	SELECT GUILD_MEMBER_UID
            FROM DBO.Pangya_Guild_Member
            WHERE GUILD_ID = @GUILD_ID AND
            GUILD_POSITION IN (1,2,3) 
        ) A
        INNER JOIN [DBO].Pangya_Member B ON B.UID = A.GUILD_MEMBER_UID
        INNER JOIN [DBO].Pangya_User_Statistics C ON C.UID = A.GUILD_MEMBER_UID
        /* END */
    END ELSE
    BEGIN
    	SELECT Code = 10
    END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MESSENGER_PLAYER_GUILD]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MESSENGER_PLAYER_GUILD]
  @UID INT
  AS
BEGIN
  SET NOCOUNT ON;

  -- This procedure uses to get information of player except who calls

  DECLARE @GUILD_INDEX INT = 0
  
  SELECT @GUILD_INDEX = GUILDINDEX
  FROM [DBO].Pangya_Member
  WHERE UID = @UID
  
  SELECT @GUILD_INDEX AS GUILD_ID
  
  SELECT A.GUILD_MEMBER_UID,
         A.GUILD_ID,
         B.Username,
         B.Nickname,
         C.Game_Level,
         B.Logon
  FROM (
         SELECT GUILD_ID,
                GUILD_MEMBER_UID
         FROM [DBO].Pangya_Guild_Member
         WHERE GUILD_ID = @GUILD_INDEX AND
         GUILD_POSITION IN (1,2,3) AND
         GUILD_MEMBER_UID != @UID
       ) A
       INNER JOIN [DBO].Pangya_Member B ON B.UID = A.GUILD_MEMBER_UID
       INNER JOIN [DBO].Pangya_User_Statistics C ON C.UID = A.GUILD_MEMBER_UID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_NICKNAME_CHECK]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_NICKNAME_CHECK]
	@NICKNAME VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @REP_NICKNAME VARCHAR(32)
	SELECT	@REP_NICKNAME = LTRIM(RTRIM(@NICKNAME))

	IF EXISTS ( SELECT 1
				FROM ( SELECT NICKNAME
						FROM DBO.Pangya_Member
						WHERE Nickname = @REP_NICKNAME COLLATE Latin1_General_CS_AS) C
			) BEGIN
		SELECT Code = 2
	END ELSE BEGIN
		SELECT Code = 1
	END


END
GO
/****** Object:  StoredProcedure [dbo].[USP_QUEST_ALTER]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_QUEST_ALTER] 
	@UID INT,
	@CounterTypeID INT,
	@AddQuantity INT
AS
BEGIN
   SET NOCOUNT ON;
   DECLARE @@AddQuantity INT
   DECLARE @QUEST_INDEX INT
   DECLARE @INDEX INT
   DECLARE @COUNTER_TYPEID INT
   DECLARE @COUNTERQTY INT
   DECLARE @QUEST_TYPEID INT

   DECLARE @S_COUNTER_TYPEID INT
   DECLARE @S_COUNTER_QUANTITY INT
   DECLARE @S_COUNTER_ID INT
   DECLARE @S_ACHIEVEMENT_TYPEID INT
   DECLARE @S_ACHIEVEMENT_ID INT

   DECLARE @LAST_INSERT INT

   IF (OBJECT_ID('TEMPDB..#QUEST_UPDATE') IS NOT NULL)
   BEGIN
	 DROP TABLE #QUEST_UPDATE
   END
   IF (OBJECT_ID('TEMPDB..#COUNTER_UPDATE') IS NOT NULL)
   BEGIN
	 DROP TABLE #COUNTER_UPDATE
   END
   IF (OBJECT_ID('TEMPDB..#ACHIEVEMENT_TRIGGER') IS NOT NULL)
   BEGIN
	 DROP TABLE #ACHIEVEMENT_TRIGGER
   END
   CREATE TABLE #QUEST_UPDATE (ID INT IDENTITY(1,1), QUEST_INDEX INT, QUEST_TYPEID INT, COUNTER_TYPEID INT, COUNTER_QTY INT)
   CREATE TABLE #COUNTER_UPDATE (COUNTER_ID INT,COUNTER_TYPEID INT, COUNTER_OLD INT, COUNTER_NEW INT, COUNTER_ADD INT)
   CREATE TABLE #ACHIEVEMENT_TRIGGER (ACHIEVEMENT_TYPEID INT, QUEST_TYPEID INT)

   SELECT 
	 @S_COUNTER_ID			= A.ID, 
	 @S_COUNTER_TYPEID		= A.TypeID, 
	 @S_COUNTER_QUANTITY	= A.Quantity, 
	 @S_ACHIEVEMENT_TYPEID	= C.TypeID,
	 @S_ACHIEVEMENT_ID		= C.ID
   FROM [DBO].Pangya_Achievement_Counter A
   LEFT JOIN [DBO].Pangya_Achievement_Quest B
   ON B.Counter_Index = A.ID
   LEFT JOIN [DBO].Pangya_Achievement C
   ON C.ID = B.Achievement_Index
   WHERE A.UID = @UID AND A.TypeID = @CounterTypeID

   IF (@S_COUNTER_ID IS NOT NULL) AND (@S_COUNTER_ID > 0)
   BEGIN
	 UPDATE [DBO].Pangya_Achievement_Counter SET Quantity += @AddQuantity WHERE UID = @UID AND TypeID = @CounterTypeID

	 INSERT INTO #COUNTER_UPDATE(COUNTER_ID, COUNTER_TYPEID, COUNTER_OLD, COUNTER_NEW, COUNTER_ADD) 
	 VALUES (@S_COUNTER_ID, @S_COUNTER_TYPEID, @S_COUNTER_QUANTITY, (@S_COUNTER_QUANTITY + @AddQuantity) , @AddQuantity)
	 
	 INSERT INTO #QUEST_UPDATE(QUEST_INDEX, QUEST_TYPEID,COUNTER_TYPEID, COUNTER_QTY)
	 SELECT B.ID, B.Achivement_Quest_TypeID,A.TypeID, B.Count
	 FROM [DBO].Pangya_Achievement_Counter A
	 LEFT JOIN [DBO].Pangya_Achievement_Quest B
	 ON B.Counter_Index = A.ID
	 WHERE A.UID = @UID AND A.TypeID = @CounterTypeID AND A.Quantity >= B.Count AND B.SuccessDate IS NULL

	 WHILE EXISTS (SELECT 1 FROM #QUEST_UPDATE)
	 BEGIN
	    SELECT TOP 1 
	    @INDEX = ID, 
	    @QUEST_INDEX = QUEST_INDEX, 
	    @QUEST_TYPEID = QUEST_TYPEID,
	    @COUNTER_TYPEID = COUNTER_TYPEID, 
	    @COUNTERQTY = COUNTER_QTY 
	    FROM #QUEST_UPDATE

	    INSERT INTO [DBO].Pangya_Achievement_Counter(UID, TypeID, Quantity) VALUES (@UID, @COUNTER_TYPEID, @COUNTERQTY)
	    SET @LAST_INSERT = SCOPE_IDENTITY()

	    UPDATE [DBO].Pangya_Achievement_Quest SET Counter_Index = @LAST_INSERT, SuccessDate = GETDATE() WHERE UID = @UID AND ID = @QUEST_INDEX

	    INSERT INTO #COUNTER_UPDATE(COUNTER_ID, COUNTER_TYPEID, COUNTER_OLD, COUNTER_NEW, COUNTER_ADD) 
	    VALUES (@LAST_INSERT, @COUNTER_TYPEID, 0, @COUNTERQTY , @COUNTERQTY)

	    INSERT INTO #ACHIEVEMENT_TRIGGER(ACHIEVEMENT_TYPEID, QUEST_TYPEID) VALUES (@S_ACHIEVEMENT_TYPEID, @QUEST_TYPEID)

	    DELETE FROM #QUEST_UPDATE WHERE ID = @INDEX
	 END
   END

   SELECT * FROM #COUNTER_UPDATE
   SELECT * FROM #ACHIEVEMENT_TRIGGER
   SELECT @S_ACHIEVEMENT_TYPEID AS ACHIEVEMENT_TYPEID, @S_ACHIEVEMENT_ID AS ACHIEVEMENT_ID

   SELECT 
	 A.TypeID AS AchTypeID, 
	 A.ID AS AchID, 
	 B.Achivement_Quest_TypeID, 
	 C.TypeID AS CounterTypeID, 
	 C.ID AS CounterID, 
	 C.Quantity, 
	 B.SuccessDate, 
	 B.Count
   FROM
   (SELECT TypeID, ID FROM [DBO].Pangya_Achievement WHERE UID = @UID AND ID = @S_ACHIEVEMENT_ID) A
   OUTER APPLY (
	 SELECT Achievement_Index, Counter_Index, Count,Achivement_Quest_TypeID,SuccessDate 
	 FROM [DBO].Pangya_Achievement_Quest 
	 WHERE Achievement_Index = A.ID
   ) B
   OUTER APPLY (
	 SELECT TypeID, ID, Quantity FROM [DBO].Pangya_Achievement_Counter WHERE ID = B.Counter_Index
   ) C
   ORDER BY B.Achievement_Index ASC, B.Count ASC
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_CADDIE]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_SAVE_CADDIE] 
	@UID INT,
	@ITEMSTR VARCHAR(5000)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @CADDIE_IDX VARCHAR(20)
	DECLARE @SKIN_TYPEID VARCHAR(20)
	DECLARE @SKIN_DATE_END VARCHAR(20)
	DECLARE @CHECK_PAY VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s', @CADDIE_IDX OUTPUT , @SKIN_TYPEID OUTPUT, @SKIN_DATE_END OUTPUT, @CHECK_PAY OUTPUT  
		-- UPDATE ITEM
		UPDATE dbo.Pangya_Caddie SET	SKIN_TYPEID =ISNULL(@SKIN_TYPEID,0), 
										SKIN_END_DATE = Convert(date,ISNULL(@SKIN_DATE_END,GETDATE())), 
										TriggerPay = ISNULL(@CHECK_PAY, 0) 
										WHERE UID = @UID AND CID = @CADDIE_IDX
		SELECT @SKIN_DATE_END
		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_CARD]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		LuisMK
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_SAVE_CARD] 
	@UID INT,
	@ITEMSTR VARCHAR(5000)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @CARD_IDX VARCHAR(20)
	DECLARE @CARD_QTY VARCHAR(20)
	DECLARE @CARD_VALID VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ', @CARD_IDX OUTPUT , @CARD_QTY OUTPUT, @CARD_VALID OUTPUT
		-- UPDATE ITEM
		UPDATE DBO.Pangya_Card
		SET QTY = ISNULL(@CARD_QTY,0),
		VALID = ISNULL(@CARD_VALID,0)
		WHERE UID = @UID AND CARD_IDX = @CARD_IDX
		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_CARD_EQUIP]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		LuisMK
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_SAVE_CARD_EQUIP] 
	@UID INT,
	@ITEMSTR VARCHAR(5000)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @UNID VARCHAR(20)
	DECLARE @CARDTYPEID VARCHAR(20)
	DECLARE @ENDDATE VARCHAR(20)
	DECLARE @VALID VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s  ^%s ', @UNID OUTPUT , @CARDTYPEID OUTPUT,  @ENDDATE OUTPUT, @VALID OUTPUT
		-- UPDATE ITEM
		UPDATE DBO.Pangya_Card_Equip
		SET CARD_TYPEID = ISNULL(@CARDTYPEID,0),
		ENDDATE = ISNULL(ENDDATE,0),
		VALID = ISNULL(@VALID,0)
		WHERE UID = @UID AND ID = @UID
		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_CHARACTER_EQUIP]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[USP_SAVE_CHARACTER_EQUIP] 
	@UID INT,
	@EQUIPSTR VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TEMP TABLE (STRING VARCHAR(7000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(2000)
	DECLARE @sID INT
	-- UPDATE DETAIL
	DECLARE @CHARINDEX VARCHAR(20)	
	DECLARE @EQUIPTYPEID1 VARCHAR(20)
	DECLARE @EQUIPTYPEID2 VARCHAR(20)
	DECLARE @EQUIPTYPEID3 VARCHAR(20)
	DECLARE @EQUIPTYPEID4 VARCHAR(20)
	DECLARE @EQUIPTYPEID5 VARCHAR(20)
	DECLARE @EQUIPTYPEID6 VARCHAR(20)
	DECLARE @EQUIPTYPEID7 VARCHAR(20)
	DECLARE @EQUIPTYPEID8 VARCHAR(20)
	DECLARE @EQUIPTYPEID9 VARCHAR(20)
	DECLARE @EQUIPTYPEID10 VARCHAR(20)
	DECLARE @EQUIPTYPEID11 VARCHAR(20)
	DECLARE @EQUIPTYPEID12 VARCHAR(20)
	DECLARE @EQUIPTYPEID13 VARCHAR(20)
	DECLARE @EQUIPTYPEID14 VARCHAR(20)
	DECLARE @EQUIPTYPEID15 VARCHAR(20)
	DECLARE @EQUIPTYPEID16 VARCHAR(20)
	DECLARE @EQUIPTYPEID17 VARCHAR(20)
	DECLARE @EQUIPTYPEID18 VARCHAR(20)
	DECLARE @EQUIPTYPEID19 VARCHAR(20)
	DECLARE @EQUIPTYPEID20 VARCHAR(20)
	DECLARE @EQUIPTYPEID21 VARCHAR(20)
	DECLARE @EQUIPTYPEID22 VARCHAR(20)
	DECLARE @EQUIPTYPEID23 VARCHAR(20)
	DECLARE @EQUIPTYPEID24 VARCHAR(20)
	DECLARE @EQUIPINDEX1 VARCHAR(20)
	DECLARE @EQUIPINDEX2 VARCHAR(20)
	DECLARE @EQUIPINDEX3 VARCHAR(20)
	DECLARE @EQUIPINDEX4 VARCHAR(20)
	DECLARE @EQUIPINDEX5 VARCHAR(20)
	DECLARE @EQUIPINDEX6 VARCHAR(20)
	DECLARE @EQUIPINDEX7 VARCHAR(20)
	DECLARE @EQUIPINDEX8 VARCHAR(20)
	DECLARE @EQUIPINDEX9 VARCHAR(20)
	DECLARE @EQUIPINDEX10 VARCHAR(20)
	DECLARE @EQUIPINDEX11 VARCHAR(20)
	DECLARE @EQUIPINDEX12 VARCHAR(20)
	DECLARE @EQUIPINDEX13 VARCHAR(20)
	DECLARE @EQUIPINDEX14 VARCHAR(20)
	DECLARE @EQUIPINDEX15 VARCHAR(20)
	DECLARE @EQUIPINDEX16 VARCHAR(20)
	DECLARE @EQUIPINDEX17 VARCHAR(20)
	DECLARE @EQUIPINDEX18 VARCHAR(20)
	DECLARE @EQUIPINDEX19 VARCHAR(20)
	DECLARE @EQUIPINDEX20 VARCHAR(20)
	DECLARE @EQUIPINDEX21 VARCHAR(20)
	DECLARE @EQUIPINDEX22 VARCHAR(20)
	DECLARE @EQUIPINDEX23 VARCHAR(20)
	DECLARE @EQUIPINDEX24 VARCHAR(20)
	-- 58 itens
	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@EQUIPSTR, ',') WHERE LEN(VALUE) > 0

	-- UPDATE
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s', 
		@CHARINDEX OUTPUT,
		@EQUIPTYPEID1 OUTPUT,		
		@EQUIPTYPEID2 OUTPUT,		
		@EQUIPTYPEID3 OUTPUT,
		@EQUIPTYPEID4 OUTPUT, 
		@EQUIPTYPEID5 OUTPUT,
		@EQUIPTYPEID6 OUTPUT,
		@EQUIPTYPEID7 OUTPUT,
		@EQUIPTYPEID8 OUTPUT,
		@EQUIPTYPEID9 OUTPUT,
		@EQUIPTYPEID10 OUTPUT, 
		@EQUIPTYPEID11 OUTPUT, 
		@EQUIPTYPEID12 OUTPUT,
		@EQUIPTYPEID13 OUTPUT,
		@EQUIPTYPEID14 OUTPUT,
		@EQUIPTYPEID15 OUTPUT,
		@EQUIPTYPEID16 OUTPUT,
		@EQUIPTYPEID17 OUTPUT,
		@EQUIPTYPEID18 OUTPUT,
		@EQUIPTYPEID19 OUTPUT, 
		@EQUIPTYPEID20 OUTPUT, 
		@EQUIPTYPEID21 OUTPUT,
		@EQUIPTYPEID22 OUTPUT,
		@EQUIPTYPEID23 OUTPUT, 
		@EQUIPTYPEID24 OUTPUT, 
		--index
		@EQUIPINDEX1 OUTPUT,
		@EQUIPINDEX2 OUTPUT,
		@EQUIPINDEX3 OUTPUT,
		@EQUIPINDEX4 OUTPUT,
		@EQUIPINDEX5 OUTPUT,
		 @EQUIPINDEX6 OUTPUT,
		 @EQUIPINDEX7 OUTPUT,
		 @EQUIPINDEX8 OUTPUT,
		 @EQUIPINDEX9 OUTPUT,
		@EQUIPINDEX10 OUTPUT,
		@EQUIPINDEX11 OUTPUT,
		 @EQUIPINDEX12 OUTPUT,
		 @EQUIPINDEX13 OUTPUT,
		 @EQUIPINDEX14 OUTPUT,
		 @EQUIPINDEX15 OUTPUT,
		 @EQUIPINDEX16 OUTPUT,
		 @EQUIPINDEX17 OUTPUT,
		 @EQUIPINDEX18 OUTPUT,
		@EQUIPINDEX19 OUTPUT,
		@EQUIPINDEX20 OUTPUT,
		 @EQUIPINDEX21 OUTPUT,
		 @EQUIPINDEX22 OUTPUT,
		@EQUIPINDEX23 OUTPUT,
		@EQUIPINDEX24 OUTPUT

		
		-- UPDATE Pangya_Character_Equip ITEM
		UPDATE [DBO].Pangya_Character
		SET
		-- TYPEID									-- PART INDEX 
		PART_TYPEID_1 = ISNULL(@EQUIPTYPEID1, 0),	PART_IDX_1 = ISNULL(@EQUIPINDEX1, 0),
		PART_TYPEID_2 = ISNULL(@EQUIPTYPEID2, 0),	PART_IDX_2 = ISNULL(@EQUIPINDEX2, 0),
		PART_TYPEID_3 = ISNULL(@EQUIPTYPEID3, 0),	PART_IDX_3 = ISNULL(@EQUIPINDEX3, 0),
		PART_TYPEID_4 = ISNULL(@EQUIPTYPEID4, 0),	PART_IDX_4 = ISNULL(@EQUIPINDEX4, 0),
		PART_TYPEID_5 = ISNULL(@EQUIPTYPEID5, 0),	PART_IDX_5 = ISNULL(@EQUIPINDEX5, 0),
		PART_TYPEID_6 = ISNULL(@EQUIPTYPEID6, 0),	PART_IDX_6 = ISNULL(@EQUIPINDEX6, 0),
		PART_TYPEID_7 = ISNULL(@EQUIPTYPEID7, 0),	PART_IDX_7 = ISNULL(@EQUIPINDEX7, 0),
		PART_TYPEID_8 = ISNULL(@EQUIPTYPEID8, 0),	PART_IDX_8 = ISNULL(@EQUIPINDEX8, 0),
		PART_TYPEID_9 = ISNULL(@EQUIPTYPEID9, 0),	PART_IDX_9 = ISNULL(@EQUIPINDEX9, 0),
		PART_TYPEID_10 = ISNULL(@EQUIPTYPEID10, 0), PART_IDX_10 = ISNULL(@EQUIPINDEX10, 0),
		PART_TYPEID_11 = ISNULL(@EQUIPTYPEID11, 0), PART_IDX_11 = ISNULL(@EQUIPINDEX11, 0),
		PART_TYPEID_12 = ISNULL(@EQUIPTYPEID12, 0), PART_IDX_12 = ISNULL(@EQUIPINDEX12, 0),
		PART_TYPEID_13 = ISNULL(@EQUIPTYPEID13, 0), PART_IDX_13 = ISNULL(@EQUIPINDEX13, 0),
		PART_TYPEID_14 = ISNULL(@EQUIPTYPEID14, 0), PART_IDX_14 = ISNULL(@EQUIPINDEX14, 0),
		PART_TYPEID_15 = ISNULL(@EQUIPTYPEID15, 0), PART_IDX_15 = ISNULL(@EQUIPINDEX15, 0),
		PART_TYPEID_16 = ISNULL(@EQUIPTYPEID16, 0), PART_IDX_16 = ISNULL(@EQUIPINDEX16, 0),
		PART_TYPEID_17 = ISNULL(@EQUIPTYPEID17, 0), PART_IDX_17 = ISNULL(@EQUIPINDEX17, 0),
		PART_TYPEID_18 = ISNULL(@EQUIPTYPEID18, 0), PART_IDX_18 = ISNULL(@EQUIPINDEX18, 0),
		PART_TYPEID_19 = ISNULL(@EQUIPTYPEID19, 0), PART_IDX_19 = ISNULL(@EQUIPINDEX19, 0),
		PART_TYPEID_20 = ISNULL(@EQUIPTYPEID20, 0), PART_IDX_20 = ISNULL(@EQUIPINDEX20, 0),
		PART_TYPEID_21 = ISNULL(@EQUIPTYPEID21, 0), PART_IDX_21 = ISNULL(@EQUIPINDEX21, 0),
		PART_TYPEID_22 = ISNULL(@EQUIPTYPEID22, 0), PART_IDX_22 = ISNULL(@EQUIPINDEX22, 0),
		PART_TYPEID_23 = ISNULL(@EQUIPTYPEID23, 0), PART_IDX_23 = ISNULL(@EQUIPINDEX23, 0),
		PART_TYPEID_24 = ISNULL(@EQUIPTYPEID24, 0), PART_IDX_24 = ISNULL(@EQUIPINDEX24, 0)
		WHERE CID = @CHARINDEX

		DELETE FROM @TEMP WHERE ID = @sID
	END

END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_CHARACTER_STATS]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[USP_SAVE_CHARACTER_STATS] 
	@UID INT,
	@ITEMSTR VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

		INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)


	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- UPDATE DETAIL
	DECLARE @CHARINDEX VARCHAR(20)
	DECLARE @POWER VARCHAR(20)
	DECLARE @CONTROL VARCHAR(20)
	DECLARE @IMPACT VARCHAR(20)
	DECLARE @SPIN VARCHAR(20)
	DECLARE @CURVE VARCHAR(20)
	DECLARE @CUTIN VARCHAR(20)
	DECLARE @HAIR_COLOR VARCHAR(20)
	DECLARE @AUXPART VARCHAR(20)
	DECLARE @AUXPART2 VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- UPDATE
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s ^%s ^%s  ^%s  ^%s ^%s ^%s  ', 
		@CHARINDEX OUTPUT,
		@POWER OUTPUT,
		@CONTROL OUTPUT,
		@IMPACT OUTPUT,
		@SPIN OUTPUT,
		@CURVE OUTPUT,
		@CUTIN OUTPUT,
		@HAIR_COLOR OUTPUT,
		@AUXPART OUTPUT,
		@AUXPART2 OUTPUT
		
		-- UPDATE Pangya_Character ITEM
		UPDATE [DBO].Pangya_Character
		SET
		POWER = ISNULL(@POWER, 0),
		CONTROL = ISNULL(@CONTROL, 0),
		IMPACT = ISNULL(@IMPACT, 0),
		SPIN = ISNULL(@SPIN, 0),
		CURVE = ISNULL(@CURVE, 0),
		CUTIN = ISNULL(@CUTIN, 0),
		HAIR_COLOR = ISNULL(@HAIR_COLOR, 0)		
		WHERE CID = @CHARINDEX AND UID = @UID
		
		UPDATE [DBO].Pangya_Character 
		set 
		AUXPART =  ISNULL(@AUXPART, 0),
		AUXPART2 =  ISNULL(@AUXPART2, 0)
		WHERE UID = @UID

		DELETE FROM @TEMP WHERE ID = @sID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_ITEM]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[USP_SAVE_ITEM] 
	@UID INT,
	@ITEMSTR VARCHAR(8000)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TEMP TABLE (STRING VARCHAR(7000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(2000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @ITEM_IDX VARCHAR(20)
	DECLARE @ITEMC0 VARCHAR(20)
	DECLARE @ITEMC1 VARCHAR(20)
	DECLARE @ITEMC2 VARCHAR(20)
	DECLARE @ITEMC3 VARCHAR(20)
	DECLARE @ITEMC4 VARCHAR(20)
	DECLARE @ITEM_VALID VARCHAR(20)
	DECLARE @ISUCC VARCHAR(20)
	DECLARE @UCCSTATUS VARCHAR(20)
	DECLARE @UCCUNIQUE VARCHAR(20)
	DECLARE @ITEMENDDATE VARCHAR(24)
	DECLARE @ITEMFLAG VARCHAR(20)
	DECLARE @CLUB_POINT VARCHAR(20)
	DECLARE @CLUB_WORK_COUNT VARCHAR(20)
	DECLARE @POINT_LOG VARCHAR(20)
	DECLARE @PANG_LOG VARCHAR(20)
	DECLARE @C0_SLOT VARCHAR(20)
	DECLARE @C1_SLOT VARCHAR(20)
	DECLARE @C2_SLOT VARCHAR(20)
	DECLARE @C3_SLOT VARCHAR(20)
	DECLARE @C4_SLOT VARCHAR(20)
	DECLARE @CANCEL_COUNT	VARCHAR(20)
	DECLARE @IS_CLUBSET VARCHAR(20)
	
	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
		WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
			SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
			EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ', 
				 @ITEM_IDX OUTPUT, 
				 @ITEMC0 OUTPUT,
				 @ITEMC1 OUTPUT, 
				 @ITEMC2 OUTPUT,
				 @ITEMC3 OUTPUT,
				 @ITEMC4 OUTPUT,
				 @ITEM_VALID OUTPUT,
				 @ISUCC OUTPUT,
				 @UCCSTATUS OUTPUT,
				 @UCCUNIQUE OUTPUT,
				 @ITEMENDDATE OUTPUT,
				 @ITEMFLAG OUTPUT,
				 @CLUB_POINT OUTPUT,
				 @CLUB_WORK_COUNT OUTPUT,
				 @POINT_LOG OUTPUT,
				 @PANG_LOG OUTPUT,
				 @C0_SLOT OUTPUT,
				 @C1_SLOT OUTPUT,
				 @C2_SLOT OUTPUT,
				 @C3_SLOT OUTPUT,
				 @C4_SLOT OUTPUT,
				 @CANCEL_COUNT	OUTPUT,
				 @IS_CLUBSET OUTPUT

				 	-- UPDATE TO WAREHOUSE
				 UPDATE dbo.Pangya_Warehouse
				 SET C0 = Convert(smallint,ISNULL(@ITEMC0,0)),
					C1	= Convert(smallint,ISNULL(@ITEMC1,0)),
					C2	= Convert(smallint,ISNULL(@ITEMC2,0)),
					C3  = Convert(smallint,ISNULL(@ITEMC3,0)),
					C4  = Convert(smallint,ISNULL(@ITEMC4,0)),
					VALID = Convert(tinyint,ISNULL(@ITEM_VALID,0)),
					DateEnd = Convert(datetime,ISNULL(@ITEMENDDATE,GETDATE())),
					Flag =  Convert(tinyint,ISNULL(@ITEMFLAG,0))
					WHERE UID = @UID AND item_id = @ITEM_IDX

					
			-- UPDATE CLUB ITEM
			UPDATE	DBO.Pangya_Club_Info
			SET C0_SLOT  = Convert(smallint,ISNULL(@C0_SLOT,0)),
			C1_SLOT  =	Convert(smallint,ISNULL(@C1_SLOT,0)),
			C2_SLOT = Convert(smallint,ISNULL(@C2_SLOT,0)),
			C3_SLOT =Convert(smallint,ISNULL(@C3_SLOT,0)),
			C4_SLOT  = Convert(smallint,ISNULL(@C4_SLOT,0)),
			CLUB_POINT = Convert(int,ISNULL(@CLUB_POINT,0)),
			CLUB_WORK_COUNT =Convert(int,ISNULL(@CLUB_WORK_COUNT,0)),
			CLUB_SLOT_CANCEL = Convert(int,ISNULL(@CANCEL_COUNT,0)),
			CLUB_POINT_TOTAL_LOG = Convert(int,ISNULL(@POINT_LOG,0)),
			CLUB_UPGRADE_PANG_LOG = Convert(int,ISNULL(@PANG_LOG,0))
			WHERE ITEM_ID = @ITEM_IDX

			-- UPDATE UCC ITEM
				UPDATE DBO.Pangya_SelfDesign 
				SET UCC_STATUS =  Convert(tinyint,ISNULL(@UCCSTATUS,0)),
				UCC_UNIQE = ISNULL(@UCCUNIQUE ,0)
				WHERE UID = @UID AND ITEM_ID = @ITEM_IDX
			
			DELETE FROM @TEMP WHERE ID = @sID
		END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_MASCOT]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_SAVE_MASCOT] 
	@UID INT,
	@ITEMSTR VARCHAR(5000)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Pangya_String(str) VALUES(@ITEMSTR)

	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @MID VARCHAR(20)
	DECLARE @MASCOT_TYPEID VARCHAR(20)
	DECLARE @MESSAGE VARCHAR(20)
	DECLARE @VALID VARCHAR(20)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s ', @MID OUTPUT , @MASCOT_TYPEID OUTPUT, @MESSAGE OUTPUT, @VALID OUTPUT  
		-- UPDATE ITEM
		UPDATE dbo.Pangya_Mascot SET	MESSAGE = @MESSAGE, 
										VALID = ISNULL(VALID, 1) 
										WHERE UID = @UID AND MASCOT_TYPEID = @MASCOT_TYPEID AND MID = @MID
		DELETE FROM @TEMP WHERE ID = @sID
	END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_TOOLBAR]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_SAVE_TOOLBAR] 
	@UID INT,
	@ITEMSTR VARCHAR(8000)
AS
BEGIN
	SET NOCOUNT ON;



	DECLARE @TEMP TABLE (STRING VARCHAR(2000), ID INT IDENTITY(1,1))
	DECLARE @sSQL VARCHAR(1000)
	DECLARE @sID INT
	-- THE ITEM DETAIL
	DECLARE @CHARACTER_ID VARCHAR(20)
	DECLARE @CADDIE VARCHAR(20)
	DECLARE @MASCOT_ID VARCHAR(20)
	DECLARE @BALL_ID VARCHAR(20)
	DECLARE @CLUB_ID VARCHAR(20)
	DECLARE @ITEM_SLOT_1 VARCHAR(20)
	DECLARE @ITEM_SLOT_2 VARCHAR(20)
	DECLARE @ITEM_SLOT_3 VARCHAR(20)
	DECLARE @ITEM_SLOT_4 VARCHAR(20)
	DECLARE @ITEM_SLOT_5 VARCHAR(20)
	DECLARE @ITEM_SLOT_6 VARCHAR(20)
	DECLARE @ITEM_SLOT_7 VARCHAR(20)
	DECLARE @ITEM_SLOT_8 VARCHAR(20)
	DECLARE @ITEM_SLOT_9 VARCHAR(20)
	DECLARE @ITEM_SLOT_10 VARCHAR(20)
	DECLARE @SKIN1 VARCHAR(20)
	DECLARE @SKIN2 VARCHAR(20)
	DECLARE @SKIN3 VARCHAR(20)
	DECLARE @SKIN4 VARCHAR(20)
	DECLARE @SKIN5 VARCHAR(20)
	DECLARE @SKIN6 VARCHAR(20)
		INSERT INTO DBO.Pangya_String(str) VALUES(@ITEMSTR)

	-- SPLIT
	INSERT INTO @TEMP(STRING) SELECT * FROM STRING_SPLIT(@ITEMSTR, ',') WHERE LEN(VALUE) > 0

	-- INSERT IF EXISTS
	WHILE EXISTS (SELECT * FROM @TEMP) BEGIN
		SELECT TOP 1 @sSQL = REPLACE(STRING, '^', ' ^'), @sID = ID FROM @TEMP
		select @sSQL
		EXEC XP_SSCANF @sSQL,' ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s  ^%s  ^%s  ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s ^%s  ', 
		@CHARACTER_ID OUTPUT ,
		 @CADDIE OUTPUT, 
		 @MASCOT_ID OUTPUT,
		 @BALL_ID OUTPUT,
		 @CLUB_ID OUTPUT,
		@ITEM_SLOT_1 OUTPUT,
		 @ITEM_SLOT_2 OUTPUT, 
		@ITEM_SLOT_3 OUTPUT,
		 @ITEM_SLOT_4 OUTPUT,
		@ITEM_SLOT_5 OUTPUT,
		 @ITEM_SLOT_6 OUTPUT,
		@ITEM_SLOT_7 OUTPUT,
		 @ITEM_SLOT_8 OUTPUT,
		@ITEM_SLOT_9 OUTPUT,
		 @ITEM_SLOT_10 OUTPUT,
		  @SKIN1  OUTPUT,
	 @SKIN2  OUTPUT,
	 @SKIN3  OUTPUT,
	 @SKIN4  OUTPUT,
	 @SKIN5  OUTPUT,
	 @SKIN6  OUTPUT
		-- UPDATE ITEM
		IF EXISTS (SELECT 1 FROM DBO.Pangya_User_Equip WHERE UID = @UID)
	BEGIN
		UPDATE DBO.Pangya_User_Equip
		SET	CHARACTER_ID = ISNULL(@CHARACTER_ID,0),
			CADDIE = ISNULL(@CADDIE,0),
			MASCOT_ID = ISNULL(@MASCOT_ID,0),
			BALL_ID = ISNULL(@BALL_ID,0),
			CLUB_ID = ISNULL(@CLUB_ID,0),
			ITEM_SLOT_1 = ISNULL(@ITEM_SLOT_1,0),
			ITEM_SLOT_2 = ISNULL(@ITEM_SLOT_2,0),
			ITEM_SLOT_3 = ISNULL(@ITEM_SLOT_3,0),
			ITEM_SLOT_4 = ISNULL(@ITEM_SLOT_4,0),
			ITEM_SLOT_5 = ISNULL(@ITEM_SLOT_5,0),
			ITEM_SLOT_6 = ISNULL(@ITEM_SLOT_6,0),
			ITEM_SLOT_7 = ISNULL(@ITEM_SLOT_7,0),
			ITEM_SLOT_8 = ISNULL(@ITEM_SLOT_8,0),
			ITEM_SLOT_9 = ISNULL(@ITEM_SLOT_9,0),
			ITEM_SLOT_10 = ISNULL(@ITEM_SLOT_10,0),
			Skin_1 = ISNULL(@SKIN1, 0),
			Skin_2 = ISNULL(@SKIN2, 0),
			Skin_3 = ISNULL(@SKIN3, 0),
			Skin_4 = ISNULL(@SKIN4, 0),
			Skin_5 = ISNULL(@SKIN5, 0),
			Skin_6 = ISNULL(@SKIN6, 0)
			Where UID = @UID		
	END
		DELETE FROM @TEMP WHERE ID = @sID
	END	
END

GO
/****** Object:  StoredProcedure [dbo].[USP_UCC_REQUEST_UPLOAD]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[USP_UCC_REQUEST_UPLOAD] 
	@UID INT
	,@ITEMID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @KEY VARCHAR(50)

	IF EXISTS (SELECT 1 FROM [DBO].Pangya_SelfDesign WHERE UID = @UID AND ITEM_ID = @ITEMID AND UCC_STATUS IN (0,2))
	BEGIN
		SET @KEY = UPPER(LEFT(REPLACE(NEWID(), '-', ''), 20))
		UPDATE [DBO].Pangya_SelfDesign SET UCC_KEY = @KEY WHERE UID = @UID AND ITEM_ID = @ITEMID
		IF @@ROWCOUNT > 0
		BEGIN
			SELECT 1 AS CODE, @KEY AS UCCKEY, @ITEMID AS ITEM_ID
		END ELSE BEGIN
			SELECT 0 AS CODE
		END
	END ELSE BEGIN
		SELECT 0 AS CODE
	END
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_GUILD_CHECK]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[WEB_GUILD_CHECK] 
	@UID INT,
	@GUILDID INT,
	@EMBLEM_IDX INT,
	@EMBLEM VARCHAR(20)	
AS
BEGIN
   SET NOCOUNT ON;
   -- CODE = 1 SUCCESS
   -- CODE = 0 FAIL
   IF EXISTS (
	 SELECT 1 
	 FROM [dbo].Pangya_Guild_Emblem A
	 INNER JOIN [dbo].Pangya_Guild_Member B ON B.GUILD_ID = A.GUILD_ID AND B.GUILD_MEMBER_UID = @UID AND GUILD_POSITION = 1
	 WHERE A.GUILD_ID = @GUILDID AND A.EMBLEM_IDX = @EMBLEM_IDX AND A.GUILD_MARK_IMG = @EMBLEM
   )	
   BEGIN
	 SELECT CODE = 1
   END ELSE BEGIN
	 SELECT CODE = 0
   END
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UCC_CHECK]    Script Date: 23/07/2020 09:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TOP
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[WEB_UCC_CHECK] 
	@UID INT,
	@UCCKEY VARCHAR(20),
	@ITEMID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM [dbo].Pangya_SelfDesign WHERE UID = @UID AND ITEM_ID = @ITEMID AND UCC_KEY = @UCCKEY AND LEN(UCC_KEY) >= 20) BEGIN
		UPDATE [dbo].Pangya_SelfDesign SET UCC_KEY = 0 WHERE UID = @UID AND ITEM_ID = @ITEMID
		SELECT 1 AS Code
	END	ELSE BEGIN
		SELECT 0 AS Code
	END
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'90 = Pang
91 = EXP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pangya_Mail_Item', @level2type=N'COLUMN',@level2name=N'APPLY_ITEM_ID'
GO
USE [master]
GO
ALTER DATABASE [DB_Pangya] SET  READ_WRITE 
GO
