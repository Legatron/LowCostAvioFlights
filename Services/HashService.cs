using LowCostAvioFlights.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace LowCostAvioFlights.Services
{
    public class HashService: IHashService
    {
        public string CreateHash(FlightSearchParametersDto parameters)
        {
            var relevantProperties = new
            {
                parameters.OriginLocationCode,
                parameters.DestinationLocationCode,
                parameters.DepartureDate,
                parameters.ReturnDate,
                parameters.Adults,
                parameters.Children,
                parameters.Infants,
                parameters.CurrencyCode
            };

            var json = JsonConvert.SerializeObject(relevantProperties);
            var bytes = Encoding.UTF8.GetBytes(json);
            var hashBytes = SHA256.Create().ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
