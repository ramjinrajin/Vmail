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
               WHERE  object_id = Object_id(N'[dbo].[YCET_SEARCH]')
                      AND type IN ( N'P', N'PC' ))
  BEGIN
      EXEC('CREATE Procedure [dbo].[YCET_SEARCH]
@EmailID varchar(100),
@SearchTerm nvarchar(max)

AS
BEGIN
      SET NOCOUNT ON

if  EXISTS
( select * from YCET_Mails with(nolock)  WHERE    SenderID like @SearchTerm)
BEGIN
select * from YCET_Mails with(nolock)  where SenderID like @Searchterm and EmailID=@EmailID AND ISDRAFT=0 AND Isdeleted=0 order by 1 desc

END

ELSE
BEGIN

select * from YCET_Mails with(nolock)  where Message like @Searchterm  and EmailID=@EmailID AND ISDRAFT=0 AND Isdeleted=0 order by 1 desc

END


END')
  END