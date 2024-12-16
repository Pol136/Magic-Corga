using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float returnForce;
    [SerializeField] private float stunDuration = 1f;

    private Rigidbody2D rb;
    private bool isStunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void SetSpeed(float outspeed)
    {
        this.speed = outspeed;
    }

    void Update()
    {
        if (!isStunned)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Health>().TakeDamage();
            Vector2 direction = (transform.position - player.transform.position).normalized; // Направление от игрока к врагу
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(ReturnToPlayerCoroutine());
        }
    }

    IEnumerator ReturnToPlayerCoroutine()
    {
        yield return new WaitForSeconds(1f); // Небольшая задержка
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.AddForce(directionToPlayer * returnForce, ForceMode2D.Impulse);
    }

    public void Stun()
    {
        isStunned = true;
        StartCoroutine(StunCoroutine());
    }

    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
