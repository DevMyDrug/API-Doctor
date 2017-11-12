using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace DoctorAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string url = "https://agenda.direct/rendez-vous/medecine-generale/lyon?search=s";
                string txt = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                List<string> lUrl = new List<string>();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(reader.ReadToEnd());
                    HtmlNode divList = doc.GetElementbyId("searchlist");
                    int nbNode = 0;
                    foreach(HtmlNode node in divList.Descendants("div"))
                    {
                        if(nbNode <3)
                        {
                            nbNode++;
                            continue;
                        }
                        HtmlNode next = node.Descendants("div").ElementAt(1).Descendants("a").ElementAt(0);
                        string nexturl = next.Attributes["href"].Value;
                        lUrl.Add(nexturl);

                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
