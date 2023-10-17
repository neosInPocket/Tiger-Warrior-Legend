using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
	public static void Load()
	{
		MainMenuManager.Coins = PlayerPrefs.GetInt("coins", 0);
		MainMenuManager.CurrentLevel = PlayerPrefs.GetInt("currentLevel", 0);
		MainMenuManager.CurrentChainSpeedUpgrade = PlayerPrefs.GetInt("CurrentChainSpeedUpgrade", 0);
		MainMenuManager.CurrentLivesUpgrade = PlayerPrefs.GetInt("CurrentLivesUpgrade", 1);
		MainMenuManager.IsFirstTime = PlayerPrefs.GetString("isFirstTime", "yes");
	}
	
	public static void Save()
	{
		PlayerPrefs.SetInt("coins", MainMenuManager.Coins);
		PlayerPrefs.SetInt("currentLevel", MainMenuManager.CurrentLevel);
		PlayerPrefs.SetInt("CurrentChainSpeedUpgrade", MainMenuManager.CurrentChainSpeedUpgrade);
		PlayerPrefs.SetInt("CurrentLivesUpgrade", MainMenuManager.CurrentLivesUpgrade);
		PlayerPrefs.SetString("isFirstTime", MainMenuManager.IsFirstTime);
		PlayerPrefs.Save();
	}
}
