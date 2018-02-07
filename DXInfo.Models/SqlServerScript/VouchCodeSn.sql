IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VouchCodeSn]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[VouchCodeSn](
	[VouchCode] [varchar](50) NOT NULL,
	[YearMonth] [int] NOT NULL,
	[Sn] [int] NOT NULL,
 CONSTRAINT [PK_VouchCodeSn] PRIMARY KEY CLUSTERED 
(
	[VouchCode] ASC,
	[YearMonth] ASC
)
)
end
