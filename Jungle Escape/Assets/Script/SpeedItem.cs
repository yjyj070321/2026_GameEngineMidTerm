using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.ActivateSpeedBoost();

            Destroy(gameObject);
        }
    }
}