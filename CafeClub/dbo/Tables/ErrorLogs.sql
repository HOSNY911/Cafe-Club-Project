CREATE TABLE [dbo].[ErrorLogs] (
    [LogID]         INT            IDENTITY (1, 1) NOT NULL,
    [ErrorMessage]  NVARCHAR (MAX) NULL,
    [ErrorLocation] NVARCHAR (MAX) NULL,
    [ErrorDate]     DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([LogID] ASC)
);

