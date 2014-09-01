using Beaker.TechDays.Billboard.Models.Specs.Helpers;
using Billboard.Models;

namespace Beaker.TechDays.Billboard.Models.Specs.Builders
{
	public sealed class CategoryBuilder
	{
		private Maybe<string> _name;

		public CategoryBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public Category Build()
		{
			string name = _name.GetValueOrDefault(Some.String());
			return new Category(name);
		}
	}
}
