IF  not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbAssociator]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[tbAssociator](
	[iAssID] [bigint] IDENTITY(1,1) NOT NULL,
	[vcCardID] [varchar](10) NOT NULL,
	[vcAssName] [varchar](30) NULL,
	[vcSpell] [varchar](10) NULL,
	[vcAssNbr] [varchar](20) NULL,
	[vcLinkPhone] [varchar](25) NULL,
	[vcLinkAddress] [varchar](100) NULL,
	[vcEmail] [varchar](30) NULL,
	[vcAssType] [varchar](5) NULL,
	[vcAssState] [char](1) NULL,
	[nCharge] [numeric](10, 2) NULL,
	[iIgValue] [int] NULL,
	[vcCardFlag] [char](1) NULL,
	[vcComments] [varchar](200) NULL,
	[dtCreateDate] [datetime] NULL,
	[dtOperDate] [datetime] NULL,
	[vcDeptID] [varchar](5) NULL,
	[vcCardSerial] [varchar](40) NULL,
 CONSTRAINT [PK_TBASSOCIATOR] PRIMARY KEY CLUSTERED 
(
	[iAssID] ASC,
	[vcCardID] ASC
)
) 
end