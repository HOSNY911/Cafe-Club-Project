
CREATE Procedure [dbo].[SP_GetClientbyPhone]
@Phone nvarchar(20)

as
begin
	begin Try
	declare @PersonID int;
		
		select @PersonID=PersonID from People where Phone = @Phone;
		if (@PersonID is null)
			begin
				Throw 50001,'العميل الذي تحاول البحث عنه غير موجود', 6;
			End

			if not exists (select 1 from clients where PersonID = @PersonID and IsActive=1)
				begin
					Throw 50001,'العميل الذي تحاول البحث عنه غير موجود', 8;
				End

			select C.ClientID,P.FullName,P.Phone,D.AmountOwed,U.UserName as Createdby,C.UpdateAt,Updater.UserName as Updatedby from people as P
			join
			Clients as C on C.PersonID = P.PersonID
			join
			Users as U on U.UserID = C.Createdby
			left join
			Users as Updater on Updater.UserID =  C.Updatedby
			join
			Debts as D on D.ClientID = C.ClientID
			where p.PersonID =@PersonID

			
	End Try
	Begin Catch
		Throw;
	End Catch
End