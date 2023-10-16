using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Modification.Renderers
{
	public interface IGameRenderer
	{
		void Render(ModifierContext context);
	}
}
