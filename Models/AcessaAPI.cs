using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GithubAPI.Models {
	public partial class AcessaAPI {
		//URL da API para baixar meus repositórios
		private readonly string urlMyRepo = "https://api.github.com/users/leandro-sds/repos";
		//URL da API para pesquisar repositórios
		private readonly string urlPesquisaRepo = "https://api.github.com/search/repositories?q=";
		//User agent para validar a requisição
		private readonly string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36";
		private readonly WebClient client;

		public AcessaAPI() {
			client = new WebClient();
		}

		public List<Repositorio> GetMyRepoList() {
			List<Repositorio> repoList = new List<Repositorio>();

			client.Headers.Add("user-agent", this.userAgent);

			var resposta = client.DownloadString(this.urlMyRepo);

			var repoJSON = JArray.Parse(resposta);

			foreach (var repoJToken in repoJSON) {
				JObject repo = (JObject)repoJToken;
				Repositorio repoObject = new Repositorio {
					RepoID = repo.GetValue("id").ToString(),
					Name = repo.GetValue("name").ToString()
				};

				repoList.Add(repoObject);
			}
			return repoList;
		}

		public List<Repositorio> PesquisaRepo(string repoNome) {
			List<Repositorio> repositorios = new List<Repositorio>();
			string urlPesquisa = urlPesquisaRepo + repoNome;

			client.Headers.Add("user-agent", this.userAgent);

			var resposta = client.DownloadString(urlPesquisa);

			RootObject result = JsonConvert.DeserializeObject<RootObject>(resposta);

			var items = result.items;

			foreach (var item in items) {
				Repositorio repoObject = new Repositorio {
					Name = item.name,
					RepoID = item.id.ToString()
				};
				repositorios.Add(repoObject);
			}

			return repositorios;
		}

		public Repositorio GetRepoByID(string id) {
			Repositorio repo = new Repositorio();

			string urlGetSingleRepo = "https://api.github.com/repositories/" + id;
			client.Headers.Add("user-agent", userAgent);
			var resposta = client.DownloadString(urlGetSingleRepo);

			RootObject result = JsonConvert.DeserializeObject<RootObject>(resposta);
			string data = result.updated_at.Split('T')[0];

			//Formata data para o padrão DD-MM-AAAA
			string[] dataFormat = data.Split('-');
			string dataLastUpdate = dataFormat[2] + "/" + dataFormat[1] + "/" + dataFormat[0];
			
			repo.Name = result.name;
			repo.Language = result.language;
			repo.Description = result.description;
			repo.Owner = (result.owner.login);
			repo.DataUpdate = dataLastUpdate;
			
			string urlGetContributors = string.Format("https://api.github.com/repos/{0}/{1}/contributors", repo.Owner, repo.Name);
			client.Headers.Add("user-agent", userAgent);
			resposta = client.DownloadString(urlGetContributors);
			var repoJSON = JArray.Parse(resposta);

			foreach (var repoJToken in repoJSON) {
				JObject repoJObject = (JObject)repoJToken;
				repo.Contributors.Add(repoJObject.GetValue("login").ToString());
			}
			
			return repo;
		}
	}
}