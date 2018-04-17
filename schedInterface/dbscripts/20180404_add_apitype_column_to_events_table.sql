IF COL_LENGTH('[dbo].[events]','api_type') IS NULL
BEGIN
	ALTER TABLE [dbo].[events]
	  ADD [api_type] [int] NOT NULL DEFAULT 0; 
END