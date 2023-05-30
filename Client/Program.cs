using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Client
{
    //Код приложения его вызывающий
    public class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                string filePath = Console.ReadLine();
                string Url = "https://localhost:44355/sorting/post/wordcount";
                using var response = await client.PostAsJsonAsync(Url, filePath);

                    if (response.IsSuccessStatusCode)
                    {
                        Dictionary<string, int> wordCounts = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();

                        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Output.txt");

                        using (StreamWriter writer = new StreamWriter(outputFilePath))
                        {
                            foreach (KeyValuePair<string, int> v in wordCounts.OrderByDescending(x => x.Value))
                            {
                                writer.WriteLine($"{v.Key} - {v.Value}");
                            }
                        }
                        Console.WriteLine($"Публичный метод сохранен в файл {outputFilePath}");
                    }
                    else
                    {
                        Console.WriteLine("Файл не содержит слов");
                    }
                    Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}