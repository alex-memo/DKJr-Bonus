using UnityEngine;

public class Key : MonoBehaviour
{
	private void OnTriggerEnter(Collider _coll)
	{
		if (_coll.CompareTag("Player"))
		{
			GameManager.Instance.OnGameEnd?.Invoke(true);
		}
	}
}
