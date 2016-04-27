using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schedInterface;

namespace fnsign_updater
{
    class Program
    {
        // REV 1 - COMPLETE 10.19.15 @ 12:26AM 

        static void Main(string[] args)
        {
            events _events = new events();
            twitter _twitter = new twitter();
            templates _templates = new templates();
            terminals _terminals = new terminals();

            Console.WriteLine("######### BEGIN FNSIGN UPDATER v.1.0 #########");

            Console.WriteLine("");

            Console.WriteLine("######### GETTING EVENTS THAT NEED TO BE UPDATED #########");

            Console.WriteLine("");

            List<Event> evs = _events.need_updating();

            Console.WriteLine(evs.Count.ToString() + " need updating....");

            Console.WriteLine("");

            sessions _sessions = new sessions();

            foreach (Event e in evs)
            {
                Console.WriteLine("Finding Sessions for " + e.title);
                Console.WriteLine("");

                if (!string.IsNullOrEmpty(e.url))
                {
                    List<Session> sess = _sessions.all(e.url, e.api_key);

                    Console.WriteLine(sess.Count.ToString() + " Sessions Found...");

                    Console.WriteLine("");

                    // look for deletions
                    List<Session> dbsess = _sessions.by_event(e.id);

                    foreach (Session d in dbsess)
                    {
                        List<Session> found = sess.Where(x => x.event_key == d.event_key).ToList();

                        if (found.Count == 0)
                        {
                            // remove the session
                            Console.WriteLine("Session ID: " + d.id + " NOT FOUND");

                            Console.WriteLine("Removing Session: " + d.name + "...");


                            _sessions.delete(d.internal_id);
                        }
                        else
                        {
                            Console.WriteLine("Session ID: " + d.id + " FOUND");
                        }
                    }

                    foreach (Session s in sess)
                    {
                        // now we loop through the sessions and insert or update them
                        if (s.event_type != "Japanese Language")
                        {
                            _sessions.add(s, e.id);
                        }
                    }

                    _sessions.clean_summit();
                }

                Console.WriteLine("");

                Console.WriteLine("Finding Tweets for Global Event Tag...");

                Console.WriteLine("");

                if (!string.IsNullOrEmpty(e.t_username))
                {
                    Console.WriteLine("Finding Tweets for @" + e.t_username);
                    Console.WriteLine("");

                    _twitter.fetch(e.t_username, 50, true, e.id, 0);
                }

                if (e.hashtags != null)
                {
                    foreach (string h in e.hashtags)
                    {
                        Console.WriteLine("Finding Tweets for #" + h);
                        Console.WriteLine("");

                        _twitter.fetch(h, 50, e.id, 0);
                    }
                }

                Console.WriteLine("Now let's check for the templates associated with " + e.title);
                Console.WriteLine("");

                foreach (Template t in _templates.all_by_event(e.id))
                {
                    if (!string.IsNullOrEmpty(t.t_username))
                    {
                        Console.WriteLine("Fetching Twitter records for @" + t.t_username);
                        Console.WriteLine("");

                        _twitter.fetch(t.t_username, 50, true, e.id, t.id);
                    }

                    if (t.hashtags != null)
                    {
                        Console.WriteLine("Fetching Records for Hashtags...");
                        Console.WriteLine("");

                        foreach (string h in t.hashtags)
                        {
                            Console.WriteLine("Fetching tweets for #" + h);
                            Console.WriteLine("");

                            _twitter.fetch(h, 50, e.id, t.id);
                        }
                    }
                }

                Console.WriteLine("");

                Console.WriteLine("Updating Event Last Updated Time...");
                
                _events.update(e);

                Console.WriteLine("Complete...");

                Console.WriteLine("Moving to next event...");
                Console.WriteLine("");

                // here we need to update the last update time
            }

            Console.WriteLine("Check for Offline Terminals...");
            Console.WriteLine("");

            List<Terminal> terms = _terminals.offline_terminals();

            if (terms.Count() > 0)
            {
                Console.WriteLine(terms.Count() + " Offline, updating their status...");
                Console.WriteLine("");

                foreach (Terminal t in terms)
                {
                    Console.WriteLine("Taking " + t.title + " Offline");
                    Console.WriteLine("");

                    _terminals.offline(t.id);
                }
            }
            else
            {
                Console.WriteLine("All Terminals are Online...");
                Console.WriteLine("");
            }

            Console.WriteLine("######### FNSIGN v.1.0 UPDATER COMPLETE #########");
        }
    }
}
