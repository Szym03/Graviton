using UnityEngine;

public class Planet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Manager.Instance.OnPlayerCrashed();
    }
}
}
