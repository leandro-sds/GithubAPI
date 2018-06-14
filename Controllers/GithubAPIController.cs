using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GithubAPI.Controllers
{
    public class GithubAPIController : Controller
    {
        private readonly AcessaAPI api = new AcessaAPI();

        public IActionResult Index() {
            return View(api.GetRepoList());
        }
    }
}