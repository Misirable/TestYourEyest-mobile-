using System;
using System.Collections;
using UnityEngine;

namespace Static.Events
{
	public static class AppEvents
	{
		public const string QUIT = "AppEvents.QUIT";

		public const string END_GAME = "AppEvents.END_GAME";
		public const string START_GAME = "AppEvents.START_GAME";

		public const string TO_MAIN_MENU = "AppEvents.TO_MAIN_MENU";
		public const string PROGRESS_UPDATED = "AppEvents.PROGRESS_UPDATED";
	}
}