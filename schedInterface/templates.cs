using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class templates
    {
        private schedDataContext db = new schedDataContext();

        public List<Template> by_event(Int32 id)
        {
            List<Template> _templates = new List<Template>();

            var result = from tmp in db.templates
                where tmp.event_id == id
                select tmp;

            foreach (var item in result)
            {
                Template t = new Template();

                t.ad_interval = item.ad_interval;
                t.bgcolor = item.bgcolor;
                t.bgimage = item.bgimage;
                t.event_id = item.event_id;
                t.id = item.id;
                t.orientation = item.orientation;
                t.overlay = item.overlay;
                t.overlay_font = item.overlay_font;
                t.overlay_font_color = item.overlay_font_color;
                t.rotate_ads = item.rotate_ads;
                t.resolution = item.resolution;
                t.title = item.title;
                
                _templates.Add(t);
            }

            return _templates;
        }

        public Template add(Template t)
        {
            template tt = new template();

            tt.ad_interval = t.ad_interval;
            tt.bgcolor = t.bgcolor;
            tt.bgimage = t.bgimage;
            tt.event_id = t.event_id;
            tt.orientation = t.orientation;
            tt.overlay = t.overlay;
            tt.overlay_font = t.overlay_font;
            tt.overlay_font_color = t.overlay_font_color;
            tt.resolution = t.resolution;
            tt.rotate_ads = t.rotate_ads;
            tt.title = t.title;
            
            db.templates.InsertOnSubmit(tt);

            db.SubmitChanges();

            t.id = tt.id;

            return t;
        }

        public Template update(Template t)
        {
            template tt = db.templates.Single(x => x.id == t.id);

            tt.ad_interval = t.ad_interval;
            tt.bgcolor = t.bgcolor;
            tt.bgimage = t.bgimage;
            tt.event_id = t.event_id;
            tt.orientation = t.orientation;
            tt.overlay = t.overlay;
            tt.overlay_font = t.overlay_font;
            tt.overlay_font_color = t.overlay_font_color;
            tt.resolution = t.resolution;
            tt.rotate_ads = t.rotate_ads;
            tt.title = t.title;

            db.SubmitChanges();

            return t;
        }

        public Template single(Int32 id)
        {
            Template t = new Template();

            var result = from tmp in db.templates
                where tmp.id == id
                select tmp;

            foreach (var item in result)
            {
                t.ad_interval = item.ad_interval;
                t.bgcolor = item.bgcolor;
                t.bgimage = item.bgimage;
                t.event_id = item.event_id;
                t.id = id;
                t.orientation = item.orientation;
                t.overlay = item.overlay;
                t.overlay_font = item.overlay_font;
                t.overlay_font_color = item.overlay_font_color;
                t.resolution = item.resolution;
                t.rotate_ads = item.rotate_ads;
                t.title = item.title;
            }

            return t;
        }
    }

    public class Template
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public Int32 event_id { get; set; }
        public Int32 orientation { get; set; }
        public Int32 resolution { get; set; }
        public string bgcolor { get; set; }
        public string bgimage { get; set; }
        public Int32? overlay { get; set; }
        public string overlay_font { get; set; }
        public string overlay_font_color { get; set; }
        public Boolean rotate_ads { get; set; }
        public Int32 ad_interval { get; set; }
    }
}
