using System;
using System.Collections.Generic;
using System.Linq;

namespace schedInterface
{
    public class slides
    {
        private schedDataContext db = new schedDataContext();

        public List<Slide> by_deck(Int32 id)
        {
            List<Slide> _slides = new List<Slide>();

            var result = from sd in db.slides
                where sd.deck_id == id
                orderby sd.display
                select sd;

            foreach (var item in result)
            {
                Slide s = new Slide();

                s.deck_id = item.deck_id;
                s.display = item.display;
                s.id = item.id;
                s.source = item.source;

                _slides.Add(s);
            }

            return _slides;
        }

        public Slide single(Int32 id)
        {
            Slide s = new Slide();

            var result = from sds in db.slides
                where sds.id == id
                select sds;

            foreach (var item in result)
            {
                s.deck_id = item.deck_id;
                s.display = item.display;
                s.id = item.id;
                s.source = item.source;
            }

            return s;
        }

        public Slide reorder(Slide s)
        {
            slide sl = db.slides.Where(x => x.id == s.id).Single();

            sl.display = s.display;

            db.SubmitChanges();

            return s;
        }

        public Slide create(Slide s)
        {
            slide sl = new slide();

            sl.deck_id = s.deck_id;
            sl.display = 100;
            sl.source = s.source;

            db.slides.InsertOnSubmit(sl);

            db.SubmitChanges();

            s.id = sl.id;

            return s;
        }

        public Slide update(Slide s)
        {
            slide sl = db.slides.Where(x => x.id == s.id).Single();

            sl.deck_id = s.deck_id;
            sl.source = s.source;

            db.SubmitChanges();

            return s;
        }

        public Boolean delete(Int32 id)
        {
            slide sl = db.slides.Where(x => x.id == id).Single();

            db.slides.DeleteOnSubmit(sl);

            db.SubmitChanges();

            return true;
        }
    }

    public class Slide
    {
        public Int32 id { get; set; }
        public Int32 deck_id { get; set; }
        public string source { get; set; }
        public Int32 display { get; set; }
    }
}
