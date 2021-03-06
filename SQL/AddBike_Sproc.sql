USE [BikeStore]
GO
/****** Object:  StoredProcedure [dbo].[SaveBike]    Script Date: 8/15/2021 6:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SaveBike]
	-- Add the parameters for the stored procedure here
	@Id bigint OUTPUT,
	@model nvarchar(30)
	,@price money
	,@quantity int
	,@available int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF ISNULL(@Id, 0) = 0
	BEGIN
		INSERT INTO [dbo].[Bike]
		(
			[Model]
			,[Price]
			,[Quantity]
			,[Available]
		)
		VALUES
		(
			@model
			,@price
			,@quantity
			,@available
		)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Bike]
		SET
			Model = ISNULL(@model, Model)
			,Price = ISNULL(@price, Price)
			,Quantity = ISNULL(@quantity, Quantity)
			,Available = ISNULL(@available, Available)
		WHERE [Id] = @Id
	END

	IF @Id IS NOT null
		SELECT @Id;

	ELSE
		SELECT @Id = SCOPE_IDENTITY();
END
