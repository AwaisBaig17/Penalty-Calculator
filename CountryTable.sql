USE [API4099]
GO

/****** Object:  Table [dbo].[countryTable]    Script Date: 8/25/2022 2:39:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF not exists (Select 1 from sys.objects where object_id = OBJECT_ID(N'countryTable'))
Begin

CREATE TABLE [dbo].[countryTable](
	[countryID] [int] IDENTITY(1,1) NOT NULL,
	[countryName] [varchar](50) NOT NULL,
	[countryCurrency] [varchar](50) NOT NULL,
	[currencyCode] [varchar](10) NOT NULL,
	[countryWeekend] [varchar](10) NOT NULL,
	[countryTax] [int] NOT NULL,
	[countryPenaltyRate] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[countryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
End
GO


