USE [master]
GO

CREATE LOGIN [wm] WITH PASSWORD=N'wm', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [wm] ENABLE
GO

USE [ShopDb]
GO
/****** Object:  User [wm]    Script Date: 15/06/2020 14:29:36 ******/
CREATE USER [wm] FOR LOGIN [wm] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [wm]
GO

/****** Object:  Table [dbo].[Categories]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Supplier] [nvarchar](50) NOT NULL,
	[Price] [numeric](15, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Computer Components')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Audio/Video')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Servers')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Description], [CategoryId], [Make], [Supplier], [Price]) VALUES (62, N'Computer Mouse', 3, N'Genius', N'Greece', CAST(71.99 AS Numeric(15, 2)))
GO
INSERT [dbo].[Products] ([Id], [Description], [CategoryId], [Make], [Supplier], [Price]) VALUES (86, N'Camcorder', 2, N'Samsung', N'USA', CAST(99.99 AS Numeric(15, 2)))
GO
INSERT [dbo].[Products] ([Id], [Description], [CategoryId], [Make], [Supplier], [Price]) VALUES (87, N'USB Flash Memory', 1, N'Kingston', N'USA', CAST(21.99 AS Numeric(15, 2)))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_ProductCategory]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_product]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_delete_product]
    @id    int
as
begin
	delete from dbo.Products
	where id = @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_get_categories]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_get_categories]	
as
begin
	select
		Id,
		Name
	from dbo.Categories
end
GO
/****** Object:  StoredProcedure [dbo].[sp_get_products]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_get_products]	
as
begin
	select
		Id,
		Description,
		CategoryId,
		Make,
		Supplier,
		Price
	from dbo.Products
end
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_product]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_insert_product]
	--@id int,
    @description nvarchar (500),
    @category    int,
    @make        nvarchar (50),
    @supplier    nvarchar (50),
    @price       numeric (15, 2)
as
begin
	insert into dbo.Products
	--(Id, Description, CategoryId, Make, Supplier, Price)
	(Description, CategoryId, Make, Supplier, Price)
	values 
	--(@id, @description, @category, @make, @supplier, @price)
	(@description, @category, @make, @supplier, @price)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_update_product]    Script Date: 15/06/2020 14:29:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_update_product]
	@id int,
    @description nvarchar (500),
    @category    int,
    @make        nvarchar (50),
    @supplier    nvarchar (50),
    @price       numeric (15, 2)
as
begin
	update dbo.Products
	set
		Description = @description,
		CategoryId = @category,
		Make = @make,
		Supplier = @supplier,
		Price = @price
	where Id = @id
end
GO
