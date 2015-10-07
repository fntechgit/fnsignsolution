using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class resolutions
    {
        private schedDataContext db = new schedDataContext();

        public List<Resolution> all()
        {
            List<Resolution> _resolutions = new List<Resolution>();

            var result = from res in db.resolutions
                orderby res.title
                select res;

            foreach (var item in result)
            {
                Resolution r = new Resolution();

                r.height = item.height;
                r.id = item.id;
                r.title = item.title;
                r.width = item.width;

                _resolutions.Add(r);
            }

            return _resolutions;
        }
    }

    public class Resolution
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public Int32? width { get; set; }
        public Int32? height { get; set; }
    }
}
