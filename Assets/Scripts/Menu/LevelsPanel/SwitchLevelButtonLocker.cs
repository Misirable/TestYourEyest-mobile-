using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
	public class SwitchLevelButtonLocker : LockableButton
	{
		private Image _image;

		protected override void Awake()
		{
			base.Awake();
			_image = GetComponent<Image>();
		}

		public override void Lock()
		{
			base.Lock();
			_image.enabled = false;
		}

		public override void Unlock()
		{
			base.Unlock();
			_image.enabled = true;
		}
	}
}