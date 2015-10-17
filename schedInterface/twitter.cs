using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;

namespace schedInterface
{
    public class twitter
    {
        private settings _settings = new settings();
        mediaManager _media = new mediaManager();
        private events _events = new events();

        // get by twitter hashtag
        public Int32 fetch(string tag, Int32 count, Int32 event_id, Int32 template_id)
        {
            Int32 total = 0;

            var service = new TwitterService(_settings.twitter_api_key(), _settings.twitter_api_secret());
            service.AuthenticateWith(_settings.twitter_access_token(), _settings.twitter_access_token_secret());

            var options = new SearchOptions();

            options.Q = "%23" + tag;
            options.Count = count;
            options.IncludeEntities = true;
            options.Lang = "en";
            options.Resulttype = TwitterSearchResultType.Recent;

            Event ev = _events.single(event_id);

            TwitterSearchResult tweets = new TwitterSearchResult();

            try
            {
                tweets = service.Search(options);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            try
            {

                foreach (var item in tweets.Statuses)
                {

                    Media m = new Media();

                    m.added_to_db_date = DateTime.Now;
                    m.createdate = item.CreatedDate;
                    m.description = item.Text;
                    m.full_name = item.User.Name;

                    

                    //m.source_id = item.Id.ToString();

                    if (item.Location != null)
                    {
                        m.latitude = item.Location.Coordinates.Latitude.ToString();
                        m.longitude = item.Location.Coordinates.Longitude.ToString();
                    }

                    if (item.Place != null)
                    {
                        m.location_name = item.Place.FullName;
                    }

                    m.likes = item.RetweetCount;
                    m.link = "https://twitter.com/" + item.User.ScreenName + "/status/" + item.Id.ToString();
                    m.profilepic = item.User.ProfileImageUrl;
                    m.service = "Twitter";
                    m.event_id = event_id;
                    m.template_id = template_id;

                    foreach (var hashtag in item.Entities.HashTags)
                    {
                        m.tags += "#" + hashtag.Text + " ";
                    }

                    m.username = item.User.ScreenName;

                    // ################## NOW THE IMAGE STUFF 
                    foreach (TwitterMedia photo in item.Entities.Media)
                    {
                        m.source_id = photo.IdAsString;
                        m.source = photo.MediaUrl + ":large";
                        m.width = photo.Sizes.Large.Width;
                        m.height = photo.Sizes.Large.Height;

                        if (photo.MediaType.ToString() != "Photo")
                        {
                            m.is_video = true;
                        }

                            m.approved = true;
                            m.approved_by = 1;
                            m.approved_date = DateTime.Now;
                        

                        
                    }

                    m.source_id = item.IdStr;

                    _media.add(m);

                    total++;

                }

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message + ": " + ex.InnerException);
            }



            return total;
        }

        // get by twitter username
        public Int32 fetch(string tag, Int32 count, Boolean is_username, Int32 event_id, Int32 template_id)
        {
            Int32 total = 0;

            var service = new TwitterService(_settings.twitter_api_key(), _settings.twitter_api_secret());
            service.AuthenticateWith(_settings.twitter_access_token(), _settings.twitter_access_token_secret());

            var options = new ListTweetsOnUserTimelineOptions();

            options.ScreenName = tag;
            options.Count = count;
            options.ExcludeReplies = true;
            options.IncludeRts = false;

            var tweets = service.ListTweetsOnUserTimeline(options);

            foreach (TwitterStatus item in tweets)
            {
                Media m = new Media();

                m.added_to_db_date = DateTime.Now;
                m.createdate = item.CreatedDate;
                m.description = item.Text;

                if (item.User != null)
                {
                    m.full_name = item.User.Name;
                    m.link = "https://twitter.com/" + item.User.ScreenName + "/status/" + item.Id.ToString();
                    m.profilepic = item.User.ProfileImageUrl;
                }

                if (item.Location != null)
                {
                    m.latitude = item.Location.Coordinates.Latitude.ToString();
                    m.longitude = item.Location.Coordinates.Longitude.ToString();
                }

                if (item.Place != null)
                {
                    m.location_name = item.Place.FullName;
                }

                m.likes = item.RetweetCount;

                m.service = "Twitter";

                foreach (var hashtag in item.Entities.HashTags)
                {
                    m.tags += "#" + hashtag.Text + " ";
                }

                m.username = tag;

                m.event_id = event_id;
                m.template_id = template_id;

                // ################## NOW THE IMAGE STUFF 
                foreach (TwitterMedia photo in item.Entities.Media)
                {
                    m.source_id = photo.IdAsString;
                    m.source = photo.MediaUrl + ":large";
                    m.width = photo.Sizes.Large.Width;
                    m.height = photo.Sizes.Large.Height;

                    if (photo.MediaType.ToString() != "Photo")
                    {
                        m.is_video = true;
                    }

                        
                    
                }

                m.source_id = item.IdStr;

                _media.add(m);

                total++;
            }

            return total;
        }
    }
}
