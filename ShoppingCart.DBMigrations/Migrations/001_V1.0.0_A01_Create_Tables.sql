BEGIN TRY
    BEGIN TRANSACTION;

    CREATE TABLE [User] (
        [Id] INT IDENTITY(1,1),
        [Dni] BIGINT NOT NULL,
        [Name] NVARCHAR(50) NOT NULL,
        [IsVip] BIT NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_User_Dni] UNIQUE ([Dni])
    );

    CREATE TABLE [Product] (
        [Id] INT IDENTITY(1,1),
        [Name] NVARCHAR(50) NOT NULL,
        [Price] DECIMAL(9, 2) NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_Product_Name] UNIQUE ([Name])
    );

    CREATE TABLE [CartType] (
        [Id] TINYINT,
        [Name] NVARCHAR(50) NOT NULL,
        CONSTRAINT [PK_CartType] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_CartType_Name] UNIQUE ([Name])
    );

    CREATE TABLE [Cart] (
        [Id] INT IDENTITY(1,1),
        [UserId] INT NOT NULL,
        [CartTypeId] TINYINT NOT NULL,
        CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Cart_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
        CONSTRAINT [FK_Cart_CartTypeId] FOREIGN KEY ([CartTypeId]) REFERENCES [CartType]([Id]),
    );
    
    CREATE TABLE [CartItem] (
        [CartId] INT NOT NULL,
        [ProductId] INT NOT NULL,        
        CONSTRAINT [PK_CartItem] PRIMARY KEY ([CartId], [ProductId]),
        CONSTRAINT [FK_CartItem_CartId] FOREIGN KEY ([CartId]) REFERENCES [Cart]([Id]),
        CONSTRAINT [FK_CartItem_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
    );

    -- Insert initial data into [CartType]
    INSERT INTO [CartType] ([Id], [Name]) VALUES (1, 'Common');
    INSERT INTO [CartType] ([Id], [Name]) VALUES (2, 'SpecialDate');
    INSERT INTO [CartType] ([Id], [Name]) VALUES (3, 'Vip');

    COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
    THROW;
END CATCH