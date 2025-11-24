USE [CafeClub]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 11/24/2025 10:47:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[DebtID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Createdby] [int] NOT NULL,
 CONSTRAINT [PK_Payments_PaymentID] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Createdby] FOREIGN KEY([Createdby])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Createdby]
GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_DebtID] FOREIGN KEY([DebtID])
REFERENCES [dbo].[Debts] ([DebtID])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_DebtID]
GO


