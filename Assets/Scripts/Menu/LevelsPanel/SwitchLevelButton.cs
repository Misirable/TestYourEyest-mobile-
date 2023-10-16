using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Static.Events;

namespace Menu
{
	public class SwitchLevelButton : MonoBehaviour
	{
		public SwitchLevelOptions switchWay;

		private LockableButton _lockButton;

		public bool IsLocked => _lockButton.IsLocked;

		private void Awake()
		{
			_lockButton = GetComponent<LockableButton>();
		}

		public void Switch()
		{
			if (switchWay == SwitchLevelOptions.Prev)
				EventBus.Broadcast(MenuEvents.SWITCH_PREV_LEVEL);
			else
				EventBus.Broadcast(MenuEvents.SWITCH_NEXT_LEVEL);
		}

		public void Lock()
		{
			_lockButton.Lock();
		}

		public void Unlock()
		{
			_lockButton.Unlock();
		}
	}
}
