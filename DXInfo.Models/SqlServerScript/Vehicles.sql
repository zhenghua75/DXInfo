IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehicles]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Vehicles](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newsequentialid()) primary key,
	[PlateNo] [nvarchar](50) NOT NULL,
	[BrandModel] [nvarchar](50) NULL,
	[MotorNo] [nvarchar](50) NULL,
	[Comment] [nvarchar](50) NULL,
	[OwnerName] [nvarchar](200) NULL,
)
end

