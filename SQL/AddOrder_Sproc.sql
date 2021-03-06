USE [BikeStore]
GO
/****** Object:  StoredProcedure [dbo].[SaveOrder]    Script Date: 8/15/2021 6:47:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SaveOrder]
	-- Add the parameters for the stored procedure here
	@Id bigint OUTPUT
	,@bikeId nvarchar(30)
	,@quantityOfBikes int
	,@customerId bigint
	,@price money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF ISNULL(@Id, 0) = 0
	INSERT INTO [dbo].[Order]
	(
		[BikeId]
		,[QuantityOfBikes]
		,[CustomerId]
		,[Price]
	)
	VALUES
	(
		@bikeId
		,@quantityOfBikes
		,@customerId
		,@price
	)
	SELECT @Id = SCOPE_IDENTITY()
END
