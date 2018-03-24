
DECLARE @dbname nvarchar(128)
DECLARE @tblname nvarchar(128)
SET @dbname = N'YCET'
SET @tblname =N'YCET_USERS'
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
USE YCET
IF (NOT EXISTS (SELECT name 
FROM sys.tables 
WHERE ('[' + name + ']' = @tblname 
OR name = @tblname)))

CREATE TABLE [dbo]. YCET_USERS (
	[UserID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [varchar](100) NULL,
	[EmailID] [varchar](50) NULL,
	[Gender] [varchar](100) NULL,	
	[createdate] [datetime] default getdate(),
	[DOB] [datetime] NULL,
	[Password] [varchar](100) NULL,
	[SecurityQues] [varchar](200) NULL,
	[SecurityAns] [varchar](100) NULL
)  

 

