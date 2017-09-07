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
        // WHAT IF THE TOKEN EXPIRES?
        private static List<string> memeList = new List<string> {
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image1.jpg?token=ATNxeKDbCjlIc75tF6U5p9A77fpgq6syks5ZucN6wA%3D%3D", // image1
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image2.jpg?token=ATNxeLq-be4GdvEazezg8jp7DlPcPN55ks5ZucS2wA%3D%3D", // image2
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image3.jpg?token=ATNxeJ2fSlM9_a3aQyNknyB1KBlr5uB2ks5ZucTPwA%3D%3D", // image3
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image4.jpg?token=ATNxeDnnKizpv7hZszjxRFxj3EJcXtkmks5ZucTuwA%3D%3D", // image4
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image5.jpg?token=ATNxePmnzVq_jDmq9_qLxXwMidqK2HQSks5ZucUUwA%3D%3D", // image5
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image6.jpg?token=ATNxeBxbigAO8YtJd2LMTyBA1Kw_Ey2Hks5ZucUnwA%3D%3D", // image6
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image7.jpg?token=ATNxeIoWKqzYd6hfvqtam_PRD_fJPYo8ks5ZucVCwA%3D%3D", // image7
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image8.jpg?token=ATNxeCvjYHk1I8OiLauTDtG-iQsRpkaoks5ZucVZwA%3D%3D", // image8
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image9.jpg?token=ATNxeEz3AYsW62I6PGM_TowaIymKOb5aks5ZucVzwA%3D%3D", // image9
            "https://raw.githubusercontent.com/pulkitkalra/EconoBot/master/EconBot/images/image10.jpg?token=ATNxeMMVPkDvbTqaghLHIzIb0Xfwj_-Oks5ZucWJwA%3D%3D" // image10

        };

        public static string getRandomMeme()
        {
            Random random = new Random();
            string memePath = memeList[random.Next(0, memeList.Count)];
            return memePath;
        }
    }
}