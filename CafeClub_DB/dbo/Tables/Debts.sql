CREATE TABLE [dbo].[Debts] (
    [DebtID]     INT             IDENTITY (1, 1) NOT NULL,
    [ClientID]   INT             NOT NULL,
    [AmountOwed] DECIMAL (10, 2) NOT NULL,
    [Createdby]  INT             NOT NULL,
    [CreatedAt]  DATETIME        DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Debts_DebtID] PRIMARY KEY CLUSTERED ([DebtID] ASC),
    CONSTRAINT [FK_Debts_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ClientID]),
    CONSTRAINT [FK_Debts_Createdby] FOREIGN KEY ([Createdby]) REFERENCES [dbo].[Users] ([UserID])
);

