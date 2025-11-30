Create Procedure SP_AddDebit

@AmountOwed decimal(10,2),
@PhoneNumber nvarchar(20)

as
begin
	
	Begin TransAction
		Begin Try

			if not exists(
				select 1 from People
				join
				Clients on People.PersonID = Clients.PersonID
				where People.Phone = @PhoneNumber
				)
					Begin
						Throw 50001,'العميل الذي تبحث عنه غير موجود',10;
					End

			declare @ClientID int;

				select @ClientID = ClientID from Clients
				join
				People on People.PersonID = Clients.PersonID
				where People.Phone = @PhoneNumber;

				Update Clients set IsActive =1 where ClientID = @ClientID;

				update Debts set AmountOwed = AmountOwed + @AmountOwed where ClientID = @ClientID;

				Commit TransAction
					return 1;
		End Try
Begin Catch
	if(@@TRANCOUNT>0)
		Begin
			RollBack TransAction
		End
		;Throw
End Catch
End