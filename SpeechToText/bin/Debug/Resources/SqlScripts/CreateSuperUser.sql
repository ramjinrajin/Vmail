DECLARE @dbname nvarchar(128)
DECLARE @tblname nvarchar(128)
SET @dbname = N'YCET'
SET @tblname =N'YCET_USERS'
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
USE YCET
IF (EXISTS (SELECT name 
FROM sys.tables 
WHERE ('[' + name + ']' = @tblname 
OR name = @tblname)))

BEGIN
   IF NOT EXISTS (SELECT * FROM YCET_USERS 
                  WHERE EmailID='admin@heera.com')
   BEGIN
      Insert into YCET_USERS (Name,DOB,Gender,Password,EmailID) Values ('admin',GETDATE(),'Male','admin','admin@heera.com')
   END
END