SET IDENTITY_INSERT [dbo].[Cart] ON;

INSERT INTO [dbo].[Cart] ([Id], [UserId], [CartTypeId]) VALUES
(20, 2, 1),
(21, 2, 2),
(22, 3, 3),
(23, 3, 2)

SET IDENTITY_INSERT [dbo].[Cart] OFF;

INSERT INTO [dbo].[CartItem] ([CartId], [ProductId]) VALUES
(20, 1),
(20, 3),
(20, 4),
(20, 2),

(21, 1),
(21, 3),
(21, 6),

(22, 3),
(22, 8),
(22, 2),

(23, 4),
(23, 5),
(23, 2)