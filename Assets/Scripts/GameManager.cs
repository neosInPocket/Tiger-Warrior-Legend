using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[SerializeField] private TutorialScreen _tutor;
	[SerializeField] private UIHealthManager _uiHealth; 
	[SerializeField] private FadeScreen _fadeScreen;
	[SerializeField] private GameScreen _gameScreen;
	[SerializeField] private WinScreen _countDownScreen; 
	[SerializeField] private WinScreen _defeatScreen; 
	[SerializeField] private WinScreenWithCoins _winScreen; 
	[SerializeField] private ProgressBar _levelProgress;
	[SerializeField] private Transform coinContainer;
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private PlayerManager player;
	[SerializeField] private EnemySpawner enemySpawner;
	[SerializeField] private ChainController chainController;
	private float _playDelay;
	public static int _levelCoins;
	private static int _levelMaxPoints;
	public static int _points;
	public static bool _isPlaying = false;
	private bool isTutor = false;
	public static int lives;
	public static bool isWon;
	private void Awake()
	{
		_isPlaying = false;
		Initialize();
		player.TakeDamageEvent += OnEventHandler;
	}
	
	public void Initialize()
	{
		chainController.Initialize();
		_isPlaying = false;
		isWon = false;
		player.isDead = false;
		player.SpriteRenderer.color = new Color(1, 1, 1, 1);
		
		lives = MainMenuManager.CurrentLivesUpgrade;
		_levelMaxPoints = (int)(Mathf.Log(MainMenuManager.CurrentLevel + 2) * 5);
		_levelCoins = (int)(Mathf.Log(MainMenuManager.CurrentLevel + 2) * 10) + 50;
		_gameScreen.gameObject.SetActive(true);
		_gameScreen.Refresh();
		_levelProgress.Refresh(0);
		_points = 0;
		_playDelay = (int)_countDownScreen.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
		_uiHealth.RefreshLifes(MainMenuManager.CurrentLivesUpgrade);
		isTutor = false;
		
		if (MainMenuManager.IsFirstTime == "yes")
		{
			MainMenuManager.IsFirstTime = "no";
			SaveLoadSystem.Save();
			isTutor = true;
			_tutor.TutorialEnd += OnTutorialEnd;
			_tutor.PlayTutor();
		}
		else
		{
			_countDownScreen.gameObject.SetActive(true);
			_countDownScreen.Show();
			StartCoroutine(PlayDelay());
		}
	}
	
	private void OnTutorialEnd()
	{
		_countDownScreen.gameObject.SetActive(true);
		_countDownScreen.Show();
		StartCoroutine(PlayDelay());
	}
	
	private void OnEventHandler(bool value)
	{
		if (!_isPlaying) return;
		
		if (!value)
		{
			_fadeScreen.ProcessTakeDamage();
			lives--;
			_uiHealth.RefreshLifes(lives);
		}
		
		_levelProgress.Refresh((float)_points / (float)_levelMaxPoints);
		
		if (_points >= _levelMaxPoints)
		{
			_isPlaying = false;
			isWon = true;
			MainMenuManager.CurrentLevel++;
			MainMenuManager.Coins += _levelCoins;
			SaveLoadSystem.Save();
			_winScreen.gameObject.SetActive(true);
			_winScreen.Show(_levelCoins);
			enemySpawner.Disable();
			DeleteObjects();
			return;
		}
		
		if (lives <= 0)
		{
			_isPlaying = false;
			_points = 0;
			_defeatScreen.gameObject.SetActive(true);
			_defeatScreen.Show();
			enemySpawner.Disable();
			DeleteObjects();
			return;
		}
	}
	
	public void ReturnToTheMainMenu()
	{
		
		_fadeScreen.Fade();
		_fadeScreen.OnFadeEnd += OnFadeMainMenuEnd;
	}
	
	private void OnFadeMainMenuEnd()
	{
		_fadeScreen.OnFadeEnd -= OnFadeMainMenuEnd;
		_gameScreen.gameObject.SetActive(false);
		SceneManager.LoadScene("MainMenuScene");
	}
	
	private IEnumerator PlayDelay()
	{
		yield return new WaitForSeconds(_playDelay + 0.3f);
		_countDownScreen.gameObject.SetActive(false);
		_isPlaying = true;
		enemySpawner.Enable();
	}
	
	public void UpdateUI()
	{
		var progress = _points / _levelMaxPoints;
		_levelProgress.Refresh(progress);
	}
	
	public void DeleteObjects()
	{
		DeleteCoins();
		DeleteEnemies();
	}
	
	public void DeleteCoins()
	{
		foreach (Transform coin in coinContainer)
		{
			coin.GetComponent<CoinController>().PlayDeath();
		}
	}
	
	public void DeleteEnemies()
	{
		foreach (Transform enemy in enemyContainer)
		{
			enemy.GetComponent<EnemyController>().PlayDeath();
		}
	}
	
	private void OnDestroy()
	{
		player.TakeDamageEvent -= OnEventHandler;
	}
}
