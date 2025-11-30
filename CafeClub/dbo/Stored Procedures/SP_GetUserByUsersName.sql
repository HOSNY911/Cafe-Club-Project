


CREATE Procedure [dbo].[SP_GetUserByUsersName]

@UserName nvarchar(20)

as
begin 

	begin Try
		if not exists (select 1 from Users where UserName=@UserName)
			begin
				Throw 50001,'اليوزر الذي تحاول البحث عنه غير موجود',7;
			End

		select U.UserID,P.FullName,U.UserName,U.Permissions,U.IsActive,P.Phone,Createdby.UserName as Createdby,U.UpdatedAt,Updateby.UserName as Updatedby from Users as U
		Join
		People AS P on P.PersonID = U.PersonID
		join
		Users as Createdby on Createdby.UserID = U.Createdby
		left join 
		Users as Updateby on Updateby.UserID = U.Updatedby
		where U.UserName = @UserName;
	End Try
	Begin Catch
		Throw;
	End Catch
	
end