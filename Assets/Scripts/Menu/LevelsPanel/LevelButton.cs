using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Static.Events;

namespace Menu
{
	public class LevelButton : MonoBehaviour
	{
		private const float MOVE_TIME = 0.2f;
		private const float MOVE_DISTANCE = 210;

		[SerializeField] private Text text;

		public int Level { get; private set; }

		private Animator _animator;

		[SerializeField] private RectTransform button;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void Init(int level, ButtonAppearanceOptions appearanceOptions)
		{
			Level = level;

			text.text = level.ToString();

			_animator.SetBool("IsShown", true);

			switch (appearanceOptions)
			{
				case ButtonAppearanceOptions.NoAnimation:
					_animator.Play("Normal");

					break;
				case ButtonAppearanceOptions.FromLeft:
					SetRectX(-MOVE_DISTANCE);
					StartCoroutine(AnimateX(MOVE_DISTANCE, MOVE_TIME));

					break;
				case ButtonAppearanceOptions.FromRight:
					SetRectX(MOVE_DISTANCE);
					StartCoroutine(AnimateX(-MOVE_DISTANCE, MOVE_TIME));

					break;
			}
		}

		public void SelectLevel()
		{
			EventBus.Broadcast(AppEvents.START_GAME, Level);
		}

		public void SwipeLeft()
		{
			_animator.SetBool("IsShown", false);
			StartCoroutine(AnimateX(-MOVE_DISTANCE, MOVE_TIME, true));
		}

		public void SwipeRight()
		{
			_animator.SetBool("IsShown", false);
			StartCoroutine(AnimateX(MOVE_DISTANCE, MOVE_TIME, true));
		}

		private void SetRectX(float value)
		{
			float posY = button.anchoredPosition.y;
			button.anchoredPosition = new Vector2(value, posY);
		}

		private IEnumerator AnimateX(float value, float time, bool destroy=false)
		{
			float oldX = button.anchoredPosition.x;

			float speed = value / time;
			float translationX = 0;

			while (translationX < value && value >= 0 || translationX > value && value < 0)
			{
				yield return null;

				translationX += Time.deltaTime * speed;
				SetRectX(oldX + translationX);
			}

			if (destroy)
				Destroy(gameObject);
			else
				SetRectX(oldX + value);
		}
	}
}
