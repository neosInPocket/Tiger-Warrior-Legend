using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DeathZoneController : MonoBehaviour
{
	[SerializeField] private PlayerManager player;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<EnemyController>(out EnemyController enemy))
		{
			if (enemy.isDead) return;
			
			player.InvokeTakeDamageEvent(false);
			enemy.PlayDeath();
			return;
		}
		
		if (collider.TryGetComponent<CoinController>(out CoinController coin))
		{
			if (coin.isDead) return;
			
			coin.PlayDeath();
		}
	}
}
