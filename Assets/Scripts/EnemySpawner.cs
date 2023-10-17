using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private EnemyController enemyPrefab;
	[SerializeField] private CoinController coinPrefab;
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private Transform coinContainer;
	[SerializeField] private float spawnDelay;
	private bool isSpawning;
	private bool isDelay;
	public float SpawnDelay
	{
		get => spawnDelay;
		set => spawnDelay = value;
	}
	
	private void Update()
	{
		if (!isSpawning) return;
		
		StartCoroutine(Spawn());
	}
	
	public void Enable()
	{
		GameManager._isPlaying = true;
		isSpawning = true;
	}
	
	public void Disable()
	{
		isSpawning = false;
	}
	
	public IEnumerator Spawn()
	{
		if (isDelay) yield break;
		
		isDelay = true;
		var rnd = Random.Range(0, 2);
		
		if (rnd == 1)
		{
			Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity, enemyContainer);
		}
		else
		{
			Instantiate(coinPrefab, Vector2.zero, Quaternion.identity, coinContainer);
		}
		
		yield return new WaitForSeconds(spawnDelay);
		isDelay = false;
	}
}
