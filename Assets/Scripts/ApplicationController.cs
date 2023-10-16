using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Static.Events;

using Menu;
using Game;

public class ApplicationController : MonoBehaviour
{
	[SerializeField] private MainMenu menu;
	[SerializeField] private GameController game;
	[SerializeField] private FinishGameScreen finishScreen;
	[SerializeField] private ProgressContoller progress;

	private void Start()
	{
		progress.Load();

		menu.Open(progress.Progress);
		game.Close();
		finishScreen.Close();

		EventBus.Subscribe(AppEvents.QUIT, Exit);
		EventBus.Subscribe<int>(AppEvents.START_GAME, StartGame);
		EventBus.Subscribe<GameResult>(AppEvents.END_GAME, EndGame);
		EventBus.Subscribe(AppEvents.TO_MAIN_MENU, ToMainMenu);
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(AppEvents.QUIT, Exit);
		EventBus.Unsubscribe<int>(AppEvents.START_GAME, StartGame);
		EventBus.Unsubscribe<GameResult>(AppEvents.END_GAME, EndGame);
		EventBus.Unsubscribe(AppEvents.TO_MAIN_MENU, ToMainMenu);
	}

	public void Exit()
	{
		Application.Quit(0);
	}

	public void StartGame(int level)
	{
		menu.Close();
		game.StartGame(level);
	}

	public void EndGame(GameResult gameResult)
	{
		game.Close();
		finishScreen.Open(gameResult, progress.Progress);

		if (progress.TryUnlockNextLevel(gameResult))
			progress.Save();
	}

	public void ToMainMenu()
	{
		menu.Open(progress.Progress);
		finishScreen.Close();
	}
}
