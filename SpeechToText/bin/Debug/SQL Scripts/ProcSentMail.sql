--DROp Procedure YCET_MAIL_SENT
DECLARE @dbname nvarchar(128)
DECLARE @tblname nvarchar(128)
SET @dbname = N'YCET'
SET @tblname =N'YCET_Mails'
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
USE YCET

IF NOT EXISTS (SELECT *
               FROM   sys.objects
               WHERE  object_id = Object_id(N'[dbo].[YCET_MAIL_SENT]')
                      AND type IN ( N'P', N'PC' ))
  BEGIN
      EXEC('CREATE Procedure [dbo].[YCET_MAIL_SENT]
@UserID int,
@Subject varchar(100),
@Message nvarchar(max),
@SenderID varchar(100),
@FileName varchar(100)
AS
BEGIN
      SET NOCOUNT ON

      DECLARE @GETEMAIL varchar(50)
SELECT @GETEMAIL = EmailID FROM YCET_USERS WHERE UserID = @UserID

INSERT INTO [dbo].[YCET_Mails]
           ([UserID]
           ,[EmailID]
           ,[Subject]
           ,[Message]
           ,[SenderID]
           ,[Isdeleted]
		   ,[FILENAME])          
       
        
	SELECT
	@UserID,@GETEMAIL,@Subject,@Message,@SenderID,0,@FileName 

END')
  END