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
		var rndSize = Random.Range(sizeRange.x, sizeRange.y);
		var rndSpeed = Random.Range(speedRange.x, speedRange.y);
		
		spriteRenderer.size = new Vector2(rndSize, rndSize);
		circleCollider.radius = rndSize / 2;
		
		var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		var rndX = Random.Range(-screenBounds.x + spriteRenderer.bounds.size.x, screenBounds.x - spriteRenderer.bounds.size.x);
		transform.position = new Vector2(rndX, screenBounds.y);
		
		rb.velocity = Vector2.down * rndSpeed;
	}
}
