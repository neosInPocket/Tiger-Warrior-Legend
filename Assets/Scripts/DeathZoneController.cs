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
			
		}
	}
}
