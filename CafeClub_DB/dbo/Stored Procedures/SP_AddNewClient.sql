

CREATE Procedure [dbo].[SP_AddNewClient]
@Amount decimal(10,2),
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
					Throw 50001,'العميل موجود بالفعل',2
				End

				insert into People(Phone,FullName) values(@Phone,@FullName);

				set @PersonID = SCOPE_IDENTITY();

				insert into Clients (PersonID,Createdby,IsActive) values (@PersonID,@Createdby,@IsActive);

				set @ClientID = SCOPE_IDENTITY();

				insert into Debts (ClientID,AmountOwed,Createdby) values (@ClientID,@Amount,@Createdby);

				


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