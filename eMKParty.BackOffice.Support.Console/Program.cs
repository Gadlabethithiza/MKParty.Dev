// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using eMKParty.BackOffice.Support.Domain.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

try
{
    var path = @"/Users/mac/Umkhonto Wesizwe/WhatsForm-Responses.csv"; // Habeeb, "Dubai Media City, Dubai"
    using (TextFieldParser csvParser = new TextFieldParser(path))
    {
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] { "," });
        csvParser.HasFieldsEnclosedInQuotes = true;

        // Skip the row with the column names
        csvParser.ReadLine();

        while (!csvParser.EndOfData)
        {
            // Read current line fields, pointer moves to the next line.
            string[] fields = csvParser.ReadFields();


            if (fields.Count() > 0)
            {
                RegisterViewModel item = new RegisterViewModel();

                if (!string.IsNullOrWhiteSpace(fields[2]))
                    item.name = fields[2].ToString();

                if (!string.IsNullOrWhiteSpace(fields[3]))
                    item.surname = fields[3].ToString();

                if (!string.IsNullOrWhiteSpace(fields[4]))
                    item.id_no = fields[4].ToString();

                if (!string.IsNullOrWhiteSpace(fields[5]))
                {
                    if(fields[5].ToString().Length > 10)
                        item.gender = fields[5].ToString().Substring(0, 9);
                    else
                        item.gender = fields[5].ToString();
                }

                if (!string.IsNullOrWhiteSpace(fields[6]))
                    item.race = fields[6].ToString();

                if (!string.IsNullOrWhiteSpace(fields[7]))
                    item.prefered_lang = fields[7].ToString();

                if (!string.IsNullOrWhiteSpace(fields[8]))
                    item.mobile = fields[8].ToString();

                if (!string.IsNullOrWhiteSpace(fields[9]))
                    item.email = fields[9].ToString();

                if (!string.IsNullOrWhiteSpace(fields[10]))
                    item.province_name = fields[10].ToString();

                if (!string.IsNullOrWhiteSpace(fields[11]))
                    item.municipality_name = fields[11].ToString();

                if (!string.IsNullOrWhiteSpace(fields[12]))
                    item.ward_name = fields[12].ToString();

                if (!string.IsNullOrWhiteSpace(fields[13]))
                    item.building_site_no = fields[13].ToString();

                if (!string.IsNullOrWhiteSpace(fields[14]))
                    item.suburb = fields[14].ToString();

                if (!string.IsNullOrWhiteSpace(fields[15]))
                    item.city = fields[15].ToString();

                if (!string.IsNullOrWhiteSpace(fields[16]))
                {
                    if (fields[16].ToString().Length > 5)
                        item.postal_code = fields[16].ToString().Substring(0, 4);
                    else
                        item.postal_code = fields[16].ToString();
                }

                if (!string.IsNullOrWhiteSpace(fields[17]))
                    item.employment_status = fields[17].ToString();

                if (!string.IsNullOrWhiteSpace(fields[18]))
                    item.occupation = fields[18].ToString();

                var url = $"http://102.211.28.103/api/Account/Register";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = System.Text.Json.JsonSerializer.Serialize(item, options);

                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;

                var jsonStringResponse = response.Content.ReadAsStringAsync().Result;
                var dataList = JsonConvert.DeserializeObject<dynamic>(jsonStringResponse);

                var res = response.IsSuccessStatusCode;

                //if (response.IsSuccessStatusCode)
                //{
                //    //var jsonStringResponse = response.Content.ReadAsStringAsync().Result;
                //    //var dataList = JsonConvert.DeserializeObject<dynamic>(jsonStringResponse);
                //    ////var wardsLs = JsonConvert.DeserializeObject<List<Ward>>(Convert.ToString(dataList.data));
                //    //var jo_message = JObject.Parse(jsonStringResponse);

                //    //var message = dataList.messages;
                //    return RedirectToPage("./WelcomeSuccess");
                //    //return RedirectToAction("/Account/WelcomeSuccess");
                //}
            }

        }
    }
}
catch (Exception ex)
{

}


