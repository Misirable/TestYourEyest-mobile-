using System.Collections;
using UnityEngine;

namespace Game.Modification.Renderers
{
	using Modifiers;
	using Helpers;

	public class ForthLevelGameRenderer : IGameRenderer
	{
		private IModifier[] _modifiers = new IModifier[]
		{
			new SwapLettersModifier(),
			new InvertControlModifier(),
			new FlipColorLabelModifier()
		};

		public void Render(ModifierContext context)
		{
			IModifier modifier = ModifiersHelper.EqualProbabilityNull(_modifiers);
			modifier?.Apply(context);
		}
	}
}