using UnityEngine;

public class JumpItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.ActivateJumpBoost();

            Destroy(gameObject);
        }
    }
}