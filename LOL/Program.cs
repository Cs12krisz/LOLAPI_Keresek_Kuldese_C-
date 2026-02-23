using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LOL
{
    internal class Program
    {
        static string version = "1.0";
        static async Task Main(string[] args)
        {
            await VerziokBetoltes();
            Console.WriteLine(version);
        }

        static async Task VerziokBetoltes()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string url = "https://ddragon.leagueoflegends.com/api/versions.json";

                    var responseApi = await client.GetStringAsync(url);

                    string[] response = JsonSerializer.Deserialize<string[]>(responseApi);
                    version = response[0];
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
