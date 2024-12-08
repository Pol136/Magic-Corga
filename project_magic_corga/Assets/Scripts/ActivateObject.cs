using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;

    public void ActivateObj()
    {
        obj.SetActive(true);
    }

    public void DeactivateObj()
    {
        obj.SetActive(false);
    }
}
