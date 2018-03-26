 
 
Create Table VmailUser
(
Id int Identity(1,1),
Userid int NOT NULL,
AuthCount int default 0 NOT NULL,
HITCount int default 0 NOT NULL,
IsAuthenticated int default 0
)
GO
-------------------------------------------------
CREATE Proc SpVmailUser
(
	@UserId int
)
AS
BEGIN
 
Declare @HITCount int=0;
Declare @AuthCount int=0;
SET @HITCount=(SELECT HITCount FROM VmailUser Where Userid=@UserId)
SET @AuthCount=(SELECT AuthCount FROM VmailUser Where Userid=@UserId)



  SET @HITCount=(SELECT HITCount FROM VmailUser Where Userid=@UserId)
  IF(@HITCount=@AuthCount)
    Update VmailUser SET IsAuthenticated=1 Where Userid=@UserId
	ELSE 
		  Update VmailUser SET HITCount=@HITCount+1 Where Userid=@UserId


  SET @HITCount=(SELECT HITCount FROM VmailUser Where Userid=@UserId)



  SELECT @HITCount AS RESPONSE

EnD
Go

--------------------------------------------------

INSERT INTO VmailUser (Userid, AuthCount, HITCount, IsAuthenticated)
	VALUES (1, 0, 0, 0)

EXEC SpVmailUser 1