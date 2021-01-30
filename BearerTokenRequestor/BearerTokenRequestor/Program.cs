using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace BearerTokenRequestor
{
    class Program
    {
        static void Main(string[] args)
        {
            string accessToken;

            //first request a token from the Authorization Server
            using (var asClient = new WebClient())
            {
                var postValues = new NameValueCollection { { "grant_type", "password" }, { "client_id", "4B9AA781-1DF0-466D-8C1F-F0A7B142257C" }, { "client_secret", "nDn9fAOdLZbvKE6H6WlOk5RNC0Gjfto8rfpp1Dfg33zEpfgamvjefskJxznWo/X1swFhUo74+O6iR20DHwwcHTJ3FvBJWRl3VhDQlJWg1o5D5aN11z5bl8E2o863WOD+sTv8ciJCtVeD5A0OM3XWfjLd7zN2Rgsrgzvqj/KLmURAMQ+q9RmLuXMHHdL0tWdBZoEUAzUD/CU3mzrYvUWZPuQQtv6sZw8902H/dA3pFJTjY5ROyS5ZL3J3inN5wvtZ6BoISsz80A2vxidJmt1F6iZO7AXTiyGjg7Vl0pq8btT7EUDAT9KVgoeD1e1g5WaJ/aG4WpzIhQHc6RV1cnavnMEs0WFjvWInQev8qbsdac+JnzlZ+KqsNREGUnIIFBwNuO00VoTLi+CXxXXMO45HoEmnQtUhPcaxgcOoqZHop7s+z7eE9B6Jp9goQUcHVUvqg6EEhTwfKHtgGXGGNqcWkykw0FW3A7SJVFXUcyncKO+uPNZVb1SGE9JXyACct3mSk5B5+xL03ZrYFIxS2zgItMIjIHaGESPJB7+x3RM/7t/bUV8ltCrciQ9CPrgFVEZ+1s0aszd/xKqGWLhwZsmAd+yiF8ylk3AezRQuHHpJ7uVTXdHhE/7bvrkRer5mWYdA/p1yFuFPJL4GbZYBqh4AkPNCLdVabHTxWZ/WYx4V0SM=" } };

                asClient.Headers.Add("cache-control", "no-cache");
                asClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                byte[] result = asClient.UploadValues("http://localhost:57022/oauth2/token", "POST", postValues);
                string receivedResponseData = Encoding.UTF8.GetString(result);
                dynamic parsedData = JsonConvert.DeserializeObject<dynamic>(receivedResponseData);

                accessToken = parsedData.access_token.ToString();
            }
        }
    }
}
