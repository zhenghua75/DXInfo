IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvPrice]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[InvPrice](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[InvId] [uniqueidentifier] NOT NULL,
	[SalePrice] [decimal](24, 2) NOT NULL,
	[SalePoint] [decimal](24, 2) NOT NULL,
	[IsInvalid] [bit] NOT NULL DEFAULT ((0)),
	[Comment] [nvarchar](50) NULL,
)
end

