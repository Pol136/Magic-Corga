using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private WinOrLoose wol;

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
        this.health -= 1;
        CheckHearts();
        if (this.health <= 0)
        {
            wol.LooseGame();
        }
    }
}
