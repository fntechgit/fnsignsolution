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

                            foreach (OpenStackEventType oty in oTypes)
                            {
                                EventType ety = new EventType();

                                ety.title = oty.name;
                                ety.event_type_id = oty.id;
                                ety.event_id = e.id;

                                Console.WriteLine(ety.title + "...");
                                _types.addUpdate(ety);
                            }

                            List<EventType> etypes = _types.by_event(e.id);

                            // get locations and push them
                            List<OpenStackLocation> locs = _olocations.get_by_event(Convert.ToInt32(e.openstack_id));

                            Console.WriteLine(locs.Count + " Locations Found....");

                            foreach (OpenStackLocation l in locs)
                            {
                                Location lo = new Location();

                                Console.WriteLine("Processing " + l.name);

                                lo.title = l.name;
                                lo.sched_id = l.id.ToString();
                                lo.event_id = e.id;

                                _locations.add(lo);
                            }

                            // now get the speakers
                            Console.WriteLine("Getting Speakers...");
                            OpenSpeaker openspeaker = new OpenSpeaker();

                            openspeaker = _speakers.refresh(Convert.ToInt32(e.openstack_id), "1");

                            foreach (Speaker speak in openspeaker.data)
                            {
                                speak.event_id = e.id;

                                _speakers.add(speak);
                                Console.WriteLine("Speaker: " + speak.first_name + " " + speak.last_name +
                                                  " Processed...");
                            }

                            Int32 cursp = 1;

                            while (cursp <= openspeaker.last_page)
                            {
                                cursp++;

                                openspeaker = _speakers.refresh(Convert.ToInt32(e.openstack_id), cursp.ToString());

                                foreach (Speaker speak in openspeaker.data)
                                {
                                    speak.event_id = e.id;

                                    _speakers.add(speak);
                                    Console.WriteLine("Speaker: " + speak.first_name + " " + speak.last_name +
                                                      " Processed...");
                                }
                            }

                            // now get the sessions
                            Console.WriteLine("Getting Sessions....");
                            OpenStack op = new OpenStack();

                            Int32 total_count = 0;
                            op = _osessions.refresh(Convert.ToInt32(e.openstack_id), "1");

                            Console.WriteLine("Session Count:" + op.total.ToString());

                            foreach (OpenStackSession sess in op.data)
                            {
                                if (!string.IsNullOrEmpty(sess.title))
                                {
                                    _sessions.add(_osessions.parse(sess, e, etypes), e.id);
                                    Console.WriteLine("Session: " + sess.title + " Processed...");
                                }
                            }

                            Int32 cur = 1;

                            while (cur <= op.last_page)
                            {
                                // get the next records
                                cur++;

                                op = _osessions.refresh(Convert.ToInt32(e.openstack_id), cur.ToString());

                                Console.WriteLine("Session Count:" + op.total.ToString());

                                foreach (OpenStackSession sess in op.data)
                                {
                                    if (!string.IsNullOrEmpty(sess.title))
                                    {
                                        _sessions.add(_osessions.parse(sess, e, etypes), e.id);
                                        Console.WriteLine("Session: " + sess.title + " Processed...");
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
