
CREATE Procedure [dbo].[SP_UpdateClientByPhone]

@NewPhone nvarchar(20),
@FullName nvarchar(50),
@OldPhone nvarchar(20),
@Updatedby int,
@IsActive bit
as
begin
	begin TransAction
	Begin Try

		if not exists(select 1 from People where Phone=@OldPhone)
			begin
				Throw 50001,'العميل الذي تحال تحديثة غير موجود',5;
			End

			declare @PersonID int;

			select @PersonID=Clients.PersonID from Clients
			join 
			People on People.PersonID = Clients.PersonID
			where People.Phone = @OldPhone;


		Update People set Phone=@NewPhone,FullName=@FullName where Phone = @OldPhone;

		

		update Clients set IsActive = @IsActive,Updatedby=@Updatedby,UpdateAt=GETDATE() where PersonID =@PersonID;

			Commit TransAction
			return 1;
	End Try
Begin Catch
	if(@@TRANCOUNT>0)
		begin
			Rollback TransAction
		End;
	Throw
End Catch
end
