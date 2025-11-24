USE [CafeClub]
GO

/****** Object:  StoredProcedure [dbo].[SP_UpdateUserByUserName]    Script Date: 11/24/2025 11:04:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[SP_UpdateUserByUserName]


@UserName nvarchar(20),
@Password nvarchar(150),
@Permissions int,
@IsActive bit,
@Phone nvarchar(20),
@FullName nvarchar(50)

as
begin
	Begin TransAction
		Begin Try

	if not exists(select 1 from Users where UserName = @UserName)
		begin
			Throw 50001,'اليوزر غير موجود',3;
		End

		declare @PersonID int;

		select @PersonID = PersonID from Users where UserName = @UserName;

		Update People set Phone=@Phone,FullName=@FullName where PersonID = @PersonID;

		update Users set Password=case when @Password is null or @Password='' then Password else @Password end,Permissions=@Permissions,IsActive=@IsActive where UserName=@UserName;

	Commit TransAction
		return 1;
		End Try
Begin Catch
	if(@@TRANCOUNT>0)
		Begin
			Rollback TransAction
		End;
		
		Throw
End Catch
End
GO


