using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Modification.Modifiers
{
	public class InvertControlModifier : IModifier
	{
		public void Apply(ModifierContext context)
		{
			context.EnvironmentParameters.InvertedControl = true;
		}
	}
}
