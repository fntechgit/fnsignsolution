IF COL_LENGTH('[dbo].[sessions]','speaker_job_titles') IS NULL
BEGIN
	ALTER TABLE [dbo].[sessions]
	  ADD [speaker_job_titles] [varchar](max) NULL; 
END