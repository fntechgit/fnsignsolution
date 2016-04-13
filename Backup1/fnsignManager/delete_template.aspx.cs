using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_template : System.Web.UI.Page
    {
        private schedInterface.templates _templates = new schedInterface.templates();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _templates.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/templates");
        }
    }
}