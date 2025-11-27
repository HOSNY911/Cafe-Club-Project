CREATE TABLE [dbo].[Sessions] (
    [SessionID]   INT             IDENTITY (1, 1) NOT NULL,
    [ClientID]    INT             NOT NULL,
    [StartTime]   DATETIME        DEFAULT (getdate()) NOT NULL,
    [EndTime]     DATETIME        NULL,
    [GameType]    NVARCHAR (30)   NOT NULL,
    [TotalAmount] DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK_Sessions_SessionID] PRIMARY KEY CLUSTERED ([SessionID] ASC),
    CONSTRAINT [FK_Sessions_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ClientID])
);

