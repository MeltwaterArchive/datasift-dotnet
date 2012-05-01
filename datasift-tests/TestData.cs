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

        static public int stream_id = 10121;
        static public string stream_name = "DataSift API Test";
        static public string stream_description = "This stream is used by the official DataSift API tests.";
    }
}
