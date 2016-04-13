<%@ Page Title="FNSIGN Dashboard" Language="C#" MasterPageFile="~/fnsign.master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="fnsignManager.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
		                <li class="nav-active">
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
                        <li>
                            <a href="/decks">
                                <i class="fa fa-photo" aria-hidden="true"></i>
                                Decks
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
                        <li id="event_link" runat="server" Visible="false">
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
		                <h2>Dashboard</h2>
					
		                <div class="right-wrapper pull-right">
		                    <ol class="breadcrumbs">
		                        <li>
		                            <a href="/">
		                                <i class="fa fa-tachometer"></i>
		                            </a>
		                        </li>
		                        <li><span>Dashboard</span></li>
		                    </ol>
					
		                    <a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
		                </div>
		            </header>

		            <!-- start: page -->
		            <div class="row">
		                <div class="col-md-6 col-lg-12 col-xl-6">
		                    <div class="row">
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-primary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-primary">
		                                                <i class="fa fa-star"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Sessions</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= all_sessions %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/sessions" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-secondary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-secondary">
		                                                <i class="fa fa-map-marker"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Locations</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= locations %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/locations" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-tertiary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-tertiary">
		                                                <i class="fa fa-twitter"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Tweets</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= tweets %></strong>
		                                                </div>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                        <div class="col-md-12 col-lg-6 col-xl-6">
		                            <section class="panel panel-featured-left panel-featured-quartenary">
		                                <div class="panel-body">
		                                    <div class="widget-summary">
		                                        <div class="widget-summary-col widget-summary-col-icon">
		                                            <div class="summary-icon bg-quartenary">
		                                                <i class="fa fa-desktop"></i>
		                                            </div>
		                                        </div>
		                                        <div class="widget-summary-col">
		                                            <div class="summary">
		                                                <h4 class="title">Assignments</h4>
		                                                <div class="info">
		                                                    <strong class="amount"><%= terminals %></strong>
		                                                </div>
		                                            </div>
		                                            <div class="summary-footer">
		                                                <a href="/assignments" class="text-muted text-uppercase">(view all)</a>
		                                            </div>
		                                        </div>
		                                    </div>
		                                </div>
		                            </section>
		                        </div>
		                    </div>
		                </div>
		            </div>

		            <!-- end: page -->
		        </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="footer_scripts" runat="server">
    <script src="assets/vendor/jquery-ui/js/jquery-ui-1.10.4.custom.js"></script>
	<script src="assets/vendor/jquery-ui-touch-punch/jquery.ui.touch-punch.js"></script>
	<script src="assets/vendor/jquery-appear/jquery.appear.js"></script>
	
	<script src="assets/vendor/jquery-easypiechart/jquery.easypiechart.js"></script>
	<script src="assets/vendor/flot/jquery.flot.js"></script>
	<script src="assets/vendor/flot-tooltip/jquery.flot.tooltip.js"></script>
	<script src="assets/vendor/flot/jquery.flot.pie.js"></script>
	<script src="assets/vendor/flot/jquery.flot.categories.js"></script>
	<script src="assets/vendor/flot/jquery.flot.resize.js"></script>
	<script src="assets/vendor/jquery-sparkline/jquery.sparkline.js"></script>
	<script src="assets/vendor/raphael/raphael.js"></script>
	<script src="assets/vendor/morris/morris.js"></script>
	<script src="assets/vendor/gauge/gauge.js"></script>
	<script src="assets/vendor/snap-svg/snap.svg.js"></script>
	<script src="assets/vendor/liquid-meter/liquid.meter.js"></script>
	<script src="assets/vendor/jqvmap/jquery.vmap.js"></script>
	<script src="assets/vendor/jqvmap/data/jquery.vmap.sampledata.js"></script>
	<script src="assets/vendor/jqvmap/maps/jquery.vmap.world.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.africa.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.asia.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.australia.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.europe.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.north-america.js"></script>
	<script src="assets/vendor/jqvmap/maps/continents/jquery.vmap.south-america.js"></script>
    
    <!-- Examples -->
	
</asp:Content>
