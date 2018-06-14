using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GithubAPI.Models {
	public class AcessaAPI {
		// URL da API
		private string url = "https://api.github.com/users/leandro-sds/repos";
		//User agent para validar a requisição
		private string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36";

		public List<Repositorio> GetRepoList() {
			List<Repositorio> repoList = new List<Repositorio>();

			var client = new WebClient();
			client.Headers.Add("user-agent", this.userAgent);

			var resposta = client.DownloadString(this.url);

			var repoJSON = JArray.Parse(resposta);

			foreach (var repoJToken in repoJSON) {
				JObject repo = (JObject)repoJToken;
				Repositorio repoObject = new Repositorio {
					RepoURL = repo.GetValue("html_url").ToString(),
					Nome = repo.GetValue("name").ToString()
				};
				repoList.Add(repoObject);
			}
			return repoList;
		}
	}
}
