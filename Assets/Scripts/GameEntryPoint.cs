using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
	[SerializeField] private MainMenuManager _mainMenuController;
 	void Start()
	{
		_mainMenuController.Initialize();
	}
}
