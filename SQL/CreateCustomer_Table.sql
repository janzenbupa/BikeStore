USE [BikeStore]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 8/15/2021 6:50:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[NumberOfOrders] [int] NULL
) ON [PRIMARY]
GO


