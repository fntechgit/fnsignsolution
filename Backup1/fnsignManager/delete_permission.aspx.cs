using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_permission : System.Web.UI.Page
    {
        private schedInterface.permissions _permissions = new schedInterface.permissions();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _permissions.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/permissions/" + Page.RouteData.Values["user_id"]);
        }
    }
}