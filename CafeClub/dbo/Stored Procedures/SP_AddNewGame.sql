CREATE   PROCEDURE SP_AddNewGame
@GameName nvarchar(50),
@PricePerHour decimal(10,2),
@Createby int
AS
BEGIN
    SET NOCOUNT ON;
    SET @GameName = TRIM(@GameName);

  
    IF EXISTS (SELECT 1 FROM Games WHERE GameName = @GameName AND IsActive = 1)
    BEGIN
        THROW 50001, 'هذا الاسم موجود من قبل', 15;
    END

    
    IF EXISTS (SELECT 1 FROM Games WHERE GameName = @GameName AND IsActive = 0)
    BEGIN
        UPDATE Games 
        SET IsActive = 1,
            PricePerHour = @PricePerHour, 
            Updatedby = @Createby,        
            UpdatedAt = GETDATE()         
        WHERE GameName = @GameName;       

        RETURN 1;
    END

    
    INSERT INTO Games (GameName, PricePerHour, Createby, CreatedAt, IsActive) 
    VALUES (@GameName, @PricePerHour, @Createby, GETDATE(), 1);

    RETURN 1;
END