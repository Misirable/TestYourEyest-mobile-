using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class ColorLabel : MonoBehaviour
	{
		[SerializeField] private TextMeshPro text;

		private string _text = "";
		public string Text
		{
			get => _text;
			set
			{
				_text = text.text = value;
			}
		}
		private GuessedColor _color;
		public GuessedColor Color
		{
			get => _color;
			private set
			{
				_color = value;
				text.color = value.Color;
			}
		}

		public void Init(ColorLabelParameters parameters)
		{
			Text = parameters.Text;
			Color = parameters.ColorPaint;
		}
	}
}
