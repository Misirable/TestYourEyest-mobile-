using System.Collections;
using UnityEngine;

namespace Game.Modification.Modifiers
{
	public class FlipColorLabelModifier : IModifier
	{
		public void Apply(ModifierContext context)
		{
			context.Label.transform.Rotate(0, 0, 180);
		}
	}
}