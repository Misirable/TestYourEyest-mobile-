using System;
using System.Collections;
using UnityEngine;

namespace Game
{
	using Modification.Renderers;

	public class GameParameters
	{
		public IGameRenderer GameRenderer { get; set; }

		public int StartRemainingSeconds { get; set; }
		public int TimeBoost { get; set; }

		public int LevelCompleteScore { get; set; }

		private float _wrongProbability = 0;
		public float WrongProbability
		{
			get => _wrongProbability;
			set
			{
				if (value < 0 || value > 1)
					throw new ArgumentException("Probability must be between 0 and 1 inclusive");

				_wrongProbability = value;
			}
		}
	}
}
