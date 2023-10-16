using System;

namespace Game.Modification
{
	public class ModifierContext
	{
		public ColorLabel Label { get; }
		public EnvironmentParameters EnvironmentParameters { get; }

		public ModifierContext(ColorLabel label, EnvironmentParameters envParameters)
		{
			Label = label;
			EnvironmentParameters = envParameters;
		}
	}
}
