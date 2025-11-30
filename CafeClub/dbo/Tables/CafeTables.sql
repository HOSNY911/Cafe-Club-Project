CREATE TABLE [dbo].[CafeTables] (
    [TableID]   INT           IDENTITY (1, 1) NOT NULL,
    [TableName] NVARCHAR (30) NOT NULL,
    [GameID]    INT           NOT NULL,
    [IsActive]  BIT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TableID_CafeTables] PRIMARY KEY CLUSTERED ([TableID] ASC),
    CONSTRAINT [FK_GameID_CafeTables] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Games] ([GameID])
);

