using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_message : System.Web.UI.Page
    {
        private schedInterface.messages _messages = new schedInterface.messages();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _messages.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/announcements");
        }
    }
}