using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Static.Events;

using Progress;

namespace Menu
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenuPanel;
		[SerializeField] private LevelsPanel levelsPanel;

		private PlayerProgress _progress;

		private void Start()
		{
			mainMenuPanel.SetActive(true);
		}

		public void Exit()
		{
			EventBus.Broadcast(AppEvents.QUIT);
		}

		public void InitialState()
		{
			mainMenuPanel.SetActive(true);
			levelsPanel.Close();
		}

		public void StartLevelSelection()
		{
			mainMenuPanel.SetActive(false);
			levelsPanel.Open(_progress);
		}

		public void EndLevelSelection()
		{
			mainMenuPanel.SetActive(true);
			levelsPanel.Close();
		}

		public void StartGame(int level)
		{
			EventBus.Broadcast<int>(AppEvents.START_GAME, level);
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}

		public void Open(PlayerProgress progress)
		{
			gameObject.SetActive(true);
			InitialState();

			_progress = progress;
		}
	}

}
