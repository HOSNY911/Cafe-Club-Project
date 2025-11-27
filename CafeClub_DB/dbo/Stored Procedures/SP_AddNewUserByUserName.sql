


CREATE procedure [dbo].[SP_AddNewUserByUserName]

@UserName nvarchar(20),
@Password nvarchar(150),
@Permations int,
@IsActive bit,
@Createdby int,
@Phone nvarchar(20),
@FullName nvarchar(50),
@UserID int output

as
begin
	begin TransAction
		begin Try
		
		declare	@PersonID int;
			

			if exists(select 1 from Users Where UserName = @UserName)
				Begin
					throw 50001,'اليوزر موجود بالفعل',1
				End
				
					insert into People(Phone,FullName) values(@Phone,@FullName);
					
					set @PersonID = SCOPE_IDENTITY();

					insert into Users (UserName,Password,Permations,IsActive,PersonID,Createdby,CreatedAt) 
					values (@UserName,@Password,@Permations,@IsActive,@PersonID,@Createdby,GETDATE());

					set @UserID = SCOPE_IDENTITY();

					Commit TransAction;
		end try 
	begin Catch
		if(@@Trancount>0)
			begin
				Rollback TransAction
			End;

	throw;
	End Catch
End
			






