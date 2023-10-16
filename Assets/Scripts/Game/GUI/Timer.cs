using System.Collections;
using UnityEngine;

using Static.Events;
using UnityEngine.UI;

namespace Game.GUI
{
	public class Timer : MonoBehaviour
	{
		[SerializeField] private Text timerText;

		private float _secondsRemaining = 0;
		public float SecondsRemaining
		{
			get => _secondsRemaining;
			private set
			{
				timerText.text = $"{value.ToString("0.00")} с";
				_secondsRemaining = value;
			}
		}

		private bool _isStarted = false;
		public bool IsStarted
		{
			get => _isStarted;

			private set
			{
				if (value && !_isStarted)
					StartCoroutine(Step());

				else if (!value && _isStarted)
					StopCoroutine(Step());
			}
		}

		public void StartTimer(int seconds)
		{
			SecondsRemaining = seconds;
			IsStarted = true;
		}

		public void StopTimer()
		{
			IsStarted = false;
		}

		public void ResetTimer()
		{
			SecondsRemaining = 0;
			IsStarted = false;
		}

		public void AddTime(int seconds)
		{
			SecondsRemaining += seconds;
		}

		private IEnumerator Step()
		{
			while (SecondsRemaining >= 0)
			{
				SecondsRemaining -= Time.deltaTime;
				yield return null;
			}

			SecondsRemaining = 0;
			IsStarted = false;

			EventBus.Broadcast(GameEvents.TIME_EXPIRED);
		}
	}
}