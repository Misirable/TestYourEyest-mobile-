using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	using Modification.Renderers;

	public class GameParametersFactory
	{
		private Dictionary<int, GameParameters> _levelsToParams = new Dictionary<int, GameParameters>()
		{
			{ 1, new GameParameters()
				{
					StartRemainingSeconds = 20,
					TimeBoost = 1,
					WrongProbability = 0.5f,
					LevelCompleteScore = 20,
					GameRenderer = new FirstLevelGameRenderer()
				}
			},
			{ 2, new GameParameters()
				{
					StartRemainingSeconds = 15,
					TimeBoost = 1,
					WrongProbability = 0.5f,
					LevelCompleteScore = 40,
					GameRenderer = new SecondLevelGameRenderer()
				}
			},
			{ 3, new GameParameters()
				{
					StartRemainingSeconds = 10,
					TimeBoost = 1,
					WrongProbability = 0.5f,
					LevelCompleteScore = 60,
					GameRenderer = new ThirdLevelGameRenderer()
				}
			},
			{ 4, new GameParameters()
				{
					StartRemainingSeconds = 5,
					TimeBoost = 1,
					WrongProbability = 0.5f,
					LevelCompleteScore = 80,
					GameRenderer = new ForthLevelGameRenderer()
				}
			},
			{ 5, new GameParameters()
				{
					StartRemainingSeconds = 2,
					TimeBoost = 1,
					WrongProbability = 0.5f,
					LevelCompleteScore = 100,
					GameRenderer = new FifthLevelGameRenderer()
				}
			},
		};

		public int Level { get; }

		public GameParametersFactory(int level)
		{
			Level = level;
		}

		public GameParameters GetParameters()
		{
			return _levelsToParams[Level];
		}
	}
}