﻿<%@ Page Title="FNSIGN > Add Event" Language="C#" MasterPageFile="~/fnsign.master" AutoEventWireup="true" CodeBehind="add_event.aspx.cs" Inherits="fnsignManager.add_event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Specific Page Vendor CSS -->
    
    <style type="text/css">
        
        #map-canvas { width: 100%;height: 400px;margin: 0;padding: 20;}
        
    </style>
    
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDgX2zp3QpXQtiIF9X081B-_MP0XX8GH5U"></script>
    
    <script type="text/javascript">
        function initialize() {

            var lat = $("#hdn_latitude").val();
            var long = $("#hdn_longitude").val();

            var myLatlng = new google.maps.LatLng(lat, long);
            var mapOptions = {
                zoom: 11,
                center: myLatlng
            }
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

            // To add the marker to the map, use the 'map' property
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: "Event Location"
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="navigation_sidebar" runat="server">
    <!-- start: sidebar goes into the  -->
	<aside id="sidebar-left" class="sidebar-left">
				
		<div class="sidebar-header">
		    <div class="sidebar-title">
		        Navigation
		    </div>
		    <div class="sidebar-toggle hidden-xs" data-toggle-class="sidebar-left-collapsed" data-target="html" data-fire-event="sidebar-left-toggle">
		        <i class="fa fa-bars" aria-label="Toggle sidebar"></i>
		    </div>
		</div>
				
		<div class="nano">
		    <div class="nano-content">
		        <nav id="menu" class="nav-main" role="navigation">
		            <ul class="nav nav-main">
		                <li>
		                    <a href="/">
		                        <i class="fa fa-tachometer" aria-hidden="true"></i>
		                        <span>Dashboard</span>
		                    </a>
		                </li>
                        <li class="nav-parent">
                            <a href="/templates">
                                <i class="fa fa-edit" aria-hidden="true"></i>
                                <span>Templates</span>
                            </a>
                        </li>
		                <li>
		                    <a href="/announcements">
		                        <i class="fa fa-bullhorn" aria-hidden="true"></i>
		                        Announcements
		                    </a> 
		                </li>
                        <li class="nav-parent">
                            <a href="/sessions">
                                <i class="fa fa-star" aria-hidden="true"></i>
                                <span>Sessions</span>
                            </a>
                        </li>
                        <li id="preference_link" runat="server" Visible="false">
		                    <a href="/locations">
		                        <i class="fa fa-map-marker" aria-hidden="true"></i>
		                        <span>Locations</span>
		                    </a>
		                </li>
                        <li id="display_link" runat="server" Visible="false">
		                    <a href="/assignments">
		                        <i class="fa fa-desktop" aria-hidden="true"></i>
		                        <span>Assignments</span>
		                    </a>
		                </li>
                        <li id="event_link" runat="server" Visible="false" class="nav-active">
		                    <a href="/events">
		                        <i class="fa fa-calendar" aria-hidden="true"></i>
		                        <span>Events</span>
		                    </a>
		                </li>
                        <li id="user_link" runat="server" Visible="false">
		                    <a href="/users">
		                        <i class="fa fa-users" aria-hidden="true"></i>
		                        <span>Users</span>
		                    </a>
		                </li>
		            </ul>
		        </nav>
		    </div>
				
		</div>
				
	</aside>
	<!-- end: sidebar -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_main" runat="server">
    
    <section role="main" class="content-body">
					<header class="page-header">
						<h2><%= add_edit.ToUpper() %> AN EVENT</h2>
					
						<div class="right-wrapper pull-right">
							<ol class="breadcrumbs">
								<li>
									<a href="/dashboard">
										<i class="fa fa-tachometer"></i> <span>Dashboard</span>
									</a>
								</li>
                                <li>
                                    <a href="/events"><i class="fa fa-users"></i> Events</a>
                                </li>
								<li><i class="fa fa-user"></i> <span><%= add_edit %> an Event</span></li>
							</ol>
					
							<a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
						</div>
					</header>
                    
                    <asp:Panel runat="server" ID="pnl_success" Visible="false">
                        <div class="alert alert-success">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                            <strong>Success!</strong> You have successfully performed an <%= add_edit %> operation for this user!
                        </div>
                    </asp:Panel>

					<!-- start: page -->
						<div class="row">
							<div class="col-lg-12">
								<section class="panel">
									<header class="panel-heading">
										<div class="panel-actions">
											<a href="#" class="fa fa-caret-down"></a>
											<a href="#" class="fa fa-times"></a>
										</div>
						
										<h2 class="panel-title"><%= add_edit %> an Event</h2>
									</header>
									<div class="panel-body">
										<div class="form-horizontal form-bordered">
											<div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Event Title</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="title" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Start Date</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-calendar"></i>
														</span>
													    <asp:TextBox runat="server" ID="start_date" ClientIDMode="Static" data-plugin-datepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Start Time</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-clock-o"></i>
														</span>
													    <asp:TextBox runat="server" ID="start_time" ClientIDMode="Static" data-plugin-timepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">End Date</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-calendar"></i>
														</span>
													    <asp:TextBox runat="server" ID="end_date" ClientIDMode="Static" data-plugin-datepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">End Time</label>
												<div class="col-md-6">
													<div class="input-group">
														<span class="input-group-addon">
															<i class="fa fa-clock-o"></i>
														</span>
													    <asp:TextBox runat="server" ID="end_time" ClientIDMode="Static" data-plugin-timepicker CssClass="form-control" />
													</div>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Event URL</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="url" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">API Key</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="api_key" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Twitter Username</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="username" CssClass="form-control" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label" for="search_text">Twitter Hashtags</label>
												<div class="col-md-6">
												    <asp:TextBox runat="server" ID="hashtags" CssClass="form-control" data-role="tagsinput" ClientIDMode="Static" />
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="col-md-3 control-label">Update Interval (minutes)</label>
												<div class="col-md-6">
													<div class="m-md slider-primary" data-plugin-slider data-plugin-options='{ "value": 30, "range": "min" : 5, "max": 60 }' data-plugin-slider-output="#listenSlider">
                                                        <asp:HiddenField runat="server" ID="listenSlider" ClientIDMode="Static" Value="60" />
													</div>
													<p class="output">Update Interval <code>(minutes)</code>: <b>30</b></p>
												</div>
											</div>
                                            
                                            <div class="form-group">
												<label class="control-label col-md-3"></label>
												<div class="col-md-9">
													<asp:Button runat="server" ID="btn_process" CssClass="mb-xs mt-xs mr-xs btn btn-primary" Text="Submit" OnClick="update" />
												</div>
											</div>
                                            
                                            

										</div>
									</div>
								</section>
							</div>
						</div>
					<!-- end: page -->
				</section>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
    
    <!-- Specific Page Vendor -->
    
    <script src="/assets/javascripts/forms/examples.advanced.form.js" /></script>

</asp:Content>