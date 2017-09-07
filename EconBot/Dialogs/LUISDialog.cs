using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EconBot.Dialogs
{
    [LuisModel("5ced85b7-4acc-4d05-9144-57ec7be038a2", "f6ea8956f36b4379bcab23f342bfb460")]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {
        private List<string> greetingList = new List<string> { "Hello there!", "Hey!", "Hi", "Hi, how can I help?", "Howdy, Partner!" };
        private List<string> complimentList = new List<string> { "You're welcome", "Glad I could help!", "My pleasure",
            "Sure, no problem", "No worries! Econo is glad to help!"};

        private List<string> emojiList = new List<string> { " 😊", " 😀", " 😇", " 😎", " 😀" };

        [LuisIntent("Greeting")]
        public async Task RespondToGreeting(IDialogContext context, LuisResult result)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, greetingList.Count);
            await context.PostAsync(greetingList[randomNumber]);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Compliment")]
        public async Task RespondToCompliment(IDialogContext context, LuisResult result)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, complimentList.Count);
            int randomEmoji = random.Next(0, emojiList.Count);
            await context.PostAsync(complimentList[randomNumber] + emojiList[randomEmoji]);
            context.Wait(MessageReceived); ;

        }

        [LuisIntent("Meme")]
        public async Task ShowMeme(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage();

            message.Attachments.Add(new Attachment()
            {
                // generate random meme
                ContentUrl = images.ImageRandomizer.getRandomMeme(),
                ContentType = "image/jpg",
                Name = "Meme"
            });

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task NoIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, I'm not sure I understand.");
            context.Wait(MessageReceived);
        }

    }



    /**
     * Code to make card: 
     * 
     * var message = context.MakeMessage();

        List<CardAction> cardAction = new List<CardAction>();
        message.Attachments = new List<Attachment>();
        CardAction ca = new CardAction()
        {
            // card Action is for button to view more info on stock.
            Title = "See More",
            Type = "openUrl",
            Value = "http://contosochat.azurewebsites.net/"
        };
        cardAction.Add(ca);
        String value = "🙂";
        HeroCard tc = new HeroCard()
        {
            Buttons = cardAction,
            Title = "Help",
            Subtitle = value,
        };
        message.Attachments.Add(tc.ToAttachment());

        await context.PostAsync(message);
        context.Wait(MessageReceived);
     * 
     * */

}
