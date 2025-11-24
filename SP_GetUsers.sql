USE [CafeClub]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetUsers]    Script Date: 11/24/2025 10:57:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[SP_GetUsers]
@PageNumber int,
@RowPerPage int,
@TotalCount int output

as
begin

	select @TotalCount = count(*) from Users;

	select P.FullName,U.UserName,U.Password,U.Permissions,U.IsActive,P.Phone,U.Createdby,U.UpdatedAt from Users as U
	Join
	People AS P on P.PersonID = U.PersonID

	ORDER BY U.UserID

	OFFSET (@PageNumber - 1) * @RowPerPage ROWS
    FETCH NEXT @RowPerPage ROWS ONLY;

end
		
GO


