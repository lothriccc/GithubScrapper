using GithubScrapper.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace GithubScrapper.Controllers
{
    public class ScrapperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string parameter)
        {
            return RedirectToAction("ScrapPage", new { parameter });
        }
        public IActionResult ScrapPage(string parameter)
        {
            string url = "https://github.com/" + parameter;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var repositoryDataList = new List<RepositoryData>();
            string rUrl = $"https://github.com/{parameter}?tab=repositories";
            var rDoc = web.Load(rUrl);

            HtmlNode roodNode = doc.DocumentNode;
            HtmlNode githubNameNode = roodNode.SelectSingleNode("//span[@class='p-nickname vcard-username d-block']");
            HtmlNode githubRepositoryCountNode = roodNode.SelectSingleNode("//span[@data-view-component='true' and @class='Counter']");
            HtmlNode githubNickNameNode = roodNode.SelectSingleNode("//span[@class='p-name vcard-fullname d-block overflow-hidden']");

            HtmlNode contributionNode = roodNode.SelectSingleNode("//h2[contains(text(),'contributions')]");
            string contributionText = contributionNode?.InnerText?.Split(' ')[0];
            int totalContributions = int.TryParse(contributionText, out int contributions) ? contributions : 0;

            var nodeElement = rDoc.DocumentNode.SelectNodes("//div[@class='col-10 col-lg-9 d-inline-block']");
            foreach (var node in nodeElement)
            {
                var repositoryName = node.ChildNodes.FirstOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "h3").ChildNodes.FirstOrDefault(x => x.Name == "a").InnerText.Trim();
                var visibility = node.ChildNodes.FirstOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "h3").ChildNodes.LastOrDefault(x => x.Name == "span").InnerText.Trim();

                var languageNode = node.ChildNodes.LastOrDefault(x => x.Name == "div")
                             .ChildNodes.FirstOrDefault(x => x.Name == "span")?
                             .ChildNodes.LastOrDefault(x => x.Name == "span");

                
                var mostUsedLanguage = languageNode != null ? languageNode.InnerText.Trim() : "None";
                // var mostUsedLanguage = node.ChildNodes.LastOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "span").ChildNodes.LastOrDefault(x => x.Name == "span").InnerText.Trim();
                var updateDate = node.ChildNodes.LastOrDefault(x => x.Name == "div").ChildNodes.LastOrDefault(x => x.Name == "relative-time").InnerText.Trim();

                var repositoryData = new RepositoryData
                {
                    RepositoryName = repositoryName,
                    Visibility = visibility,
                    MostUsedLanguage = mostUsedLanguage,
                    UpdateDate = updateDate
                };
                repositoryDataList.Add(repositoryData);
            }
            var languageCounts = repositoryDataList
     .GroupBy(repo => repo.MostUsedLanguage)
     .Select(group => new
     {
         Language = group.Key,
         Count = group.Count()
     })
     .OrderByDescending(l => l.Count)
     .ToList();

            string mostUsedOverallLanguage = languageCounts.FirstOrDefault()?.Language ?? "N/A";
            

            string name = githubNameNode.InnerText;
            string repositorycount = githubRepositoryCountNode.InnerText;
            string nickname = githubNickNameNode.InnerText;

            string activityLevel = "Az Aktif"; // Default

            if (totalContributions > 5)
            {
                activityLevel = "Çok Aktif";
            }
            else if (totalContributions > 1)
            {
                activityLevel = "Aktif";
            }
            ViewBag.activityLevel = activityLevel;
            ViewBag.mostUsedOverallLanguage = mostUsedOverallLanguage;  
            ViewBag.repositoryCount = repositorycount;
            ViewBag.name = name;
            ViewBag.nickname = nickname;
            return View(repositoryDataList);
        }
    }
}
