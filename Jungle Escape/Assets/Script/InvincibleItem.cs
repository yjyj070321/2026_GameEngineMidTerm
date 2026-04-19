using UnityEngine;

public class InvincibleItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.ActivateInvincibility();

            Destroy(gameObject);
        }
    }
}