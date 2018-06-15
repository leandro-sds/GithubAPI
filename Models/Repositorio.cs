using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubAPI.Models {
	public class Repositorio {
		public string Name { get; set; }
		public string RepoID { get; set; }
		public string Language { get; set; }
		public string Description { get; set; }
		public string DataUpdate { get; set; }
		public string Owner { get; set; }
		public List<string> Contributors;

		public Repositorio() {
			Contributors = new List<string>();
		}
	}
}
