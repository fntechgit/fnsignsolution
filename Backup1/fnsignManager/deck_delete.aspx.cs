using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class deck_delete : System.Web.UI.Page
    {
        private schedInterface.decks _decks = new schedInterface.decks();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _decks.delete(Convert.ToInt32(Page.RouteData.Values["id"]));

            Response.Redirect("/decks");
        }
    }
}