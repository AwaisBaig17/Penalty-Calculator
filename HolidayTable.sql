USE [API4099]
GO

/****** Object:  Table [dbo].[holidayTable]    Script Date: 8/25/2022 2:52:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF Not Exists (Select 1 From sys.objects where object_id = OBJECT_ID(N'[holidayTable]'))
Begin
CREATE TABLE [dbo].[holidayTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[holidayName] [varchar](50) NOT NULL,
	[holidayDate] [date] NOT NULL,
	[countryID] [int] NULL
) ON [PRIMARY]
End
GO

ALTER TABLE [dbo].[holidayTable]  WITH CHECK ADD FOREIGN KEY([countryID])
REFERENCES [dbo].[countryTable] ([countryID])
GO


