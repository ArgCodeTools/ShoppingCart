SET IDENTITY_INSERT [dbo].[User] ON;

INSERT INTO [dbo].[User] ([Id], [Dni], [Name], [IsVip]) VALUES
(2, 1122334455, 'Roberto Gomez', 1),
(3, 66778899, 'Florinda Meza', 0)

SET IDENTITY_INSERT [dbo].[User] OFF;
