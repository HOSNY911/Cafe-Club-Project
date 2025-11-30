create   procedure SP_UpdateCafeTable

@TableID int,
@TableName nvarchar(30),
@GameID int

as
begin
	set nocount on;
	Begin Try
		if exists(select 1 from CafeTables where TableName=@TableName and TableID!=@TableID)
			Begin
				 THROW 50001, 'هذا الاسم موجود من قبل اختار اسم اخر', 14;
			End

		update CafeTables set TableName = @TableName,GameID = @GameID where TableID=@TableID;
			return 1
	End Try
Begin Catch
	;Throw;
End Catch
End