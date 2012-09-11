using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift_tests
{
    public class TestData
    {
        static public string username = "<your datasift username>";
        static public string api_key = "<your datasift api_key>";

        static public string definition = "interaction.content contains \"datasift\"";
        static public string definition_hash = "947b690ec9dca525fb8724645e088d79";

        static public string invalid_definition = "interactin.content contains \"datasift\"";

        // Historic
        static public string historic_playback_id = "93558e17de15072fa126370c37c5bd8f";
        static public DateTime historic_start = new DateTime(2012, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        static public DateTime historic_end = new DateTime(2012, 6, 1, 23, 59, 59, DateTimeKind.Utc);
        static public DateTime historic_created_at = new DateTime(2012, 8, 1, 12, 0, 0, DateTimeKind.Utc);
        static public List<string> historic_sources = new List<string>();
        static public double historic_sample = 10.0;
        static public string historic_name = "Historic for unit tests";

        // PushSubscription
        static public string push_id = "b665761917bbcb7afd3102b4a645b41e";
        static public string push_name = "Push subscription for unit tests";
        static public DateTime push_created_at = new DateTime(2012, 7, 20, 0, 0, 0, DateTimeKind.Utc);
        static public string push_status = "active";
        static public string push_hash_type = "stream";
        static public DateTime push_last_request = new DateTime(2012, 7, 20, 7, 10, 0, DateTimeKind.Utc);
        static public DateTime push_last_success = new DateTime(2012, 7, 20, 7, 0, 0, DateTimeKind.Utc);
        static public string push_output_type = "http";
        static public Dictionary<String,String> push_output_params = new Dictionary<String,String>();

        static public void init()
        {
            historic_sources.Clear();
            historic_sources.Add("twitter");
            historic_sources.Add("facebook");

            push_output_params.Clear();
            push_output_params.Add("delivery_frequency", "60");
            push_output_params.Add("max_size", "10240");
            push_output_params.Add("url", "http://www.example.com/push_endpoint");
            push_output_params.Add("auth.type", "basic");
            push_output_params.Add("auth.username", "myuser");
            push_output_params.Add("auth.password", "mypass");
        }
    }
}
