IF COL_LENGTH('[dbo].[sessions]','sub_type') IS NULL
BEGIN
	ALTER TABLE [dbo].[sessions]
	  ADD [sub_type] [varchar](255) NULL; 
END