using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Modification.Modifiers
{
	public class ChangeBackgroundModifier : IModifier
	{
		private static Color NormalizeRGB(int r, int g, int b)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
		}

		private static Color[] BackgroundColors = new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.blue,
			Color.cyan,
			NormalizeRGB(255, 0, 255),
			NormalizeRGB(255, 128, 0),
		};

		static ChangeBackgroundModifier()
		{
			for (int i = 0; i < BackgroundColors.Length; i++)
				BackgroundColors[i] = Color.Lerp(BackgroundColors[i], Color.black, 0.4f);
		}

		public void Apply(ModifierContext context)
		{
			int index = Random.Range(0, BackgroundColors.Length);
			context.EnvironmentParameters.BackgroundColor = BackgroundColors[index];
		}
	}
}
