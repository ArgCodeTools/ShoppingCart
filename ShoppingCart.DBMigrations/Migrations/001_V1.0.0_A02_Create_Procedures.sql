CREATE PROCEDURE [dbo].[SP_User_GetById]
    @p_Id INT
AS
BEGIN
    SELECT [Id], [Dni], [Name]
    FROM [User]
    WHERE [Id] = @p_Id;
END
GO

CREATE PROCEDURE [dbo].[SP_User_GetByDni]
    @p_Dni BIGINT
AS
BEGIN
    SELECT [Id], [Dni], [Name]
    FROM [User]
    WHERE [Dni] = @p_Dni;
END
GO

CREATE PROCEDURE [dbo].[SP_User_Exists]
    @p_Dni BIGINT
AS
BEGIN
    SELECT CAST(CASE 
        WHEN EXISTS (SELECT 1 FROM [User] WHERE [Dni] = @p_Dni) 
        THEN 1 
        ELSE 0 
    END AS BIT);
END
GO

CREATE PROCEDURE [dbo].[SP_Product_GetById]
    @p_Id INT
AS
BEGIN
    SELECT [Id], [Name], [Price]
    FROM [Product]
    WHERE [Id] = @p_Id;
END
GO

CREATE PROCEDURE [dbo].[SP_Product_Exists]
    @p_Id BIGINT
AS
BEGIN
    SELECT CAST(CASE 
        WHEN EXISTS (SELECT 1 FROM [Product] WHERE [Id] = @p_Id) 
        THEN 1 
        ELSE 0 
    END AS BIT);
END
GO

CREATE PROCEDURE [dbo].[SP_Cart_GetById]
    @p_Id INT
AS
BEGIN
    SELECT [Id], [UserId], [CartTypeId]
    FROM [Cart]
    WHERE [Id] = @p_Id;
END
GO

CREATE PROCEDURE [dbo].[SP_CartItem_GetByCartId]
    @p_CartId INT
AS
BEGIN
    SELECT p.[Id], 
           p.[Name],
           p.[Price]
    FROM [CartItem] AS ci
    INNER JOIN [Product] AS p ON ci.[ProductId] = p.[Id]
    WHERE ci.[CartId] = @p_CartId;
END
GO

CREATE PROCEDURE [dbo].[SP_Cart_Exists]
    @p_Id BIGINT
AS
BEGIN
    SELECT CAST(CASE 
        WHEN EXISTS (SELECT 1 FROM [Cart] WHERE [Id] = @p_Id) 
        THEN 1 
        ELSE 0 
    END AS BIT);
END
GO

CREATE PROCEDURE [dbo].[SP_Cart_Insert]
    @p_UserId INT,
    @p_CartTypeId TINYINT,
    @p_CartId INT OUTPUT
AS
BEGIN
  INSERT INTO [Cart] ([UserId], [CartTypeId])
  VALUES (@p_UserId, @p_CartTypeId);
  
  SET @p_CartId = SCOPE_IDENTITY();   
END
GO

CREATE PROCEDURE [dbo].[SP_Cart_Remove]
    @p_Id INT
AS
BEGIN
   DELETE FROM [CartItem] WHERE [CartId] = @p_Id;
   DELETE FROM [Cart] WHERE [Id] = @p_Id;
END
GO

CREATE PROCEDURE [dbo].[SP_CartItem_Add]
    @p_CartId INT,
    @p_ProductId INT
AS
BEGIN
   INSERT INTO [CartItem] ([CartId], [ProductId])
   VALUES (@p_CartId, @p_ProductId);
END
GO

CREATE PROCEDURE [dbo].[SP_CartItem_Remove]
    @p_CartId INT,
    @p_ProductId INT
AS
BEGIN
   DELETE FROM [CartItem] 
   WHERE [CartId] = @p_CartId AND [ProductId] = @p_ProductId;
END
GO

CREATE PROCEDURE [dbo].[SP_Product_GetMostExpensiveByUser]
    @p_UserDni BIGINT,
    @p_Top INT
AS
BEGIN
    SELECT TOP (@p_Top)
        p.[Id],
        p.[Name],
        p.[Price]
    FROM [CartItem] AS ci
    INNER JOIN [Cart] AS c ON ci.[CartId] = c.[Id]
    INNER JOIN [User] AS u ON c.[UserId] = u.[Id]
    INNER JOIN [Product] AS p ON ci.[ProductId] = p.[Id]
    WHERE u.[Dni] = @p_UserDni
    GROUP BY p.[Id], p.[Name], p.[Price]
    ORDER BY p.[Price] DESC;
END
GO
