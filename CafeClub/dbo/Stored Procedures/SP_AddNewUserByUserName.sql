



CREATE procedure [dbo].[SP_AddNewUserByUserName]

@UserName nvarchar(20),
@Password nvarchar(150),
@Permissions int,
@IsActive bit,
@Createdby nvarchar(20),
@Phone nvarchar(20),
@FullName nvarchar(50),
@UserID int output

as
begin
	begin TransAction
		begin Try
		
		declare	@PersonID int;
		declare @CreatedbyUserID int;
		
		select @CreatedbyUserID = Users.UserID from Users where UserName = @Createdby;

			if exists(select 1 from Users Where UserName = @UserName)
				Begin
					throw 50001,'اليوزر موجود بالفعل',1
				End
				
					insert into People(Phone,FullName) values(@Phone,@FullName);
					
					set @PersonID = SCOPE_IDENTITY();

					insert into Users (UserName,Password,Permissions,IsActive,PersonID,Createdby,CreatedAt) 
					values (@UserName,@Password,@Permissions,@IsActive,@PersonID,@CreatedbyUserID,GETDATE());

					set @UserID = SCOPE_IDENTITY();

					Commit TransAction;

					return 1;
		end try 
	begin Catch
		if(@@Trancount>0)
			begin
				Rollback TransAction
			End;

	throw;
	End Catch
End