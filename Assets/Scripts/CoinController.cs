using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Vector2 speedRange;
	public Rigidbody2D Rb => rb;
	
	private void Start()
	{
		var rndSpeed = Random.Range(speedRange.x, speedRange.y);
		
		var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		var rndX = Random.Range(-screenBounds.x + spriteRenderer.bounds.size.x, screenBounds.x - spriteRenderer.bounds.size.x);
		transform.position = new Vector2(rndX, screenBounds.y);
		
		rb.velocity = Vector2.down * rndSpeed;
	}
}
