DECLARE @dbname nvarchar(128)
SET @dbname = N'YCET'

IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))

-- code mine :)
PRINT 'db exists'

ELSE

create database YCET 