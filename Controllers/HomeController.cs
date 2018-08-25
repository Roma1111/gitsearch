using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;
using GitSearch.Models;

namespace GitSearch.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        
        //search for repository by the searchWord
        public ActionResult gitSearch(string searchWord)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var str = "https://api.github.com/search/repositories?q=" + searchWord;
            //create the url
            Uri myUri = new Uri(str);
            //create the http get request
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(myUri);
            webReq.Method = "GET";
            webReq.Accept = "application/json";
            webReq.UserAgent = "Mozilla/5.0";
            try
            { 
                WebResponse myWebResponse = webReq.GetResponse();
                Stream dataStream = myWebResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd(); 
                //deserialize the json results from the servers and insert the data into the result model to enable strong types
                var repoModel = new JavaScriptSerializer().Deserialize<results>(responseFromServer);
                // Clean up the streams and the response.  
                reader.Close();
                myWebResponse.Close();                
                return View(repoModel);
            }
            //for empty search string
            catch (System.Net.WebException)
            {                
                return RedirectToAction("nullSearchWord"); 
            }

                         
        }
        //error page
        public ActionResult nullSearchWord()
        {
            return View();
        }

        //bookmark repository by save it in current session
        public ActionResult BookMark (int id, string avatar, string name)
        {           
            RepositoryViewModel repoVm = new RepositoryViewModel();
            repoVm.id = id;
            repoVm.name = name;
            repoVm.avatar = avatar;
            if (Session[name] == null) {                
                Session[name] = repoVm;
            }              
            return View(repoVm);
        }

        //view the bookmarked repositories by iterating over the session keys
        public ActionResult viewBookmarked ()
        {            
            List<RepositoryViewModel> repList = new List<RepositoryViewModel>();
            for (int i = 0; i < Session.Contents.Count; i++)
            {                
                RepositoryViewModel rep = (RepositoryViewModel)Session[Session.Keys[i]];
                repList.Add(rep);                
            }
            return View(repList);
        }
            
    }
}