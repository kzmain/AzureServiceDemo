using System.Text;
using Newtonsoft.Json;

namespace AzureClassLibrary.Cognitive;

public class Translator
{
    public Translator()
    {
    }

    public int Demo()
    {
        string key = Environment.GetEnvironmentVariable("COGNITIVE_TRANSLATOR_KEY") ?? "";
        string endpoint = Environment.GetEnvironmentVariable("COGNITIVE_TRANSLATOR_ENDPOINT") ?? ""; 
        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        string location = Environment.GetEnvironmentVariable("COGNITIVE_TRANSLATOR_LOCATION") ?? "";
        
        string route = "/translate?api-version=3.0&from=en&to=zh-Hans";
        string textToTranslate = "Hello, world!";
        object[] body = new object[] { new { Text = textToTranslate } };
        var requestBody = JsonConvert.SerializeObject(body);
    
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);
    
            // Send the request and get response.
            HttpResponseMessage response = client.Send(request);
            // Read response as a string.
            string result =  response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }

        return 0;
    }
}