using Microsoft.AspNetCore.Mvc;

namespace GithubScrapper.ViewComponents
{
    public class _GithubSearchComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            return View();
        }
    }
}
