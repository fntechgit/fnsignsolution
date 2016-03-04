using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace schedInterface
{
    public class decks
    {
        private schedDataContext db = new schedDataContext();

        public List<Deck> by_event(Int32 id)
        {
            List<Deck> _decks = new List<Deck>();

            var result = from ds in db.decks
                where ds.event_id == id
                orderby ds.title
                select ds;

            foreach (var item in result)
            {
                Deck d = new Deck();

                d.description = item.description;
                d.event_id = item.event_id;
                d.id = item.id;
                d.title = item.title;

                _decks.Add(d);
            }

            return _decks;
        }

        public Deck single(Int32 id)
        {
            Deck d = new Deck();

            var result = from ds in db.decks
                where ds.id == id
                select ds;

            foreach (var item in result)
            {
                d.description = item.description;
                d.event_id = item.event_id;
                d.id = item.id;
                d.title = item.title;
            }

            return d;
        }

        public Deck create(Deck d)
        {
            deck de = new deck();

            de.description = d.description;
            de.event_id = d.event_id;
            de.title = d.title;

            db.decks.InsertOnSubmit(de);

            db.SubmitChanges();

            d.id = de.id;

            return d;
        }

        public Deck update(Deck d)
        {
            deck de = db.decks.Where(x => x.id == d.id).Single();

            de.description = d.description;
            de.event_id = d.event_id;
            de.title = d.title;

            db.SubmitChanges();

            return d;
        }

        public Boolean delete(Int32 id)
        {
            deck de = db.decks.Where(x => x.id == id).Single();

            db.decks.DeleteOnSubmit(de);

            db.SubmitChanges();

            return true;
        }
    }

    public class Deck
    {
        public Int32 id { get; set; }
        public Int32 event_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
