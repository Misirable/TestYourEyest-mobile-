using System;
using UnityEditor;
using UnityEngine;

namespace Progress
{
	public class ProgressSerializer
	{
		private const string _levelKeyName = "level";

		public void Save(PlayerProgress progress)
		{
			//BinaryFormatter formatter = new BinaryFormatter();

			//using (StreamWriter writer = new StreamWriter("progress"))
			//{
			//	formatter.Serialize(writer.BaseStream, progress);
			//}		

			PlayerPrefs.SetInt(_levelKeyName, progress.LevelsUnlocked);
		}

		public PlayerProgress Load()
		{
			//BinaryFormatter formatter = new BinaryFormatter();
			//object progress;

			//using (StreamReader reader = new StreamReader("progress"))
			//{
			//	progress = formatter.Deserialize(reader.BaseStream);
			//}

			//return progress as PlayerProgress;

			int levelsUnlocked;

			try
			{
				levelsUnlocked = PlayerPrefs.GetInt(_levelKeyName, 1);
			}
			catch
			{
				levelsUnlocked = 1;
			}

			return new PlayerProgress(levelsUnlocked);
		}
	}
}
