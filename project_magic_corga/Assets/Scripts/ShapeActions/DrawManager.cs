using UnityEngine;
using System.Collections;

public class DrawManager : MonoBehaviour
{
    public Camera _cam;
    public Screenshot screen;
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;
    public const float RESOLUTION = 0.01f;
    public float fadeTime = 1.2f; // Время выцветания в секундах

    ImageSender imageSender = new ImageSender();

    void Start()
    {
        //_cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }
        else if (Input.GetMouseButton(0) && _currentLine != null)
        {
            _currentLine.SetPosition(mousePos);
        }
        else if (Input.GetMouseButtonUp(0) && _currentLine != null)
        {
            //AudioManager.Instance.PlaySFX("YouDrow");
            string filePath = screen.CaptureLayerScreenshot();
            Debug.Log(filePath);
            string digit = ImageSender.SendImageAndGetDigitPublic(filePath);
            Debug.Log(digit);
            StartCoroutine(FadeOutLine(_currentLine));
            _currentLine = null;
        }
    }

    IEnumerator FadeOutLine(Line line)
    {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("Line object doesn't have a LineRenderer!");
            yield break;
        }

        Color startColor = lineRenderer.startColor;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeTime;
            Color fadedColor = Color.Lerp(startColor, Color.clear, t);
            lineRenderer.startColor = fadedColor;
            lineRenderer.endColor = fadedColor;
            yield return null;
        }

        Destroy(line.gameObject);
    }
}

