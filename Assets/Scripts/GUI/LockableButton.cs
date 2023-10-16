using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockableButton : MonoBehaviour
{
	public bool IsLocked { get; private set; }

	private Button _button;

	protected virtual void Awake()
	{
		_button = GetComponent<Button>();
	}

	public virtual void Lock()
	{
		_button.enabled = false;
		IsLocked = true;
	}

	public virtual void Unlock()
	{
		_button.enabled = true;
		IsLocked = false;
	}
}
