using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Modification.Modifiers
{
	public interface IModifier
	{
		void Apply(ModifierContext context);
	}
}
