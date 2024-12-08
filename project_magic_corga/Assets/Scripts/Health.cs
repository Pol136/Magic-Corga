using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;

    public int health;
    public int maxHealth;

    public Image[] hearts;
    public Sprite fullHeart, emptyHeart;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        CheckHearts();
    }

    public void CheckHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void TakeHeal()
    {
        if (health < maxHealth) 
        {
            health += 1;
        }
        CheckHearts();
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Debug.Log("You dead");
        }
        CheckHearts();
    }
}
