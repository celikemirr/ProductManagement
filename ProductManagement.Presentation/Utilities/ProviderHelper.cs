using ProductManagement.Presentation.Models.ViewModels;
using System.Text;
using System.Text.Json;

namespace ProductManagement.Presentation.Utilities
{
    public class ProviderHelper
    {
        public static async Task<IEnumerable<MessageViewModel>> Get(string url)  //APİ VERİLERİNE ENTEGRE OLMA
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<IEnumerable<MessageViewModel>>(json);
            return model;
        }

        public static async Task<MessageViewModel> Post(string url,MessageCreateViewModel model)
        {
            HttpClient client = new HttpClient();
            var json = JsonSerializer.Serialize(model);

            StringContent contect = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, contect);

         

            if (response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            var jsonRes = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MessageViewModel>(jsonRes);
            return result;
        }
    }
}
