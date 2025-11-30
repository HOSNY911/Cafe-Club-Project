CREATE TABLE [dbo].[Sessions] (
    [SessionID]   INT             IDENTITY (1, 1) NOT NULL,
    [ClientID]    INT             NULL,
    [StartTime]   DATETIME        CONSTRAINT [DF__Sessions__StartT__5AEE82B9] DEFAULT (getdate()) NOT NULL,
    [EndTime]     DATETIME        NULL,
    [TableID]     INT             NOT NULL,
    [TotalAmount] DECIMAL (10, 2) NOT NULL,
    [CreatedAt]   DATE            NOT NULL,
    CONSTRAINT [PK_Sessions_SessionID] PRIMARY KEY CLUSTERED ([SessionID] ASC),
    CONSTRAINT [FK_Sessions_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ClientID]),
    CONSTRAINT [FK_TableID_Sessions] FOREIGN KEY ([TableID]) REFERENCES [dbo].[CafeTables] ([TableID])
);

