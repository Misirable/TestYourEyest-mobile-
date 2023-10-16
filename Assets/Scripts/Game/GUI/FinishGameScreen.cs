using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Static.Events;

using Progress;

namespace Game
{
	public class FinishGameScreen : MonoBehaviour
	{
		private static Dictionary<GameResultStatuses, string> _gameResultToString =
			new Dictionary<GameResultStatuses, string>()
			{
				{ GameResultStatuses.TimeExpired, "Time expired" },
				{ GameResultStatuses.WrongAnswer, "You gave wrong answer" },
			};

		[SerializeField] private Canvas canvas;

		[SerializeField] private Text title;
		[SerializeField] private Text info;

		public void Close()
		{
			canvas.gameObject.SetActive(false);
			title.text = "";
			info.text = "";
		}

		public void Open(GameResult gameResult, PlayerProgress oldProgress)
		{
			canvas.gameObject.SetActive(true);

			if (gameResult.GameStatus == GameResultStatuses.Win)
			{
				ShowWin(gameResult, oldProgress);
				return;
			}

			ShowDefeat(gameResult);
		}

		private void ShowWin(GameResult gameResult, PlayerProgress progress)
		{
			title.text = "You won!";

			if (progress.LevelsUnlocked == gameResult.LevelNumber &&
				gameResult.LevelNumber != PlayerProgress.TOTAL_LEVELS)
			{
				info.text = $"You have unlocked a level {gameResult.LevelNumber + 1}";
			}
			else
			{
				info.text = $"Level complete!";
			}
		}

		private void ShowDefeat(GameResult gameResult)
		{
			string reason = _gameResultToString[gameResult.GameStatus];

			title.text = "You lost!";
			info.text =
				$"You gave {gameResult.ScoreGained} right answers\n\n" +
				$"Reason of defeat:\n" +
				$"{reason}";
		}

		public void GoToMenu()
		{
			EventBus.Broadcast(AppEvents.TO_MAIN_MENU);
		}
	}
}
