using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using schedInterface;

namespace fnsignManager.services
{
    /// <summary>
    /// Summary description for slides
    /// </summary>
    [WebService(Namespace = "https://fnsign.fntech.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class slides : System.Web.Services.WebService
    {

        private schedInterface.slides _slides = new schedInterface.slides();

        [WebMethod(Description = "Reorder Awards")]
        public Boolean reorder(string _photos)
        {
            ArrayList pageList = new ArrayList(_photos.Split(new char[] { ',' }));

            int pageCount = 1;

            foreach (string photo in pageList)
            {
                Slide s = _slides.single(Convert.ToInt32(photo));

                s.display = pageCount;

                _slides.reorder(s);

                pageCount++;
            }

            return true;
        }
    }
}
