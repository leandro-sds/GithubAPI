using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GithubAPI.Controllers {
	public class GithubAPIController : Controller {
		private readonly AcessaAPI api = new AcessaAPI();

		public IActionResult Index() {
			return View(api.GetMyRepoList());
		}

		public IActionResult PesquisaRepo(string termoPesquisa) {
			ViewData["pesquisa"] = !String.IsNullOrEmpty(termoPesquisa) ? termoPesquisa : "";

			if (!string.IsNullOrEmpty(termoPesquisa)) {
				return View(api.PesquisaRepo(termoPesquisa));
			}
			return View();
		}

		public IActionResult ExibeDetalhes(string id) {
			return View(api.GetRepoByID(id));
		}
	}
}