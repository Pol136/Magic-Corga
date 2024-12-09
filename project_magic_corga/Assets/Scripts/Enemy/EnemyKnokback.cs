using UnityEngine;

public class EnemyKnokback : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Transform playerTransform, float knockbackForce)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackForce * direction, ForceMode2D.Impulse);
    }
}
