using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI
{
	public class ColorsCounter : MonoBehaviour
	{
		[SerializeField] private Text text;

		public int Counter { get; private set; }

		public void SetValue(int value)
		{
			Counter = value;
			text.gameObject.SetActive(value > 0);

			text.text = value.ToString();
		}
	}
}
