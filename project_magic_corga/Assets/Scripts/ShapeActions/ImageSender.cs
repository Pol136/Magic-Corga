using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

public class ImageSender
{
    private static readonly HttpClient client = new HttpClient();

    public static string SendImageAndGetDigit(string imagePath)
    {
        try
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found.", imagePath);
            }

            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    var imageContent = new StreamContent(fileStream);
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    content.Add(imageContent, "file", Path.GetFileName(imagePath));

                    var response = client.PostAsync("http://localhost:8000/detect", content).Result;
                    response.EnsureSuccessStatusCode();

                    var responseString = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        using (JsonDocument document = JsonDocument.Parse(responseString))
                        {
                            JsonElement root = document.RootElement;
                            return root.GetProperty("digit").GetInt32().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error parsing JSON response: " + ex.Message);
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error sending image: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message);
        }
    }

    public static string SendImageAndGetDigitPublic(string imagePath)
    {
        try
        {
            return SendImageAndGetDigit(imagePath);
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}


