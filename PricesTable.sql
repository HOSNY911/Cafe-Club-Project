USE [CafeClub]
GO

/****** Object:  Table [dbo].[Prices]    Script Date: 11/24/2025 10:48:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prices](
	[PriceID] [int] IDENTITY(1,1) NOT NULL,
	[BilliardPrice] [decimal](10, 2) NOT NULL,
	[PingPongPrice] [decimal](10, 2) NOT NULL,
	[PlaystationPrice] [decimal](10, 2) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Createby] [int] NOT NULL,
	[Updatedby] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Prices_PriceID] PRIMARY KEY CLUSTERED 
(
	[PriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prices] ADD  DEFAULT ((1.00)) FOR [BilliardPrice]
GO

ALTER TABLE [dbo].[Prices] ADD  DEFAULT ((1.00)) FOR [PingPongPrice]
GO

ALTER TABLE [dbo].[Prices] ADD  DEFAULT ((1.00)) FOR [PlaystationPrice]
GO

ALTER TABLE [dbo].[Prices] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Prices] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Createby] FOREIGN KEY([Createby])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Createby]
GO

ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Updatedby] FOREIGN KEY([Updatedby])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Updatedby]
GO


