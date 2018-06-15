using System.Collections.Generic;

namespace GithubAPI.Models {
	public partial class AcessaAPI {
		public class RootObject {
			//Atributos para pesquisa de repositórios por nome
			public int total_count { get; set; }
			public bool incomplete_results { get; set; }
			public List<Item> items { get; set; }

			//Atributos para detalhes de um único repositório
			public Owner owner;
			public string name { get; set; }
			public string language { get; set; }
			public string description { get; set; }
			public string updated_at { get; set; }
			public List<string> Contributors { get; set; }
		}
	}
}