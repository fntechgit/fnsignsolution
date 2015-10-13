using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_event : System.Web.UI.Page
    {
        private schedInterface.events _events = new schedInterface.events();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _events.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/events");
        }
    }
}