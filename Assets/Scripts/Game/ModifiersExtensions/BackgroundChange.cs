using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Static.Events;

public class BackgroundChange : MonoBehaviour
{
	private Camera _camera;
	private Color _originalColor;

	private void Start()
    {
        _camera = GetComponent<Camera>();
		_originalColor = _camera.backgroundColor;

		EventBus.Subscribe<Color>(GameEvents.BACKGROUND_CHANGED, ChangeBackground);
		EventBus.Subscribe(GameEvents.BACKGROUND_RESET, ResetBackground);
    }

	private void OnDestroy()
	{
		EventBus.Unsubscribe<Color>(GameEvents.BACKGROUND_CHANGED, ChangeBackground);
		EventBus.Unsubscribe(GameEvents.BACKGROUND_RESET, ResetBackground);

		ResetBackground();
	}

	private void ChangeBackground(Color color)
	{
		_camera.backgroundColor = color;
	}

	private void ResetBackground()
	{
		_camera.backgroundColor = _originalColor;
	}
}
