using System.Collections;
using UnityEngine;

namespace Game
{
	using Progress;
	using Modification.Modifiers;

	public class GameResult
	{
		public int LevelNumber { get; }

		public int LevelCompleteScore { get; }
		public int ScoreGained { get; }

		public GameResultStatuses GameStatus { get; }
		// public IModifier CurrentModifier { get; }

		public GameResult(
			int levelNumber,
			int levelCompleteScore, int scoreGained,
			GameResultStatuses gameStatus // IModifier currentModifier
		)
		{
			LevelNumber = levelNumber;
			LevelCompleteScore = levelCompleteScore;
			ScoreGained = scoreGained;
			GameStatus = gameStatus;
			//CurrentModifier = currentModifier;
		}
	}
}