using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
	[SerializeField] private Button chainSpeedButton;
	[SerializeField] private Button _livesButton;
	[SerializeField] private TMP_Text chainSpeedUpgradeAmount;
	[SerializeField] private TMP_Text _maxLivesUpgradeAmount;
	[SerializeField] private TMP_Text _coinsText;
	
	private void Start()
	{
		Refresh();
	}
	
	public void BuyLivesUpgrade()
	{
		var leftCoins = MainMenuManager.Coins - 100;
		if (leftCoins < 0)
		{
			return;
		}
		MainMenuManager.CurrentLivesUpgrade++;
		MainMenuManager.Coins -= 100;
		SaveLoadSystem.Save();
		Refresh();
	}
	
	public void BuyShainHookDistanceUpgrade()
	{
		var leftCoins = MainMenuManager.Coins - 50;
		if (leftCoins < 0)
		{
			return;
		}
		MainMenuManager.CurrentChainSpeedUpgrade++;
		MainMenuManager.Coins -= 50;
		SaveLoadSystem.Save();
		Refresh();
	}
	
	public void Refresh()
	{
		chainSpeedButton.interactable = true;
		_livesButton.interactable = true;
		_coinsText.text = MainMenuManager.Coins.ToString();
		chainSpeedUpgradeAmount.text = MainMenuManager.CurrentChainSpeedUpgrade.ToString() + "/3";
		_maxLivesUpgradeAmount.text = MainMenuManager.CurrentLivesUpgrade.ToString() + "/3";
		
		if (MainMenuManager.CurrentChainSpeedUpgrade == 3 || MainMenuManager.Coins - 50 < 0)
		{
			chainSpeedButton.interactable = false;
		}
		
		if (MainMenuManager.CurrentLivesUpgrade == 3 || MainMenuManager.Coins - 100 < 0)
		{
			_livesButton.interactable = false;
		}
	}
}
