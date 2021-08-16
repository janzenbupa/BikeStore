USE [BikeStore]
GO

/****** Object:  Table [dbo].[Bike]    Script Date: 8/15/2021 6:50:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bike](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](30) NULL,
	[Price] [money] NULL,
	[Quantity] [int] NULL,
	[Available] [int] NULL
) ON [PRIMARY]
GO


