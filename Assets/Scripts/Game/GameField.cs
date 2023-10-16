using System.Collections;
using UnityEngine;

namespace Game
{
	public class GameField : MonoBehaviour
	{
		[SerializeField] private ColorLabel colorLabelPrefab;

		private ColorLabel _currentLabel;

		public ColorLabel PutLabel(ColorLabelParameters parameters)
		{
			_currentLabel = Instantiate(colorLabelPrefab);
			_currentLabel.Init(parameters);

			return _currentLabel;
		}

		public void RemoveLabel()
		{
			Destroy(_currentLabel.gameObject);
			_currentLabel = null;
		}
	}
}