USE [CafeClub]
GO

/****** Object:  StoredProcedure [dbo].[SP_AddNewClient]    Script Date: 11/24/2025 10:54:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[SP_AddNewClient]
@Phone nvarchar(20),
@FullName nvarchar(50),
@Createdby int,
@ClientID int output,
@IsActive bit =1
as
begin
	Begin TransAction
		Begin Try

		declare @PersonID int;
			if exists(select 1 from People where Phone = @Phone)
				Begin
					Throw 50001,'?????? ????? ??????',2
				End

				insert into People(Phone,FullName) values(@Phone,@FullName);

				set @PersonID = SCOPE_IDENTITY();

				insert into Clients (PersonID,Createdby,IsActive) values (@PersonID,@Createdby,@IsActive);

				set @ClientID = SCOPE_IDENTITY();

	Commit TransAction
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


