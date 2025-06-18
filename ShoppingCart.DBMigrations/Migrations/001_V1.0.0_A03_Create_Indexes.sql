CREATE NONCLUSTERED INDEX IX_Cart_UserId
ON [Cart](UserId);

CREATE NONCLUSTERED INDEX IX_CartItem_CartId
ON [CartItem](CartId);

CREATE NONCLUSTERED INDEX IX_CartItem_ProductId
ON [CartItem](ProductId);

CREATE NONCLUSTERED INDEX IX_Product_Price
ON [Product](Price);
