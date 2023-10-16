using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress
{
	[Serializable]
	public class PlayerProgress
	{
		[NonSerialized]
		public const int TOTAL_LEVELS = 5;
		[NonSerialized]
		public const int FIRST_LEVEL_NUMBER = 1;

		public int LevelsUnlocked { get; }

		public PlayerProgress(int levelsUnlocked)
		{
			LevelsUnlocked = levelsUnlocked;
		}
	}
}
