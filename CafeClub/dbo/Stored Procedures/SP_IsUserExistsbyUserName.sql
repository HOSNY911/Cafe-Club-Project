Create   Procedure SP_IsUserExistsbyUserName

@UserName nvarchar(20)

as
Begin
set nocount on;
	if exists(select 1 from Users where UserName = @UserName)
		Begin
			return 1
		End
		
		return 0;
End