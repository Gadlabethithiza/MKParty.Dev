using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Models;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using eMKParty.BackOffice.Support.Web.Razor.Pages.Shared;
using Google.Protobuf.Compiler;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.Account
{
	public class RegisterMemberModel : SharedPageModel
    {
        private readonly ILogger<RegisterMemberModel> _logger;
        public SelectList ProvincesSL { get; set; }
        public SelectList MunicipalitiesSL { get; set; }
        public SelectList WardsSL { get; set; }
        public SelectList GenderLS { get; set; }
        public SelectList PreferredLanguageLS { get; set; }
        public SelectList EmploymentSL { get; set; }
        public SelectList RaceSL { get; set; }


        [BindProperty]
        public RegisterViewModel User { get; set; }

        public RegisterMemberModel(ILogger<RegisterMemberModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            this.ProvincesSL = new SelectList(PopulatepProvincesDropDownList(), "provinceDesc", "provinceDesc");
            this.MunicipalitiesSL = new SelectList(PopulatepMunicipalityDropDownList(), "MunicipalityName", "MunicipalityName");
            this.WardsSL = new SelectList(PopulatepWardsDropDownList(), "WardCode", "WardCode");
            this.GenderLS = new SelectList(PopulateGender(), "Value", "Value");
            this.PreferredLanguageLS = new SelectList(PreferredLanguage(), "Value", "Value");
            this.EmploymentSL = new SelectList(PopulateEmployment(), "Value", "Value");
            this.RaceSL = new SelectList(PopulateRace(), "Value", "Value");
        }

        public async Task<IActionResult> OnPost()
        {
            //var race = User.race;
            //var employment_status = User.employment_status;
            //var prefered_lang = User.prefered_lang;

            //if (ModelState.IsValid)            
            //ModelState.AddModelError("User.Username", "The Username is already registered.");

            var url = $"http://102.211.28.103/api/Account/Register";
            //var parameters = $"?query={query}&apiKey={Consts.SpoonacularKey}&number=5";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(User, options);

            //var data = JsonConvert.SerializeObject(User);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;

            //// strong typed instance
            //var values = new JObject();
            //values.Add("name", User.name);
            //values.Add("surname", User.surname);
            //values.Add("id_no", User.id_no);

            //if(User.BirthDate != null)
            //     values.Add("BirthDate", User.BirthDate);

            //values.Add("mobile", User.mobile);
            //values.Add("email", User.email);
            //values.Add("gender", User.gender);
            //values.Add("prefered_lang", User.prefered_lang);
            //values.Add("building_site_no", User.building_site_no);
            //values.Add("suburb", User.suburb);
            //values.Add("city", User.city);
            //values.Add("postal_code", User.postal_code);
            //values.Add("race", User.race);
            //values.Add("province_name", User.province_name);
            //values.Add("municipality_name", User.municipality_name);
            //values.Add("employment_status", User.employment_status);

            //if (!string.IsNullOrWhiteSpace(User.ward_name))
            //    values.Add("ward_name", User.ward_name);

            //if (!string.IsNullOrWhiteSpace(User.branch_name))
            //    values.Add("branch_name", User.branch_name);

            //if (!string.IsNullOrWhiteSpace(User.occupation))
            //    values.Add("occupation", User.occupation);

            //if (!string.IsNullOrWhiteSpace(User.region))
            //    values.Add("region", User.region);

            //if (!string.IsNullOrWhiteSpace(User.sub_region))
            //    values.Add("sub_region", User.sub_region);

            //if (!string.IsNullOrWhiteSpace(User.tel))
            //    values.Add("tel", User.tel);

            //values.Add("mobile_use_whatsapp", User.mobile_use_whatsapp);

            //var sf = JSON.stringify(JSON.parse(jsString);

            //var data = JsonConvert.SerializeObject(values);
            //HttpContent content = new StringContent(values.ToString(), Encoding.UTF8, "application/json");
            //var response = client.PostAsJsonAsync("", content).Result;
            var jsonStringResponse = response.Content.ReadAsStringAsync().Result;
            var dataList = JsonConvert.DeserializeObject<dynamic>(jsonStringResponse);

            if (response.IsSuccessStatusCode)
            {
                //var jsonStringResponse = response.Content.ReadAsStringAsync().Result;
                //var dataList = JsonConvert.DeserializeObject<dynamic>(jsonStringResponse);
                ////var wardsLs = JsonConvert.DeserializeObject<List<Ward>>(Convert.ToString(dataList.data));
                //var jo_message = JObject.Parse(jsonStringResponse);

                //var message = dataList.messages;
                return RedirectToPage("./WelcomeSuccess");
                //return RedirectToAction("/Account/WelcomeSuccess");
            }
            else
            {
                // read error message from
                this.ProvincesSL = new SelectList(PopulatepProvincesDropDownList(), "provinceDesc", "provinceDesc");
                this.MunicipalitiesSL = new SelectList(PopulatepMunicipalityDropDownList(), "MunicipalityName", "MunicipalityName"); 
                this.WardsSL = new SelectList(PopulatepWardsDropDownList(), "WardCode", "WardCode");
                this.GenderLS = new SelectList(PopulateGender(), "Value", "Value");
                this.PreferredLanguageLS = new SelectList(PreferredLanguage(), "Value", "Value");
                this.EmploymentSL = new SelectList(PopulateEmployment(), "Value", "Value");
                this.RaceSL = new SelectList(PopulateRace(), "Value", "Value");

                var message = dataList.errors;
                //var data = JsonConvert.SerializeObject(User);
                return Page();
            }
        }

        //Get list of Provinces
        public static Collection<ProvinceViewModel> PopulatepProvincesDropDownList()
        {
            Collection<ProvinceViewModel> pro = new Collection<ProvinceViewModel>();

            var url = $"http://102.211.28.103/api/SharedServices/Provinces/";
            //var parameters = $"?query={query}&apiKey={Consts.SpoonacularKey}&number=5";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var dataList = JsonConvert.DeserializeObject<dynamic>(jsonString);
                var provinces = JsonConvert.DeserializeObject<List<ProvinceViewModel>>(Convert.ToString(dataList.data));
                if (provinces.Count != 0)
                {
                    foreach (var item in provinces)
                    {
                        pro.Add(item);
                    }
                }
            }

            return pro;
        }

        public static Collection<Municipality> PopulatepMunicipalityDropDownList()
        {
            Collection<Municipality> municipalities = new Collection<Municipality>();

            var url = $"http://102.211.28.103/api/SharedServices/Municipalities/";
            //var parameters = $"?query={query}&apiKey={Consts.SpoonacularKey}&number=5";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var dataList = JsonConvert.DeserializeObject<dynamic>(jsonString);
                var municipalitiesLs = JsonConvert.DeserializeObject<List<Municipality>>(Convert.ToString(dataList.data));
                if (municipalitiesLs.Count != 0)
                {
                    foreach (var item in municipalitiesLs)
                    {
                        municipalities.Add(item);
                    }
                }
            }

            //var sortedCollection = municipalities.OrderBy(item => item.MunicipalityName);
            //var data = municipalities.OrderBy(x => x.MunicipalityName).ToList();
            return municipalities;
        }

        public static Collection<Ward> PopulatepWardsDropDownList()
        {
            Collection<Ward> wards = new Collection<Ward>();

            var url = $"http://102.211.28.103/api/SharedServices/AllWards/";
            //var parameters = $"?query={query}&apiKey={Consts.SpoonacularKey}&number=5";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var dataList = JsonConvert.DeserializeObject<dynamic>(jsonString);
                var wardsLs = JsonConvert.DeserializeObject<List<Ward>>(Convert.ToString(dataList.data));
                if (wardsLs.Count != 0)
                {
                    foreach (var item in wardsLs)
                    {
                        wards.Add(item);
                    }
                }
            }

            return wards;
        }

        public static List<SelectListItem> PopulateGender()
        {
            var gendersLS = new List<string> { "Male", "Female", "Other" };
            var genders = gendersLS.Select(s => new SelectListItem { Value = s })
                .ToList();
            return genders;
        }

        public static List<SelectListItem> PreferredLanguage()
        {
            var gendersLS = new List<string> { "Engish", "Afrikaans", "Xhosa", "Southern Sotho", "Northern Sotho", "Tswana", "Tsonga", "Venda", "Swati", "Zulu", "Ndebele", "South African Sign", "Other"};
            var genders = gendersLS.Select(s => new SelectListItem { Value = s })
                .ToList();
            return genders;
        }

        public static List<SelectListItem> PopulateEmployment()
        {
            var gendersLS = new List<string> { "Employed", "Self Employed", "Unemployed" };
            var genders = gendersLS.Select(s => new SelectListItem { Value = s })
                .ToList();
            return genders;
        }

        public static List<SelectListItem> PopulateRace()
        {
            var gendersLS = new List<string> { "African", "Asian", "Coloured", "Indian", "White","Other" };
            var genders = gendersLS.Select(s => new SelectListItem { Value = s })
                .ToList();
            return genders;
        }
    }
}
