create Procedure SP_GetAllPaymentsbyPhoneNumber
@PageNumber int,
@RowPerPage int,
@TotalCount int output,
@PhoneNumber nvarchar(20)

as
begin
	
	Begin Try

	declare @PersonID int;
	declare @ClientID int;

        
		select @PersonID = PersonID from People where Phone = @PhoneNumber;

		if(@PersonID is null)
			begin
				Throw 50001,'العميل الذي تبحث عنه غير موجود',9;
			End

        
		select @ClientID = ClientID from Clients where PersonID = @PersonID;


       
		select @TotalCount = count(*) from Payments where ClientID = @ClientID;


       
		select 
            P.FullName,
            P.Phone,
            Pa.Amount,
            Pa.CreatedAt,
            U.UserName as Createdby 
        from Payments AS Pa 
		
        inner join Clients as C 
            on C.ClientID = Pa.ClientID
		inner join People as P 
            on P.PersonID = C.PersonID
		inner join Users as U 
            on U.UserID = Pa.Createdby
		
		where Pa.ClientID = @ClientID

		order by Pa.CreatedAt DESC 

		Offset(@PageNumber-1)*@RowPerPage Rows
		Fetch Next @RowPerPage Rows Only


	End Try
	Begin Catch
		Throw;
	End Catch
End