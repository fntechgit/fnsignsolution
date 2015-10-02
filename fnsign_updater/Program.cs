using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schedInterface;

namespace fnsign_updater
{
    class Program
    {
        

        static void Main(string[] args)
        {
            events _events = new events();

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

                List<Session> sess = _sessions.all(e.url, e.api_key);

                Console.WriteLine(sess.Count.ToString() + " Sessions Found...");

                Console.WriteLine("");

                foreach (Session s in sess)
                {
                    // now we loop through the sessions and insert or update them
                    _sessions.add(s, e.id);
                }

                Console.WriteLine("");

                Console.WriteLine("Updating Event Last Updated Time...");
                
                _events.update(e);

                Console.WriteLine("Complete...");

                Console.WriteLine("Moving to next event...");
                Console.WriteLine("");

                // here we need to update the last update time
            }

            Console.WriteLine("######### FNSIGN v.1.0 UPDATER COMPLETE #########");
        }
    }
}
