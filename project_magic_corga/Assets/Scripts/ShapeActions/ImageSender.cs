using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class ImageSender : MonoBehaviour
{
    private string serverUrl = "http://localhost:8000/detect"; // Замените на ваш URL

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
                Debug.Log("Изображение успешно отправлено!");
                string responseText = www.downloadHandler.text;
                Debug.Log("Ответ сервера: " + responseText);
                try
                {
                    // Парсинг JSON ответа
                    //var responseJson = SimpleJSON.JSON.Parse(responseText);
                    //int digit = responseJson["digit"].AsInt;
                    Debug.Log("Распознанная цифра: " + digit);
                    //Обработка результата
                }
                catch (Exception ex)
                {
                    Debug.LogError("Ошибка парсинга JSON ответа: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Ошибка отправки изображения: " + www.error);
            }
        }
    }
}
