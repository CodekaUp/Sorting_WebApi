using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Unicode;

namespace Client
{
    //Код приложения его вызывающий
    public class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = Console.ReadLine();

            string fileText = File.ReadAllText(filePath);
            string requestText = JsonConvert.SerializeObject(fileText);

            string url = "https://localhost:44355/sorting/post/wordcount";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "*/*";
            request.KeepAlive = true;

            string responseText = "";
            using (var mStream = new  MemoryStream())
            {
                var sw = new StreamWriter(mStream);
                sw.Write(requestText);

                byte[] byteArray = Encoding.UTF8.GetBytes(requestText);
                var arrayLen = byteArray.Length;
                request.ContentLength = byteArray.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var responseCode = (int)response.StatusCode;
                if (responseCode != 200)
                    response.Close();

                var stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, true);

                try
                {
                    responseText = sr.ReadToEnd();
                }
                finally
                { 
                    sr.Close(); 
                }

                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!string.IsNullOrEmpty(responseText))
            {
                Dictionary<string, int> wordCounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseText);

                string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Output.txt");

                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (KeyValuePair<string, int> v in wordCounts.OrderByDescending(x => x.Value))
                    {
                        writer.WriteLine($"{v.Key} - {v.Value}");
                    }
                }
                Console.WriteLine($"Текст сохранен в файл {outputFilePath}");
            }
            else
                Console.WriteLine("Не удалось получить ответ от веб-сервиса");
            Console.ReadLine();
        }
    }
}