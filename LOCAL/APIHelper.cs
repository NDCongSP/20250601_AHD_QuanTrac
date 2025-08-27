using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegistrationForm1
{
    public class APIHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<DataMucNuocModel> GetMucNuocFromAPIAsync(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string text = await response.Content.ReadAsStringAsync();

            // Tạo object DataMucNuocModel
            var data = new DataMucNuocModel
            {
                CreateAt = DateTime.Now
            };

            // Regex parse
            Regex regex = new Regex(@"(F\d{5});\d{2}/\d{2}/\d{4};\d{2}:\d{2};value=(\d+);", RegexOptions.Multiline);

            var matches = regex.Matches(text);
            foreach (Match match in matches)
            {
                string stationCode = match.Groups[1].Value;
                string value = match.Groups[2].Value;

                switch (stationCode)
                {
                    case "F01877":
                        data.Fllow_SonDai = value;
                        break;
                    case "F01203":
                        data.Fllow_BenSuc = value;
                        break;
                    case "F01849":
                        data.Fllow_DauTieng = value;
                        break;
                    // Nếu có mã Hồ
                    case "Fxxxxx":
                        data.Fllow_Ho = value;
                        break;
                }
            }

            return data;
        }
    }
}