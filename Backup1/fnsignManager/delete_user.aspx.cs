using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_user : System.Web.UI.Page
    {
        private schedInterface.users _users = new schedInterface.users();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _users.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/users");
        }
    }
}