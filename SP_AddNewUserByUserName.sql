USE [CafeClub]
GO

/****** Object:  StoredProcedure [dbo].[SP_AddNewUserByUserName]    Script Date: 11/24/2025 10:55:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




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
					throw 50001,'?????? ????? ??????',1
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
			






GO


