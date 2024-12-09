using UnityEngine;
using UnityEngine.UI;

public class WinOrLoose : MonoBehaviour
{

    [SerializeField] private GameObject loosePanle, winPanel;

    private int colStars;
    public Image[] stars;
    public Sprite fullStar, emptyStar;

    public void LooseGame()
    {
        loosePanle.SetActive(true);
    }

    public void WinGame(int health)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = fullStar;
        }

        if (health == 5)
        {
            colStars = 3;
        }
        else if (health >= 3)
        {
            colStars = 2;
            stars[2].sprite = emptyStar;
        }
        else
        {
            colStars = 1;
            stars[2].sprite = emptyStar;
            stars[1].sprite = emptyStar;
        }

        winPanel.SetActive(true);
    }
}
