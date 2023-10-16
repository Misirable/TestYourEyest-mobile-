using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game;
using Progress;
using UnityEditor;
using System.Diagnostics;

public class ProgressContoller : MonoBehaviour
{
	private readonly PlayerProgress DefaultProgress = new PlayerProgress(1);

	private PlayerProgress _progress;
	public PlayerProgress Progress
	{
		get => _progress ?? DefaultProgress;
		set => _progress = value;
	}

	public bool TryUnlockNextLevel(GameResult gameResult)
	{
		if (gameResult.GameStatus == GameResultStatuses.Win &&
			Progress.LevelsUnlocked == gameResult.LevelNumber &&
			gameResult.LevelNumber != PlayerProgress.TOTAL_LEVELS)
		{
			_progress = new PlayerProgress(_progress.LevelsUnlocked + 1);
			return true;
		}

		return false;
	}

	public void Save()
	{
		ProgressSerializer serializer = new ProgressSerializer();
		serializer.Save(Progress);
	}

	public void Load()
	{
		ProgressSerializer serializer = new ProgressSerializer();

		try
		{
			_progress = serializer.Load();
		}
		catch
		{
			_progress = DefaultProgress;
		}
	}
}
