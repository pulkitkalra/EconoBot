using EconBot.IndicatorAPI;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace EconBot.IndicatorAPI
{
    [Serializable]
    public class WorldBankRequest
    {
        static HttpClient client = new HttpClient();

        public static async Task<PageModel> GetIndicator(string RequestURI)
        {
            PageModel Data = new PageModel();
            using (HttpClient client = new HttpClient())
            {
             
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    // removing the first [ bracket
                    string modifiedResponse = JsonDataResponse.Substring(1, JsonDataResponse.Length - 1);

                    // replacing the last [ bracket with a { bracket
                    StringBuilder sb = new StringBuilder(modifiedResponse);
                    sb[modifiedResponse.Length-1] = '}';
                    modifiedResponse = sb.ToString();

                    // adding a List string to put all entries into a list.
                    Regex regex = new Regex(@"},\[{");
                    string result = regex.Replace(modifiedResponse, "", 1);
                    Regex regexUpdated = new Regex("\"indicator\"");
                    string finalResult = regexUpdated.Replace(result, ",\"List\":[{\"indicator\"",1);

                    // deserializing JSON to PageModel object
                    Data = JsonConvert.DeserializeObject<PageModel>(finalResult);
                }
            }
            return Data;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something to return
            string year  = activity.Text;
            var ind = await GetIndicator(year);

            // return our reply to the user
            await context.PostAsync($"The {ind.List[4].Indicator.Value} of {ind.List[4].Country.Value} as of {ind.List[4].Date} is {ind.List[4].Value}");

            context.Wait(MessageReceivedAsync);
        }
    }
}