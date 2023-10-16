using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ChainController : MonoBehaviour
{
	[SerializeField] private PlayerManager player;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] private Transform pointerHolder;
	[SerializeField] private Transform pointer;
	[SerializeField] private float chainSpeed;
	[SerializeField] private float hookSpeed;
	private bool isShooted;
	private bool isDecreasing;
	private bool isHooked;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		Touch.onFingerDown += OnFingerDown;
		Touch.onFingerMove += OnFingerMove;
		Touch.onFingerUp += OnFingerUp;
		pointer.gameObject.SetActive(false);
		pointerHolder.transform.position = player.transform.position;
		isShooted = false;
		isDecreasing = false;
	}
	
	private void Update()
	{
		if (!isShooted) return;
		
		if (spriteRenderer.size.x <= 1 && !isDecreasing)
		{
			spriteRenderer.size = new Vector2(spriteRenderer.size.x + chainSpeed * Time.deltaTime, spriteRenderer.size.y);
			boxCollider.size = new Vector2(boxCollider.size.x + chainSpeed * Time.deltaTime, boxCollider.size.y);
			boxCollider.offset = new Vector2(boxCollider.offset.x + chainSpeed * Time.deltaTime / 2, boxCollider.offset.y);
			return;
		}
		
		isDecreasing = true;
		
		if (spriteRenderer.size.x >= 0)
		{
			spriteRenderer.size = new Vector2(spriteRenderer.size.x - chainSpeed * Time.deltaTime, spriteRenderer.size.y);
			boxCollider.size = new Vector2(boxCollider.size.x - chainSpeed * Time.deltaTime, boxCollider.size.y);
			boxCollider.offset = new Vector2(boxCollider.offset.x - chainSpeed * Time.deltaTime / 2, boxCollider.offset.y);
		}
		else
		{
			isDecreasing = false;
			isShooted = false;
			isHooked = false;
		}
		
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<EnemyController>(out EnemyController enemy))
		{
			if (isHooked) return;
			
			isHooked = true;
			isDecreasing = true;
			RotateTowards(enemy.transform.position);
			enemy.transform.right = player.transform.position - enemy.transform.position;
			enemy.Rb.velocity = hookSpeed * enemy.transform.right;
		}
	}
	
	private void OnFingerDown(Finger finger)
	{
		if (isShooted) return;
		
		pointer.gameObject.SetActive(true);
		RotatePointer(finger.screenPosition);
	}
	
	private void RotateTowards(Vector3 point)
	{
		transform.right = point - player.transform.position;
	}
	
	private void OnFingerMove(Finger finger)
	{
		if (isShooted) return;
		
		RotatePointer(finger.screenPosition);
	}
	
	private void OnFingerUp(Finger finger)
	{
		if (isShooted) return;
		
		pointer.gameObject.SetActive(false);
		isShooted = true;
	}
	
	private void RotatePointer(Vector3 point)
	{
		Vector2 lookDirection = Camera.main.ScreenToWorldPoint(point) - pointerHolder.transform.position;
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
		
		pointerHolder.transform.eulerAngles = new Vector3(0, 0, angle);
		transform.eulerAngles = new Vector3(0, 0, angle + 90f);
	}
	
	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
		Touch.onFingerMove -= OnFingerMove;
		Touch.onFingerUp -= OnFingerUp;
		EnhancedTouchSupport.Disable();
	}
}
