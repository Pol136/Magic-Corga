using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{
    public int layerToCapture; // ������� ����� ����, ������� ����� ���������
    private string fileNamePrefix = Application.dataPath; // ������� ����� �����

    public string CaptureLayerScreenshot()
    {
        // ������� ������, ������� �������� ������ ���� (����� ��������� ����� �����)
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Main camera not found!");
            return "Error";
        }

        // ������ RenderTexture ���� �� ����������, ��� � ������
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        camera.targetTexture = rt;

        // �������� ������ ������ ����
        camera.cullingMask = 1 << layerToCapture;

        // �������� ����� � RenderTexture
        camera.Render();

        // ������ �������� �� RenderTexture
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // ��������� cullingMask, ����� ������� ������ � ������� �����
        camera.cullingMask = -1;
        camera.targetTexture = null;

        // ����������� Texture2D � �����
        byte[] bytes = screenShot.EncodeToPNG();

        // ��������� ��������
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string filePath = fileNamePrefix + "/Screens/" + timestamp + ".png";
        System.IO.File.WriteAllBytes(filePath, bytes);

        // ����������� �������
        Destroy(rt);
        Destroy(screenShot);

        return filePath;

        //Debug.Log("�������� ���� " + layerToCapture + " ������� � " + filePath);
    }
}
