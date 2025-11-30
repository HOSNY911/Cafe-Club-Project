CREATE   Procedure SP_AddCafeTable
@TableName nvarchar(50),
@GameID int
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF EXISTS(SELECT 1 FROM CafeTables WHERE TableName = @TableName)
        BEGIN
            THROW 50001, 'هذا الاسم موجود من قبل اختار اسم اخر', 1;
        END 

        IF NOT EXISTS(SELECT 1 FROM Games WHERE GameID = @GameID)
        BEGIN
             THROW 50002, 'نوع اللعبة المختار غير موجود', 1;
        END

        
        INSERT INTO CafeTables (TableName, GameID, IsActive) 
        VALUES (@TableName, @GameID, 1);
        
        RETURN 1;

    END TRY 
    BEGIN CATCH
        ;THROW;
    END CATCH
END