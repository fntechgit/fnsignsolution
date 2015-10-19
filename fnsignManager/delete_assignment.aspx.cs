using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class delete_assignment : System.Web.UI.Page
    {
        private terminals _terminals = new terminals();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _terminals.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/assignments");
            
        }
    }
}