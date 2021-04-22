using System;
using System.Xml;


namespace Cycliq.Agents
{
    public static class BooruAgent
    {
        public static void GetImageFromR34XMLDoc(out string ImageUrl, out string ImageTags, XmlDocument Res, Random rand)
            {
                int Index = rand.Next(Res.DocumentElement.SelectNodes("/post/posts").Count);
                XmlAttributeCollection a = Res.DocumentElement.SelectNodes("/posts/post")[Index].Attributes;
                while (a.GetNamedItem("file_url").Value.EndsWith("mp4") || a.GetNamedItem("file_url").Value.EndsWith("webm"))
                    a = Res.DocumentElement.SelectNodes("/posts/post")[rand.Next(Res.DocumentElement.SelectNodes("/post/posts").Count)].Attributes;

                ImageUrl = a.GetNamedItem("file_url").Value;
                ImageTags = a.GetNamedItem("tags").Value;
                if (ImageTags.Length > 100)
                    ImageTags = ImageTags.Substring(0, 95) + "...";
            }
    }
}