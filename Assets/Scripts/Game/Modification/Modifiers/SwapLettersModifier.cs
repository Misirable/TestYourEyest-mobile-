using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Modification.Modifiers
{
	public class SwapLettersModifier : IModifier
	{
		public void Apply(ModifierContext context)
		{
			string lblText = context.Label.Text;
			lblText = string.Join("", lblText.OrderBy(ch => Random.Range(0f, 1f)));

			context.Label.Text = lblText;
		}
	}
}
