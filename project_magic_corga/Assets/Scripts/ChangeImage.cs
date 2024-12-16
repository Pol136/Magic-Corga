using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public GameObject button;

    public Sprite img1, img2;

    public void ChangeImg()
    {
        Image buttonImage = button.GetComponent<Image>();

        if (buttonImage != null)
        {
            if (buttonImage.sprite == img1)
            {
                buttonImage.sprite = img2;
            }
            else
            {
                buttonImage.sprite = img1;
            }
        }
    }
}

