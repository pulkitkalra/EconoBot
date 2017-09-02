using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EconBot.Dialogs
{
    [LuisModel("5ced85b7-4acc-4d05-9144-57ec7be038a2", "f6ea8956f36b4379bcab23f342bfb460")]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {
        private List<String> greetingList = new List<string>{ "Hello there!","Hey!","Hi","Hi, how can I help?","Howdy, Partner!" };

        [LuisIntent("Greeting")]
        public async Task RespondToGreeting(IDialogContext context, LuisResult result)
        {
            Random random = new Random();
            //int size = greetingList.Count - 1;
            int randomNumber = random.Next(0, greetingList.Count - 1);
            await context.PostAsync(greetingList[randomNumber]);
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task NoIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I'm not sure I understand.");
            context.Wait(MessageReceived);
        }
    }
}