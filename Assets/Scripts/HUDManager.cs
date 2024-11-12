using UnityEngine;

public class HUDManager : MonoBehaviour
{
	public static HUDManager Instance { get; private set; }
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject loseScreen;
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}
	public void Start()
	{
		GameManager.Instance.OnGameEnd += OnGameEnd;
	}
	private void OnGameEnd(bool _won)
	{
		switch (_won)
		{
			case true:
				winScreen.SetActive(true);
				break;
			case false:
				loseScreen.SetActive(true);
				break;
		}
	}
}
