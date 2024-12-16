using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string health;
    [SerializeField] private TextMeshProUGUI textHealth;

    private void Start()
    {
        textHealth.text = health;
    }

    public void SetHealth(string outhealth)
    {
        this.health = outhealth;
    }

    public void CheckUpdateHealth(string digit)
    {
        if (health[0] == char.Parse(digit))
        {
            health = health.Remove(0, 1);
            //Debug.Log(health);
            textHealth.text = health;
            gameObject.GetComponent<Movement>().Stun();
        }

        if (health.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
