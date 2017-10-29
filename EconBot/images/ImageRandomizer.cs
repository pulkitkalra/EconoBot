using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EconBot.images
{
    /**
     * Class contains references to all meme paths. Contains a function that randomizes each meme. Add any new memes 
     * by uploading to github and then adding it's raw path (with the auth token) to the memeList.
     * */
    public class ImageRandomizer
    {
        // NEED TO ENSURE THAT LINK DOES'T BREAK OVER TIME
        private static List<string> memeList = new List<string> {
            "https://image.ibb.co/bymEgR/image1.jpg", // image1
            "https://image.ibb.co/nceZgR/image2.jpg", // image2
            "https://image.ibb.co/hQ7y86/image3.png", // image3
            "https://image.ibb.co/dxfUFm/image4.png", // image4
            "https://image.ibb.co/fGL71R/image5.png", // image5
            "https://image.ibb.co/knA71R/image6.jpg", // image6
            "https://image.ibb.co/iWY5o6/image7.png", // image7
            "https://image.ibb.co/gLbQo6/image8.png", // image8
            "https://image.ibb.co/fLCbvm/image9.jpg", // image9
            "https://image.ibb.co/cVf71R/image10.jpg"  // image10
        };

        public static string getRandomMeme()
        {
            Random random = new Random();
            string memePath = memeList[random.Next(0, memeList.Count)];
            return memePath;
        }
    }
}