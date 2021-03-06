USE [BikeStore]
GO
/****** Object:  StoredProcedure [dbo].[SaveCustomer]    Script Date: 8/15/2021 6:47:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SaveCustomer]
	-- Add the parameters for the stored procedure here
	@Id bigint OUTPUT
	,@firstName nvarchar(30)
	,@lastName nvarchar(30)
	,@numberOfOrders int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF ISNULL(@Id, 0) = 0 OR @Id = 0
	BEGIN
		INSERT INTO [dbo].[Customer]
		(
			[FirstName]
			,[LastName]
			,[NumberOfOrders]
		)
		VALUES
		(
			@firstName
			,@lastName
			,@numberOfOrders
		)
	
		SET @Id = SCOPE_IDENTITY();
	END
	
	ELSE
	BEGIN
		UPDATE [dbo].[Customer]
		SET
			FirstName = ISNULL(@firstName, FirstName)
			,LastName = ISNULL(@lastName, LastName)
			,NumberOfOrders = ISNULL(@numberOfOrders, NumberOfOrders)
		WHERE [Id] = @id AND [FirstName] = @firstName AND [LastName] = @lastName;
		SET @id = @id;
	END
	
	SELECT @id;
END
