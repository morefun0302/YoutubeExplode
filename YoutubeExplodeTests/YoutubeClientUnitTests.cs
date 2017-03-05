﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YoutubeExplode.Tests
{
    [TestClass]
    public class YoutubeClientUnitTests
    {
        [TestMethod]
        public void ValidateVideoId_Test()
        {
            string[] validVideoIds =
            {
                "cpm00Hv1Umg",
                "aI5pUqiVJdw",
                "9onx5sgnkPQ",
                "lg0s242Hg-8",
                "fIDyDVzlqN4",
                "JE1Gvzxfm1E",
                "OpV62-86Fv4",
                "UnUkNfX8v1E",
                "aGTz8o_fey8",
                "10V6xet5ODk"
            };
            string[] invalidVideoIds =
            {
                null,
                "",
                "@pm!!Hv#Lmg",
                "lg0s242Hg#8",
                "f`DyDVzlqN`",
                "JE1Gv[]fm1E",
                "***62-86Fv4",
                "U  kNfX8v1E",
                "aGяк8o_fey8",
                "10Vあxet5ODk"
            };

            foreach (string id in validVideoIds)
                Assert.IsTrue(YoutubeClient.ValidateVideoId(id));
            foreach (string id in invalidVideoIds)
                Assert.IsFalse(YoutubeClient.ValidateVideoId(id));
        }

        [TestMethod]
        public void ParseVideoId_Test()
        {
            string[] urls =
            {
                "https://www.youtube.com/watch?v=cpm00Hv1Umg",
                "https://youtu.be/yIVRs6YSbOM",
                "https://www.youtube.com.ua/watch?v=10V6xet5ODk",
                "https://www.youtube.co.il/watch?v=OpV62-86Fv4",
                "https://www.youtube.be/watch?v=Nsib94LHi9I&gl=BE"
            };
            string[] ids =
            {
                "cpm00Hv1Umg",
                "yIVRs6YSbOM",
                "10V6xet5ODk",
                "OpV62-86Fv4",
                "Nsib94LHi9I"
            };

            for (int i = 0; i < urls.Length; i++)
            {
                string url = urls[i];
                string id = ids[i];
                string parsed = YoutubeClient.ParseVideoId(url);
                Assert.AreEqual(id, parsed);
            }
        }

        [TestMethod]
        public void TryParseVideoId_Test()
        {
            string[] validUrls =
            {
                "https://www.youtube.com/watch?v=cpm00Hv1Umg",
                "https://youtu.be/yIVRs6YSbOM",
                "https://www.youtube.com.ua/watch?v=10V6xet5ODk",
                "https://www.youtube.co.il/watch?v=OpV62-86Fv4",
                "https://www.youtube.be/watch?v=Nsib94LHi9I&gl=BE"
            };
            string[] validIds =
            {
                "cpm00Hv1Umg",
                "yIVRs6YSbOM",
                "10V6xet5ODk",
                "OpV62-86Fv4",
                "Nsib94LHi9I"
            };

            for (int i = 0; i < validUrls.Length; i++)
            {
                string url = validUrls[i];
                string id = validIds[i];
                string parsed;
                bool success = YoutubeClient.TryParseVideoId(url, out parsed);
                Assert.IsTrue(success);
                Assert.AreEqual(id, parsed);
            }

            string[] invalidUrls =
            {
                null,
                "",
                "https://www.youtube.com",
                "https://www.youtube.com/watch?v=@pm!!Hv#Lmg",
                "https://www.youtube.com/qweasd?v=Nsib94LHi9I"
            };

            foreach (string url in invalidUrls)
            {
                string parsed;
                bool success = YoutubeClient.TryParseVideoId(url, out parsed);
                Assert.IsFalse(success);
            }
        }
    }
}