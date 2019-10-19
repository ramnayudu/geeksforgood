using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CallRequestResponseService
{
    public class Program
    {
        public int user1Price = 44000;
        public int user2Price = 45000;
        public static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
        }

        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Date", "2019-06-10T00:00:00"
                                            },
                                            {
                                                "Open", "44300"
                                            },
                                            {
                                                "High", "45050"
                                            },
                                            {
                                                "Low", "43300"
                                            },
                                            {
                                                "Close", "0"
                                            },
                                            {
                                                "Adj Close", "0"
                                            },
                                            {
                                                "Volume", "50293907"
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "88lpIxx9SdkIbrVSijJAGX0uzDYMIXlZYehfv6W5zPHV3Um1XrlpcZ056lRni0fQ+YLGWSjdm3KZEAgfbjDLjQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/a35c8bec1cd249418f60d766ca88e8af/services/f4ea97da43b8473498f6f6dc5c6cffa8/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

#pragma warning disable CS1701 // Assuming assembly reference matches identity
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
#pragma warning restore CS1701 // Assuming assembly reference matches identity

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                    //  Condition to check the predicted stock  price with user stock price and send the message accordingly
                    var client1 = new HttpClient();
                    client1.BaseAddress = new Uri("https://alerts.solutionsinfini.com/api/v4/index.php?method=sms&message=Message from v4 api&sender=HACKAT&api_key=A91862b9c45ff3872032bb46332b1be86&unicode=1&dlrurl=https://g3wpwxr9eg.execute-api.us-east-1.amazonaws.com/dev/?app_type=alerts-sms%26sent={sent}%26delivered={delivered}%26msgid={msgid}%26sid={sid}%26status={status}%26reference={reference}%26custom1=\"global\"%26custom2=\"testing\"%26credits={credits}%26units={units}%26sentat={sentat}%26delivat={delivat}%26submitat={submitat}%26submittime={submittime}%26senttime={senttime}&to=9959576363\n");
                    HttpResponseMessage response1 = await client1.GetAsync("");
                    string result1 = await response1.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result1);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }
    }

}