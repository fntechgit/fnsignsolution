<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="overlay_1920V_no_twitter.aspx.cs" Inherits="fnsignDisplay.overlays.overlay_1920V_no_twitter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    	<script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
		<script src="http://momentjs.com/downloads/moment.min.js"></script>
		
		<style type="text/css">

			body { width: 1080px; height: 1920px; background-color: #000000; font-family: "Gotham Narrow Book", Gotham; font-size: 36px;color: #000000;background-image: url('<%= fnsignUrl %>/uploads/<%= bgimage %>'); }	
			.wrapper { width: 1080px; height: 1920px;padding: 40px;  }
			.session-type {font-family: "Gotham Narrow Book", Gotham; margin-top: 340px; font-size: 50px; margin-bottom:0px;color: #000000;width: 100%; }
			.session-title {font-family: "Gotham Narrow Book", Gotham; font-size: 36px;font-weight:bold;margin-bottom: 40px;margin-top:0px;color: #82262f; }
			.start-time { margin-top: 55px;font-family: "Gotham Narrow Book", Gotham;color: #000000;font-size: 80px;float: left;width: 387px; }
			.current-time { float: left;margin-top: 85px;color: #82262f;font-size: 48px;font-family: "Gotham Narrow Book", Gotham; }
			.next-header { margin-top: 50px;color: #82262f;font-family: "Gotham Narrow Book", Gotham;font-size: 35px; }
			.next-session { margin-top: 0px;color: #000000;font-family: "Gotham Narrow Book", Gotham;font-size: 36px; }
			.left { float:left; }
			.right { float:right; }
			.next { margin-top: 55px;font-size: 24px; }
			#current_date { text-transform: uppercase; }
			
		</style>

</head>
<body id="thebody">
    <form id="form1" runat="server">
    
    <asp:HiddenField runat="server" ID="event_id" />
    <asp:HiddenField runat="server" ID="location_sched" />
    <asp:HiddenField runat="server" ID="terminal_id" />
    
    <div class="wrapper">
        <div class="session-type" id="session_title"><%= session_title %></div>
        <div class="session-title" id="session_type"><%= session_type %></div>
        <div class="start-time" id="start_time"><%= start_time %></div>
        <div class="current-time" id="current_time">CURRENT TIME IS <span id="current_date"></span></div>
        <div style="clear:both;"></div>
        <div class="next-header">NEXT SESSION</div>
        <div class="next-session" id="next_session"><%= next_session %></div>
    </div>
    
    <script type="text/javascript">

        function setDate() {

            $("#current_date").text(moment().format('h:mm a'));

        }

        setInterval(setDate, 1000);
		
		</script>
        
        <script type="text/javascript" src="/js/display.js"></script>

    </form>
</body>
</html>
