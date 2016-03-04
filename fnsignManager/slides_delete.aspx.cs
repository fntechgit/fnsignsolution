using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class slides_delete : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            schedInterface.slides _slides = new schedInterface.slides();

            Slide s = _slides.single(Convert.ToInt32(Page.RouteData.Values["id"]));

            _slides.delete(s.id);

            Response.Redirect("/decks/slides/" + s.deck_id);
        }
    }
}