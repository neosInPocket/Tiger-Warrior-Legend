using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{	
	[SerializeField] private GameObject effect;
	[SerializeField] private SpriteRenderer spriteRenderer;
	public Action<bool> TakeDamageEvent;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public bool isDead;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<EnemyController>(out EnemyController enemyController))
		{
			enemyController.PlayDeath();
			return;
		}
		
		if (collider.TryGetComponent<CoinController>(out CoinController coinController))
		{
			GameManager._points += 2;
			coinController.PlayDeath();
			TakeDamageEvent?.Invoke(true);
		}
	}
	
	public void InvokeTakeDamageEvent(bool value)
	{
		TakeDamageEvent?.Invoke(value);
	}
	
	public void PlayDeath(bool isWon)
	{
		if (isWon)
		{
			StartCoroutine(PlayEffect());
			return;
		}
		
		TakeDamageEvent?.Invoke(false);
		if (GameManager.lives != 0)
		{
			StopCoroutine(TakeDamage());
			StartCoroutine(TakeDamage());
			return;
		}
		
		if (GameManager.lives == 0)
		{
			StartCoroutine(PlayEffect());
		}
	}
	
	private IEnumerator PlayEffect()
	{
		isDead = true;
		spriteRenderer.color = new Color(0, 0, 0, 0);
		var deathEffect = Instantiate(effect, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		Destroy(deathEffect);
	}
	
	private IEnumerator TakeDamage()
	{
		for (int i = 0; i < 9; i++)
		{
			spriteRenderer.color = spriteRenderer.color = new Color(1f, 1f, 1f, 0);
			yield return new WaitForSeconds(0.1f);
			spriteRenderer.color = spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
