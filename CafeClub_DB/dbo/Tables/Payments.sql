CREATE TABLE [dbo].[Payments] (
    [PaymentID] INT             IDENTITY (1, 1) NOT NULL,
    [DebtID]    INT             NOT NULL,
    [Amount]    DECIMAL (10, 2) NOT NULL,
    [CreatedAt] DATETIME        DEFAULT (getdate()) NOT NULL,
    [Createdby] INT             NOT NULL,
    CONSTRAINT [PK_Payments_PaymentID] PRIMARY KEY CLUSTERED ([PaymentID] ASC),
    CONSTRAINT [FK_Payments_Createdby] FOREIGN KEY ([Createdby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Payments_DebtID] FOREIGN KEY ([DebtID]) REFERENCES [dbo].[Debts] ([DebtID])
);

