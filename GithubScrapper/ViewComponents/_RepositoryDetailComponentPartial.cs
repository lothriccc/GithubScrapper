using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace GithubScrapper.ViewComponents
{
	public class _RepositoryDetailComponentPartial:ViewComponent
	{
		public IViewComponentResult Invoke(string parameter, string repoName)
		{
			string url = $"https://github.com/{parameter}/{repoName}";
			var web = new HtmlWeb();
			var doc = web.Load(url);

			HtmlNode roodNode = doc.DocumentNode;
			//HtmlNode denemeNode = roodNode.SelectSingleNode("//span[@class='Label Label--secondary v-align-middle mr-1 d-none d-md-block']");

			HtmlNode repositoryCommitNode = roodNode.SelectSingleNode("//span[@class='fgColor-default']");
			HtmlNode repositoryDescriptionNode = roodNode.SelectSingleNode("//p[@class='f4 my-3']");


			//Yıldız Sayısı
			var starNode = roodNode.SelectSingleNode("//a[contains(@href, '/stargazers')]/strong");
			ViewBag.StarCount = starNode.InnerText.Trim();

			// İzleyici sayısını 
			var watchersNode = roodNode.SelectSingleNode("//a[contains(@href, '/watchers')]/strong");
			ViewBag.WatchersCount = watchersNode != null ? watchersNode.InnerText.Trim() : "0";


			string repositoryName = parameter + "/" + repoName;
			string repositoryCommit = repositoryCommitNode.InnerText;
			string repositoryDescription = repositoryDescriptionNode != null ? repositoryDescriptionNode.InnerText : "Açıklama Yok";


			ViewBag.repositoryDescription = repositoryDescription;


			ViewBag.repositoryName = repositoryName;
			ViewBag.repositoryCommit = repositoryCommit;

			return View();
		}
	}
}
