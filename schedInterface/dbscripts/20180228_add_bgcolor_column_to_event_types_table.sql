IF COL_LENGTH('[dbo].[event_types]','bg_color') IS NULL
BEGIN
	ALTER TABLE [dbo].[event_types]
	  ADD [bg_color] [varchar](255) NOT NULL DEFAULT ''; 
END