
CREATE Procedure [dbo].[SP_AddPayment]
@Amount decimal(10,2),
@Createdby int,
@PhoneNumber nvarchar(20)

as
begin

	Begin TransAction
		Begin Try

		if not exists(
		select 1 from People
		join
		Clients on People.PersonID = Clients.PersonID
		where People.Phone = @PhoneNumber and Clients.IsActive =1
		)
			Begin
				Throw 50001,'العميل الذي تبحث عنه غير موجود',9;
			End

			declare @DebtID int;
			declare @ClientID int;	
			declare @AmountOwed decimal(10,2);
			select @ClientID = C.ClientID from Clients as C
			join
			People as P on P.PersonID = C.PersonID
			where P.Phone = @PhoneNumber;

			select @DebtID = DebtID from Debts where ClientID = @ClientID ;

			insert into Payments (DebtID,Amount,CreatedAt,Createdby,ClientID) values (@DebtID,@Amount,GETDATE(),@Createdby,@ClientID);

			update Debts set AmountOwed = AmountOwed-@Amount where ClientID = @ClientID;

			select @AmountOwed = AmountOwed from Debts where ClientID = @ClientID;

			If @AmountOwed <= 0
				Begin
					Update Clients set IsActive = 0 where ClientID = @ClientID
				End

			Commit TransAction;

			return 1;
		End Try
Begin Catch
	if(@@TRANCOUNT>0)
		Begin
			Rollback TransAction
		End
			;Throw
End Catch

End