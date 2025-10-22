using UnityEngine;

public class StarScript : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Manager.Instance.Stars += 1;

            Destroy(gameObject);
        }
    }
}
