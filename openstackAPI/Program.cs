using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schedInterface;

namespace openstackAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            schedInterface.auth _auth = new auth();
            schedInterface.openstackEvents _events = new openstackEvents();
            schedInterface.osettings _settings = new osettings();
            schedInterface.events _eventsDB = new events();
            functions _functions = new functions();
            olocations _olocations = new olocations();
            locations _locations = new locations();
            osessions _osessions = new osessions();
            sessions _sessions = new sessions();
            speakers _speakers = new speakers();
            event_types _types = new event_types();

            Console.WriteLine("Begin Process...");

            //Console.WriteLine("Getting Authorization...");

            //Console.WriteLine(_auth.auth_justin(_settings.client_id(), _settings.client_secret()));
            //Console.ReadLine();

            List<OpenStackEvent> evs = _events.push_events();

            Console.WriteLine("Event Count:" + evs.Count.ToString());

            foreach (OpenStackEvent ev in evs)
            {
                Console.WriteLine(ev.name);
                if (ev.active)
                {
                    schedInterface.Event e = new Event();

                    e.event_end = _functions.ConvertUnixTimeStamp(ev.end_date.ToString());
                    e.event_start = _functions.ConvertUnixTimeStamp(ev.start_date.ToString());
                    e.interval = 5;
                    e.last_update = DateTime.Now;
                    e.openstack_id = ev.id;
                    e.title = ev.name;

                    if (e.event_start != null)
                    {
                        if (Convert.ToDateTime(e.event_start) > DateTime.Now)
                        {
                            e.offset = ev.time_zone.offset != null ? Convert.ToInt32(ev.time_zone.offset) : 0;

                            // check to see if it exists
                            if (_eventsDB.find_by_openstack_id(Convert.ToInt32(e.openstack_id)).id > 0)
                            {
                                e.id = _eventsDB.find_by_openstack_id(Convert.ToInt32(e.openstack_id)).id;

                                e = _eventsDB.appupdate(e);
                                Console.WriteLine(" Updated...");
                            }
                            else
                            {
                                e = _eventsDB.add(e);
                                Console.WriteLine(" Added...");
                            }

                            // Get Event TYpes and push them
                            Console.WriteLine("Updating Event Types...");
                            Console.WriteLine("");

                            List<OpenStackEventType> oTypes = _events.push_event_types(Convert.ToInt32(e.openstack_id));

                            Console.WriteLine(oTypes.Count + " Types Found...");

                            Int32 tcnt = 0;

                            foreach (OpenStackEventType oty in oTypes)
                            {
                                EventType ety = new EventType();

                                ety.title = oty.name;
                                ety.event_type_id = oty.id;
                                ety.event_id = e.id;

                                //Console.WriteLine(ety.title + "...");
                                _types.addUpdate(ety);

                                tcnt++;

                                drawTextProgressBar(tcnt, oTypes.Count);
                            }

                            List<EventType> etypes = _types.by_event(e.id);

                            // get locations and push them
                            List<OpenStackLocation> locs = _olocations.get_by_event(Convert.ToInt32(e.openstack_id));

                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine(locs.Count + " Locations Found....");
                            

                            Int32 lcnt = 0;

                            foreach (OpenStackLocation l in locs)
                            {
                                Location lo = new Location();

                                //Console.WriteLine("Processing " + l.name);

                                lo.title = l.name;
                                lo.sched_id = l.id.ToString();
                                lo.event_id = e.id;

                                _locations.add(lo);

                                lcnt++;

                                drawTextProgressBar(lcnt, locs.Count);
                            }

                            //// now get the speakers
                            //Console.WriteLine("");
                            //Console.WriteLine("Getting Speakers... (Processing Pages...)");
                            //Console.WriteLine("");
                            //OpenSpeaker openspeaker = new OpenSpeaker();

                            //openspeaker = _speakers.refresh(Convert.ToInt32(e.openstack_id), "1");

                            //drawTextProgressBar(0, openspeaker.last_page);

                            //foreach (Speaker speak in openspeaker.data)
                            //{
                            //    speak.event_id = e.id;

                            //    _speakers.add(speak);
                            //}

                            //Int32 cursp = 0;



                            //while (cursp <= openspeaker.last_page)
                            //{
                            //    cursp++;

                            //    drawTextProgressBar(cursp, openspeaker.last_page);

                            //    openspeaker = _speakers.refresh(Convert.ToInt32(e.openstack_id), cursp.ToString());

                            //    foreach (Speaker speak in openspeaker.data)
                            //    {
                            //        speak.event_id = e.id;

                            //        _speakers.add(speak);
                            //    }
                            //}

                            // now get the sessions
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Getting Sessions....");
                            Console.WriteLine("");
                            

                            OpenStack op = new OpenStack();

                            Int32 total_count = 0;
                            op = _osessions.refresh(Convert.ToInt32(e.openstack_id), "1");

                            Console.WriteLine("Processing Pages from API");

                            

                            List<Session> fromAPI = new List<Session>();

                            List<OpenStackSession> allsessions = new List<OpenStackSession>();

                            allsessions.AddRange(op.data);

                            drawTextProgressBar(0, op.last_page);

                            Int32 cur = 1;

                            while (cur <= op.last_page)
                            {
                                drawTextProgressBar(cur, op.last_page);
                                // get the next records

                                cur++;

                                op = _osessions.refresh(Convert.ToInt32(e.openstack_id), cur.ToString());

                                allsessions.AddRange(op.data);
                            }

                            Int32 cursess = 0;

                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Session Count:" + op.total.ToString());
                            Console.WriteLine("Processing Session Records...");

                            drawTextProgressBar(cursess, allsessions.Count);

                            foreach (OpenStackSession sess in allsessions)
                            {
                                if (!string.IsNullOrEmpty(sess.title))
                                {
                                    fromAPI.Add(_sessions.add(_osessions.parse(sess, e, etypes), e.id));
                                }

                                cursess++;

                                drawTextProgressBar(cursess, allsessions.Count);
                            }

                            //Console.WriteLine("");
                            //Console.WriteLine("");
                            //Console.WriteLine("Finding Sessions to Remove...");

                            //// find sessions that have been removed and delete them from the database
                            //List<Session> fromDB = _sessions.by_event(e.id);

                            //Int32 dcnt = 0;

                            //var result = fromDB.Where(p => !fromAPI.Any(p2 => p2.id == p.event_key));
                            //// iterate through them and delete them
                            //foreach (var item in result)
                            //{
                            //    Console.WriteLine("Removing Session: " + item.event_key + " " + item.name);
                            //    //if (_sessions.remove(item.event_key))
                            //    //{
                            //    //    drawTextProgressBar(dcnt, result.Count());
                            //    //}

                            //    //dcnt++;
                            //}
                        }
                    }

                }
            }
        }

        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
