USE [fntech_signage]
GO
/****** Object:  StoredProcedure [dbo].[events_to_update]    Script Date: 3/1/2018 11:55:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[events_to_update]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select * from events
    --where last_update<DATEADD(minute, interval, GETDATE())
	--where GETDATE() > DATEADD(minute, interval, last_update)
	where GETDATE() < event_end
    
END
