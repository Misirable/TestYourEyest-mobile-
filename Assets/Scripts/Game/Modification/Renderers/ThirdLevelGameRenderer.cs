using System.Collections;
using UnityEngine;

namespace Game.Modification.Renderers
{
	using Modifiers;
	using Helpers;

	public class ThirdLevelGameRenderer : IGameRenderer
	{
		private IModifier[] _modifiers = new IModifier[]
		{
			new SwapLettersModifier(),
			new InvertControlModifier()
		};

		public void Render(ModifierContext context)
		{
			IModifier modifier = ModifiersHelper.EqualProbabilityNull(_modifiers);
			modifier?.Apply(context);
		}
	}
}