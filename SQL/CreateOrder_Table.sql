USE [BikeStore]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 8/15/2021 6:50:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BikeId] [nvarchar](30) NULL,
	[QuantityOfBikes] [int] NULL,
	[CustomerId] [bigint] NULL,
	[Price] [money] NULL
) ON [PRIMARY]
GO


