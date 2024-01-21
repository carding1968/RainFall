using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainfallAPI
{
    public class Items
    {
        [JsonProperty("dateTime")]
        public string? Date { get; set; }
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
    public class ApiClient
    {


        public HttpClient client = new HttpClient();
        public async Task<List<Items>> GetRainfallAsync(string path)
        {



            
            HttpResponseMessage response = await client.GetAsync(path);
            var items = new List<Items>();

            if (response.IsSuccessStatusCode)
            {
                string rb = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(rb);
                var dsa = data.items.Count;
                for (var i = 0; i < data.items.Count; i++) {
                    items.Add(new Items { Date = data.items[i]["dateTime"], Value = data.items[i]["value"] });
                    
                }
            }

            return items;



        }

        public async Task<List<Items>> RunAsync(string stationId)
        {

            string rootUrl = "https://environment.data.gov.uk/flood-monitoring/id/stations/" + stationId + "/readings";


            // Update port # in the following line.
            client.BaseAddress = new Uri(rootUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var items = new List<Items>();
                // Get the product
                items = await GetRainfallAsync(rootUrl);

                return items;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
