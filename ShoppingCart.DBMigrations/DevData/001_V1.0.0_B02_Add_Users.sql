SET IDENTITY_INSERT [dbo].[User] ON;

INSERT INTO [dbo].[User] ([Id], [Dni], [Name], [IsVip]) VALUES
(1, 20345678901, 'Juan Pérez', 0),
(2, 27456789012, 'María Gómez', 1),
(3, 30123456789, 'Carlos Rodríguez', 0),
(4, 31234567890, 'Lucía Fernández', 0),
(5, 27890123456, 'Sofía Martínez', 1),
(6, 29876543210, 'Martín López', 0),
(7, 26789012345, 'Ana Torres', 1),
(8, 30987654321, 'Javier Morales', 0),
(9, 28432109876, 'Valentina Herrera', 0),
(10, 27654321098, 'Tomás Ruiz', 1);

SET IDENTITY_INSERT [dbo].[User] OFF;
