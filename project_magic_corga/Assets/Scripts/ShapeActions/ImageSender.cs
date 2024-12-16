using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class ImageSender : MonoBehaviour
{
    private string serverUrl = "http://localhost:8000/detect"; // �������� �� ��� URL

    public void SendImage(Texture2D image)
    {
        StartCoroutine(UploadImage(image));
    }

    private IEnumerator UploadImage(Texture2D image)
    {
        byte[] imageBytes = image.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageBytes, "image.png");


        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("����������� ������� ����������!");
                string responseText = www.downloadHandler.text;
                Debug.Log("����� �������: " + responseText);
                try
                {
                    // ������� JSON ������
                    //var responseJson = SimpleJSON.JSON.Parse(responseText);
                    //int digit = responseJson["digit"].AsInt;
                    Debug.Log("������������ �����: " + digit);
                    //��������� ����������
                }
                catch (Exception ex)
                {
                    Debug.LogError("������ �������� JSON ������: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("������ �������� �����������: " + www.error);
            }
        }
    }
}
