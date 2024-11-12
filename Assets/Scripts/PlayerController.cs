using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private bool isGrounded => controller.isGrounded;
	[SerializeField] private LayerMask groundLayer;

	private CharacterController controller => GetComponent<CharacterController>();
	[SerializeField] private float speed = 9;
	[SerializeField] private float jumpHeight = 6;
	[SerializeField] private float gravity = -30;

	[SerializeField][Range(0, .5f)] private float moveSmoothTime = .3f;

	[SerializeField] private Transform groundCheck;

	private Vector2 currentDir;
	private Vector2 currentDirVelocity;
	private float velocityY;
	private bool canMove = true;
	private void Start()
	{
		GameManager.Instance.OnGameEnd += OnGameEnd;
	}
	private void OnGameEnd(bool _)
	{
		canMove = false;
	}
	private void Update()
	{
		if (!canMove) { return; }
		move();
		if (Input.GetKeyDown(KeyCode.S))
		{
			var _go = ModelSwapperPlugin.ModelSwapper.SwapModel(transform.GetChild(0).gameObject, GameManager.Instance.GetNextSkin());
			_go.transform.SetParent(transform);
			_go.transform.localScale = Vector3.one;
		}
	}
	private void move()
	{
		Vector2 _targetDir = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
		{
			y = 0
		};
		_targetDir.Normalize();

		currentDir = Vector2.SmoothDamp(currentDir, _targetDir, ref currentDirVelocity, moveSmoothTime);
		Vector3 _velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
		controller.Move(_velocity * Time.deltaTime);
		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			velocityY = jumpHeight;
		}
		if (!isGrounded)
		{
			velocityY += gravity * Time.deltaTime;
			velocityY = Mathf.Clamp(velocityY, -20, 20);
		}
		if (!isGrounded && controller.velocity.y < 1)
		{
			velocityY = -5;
		}
	}
}
