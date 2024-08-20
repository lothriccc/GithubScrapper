using GithubScrapper.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace GithubScrapper.ViewComponents
{
    public class _GithubProfileInfoComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke(string parameter)
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




     //       var nodeElement = rDoc.DocumentNode.SelectNodes("//div[@class='col-10 col-lg-9 d-inline-block']");
     //       foreach (var node in nodeElement)
     //       {
     //           var repositoryName = node.ChildNodes.FirstOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "h3").ChildNodes.FirstOrDefault(x => x.Name == "a").InnerText.Trim();
     //           var visibility = node.ChildNodes.FirstOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "h3").ChildNodes.LastOrDefault(x => x.Name == "span").InnerText.Trim();

     //           var languageNode = node.ChildNodes.LastOrDefault(x => x.Name == "div")
     //                        .ChildNodes.FirstOrDefault(x => x.Name == "span")?
     //                        .ChildNodes.LastOrDefault(x => x.Name == "span");


     //           var mostUsedLanguage = languageNode != null ? languageNode.InnerText.Trim() : "None";
     //           // var mostUsedLanguage = node.ChildNodes.LastOrDefault(x => x.Name == "div").ChildNodes.FirstOrDefault(x => x.Name == "span").ChildNodes.LastOrDefault(x => x.Name == "span").InnerText.Trim();
     //           var updateDate = node.ChildNodes.LastOrDefault(x => x.Name == "div").ChildNodes.LastOrDefault(x => x.Name == "relative-time").InnerText.Trim();

     //           var repositoryData = new RepositoryData
     //           {
     //               RepositoryName = repositoryName,
     //               Visibility = visibility,
     //               MostUsedLanguage = mostUsedLanguage,
     //               UpdateDate = updateDate
     //           };
     //           repositoryDataList.Add(repositoryData);
     //       }
     //       var languageCounts = repositoryDataList
     //.GroupBy(repo => repo.MostUsedLanguage)
     //.Select(group => new
     //{
     //    Language = group.Key,
     //    Count = group.Count()
     //})
     //.OrderByDescending(l => l.Count)
     //.ToList();

     //       string mostUsedOverallLanguage = languageCounts.FirstOrDefault()?.Language ?? "N/A";


            string name = githubNameNode.InnerText;
            string repositorycount = githubRepositoryCountNode.InnerText;
            string nickname = githubNickNameNode.InnerText;





            //ViewBag.mostUsedOverallLanguage = mostUsedOverallLanguage;
            ViewBag.repositoryCount = repositorycount;
            ViewBag.name = name;
            ViewBag.nickname = nickname;
            ViewBag.parameter = parameter;

            return View(repositoryDataList);
        }
    }
}
