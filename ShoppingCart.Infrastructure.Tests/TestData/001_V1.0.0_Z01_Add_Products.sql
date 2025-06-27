SET IDENTITY_INSERT [dbo].[Product] ON;

INSERT INTO [dbo].[Product] ([Id], [Name], [Price]) VALUES
(1, 'Remera negra', 10000.50),
(2, 'Campera', 20000.50),
(3, 'Zapato', 30000.50),
(4, 'Pantalon', 40000.49),
(5, 'Buzo', 50000.49),
(6, 'Chaleco', 60000.51),
(7, 'Medias', 70000.99),
(8, 'Bufanda', 80000.00)

SET IDENTITY_INSERT [dbo].[Product] OFF;
