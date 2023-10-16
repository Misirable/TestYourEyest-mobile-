using System.Collections;
using UnityEngine;

namespace Game.Modification.Helpers
{
	using Modifiers;

	internal static class ModifiersHelper
	{
		public static IModifier EqualProbability(IModifier[] modifiers)
		{
			int random = Random.Range(0, modifiers.Length);

			return modifiers[random];
		}

		public static IModifier EqualProbabilityNull(IModifier[] modifiers)
		{
			int random = Random.Range(0, modifiers.Length + 1);

			return random == modifiers.Length ? null : modifiers[random];
		}
	}
}