using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace fnsignDisplay
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // this is the routing engine
            RegisterRoutes(RouteTable.Routes);
        }

        void RegisterRoutes(RouteCollection routes)
        {
            // ######################## LOGIN SECTION ################################### //
            routes.MapPageRoute("login-route", "login", "~/login.aspx");
            routes.MapPageRoute("details-route", "details", "~/details.aspx");
            routes.MapPageRoute("display-route", "displays/{id}", "~/display.aspx");

            // ####################### DISPLAYS SECTION ################################# //
            routes.MapPageRoute("no-twitter-1920-route", "no-twitter-1920/{id}", "~/overlays/overlay_1920V_no_twitter.aspx");
            routes.MapPageRoute("twitter-1920-route", "twitter-1920/{id}", "~/overlays/overlay_1920V_twitter.aspx");
            routes.MapPageRoute("blank-1920-route", "blank-1920/{id}", "~/overlays/overlay_blank.aspx");
            routes.MapPageRoute("marketplace-1920-route", "market-1920/{id}", "~/overlays/overlay_1920_market.aspx");
            routes.MapPageRoute("brownbag-route", "brownbag-1920/{id}", "~/overlays/overlay_1920_brownbag.aspx");
            routes.MapPageRoute("design-summit-route", "summit-1920/{id}", "~/overlays/overlay_1920_summit.aspx");
            routes.MapPageRoute("housekeeping-route", "housekeeping/{id}", "~/overlays/overlay_housekeeping_slides.aspx");
            routes.MapPageRoute("ocp-route", "ocp/{id}", "~/overlays/overlay_ocp.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}