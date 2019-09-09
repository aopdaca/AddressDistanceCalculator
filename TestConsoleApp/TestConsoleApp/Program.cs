using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        private static string _apiKey = "AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Address one = new Address();
            one.StreetAddress = "912 Seymour Way";
            one.City = "Perris";
            one.State = "Ca";
            one.Zipcode = "92571";
            Address two = new Address();
            two.StreetAddress = "3579 Comer Ave";
            two.City = "Riverside";
            two.State = "Ca";
            two.Zipcode = "92507";
            Console.WriteLine(GetDistanceAsync(one, two).GetAwaiter().GetResult());
        }
        public static async System.Threading.Tasks.Task<string> GetDistanceAsync(Address origin, Address destination)
        {

            string formatedOrigin = formatAddress(origin);
            Console.WriteLine(formatedOrigin);
            string formatedDestination = formatAddress(destination);
            Console.WriteLine(formatedDestination);
            string googleApiUrlParameter = GetGoogleApiUrl(formatedOrigin, formatedDestination);
            string googleBaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";

            DistanceMatrixResponse deserialized;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(googleBaseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(googleApiUrlParameter).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                deserialized = JsonConvert.DeserializeObject<DistanceMatrixResponse>(await response.Content.ReadAsStringAsync());
                // Parse the response body.
                Console.WriteLine(deserialized.rows[0].elements[0].distance.text);
                return deserialized.rows[0].elements[0].distance.text;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return "";
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //client.Dispose();

        }
        public static string GetGoogleApiUrl(string origin, string destination)
        {
            return $"?units=imperial&origins={origin}&destinations={destination}&key=" + _apiKey;
        }
        public static string formatAddress(Address address)
        {
            return address._streetaddress.Replace(" ", "+") + "+" + address._city.Replace(" ", "+") + "," + address._state.Replace(" ", "+") + "+" + address._zip; ;
        }
    }
    public class DistanceMatrixResponse
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }

    }
    public class Row
    {
        public Element[] elements { get; set; }
    }
    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }
    public class Distance
    {
        public string text { get; set; }
        public string value { get; set; }
    }
    public class Duration
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
