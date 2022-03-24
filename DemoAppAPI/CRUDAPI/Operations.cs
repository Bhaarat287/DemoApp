using DemoAppAPI.Models;

namespace DemoAppAPI.CRUDAPI
{
    public class Operations : IOperations
    {
        private static readonly string APIUrl = "http://localhost:3000/";
        public IEnumerable<Address> ListAddress()
        {
            IEnumerable<Address> Addresss = Enumerable.Empty<Address>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                //HTTP GET
                var responseTask = client.GetAsync("addresses");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<Address>>();
                    readTask.Wait();

                    Addresss = readTask.Result;
                }
            }
            return Addresss;
        }

        public bool CreateAddress(Address Address)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Address>("addresses", Address);
            postTask.Wait();

            var result = postTask.Result;
            return result.IsSuccessStatusCode;
        }

        public Address DetailAddress(int id)
        {
            Address Address = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                //HTTP GET
                var responseTask = client.GetAsync("addresses/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Address>();
                    readTask.Wait();

                    Address = readTask.Result;
                }
            }
            return Address;
        }

        public bool EditAddress(int id, Address Address)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);

            //HTTP POST
            var postTask = client.PutAsJsonAsync<Address>("addresses/" + id.ToString(), Address);
            postTask.Wait();

            var result = postTask.Result;
            return result.IsSuccessStatusCode;
        }

        public bool DeleteAddress(int id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);

            //HTTP DELETE
            var deleteTask = client.DeleteAsync("addresses/" + id.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            return result.IsSuccessStatusCode;
        }

        public IEnumerable<Address> SearchAddress(string searchText)
        {
            var AllAddress = ListAddress();
            return AllAddress.Where(c => c.Line1.ToLower().Contains(searchText) || c.Line1.ToLower().Contains(searchText.ToLower()) || c.City.ToLower().Contains(searchText) || c.PostCode.ToLower().Contains(searchText))
                            .ToList();
        }

        public bool ValidateKey(string apiKey)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);
            //HTTP GET
            var responseTask = client.GetAsync("apiKeys/" + apiKey);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadFromJsonAsync<ApiKey>();
                readTask.Wait();

                return !string.IsNullOrWhiteSpace(readTask.Result.Id);
            }
            return false;
        }
    }
}
