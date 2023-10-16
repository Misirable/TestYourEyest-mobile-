using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Static.Events;

namespace Game.GUI
{
	public class ColorSelectCanvas : MonoBehaviour
	{
		[SerializeField] private Button yesButton;
		[SerializeField] private Button noButton;

		[SerializeField] private Text yesButtonText;
		[SerializeField] private Text noButtonText;

		[SerializeField] private GameObject leftContainer;
		[SerializeField] private GameObject rightContainer;

		private void Start()
		{
			EventBus.Subscribe(GameEvents.INVERT_CONTROLS, SwapControls);
			EventBus.Subscribe(GameEvents.RESET_CONTROLS, SwapControls);
		}

		private void OnDestroy()
		{
			EventBus.Unsubscribe(GameEvents.INVERT_CONTROLS, SwapControls);
			EventBus.Unsubscribe(GameEvents.RESET_CONTROLS, SwapControls);
		}

		private void SwapText(Text txt1, Text txt2)
		{
			string tmp = txt1.text;

			txt1.text = txt2.text;
			txt2.text = tmp;
		}

		private void SwapControls()
		{
			SwapText(yesButtonText, noButtonText);

			Button.ButtonClickedEvent tmp = yesButton.onClick;
			yesButton.onClick = noButton.onClick;
			noButton.onClick = tmp;
		}
	}
}
