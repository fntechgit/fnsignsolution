﻿using System;
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
                o.html = item.html;
                o.id = item.id;
                o.preview = item.preview;
                o.title = item.title;

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
            ov.html = o.html;
            ov.preview = o.preview;
            ov.title = o.title;

            db.overlays.InsertOnSubmit(ov);

            db.SubmitChanges();

            o.id = ov.id;

            return o;
        }

        public Overlay update(Overlay o)
        {
            overlay ov = db.overlays.Single(x => x.id == o.id);

            ov.active = o.active;
            ov.html = o.html;
            ov.preview = o.preview;
            ov.title = o.title;

            db.SubmitChanges();

            return o;
        }

        public Overlay single(Int32 id)
        {
            overlay ov = db.overlays.Single(x => x.id == id);

            Overlay o = new Overlay();

            o.active = ov.active;
            o.html = ov.html;
            o.id = id;
            o.preview = ov.preview;
            o.title = ov.title;

            return o;
        }
    }

    public class Overlay
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public string html { get; set; }
        public string preview { get; set; }
        public Boolean active { get; set; }
    }
}