using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{
    public int layerToCapture; // Укажите номер слоя, который нужно захватить
    private string fileNamePrefix = Application.dataPath; // Префикс имени файла

    public string CaptureLayerScreenshot()
    {
        // Находим камеру, которая рендерит нужный слой (можно настроить более гибко)
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Main camera not found!");
            return "Error";
        }

        // Создаём RenderTexture того же разрешения, что и камера
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        camera.targetTexture = rt;

        // Включаем только нужный слой
        camera.cullingMask = 1 << layerToCapture;

        // Рендерим сцену в RenderTexture
        camera.Render();

        // Создаём текстуру из RenderTexture
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // Выключаем cullingMask, чтобы вернуть камеру в обычный режим
        camera.cullingMask = -1;
        camera.targetTexture = null;

        // Преобразуем Texture2D в байты
        byte[] bytes = screenShot.EncodeToPNG();

        // Сохраняем скриншот
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string filePath = fileNamePrefix + "/Screens/" + timestamp + ".png";
        System.IO.File.WriteAllBytes(filePath, bytes);

        // Освобождаем ресурсы
        Destroy(rt);
        Destroy(screenShot);

        return filePath;

        //Debug.Log("Скриншот слоя " + layerToCapture + " сохранён в " + filePath);
    }
}
