create   procedure SP_DeleteCafeTables
@TableID int

as
begin
	Begin Try
		update CafeTables set IsActive=0 where TableID = @TableID
		return 1;
	End Try
Begin Catch
	;Throw;
End Catch
End