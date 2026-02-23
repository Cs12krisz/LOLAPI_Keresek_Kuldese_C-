using LOL;
using LOL.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] languages = null;
        string nyelv = "";
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        public async Task Load()
        {
            await Program.VerziokBetoltes();
            await LoadLanguages();
            await LoadChampions();

        }

        public async Task LoadChampions()
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string url = $"https://ddragon.leagueoflegends.com/cdn/{Program.version}/data/{nyelv}/champion.json";

                    var responseApi = await client.GetStringAsync(url);

                    var response = JsonSerializer.Deserialize<ChampionData>(responseApi);
                    Program.champions = response.Data.Values.ToList();
                    lbxChampions.ItemsSource = Program.champions.Select(c => c.Title);

                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        public async Task LoadLanguages()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string url = "https://ddragon.leagueoflegends.com/cdn/languages.json";

                    var responseApi = await client.GetStringAsync(url);

                    string[] response = JsonSerializer.Deserialize<string[]>(responseApi);
                    languages = response;
                    cbxNyelvek.ItemsSource = languages;
                    cbxNyelvek.SelectedIndex = 1;
                    nyelv = cbxNyelvek.SelectedValue.ToString();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        private async void cbxNyelvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nyelv = cbxNyelvek.SelectedValue.ToString();
            await LoadChampions();
        }

        private void lbxChampions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(Program.champions[lbxChampions.SelectedIndex].Name);
        }
    }
}