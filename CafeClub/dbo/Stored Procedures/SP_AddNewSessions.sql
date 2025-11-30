create   procedure SP_AddNewSessions
@ClientID int,
@StartTime datetime2,
@EndTime datetime2,
@TableID int,
@TotalAmount decimal(10,2),
@CreatedAt datetime2

as
begin
	

	insert into Sessions (ClientID,StartTime,EndTime,TableID,TotalAmount,CreatedAt) values (@ClientID,@StartTime,@EndTime,@TableID,@TotalAmount,@CreatedAt)

	return 1;

	

End