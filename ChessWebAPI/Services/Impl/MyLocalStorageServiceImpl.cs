using System.Text.Json;
using System.Xml.Linq;

namespace AuthWebAPI.Services.Impl
{
    public class MyLocalStorageServiceImpl : IMyLocalStorageService
    {
        private readonly string path;

        public MyLocalStorageServiceImpl(string path = "refresh.json")
        {
            this.path = path;
        }
        public async Task<string> GetItemAsync(string key = "")
        {
            var dictionary = await OpenFile();

            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
                return null;
        }

        public async Task RemoveItemAsync(string key)
        {
            var dictionary = await OpenFile();

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
                await SaveFile(dictionary);
            }
        }

        public async Task SetItemAsync(string key, string value)
        {
            var dictionary = await OpenFile();

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);

            await SaveFile(dictionary);
        }

        private async Task<Dictionary<string, string>> OpenFile()
        {
            if (File.Exists(path))
            {
                await using (FileStream fstream = new FileStream(path, FileMode.Open))
                {
                    return await JsonSerializer.DeserializeAsync(fstream, typeof(Dictionary<string, string>)) as Dictionary<string, string>;
                }
            }
            else
            {
                return new Dictionary<string, string>(); 
            }
        }

        private async Task SaveFile(Dictionary<string, string> data)
        {
            await using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fstream, data);
            }
        }
    }
}
