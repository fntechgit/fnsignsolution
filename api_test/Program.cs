using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schedInterface;

namespace api_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("###################  TESTING API ####################");

            string api_session;

            auth _auth = new auth();

            string conference_url = "https://openstacksummitoctober2015tokyo.sched.org";
            string api_key = "47dfbdc49d82ff16669df259952656fa";

            api_session = _auth.login(conference_url, api_key);

            Console.WriteLine("Session Value: " + api_session);

            sessions _sessions = new sessions();

            foreach (Session s in _sessions.all(conference_url, api_key))
            {
                Console.WriteLine("Title: " + s.name);
                Console.WriteLine("Description: " + s.description);


            }

            Console.ReadLine();
        }
    }
}
