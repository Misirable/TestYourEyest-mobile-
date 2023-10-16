using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Static.Events;
using Game.Modification;

namespace Game
{
	using GUI;

	public class GameController : MonoBehaviour
	{
		private static Color NormalizeRGB(int r, int g, int b)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
		}

		public static GuessedColor[] colors = new GuessedColor[]
		{
			new GuessedColor("Red", Color.red),
			new GuessedColor("Yellow", Color.yellow),
			new GuessedColor("Green", Color.green),
			new GuessedColor("Blue", Color.blue),
			new GuessedColor("Cyan", Color.cyan),
			new GuessedColor("Purple", NormalizeRGB(255, 0, 255)),
			new GuessedColor("Orange", NormalizeRGB(255, 128, 0)),
		};

		[SerializeField] private Timer timer;
		[SerializeField] private ColorsCounter counter;
		[SerializeField] private GameField gameField;

		private int _level;
		private GameParameters _parameters;

		private bool _isWrong = false;
		private bool _invertContols = false;
		private Color? _background = null;

		private int _score;

		private void Start()
		{
			EventBus.Subscribe(GameEvents.TIME_EXPIRED, OnTimeExpired);
		}

		private void OnDestroy()
		{
			EventBus.Unsubscribe(GameEvents.TIME_EXPIRED, OnTimeExpired);
		}

		private GameResult GetGameResult(GameResultStatuses gameStatus)
		{
			return new GameResult(_level, _parameters.LevelCompleteScore, _score, gameStatus);
		}

		private void OnTimeExpired()
		{
			EndGame(GameResultStatuses.TimeExpired);
		}

		private void EndGame(GameResultStatuses gameStatus)
		{
			gameField.RemoveLabel();
			timer.ResetTimer();

			GameResult gameResult = GetGameResult(gameStatus);

			EventBus.Broadcast(AppEvents.END_GAME, gameResult);

			if (_background != null)
				EventBus.Broadcast(GameEvents.BACKGROUND_RESET);
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}

		public void StartGame(int level)
		{
			gameObject.SetActive(true);

			GameParametersFactory paramsFactory = new GameParametersFactory(level);
			_parameters = paramsFactory.GetParameters();

			_score = 0;
			_level = level;
			_background = null;

			timer.StartTimer(_parameters.StartRemainingSeconds);
			counter.SetValue(_parameters.LevelCompleteScore);

			Next();
		}

		private ModifierContext ModifyTurn(ColorLabel label)
		{
			EnvironmentParameters envParams = new EnvironmentParameters()
			{
				InvertedControl = false
			};

			ModifierContext context = new ModifierContext(label, envParams);

			_parameters.GameRenderer.Render(context);

			return context;
		}

		private ColorLabelParameters GenerateLabel()
		{
			int paintIndex = Random.Range(0, colors.Length);
			GuessedColor colorPaint = colors[paintIndex];

			bool isWrong = Random.Range(0f, 1f) < _parameters.WrongProbability;
			GuessedColor colorTitle;

			if (isWrong)
			{
				int titleIndex = Random.Range(0, colors.Length - 1);

				if (titleIndex >= paintIndex)
					titleIndex++;

				colorTitle = colors[titleIndex];
			}
			else
			{
				colorTitle = colorPaint;
			}

			_isWrong = isWrong;
			//Debug.Log(!isWrong);

			return new ColorLabelParameters()
			{
				ColorPaint = colorPaint,
				Text = colorTitle.Title
			};
		}

		private void ParseContext(ModifierContext context)
		{
			if (context.EnvironmentParameters.InvertedControl && !_invertContols)
				EventBus.Broadcast(GameEvents.INVERT_CONTROLS);

			else if (!context.EnvironmentParameters.InvertedControl && _invertContols)
				EventBus.Broadcast(GameEvents.RESET_CONTROLS);

			_invertContols = context.EnvironmentParameters.InvertedControl;


			Color? background = context.EnvironmentParameters.BackgroundColor;

			if (background != null)
				EventBus.Broadcast(GameEvents.BACKGROUND_CHANGED, background.Value);
			else if (background == null && _background != null)
				EventBus.Broadcast(GameEvents.BACKGROUND_RESET);

			_background = background;
		}

		private void Next()
		{
			ColorLabelParameters lblParams = GenerateLabel();
			ColorLabel label = gameField.PutLabel(lblParams);

			ModifierContext context = ModifyTurn(label);

			ParseContext(context);
		}

		private void TakeAnswer(PlayerAnswers answer)
		{
			if (answer == PlayerAnswers.Yes && _isWrong || answer == PlayerAnswers.No && !_isWrong)
			{
				EndGame(GameResultStatuses.WrongAnswer);
				return;
			}

			_score++;
			counter.SetValue(_parameters.LevelCompleteScore - _score);

			if (_score == _parameters.LevelCompleteScore)
			{
				EndGame(GameResultStatuses.Win);
				return;
			}

			timer.AddTime(_parameters.TimeBoost);
			gameField.RemoveLabel();
			Next();
		}

		public void Yes()
		{
			TakeAnswer(PlayerAnswers.Yes);
		}

		public void No()
		{
			TakeAnswer(PlayerAnswers.No);
		}
	}
}
