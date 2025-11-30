CREATE procedure [dbo].[SP_UpdateUserByUserName]

@UserName nvarchar(20),
@Password nvarchar(150),
@Permissions int,
@IsActive bit,
@Phone nvarchar(20),
@FullName nvarchar(50),
@Updatedby nvarchar(20)

as
begin
	set nocount on;
	Begin TransAction
		Begin Try

		
		if not exists(select 1 from Users where UserName = @UserName)
			begin
				Throw 50001,'اليوزر المراد تعديله غير موجود',1;
			End

		declare @PersonID int;
		declare @UpdatedbyUserID int;

		
		select @UpdatedbyUserID = Users.UserID from Users where UserName = @Updatedby;

		
		IF @UpdatedbyUserID IS NULL
		BEGIN
			THROW 50002, 'الشخص الذي يقوم بالتعديل غير موجود', 1;
		END

		select @PersonID = PersonID from Users where UserName = @UserName;

		Update People set Phone=@Phone,FullName=@FullName where PersonID = @PersonID;

		update Users set 
			Password = case when @Password is null or @Password='' then Password else @Password end,
			Permissions = @Permissions,
			IsActive = @IsActive,
			Updatedby = @UpdatedbyUserID, 
			UpdatedAt = GETDATE()
		where UserName = @UserName;

		Commit TransAction
		return 1; 

		End Try
	Begin Catch
		if(@@TRANCOUNT>0)
			Begin
				Rollback TransAction
			End;
		
		Throw; 
	End Catch
End