using System;
using System.Web.Routing;

namespace fnsignManager
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
            routes.MapPageRoute("no-events-route", "login/{noevents}", "~/login.aspx");
            routes.MapPageRoute("logout-route", "logout", "~/logout.aspx");

            // ######################## DASHBOARD SECTION ################################### //
            routes.MapPageRoute("dashboard-route", "dashboard", "~/dashboard.aspx");

            // ########################## USERS SECTION ################################# //
            routes.MapPageRoute("users-route", "users", "~/users.aspx");
            routes.MapPageRoute("users-add-route", "users/add", "~/add_user.aspx");
            routes.MapPageRoute("users-edit-route", "users/edit/{id}", "~/add_user.aspx");
            routes.MapPageRoute("users-delete-route", "users/delete/{id}", "~/delete_user.aspx");

            // ######################## PERMISSIONS SECTION ################################### //
            routes.MapPageRoute("permissions-by-user-route", "permissions/{id}", "~/permissions.aspx");
            routes.MapPageRoute("permissions-add-route", "permissions/add/{id}", "~/add_permission.aspx");
            routes.MapPageRoute("permissions-edit-route", "permissions/edit/{id}/{permission_id}", "~/add_permission.aspx");
            routes.MapPageRoute("permissions-delete-route", "permissions/delete/{user_id}/{id}", "~/delete_permission.aspx");

            // ######################## EVENTS SECTION ################################### //
            routes.MapPageRoute("events-manage-route", "events", "~/events.aspx");
            routes.MapPageRoute("events-add-route", "events/add", "~/add_event.aspx");
            routes.MapPageRoute("events-edit-rotue", "events/edit/{id}", "~/add_event.aspx");
            routes.MapPageRoute("events-delete-route", "events/delete/{id}", "~/delete_event.aspx");

            // ####################### SESSIONS SECTION ########################### //
            routes.MapPageRoute("sessions-route", "sessions", "~/sessions.aspx");

            // ####################### LOCATIONS ROUTE ##########################//
            routes.MapPageRoute("locations-route", "locations", "~/locations.aspx");

            // ####################### TEMPLATES ROUTE ############################ //
            routes.MapPageRoute("templates-add-route", "templates/add", "~/template_add.aspx");
            routes.MapPageRoute("templates-update-route", "templates/edit/{id}", "~/template_add.aspx");

            // this has to be last!!!!
            //routes.MapPageRoute("page-by-url-route", "{url}", "~/page.aspx");
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