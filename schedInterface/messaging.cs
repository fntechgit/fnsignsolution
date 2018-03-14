using hostoverride;
using System;

namespace schedInterface
{
    public class messaging
    {
        private api _api = new api();
        private dataconnection dc = new dataconnection();
        private DAL dal = new DAL();
        private email_queue _queue = new email_queue();
        private settings _settings = new settings();
        private carriers _carriers = new carriers();

        public bool display_down(User u, bool mms, Terminal t)
        {
            Queue q = new Queue();
            int? carrier1 = u.carrier;
            int num = 0;
            if (carrier1.GetValueOrDefault() <= num || !carrier1.HasValue)
                return false;
            Carrier carrier2 = this._carriers.single(Convert.ToInt32((object)u.carrier));
            Key clientByApiKey = this._api.getClientByAPIKey(this._settings.hostoverride_api_key());
            string str = mms ? u.mobile + "@" + carrier2.mms : u.mobile + "@" + carrier2.sms;
            q.api_id = clientByApiKey.id;
            q.email_to = str;
            q.email_from = "FNSIGN <notify@fnsign.fntech.com>";
            q.email_subject = "URGENT: " + t.title + " Display Down: The " + t.title + " IS DOWN AT THE FOLLOWING LOCATION: " + t.location_title + ".  The last time it was online was: " + t.last_online.ToShortDateString() + " @ " + t.last_online.ToShortTimeString();
            q.email_body = "";
            q.email_bcc = string.Empty;
            q.email_cc = string.Empty;
            this._queue.add(q);
            return true;
        }
    }
}
