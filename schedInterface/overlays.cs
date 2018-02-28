using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace schedInterface
{
    public class overlays
    {
        private schedDataContext db = new schedDataContext();

        public List<Overlay> all()
        {
            List<Overlay> _overlays = new List<Overlay>();

            var result = from ovr in db.overlays
                         orderby ovr.title
                         select ovr;

            foreach (var item in result)
            {
                Overlay o = new Overlay();

                o.active = item.active;
                o.id = item.id;
                o.preview = item.preview;
                o.title = item.title;
                o.header = item.header;
                o.footer = item.footer;
                o.body = item.body;
                o.width = item.width;
                o.height = item.height;
                o.guide = item.guide;
                o.bottom_overlay = item.bottom_overlay;
                o.layout = item.layout;
                o.speed = item.speed;
                o.group_by_start = item.group_by_start;
                o.group_by_location = item.group_by_location;
                o.all_sessions = item.all_sessions;
                o.template_id_announce = item.template_id_announce;
                o.template_id_end = item.template_id_end;
                o.bottom_height = item.bottom_height;

                _overlays.Add(o);
            }

            return _overlays;
        }

        public List<Overlay> active()
        {
            return all().Where(x => x.active == true).ToList();
        }

        public Overlay add(Overlay o)
        {
            overlay ov = new overlay();

            ov.active = o.active;
            ov.header = o.header;
            ov.footer = o.footer;
            ov.body = o.body;
            ov.preview = o.preview;
            ov.title = o.title;
            ov.width = o.width;
            ov.height = o.height;
            ov.guide = o.guide;
            ov.bottom_overlay = o.bottom_overlay;
            ov.layout = o.layout;
            ov.speed = o.speed;
            ov.group_by_start = o.group_by_start;
            ov.group_by_location = o.group_by_location;
            ov.all_sessions = o.all_sessions;
            ov.template_id_announce = o.template_id_announce;
            ov.template_id_end = o.template_id_end;
            ov.bottom_height = o.bottom_height;

            db.overlays.InsertOnSubmit(ov);

            db.SubmitChanges();

            o.id = ov.id;

            return o;
        }

        public Boolean delete(Int32 id)
        {
            @overlay o = db.overlays.Single(x => x.id == id);

            db.overlays.DeleteOnSubmit(o);

            db.SubmitChanges();

            return true;
        }

        public Overlay update(Overlay o)
        {
            overlay ov = db.overlays.Single(x => x.id == o.id);

            ov.active = o.active;
            ov.header = o.header;
            ov.footer = o.footer;
            ov.body = o.body;
            ov.preview = o.preview;
            ov.title = o.title;
            ov.width = o.width;
            ov.height = o.height;
            ov.guide = o.guide;
            ov.bottom_overlay = o.bottom_overlay;
            ov.layout = o.layout;
            ov.speed = o.speed;
            ov.group_by_start = o.group_by_start;
            ov.group_by_location = o.group_by_location;
            ov.all_sessions = o.all_sessions;
            ov.template_id_announce = o.template_id_announce;
            ov.template_id_end = o.template_id_end;
            ov.bottom_height = o.bottom_height;

            db.SubmitChanges();

            return o;
        }

        public Overlay addUpdate(Overlay o)
        {
            if (o.id > 0)
                this.update(o);
            else
                this.add(o);
            return o;
        }

        public Overlay single(Int32 id)
        {
            overlay ov = db.overlays.Single(x => x.id == id);

            Overlay o = new Overlay();

            o.active = ov.active;
            o.header = ov.header;
            o.footer = ov.footer;
            o.body = ov.body;
            o.id = id;
            o.preview = ov.preview;
            o.title = ov.title;
            o.width = ov.width;
            o.height = ov.height;
            o.guide = ov.guide;
            o.bottom_overlay = ov.bottom_overlay;
            o.layout = ov.layout;
            o.speed = ov.speed;
            o.group_by_start = ov.group_by_start;
            o.group_by_location = ov.group_by_location;
            o.all_sessions = ov.all_sessions;
            o.template_id_announce = ov.template_id_announce;
            o.template_id_end = ov.template_id_end;
            o.bottom_height = ov.bottom_height;

            return o;
        }
    }

    public class Overlay
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string header { get; set; }
        public string footer { get; set; }
        public string preview { get; set; }
        public Boolean active { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        public string guide { get; set; }
        public string bottom_overlay { get; set; }
        public string layout { get; set; }
        public Int32 speed { get; set; }
        public Boolean group_by_start { get; set; }
        public Boolean group_by_location { get; set; }
        public Boolean all_sessions { get; set; }
        public Int32? template_id_announce { get; set; }
        public Int32? template_id_end { get; set; }
        public Int32 bottom_height { get; set; }
    }

    [Serializable]
    public class OverlayJSON
    {
        public string pages { get; set; }

        public bool delete { get; set; }

        public string responsiveMode { get; set; }
    }
}
