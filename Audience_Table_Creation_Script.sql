USE [URDEV_SW_MOBILITY]
GO

/****** Object:  Table [dbo].[Audience]    Script Date: 30/01/2021 18:35:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Audience](
	[ClientId] [nvarchar](150) NOT NULL,
	[Secret] [nvarchar](max) NOT NULL,
	[ClientName] [nvarchar](150) NOT NULL,
	[Issuer] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[StatusCode] [smallint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 : Active - 1 : Disabled - 2 : Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Audience', @level2type=N'COLUMN',@level2name=N'ModifiedOn'
GO

