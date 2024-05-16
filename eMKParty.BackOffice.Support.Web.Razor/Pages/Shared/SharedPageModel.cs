using System;
using System.Data;
using System.Net.Http.Headers;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.Shared
{
    public class SharedPageModel : PageModel
    {
        public SelectList ProvincesSL { get; set; }
        public SelectList MunicipalitySL { get; set; }
        public SelectList WardsSL { get; set; }

        //private readonly IMediator _mediator;

        //public SharedPageModel(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //Get list of Provinces
        public async void PopulatepProvincesDropDownList(object selectedDepartment = null)
        {
            ////return await _mediator.Send(new GetAllProvincesQuery());

            //var Query = from d in _context.Provinces
            //                       orderby d.ProvinceDesc
            //                       select new
            //                       {
            //                           Id = d.ProvinceCode,
            //                           Name = d.ProvinceDesc
            //                       };

            //ProvincesSL = new SelectList(Query.AsNoTracking(),
            //    nameof(Province.ProvinceCode),
            //    nameof(Province.ProvinceDesc),
            //    selectedDepartment);


            var url = $"http://102.211.28.103/api/SharedServices/Provinces/";
            //var parameters = $"?query={query}&apiKey={Consts.SpoonacularKey}&number=5";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var recipeList = JsonConvert.DeserializeObject<dynamic>(jsonString);
                List<ProvinceViewModel> provinces = JsonConvert.DeserializeObject<List<ProvinceViewModel>>(Convert.ToString(recipeList.data));
                if (provinces.Count != 0)
                {
                    var Query = from d in provinces
                                orderby d.provinceDesc
                                select new
                                {
                                    Id = d.provinceCode,
                                    Name = d.provinceDesc
                                };

                    ProvincesSL = new SelectList(Query,
                        nameof(ProvinceViewModel.provinceCode),
                        nameof(ProvinceViewModel.provinceDesc),
                        selectedDepartment);
                }
            }
        }

        ////Get list of Municipalities
        //public void PopulatepMunicipalitiesDropDownList(ApplicationMySqlDbContext _context, object selectedDepartment = null)
        //{
        //    var Query = from d in _context.Municipalities
        //                //orderby d.
        //                select new
        //                {
        //                    Id = d.Id,
        //                    Name = d.MunicipalityName
        //                };

        //    MunicipalitySL = new SelectList(Query.AsNoTracking(),
        //        nameof(Municipality.Id),
        //        nameof(Municipality.MunicipalityName),
        //        selectedDepartment);
        //}

        ////Get list of Wards
        //public void PopulatepWardsDropDownList(ApplicationMySqlDbContext _context, object selectedDepartment = null)
        //{
        //    var Query = from d in _context.Wards
        //                    //orderby d.
        //                select new
        //                {
        //                    Id = d.Id,
        //                    Name = d.WardCode
        //                };

        //    WardsSL = new SelectList(Query.AsNoTracking(),
        //        nameof(Ward.Id),
        //        nameof(Ward.WardCode),
        //        selectedDepartment);
        //}

    }
}

