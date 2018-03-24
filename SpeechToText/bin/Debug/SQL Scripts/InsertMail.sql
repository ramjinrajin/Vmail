
DECLARE @dbname nvarchar(128)
DECLARE @tblname nvarchar(128)
SET @dbname = N'YCET'
SET @tblname =N'YCET_Mails'
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
USE YCET
IF (NOT EXISTS (SELECT name 
FROM sys.tables 
WHERE ('[' + name + ']' = @tblname 
OR name = @tblname)))
 
Create table YCET_Mails
(
MsgID int primary key identity(1,1),
UserID int Foreign Key References YCET_USERS(UserID),
EmailID [varchar](50),
[Subject] [varchar](50),
Message nvarchar(max),
SenderID [varchar](50),
Isdeleted int,
IsRead int default 0,
IsDraft int default 0,
createdate datetime default getdate(),
modifieddate datetime,
[FileName] varchar(100)
)
 