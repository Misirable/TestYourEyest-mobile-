using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Progress;

using Static.Events;

namespace Menu
{
	public class LevelsPanel : MonoBehaviour
	{
		[SerializeField] private LevelButton levelButtonPrefab;
		[SerializeField] private GameObject panel;

		[SerializeField] private SwitchLevelButton prevButton;
		[SerializeField] private SwitchLevelButton nextButton;

		[SerializeField] private LevelButton currentButton;

		private int _current = 0;
		public int Current => _current;

		private PlayerProgress _progress;

		private void Start()
		{
			panel.SetActive(false);

			EventBus.Subscribe(MenuEvents.SWITCH_NEXT_LEVEL, Next);
			EventBus.Subscribe(MenuEvents.SWITCH_PREV_LEVEL, Prev);
		}

		private void OnDestroy()
		{
			EventBus.Unsubscribe(MenuEvents.SWITCH_NEXT_LEVEL, Next);
			EventBus.Unsubscribe(MenuEvents.SWITCH_PREV_LEVEL, Prev);
		}

		public void Open(PlayerProgress progress)
		{
			panel.SetActive(true);

			_current = PlayerProgress.FIRST_LEVEL_NUMBER;
			_progress = progress;

			EnsureButtonsLocked();

			currentButton = InstanciateButton(ButtonAppearanceOptions.NoAnimation);
		}

		public void Close()
		{
			panel.SetActive(false);
		}

		private void Prev()
		{
			_current--;

			currentButton.SwipeRight();
			currentButton = InstanciateButton(ButtonAppearanceOptions.FromLeft);

			EnsureButtonsLocked();
		}

		private void Next()
		{
			_current++;

			currentButton.SwipeLeft();
			currentButton = InstanciateButton(ButtonAppearanceOptions.FromRight);

			EnsureButtonsLocked();
		}
		 
		private void EnsureButtonsLocked()
		{
			if (_current == PlayerProgress.FIRST_LEVEL_NUMBER)
				prevButton.Lock();
			else if (prevButton.IsLocked)
				prevButton.Unlock();

			if (_current == _progress.LevelsUnlocked + PlayerProgress.FIRST_LEVEL_NUMBER - 1)
				nextButton.Lock();
			else if (nextButton.IsLocked)
				nextButton.Unlock();
		}

		private LevelButton InstanciateButton(ButtonAppearanceOptions appearanceOptions)
		{
			LevelButton levelButton = Instantiate(levelButtonPrefab);
			levelButton.Init(_current, appearanceOptions);

			RectTransform levelButtonRect = levelButton.GetComponent<RectTransform>();
			levelButtonRect.parent = panel.GetComponent<RectTransform>();

			levelButtonRect.offsetMax = Vector2.zero;
			levelButtonRect.offsetMin = Vector2.zero;

			return levelButton;
		}
	}
}
