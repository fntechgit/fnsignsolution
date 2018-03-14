using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class settings
    {
        #region twitter

        public string twitter_api_key()
        {
            return @"qzWV5F02vuWwyqyHkW1YQ1Tiy";
        }

        public string twitter_api_secret()
        {
            return @"PxI7UKtG7mxvt2RixH2PUvOaw1MOor5aBdSmMWjpiPVyu5aqf3";
        }

        public string twitter_access_token()
        {
            return @"22405550-1FoDgNozlwgrkFjCaY1zWgTtTOZ3kN8XmQfq3vif8";
        }

        public string twitter_access_token_secret()
        {
            return @"Zljk61eXQnsTnGu6PMIeRupmMdKYm8dzykEx3YbB79Zu5";
        }

        public string site_url()
        {
            return @"https://fnsign.fntech.com";
        }

        public string display_url()
        {
            return @"https://fndisplay.fntech.com";
        }

        public string hostoverride_api_key()
        {
            return "9433156e-fa39-42ff-91e3-b56791d66158";
        }

        #endregion
    }

    public class osettings
    {
        private schedDataContext db = new schedDataContext();

        public string client_id()
        {
            return "Bd6G58pczm7yHgU5VVpI2qyGH~JY.poV.openstack.client";
        }

        public string client_secret()
        {
            return "Ey8lC_y3Zzo9U52fKnbpqzFDmPoP-ap3qZNcqgFpsKoPzvn6nS.nGfas46l4i_eD";
        }

        public string auth_key()
        {
            var result = from st in db.settings
                where st.id == 1
                select st;

            string auth = string.Empty;

            foreach (var item in result)
            {
                auth = item.value;
            }

            return auth;
        }
    }
}
