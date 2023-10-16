using System.Collections;
using UnityEngine;

namespace Game
{
	public class GuessedColor
	{
		public string Title { get; }
		public Color Color { get; }

		public GuessedColor(string title, Color color)
		{
			Title = title;
			Color = color;
		}
	}
}