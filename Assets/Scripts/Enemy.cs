using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            GameManager.Instance.OnGameEnd?.Invoke(false);
        }
    }
}
