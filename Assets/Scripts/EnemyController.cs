using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Vector2 speedRange;
	[SerializeField] private Vector2 sizeRange;
	public Rigidbody2D Rb => rb;
	
	private void Start()
	{
		PlayerManager player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>();
		var rndSize = Random.Range(sizeRange.x, sizeRange.y);
		var rndSpeed = Random.Range(speedRange.x, speedRange.y);
		
		spriteRenderer.size = new Vector2(rndSize, rndSize);
		circleCollider.radius = rndSize / 2;
		
		var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		var rndX1 = Random.Range(-screenBounds.x + spriteRenderer.bounds.size.x, -player.SpriteRenderer.bounds.size.x);
		var rndX2 = Random.Range(player.SpriteRenderer.bounds.size.x, screenBounds.x - spriteRenderer.bounds.size.x);
		var rnd = Random.Range(0, 2);
		
		if (rnd == 0)
		{
			transform.position = new Vector2(rndX1, screenBounds.y);
		}
		else
		{
			transform.position = new Vector2(rndX2, screenBounds.y);
		}
		
		rb.velocity = Vector2.down * rndSpeed;
	}
}
