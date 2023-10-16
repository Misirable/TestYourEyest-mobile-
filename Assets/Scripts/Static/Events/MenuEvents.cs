using System.Collections;
using UnityEngine;

namespace Static.Events
{
	public class MenuEvents : MonoBehaviour
	{
		public const string START_LEVEL_SELECT = "MenuEvents.START_LEVEL_SELECT";
		public const string UNDO_LEVEL_SELECT = "MenuEvents.UNDO_LEVEL_SELECT";

		public const string SWITCH_NEXT_LEVEL = "MenuEvents.SWITCH_NEXT_LEVEL";
		public const string SWITCH_PREV_LEVEL = "MenuEvents.SWITCH_PREV_LEVEL";
	}
}