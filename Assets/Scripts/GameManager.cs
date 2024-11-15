using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject[] skins;

	private int skinIndex = 0;
	public OnGameEnd OnGameEnd;

	public static GameManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}
	public GameObject GetNextSkin()
	{
		skinIndex = (skinIndex + 1) % skins.Length;
		return skins[skinIndex];
	}
}
	public delegate void OnGameEnd(bool _won);