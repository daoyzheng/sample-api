using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace sample_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacesController : ControllerBase
    {
        // private static readonly string[] Summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        private readonly ILogger<PlacesController> _logger;

        public PlacesController(ILogger<PlacesController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public Response<Place> Get([FromQuery]string page, [FromQuery]string perPage, [FromQuery]string type, [FromQuery]string keyword)
        {
            // List<Place> places = GenerateList();
            string Code = "Success";
            string Message = null;
            List<Place> places = LoadJson();
            int pageQuery = 0;
            bool pageQuerySuccess = int.TryParse(page, out pageQuery);
            if (!pageQuerySuccess)
            {
                Code = "Fail";
                Message = "Invalid page query parameter";
            }
            int perPageQuery = 10;
            if (!string.IsNullOrEmpty(perPage))
            {
                bool perPageQuerySuccess = int.TryParse(perPage, out perPageQuery);
                if (!perPageQuerySuccess)
                {
                    Code = "Fail";
                    Message = "Invalid per page query parameter";
                }
            }
            List<string> types = new List<string>();
            if (!string.IsNullOrEmpty(type))
            {
                Regex.Replace(type, @"\s+", "");
                types = type.Split(",").ToList();
            }


            List<Place> result = places.Where(place => (types.Count > 0 ? types.Contains(place.Type) : true) && (!string.IsNullOrEmpty(keyword) ? place.Name.ToLower().Contains(keyword.ToLower()) : true)).Skip(pageQuery * perPageQuery).Take(perPageQuery).ToList();
            decimal count = (decimal)places.Count / (decimal)perPageQuery;
            int totalPages = (int)Math.Ceiling(count);

            return new Response<Place>()
            {
                Data = new Data<Place>
                {
                    Items = result,
                    TotalPages = totalPages,
                    TotalItems = places.Count,
                    PerPage = perPageQuery,
                    CurrentPage = pageQuery
                },
                Message = Message,
                Code = Code,
                ExceptionName = null,
                StackTrace = null
            };
        }

        private List<Place> LoadJson()
        {
            using (StreamReader r = new StreamReader("./data.json"))
            {
                string json = r.ReadToEnd();
                List<Place> places = JsonConvert.DeserializeObject<List<Place>>(json);
                return places;
            }
        }

        private List<Place> GenerateList(int items = 1000)
        {
            List<Place> places = new List<Place>();
            for (int i = 0; i < items; i++)
            {
                places.Add(GeneratePlace());
            }
            return places;
        }

        private Place GeneratePlace()
        {
            Random rng = new Random();
            Guid id = Guid.NewGuid();
            string type = RandomString(Data.TypeList);

            string name1 = RandomString(Data.NameList1);
            string name2 = RandomString(Data.NameList2);
            string name3 = RandomString(Data.NameList3);
            string name = $"{name1} {name2} {name3}";

            string address1 = $"{rng.Next(100, 2667)}";
            string address2 = $"{rng.Next(1, 71)}";
            string address3 = RandomString(Data.AddressPart3);
            string address4 = RandomString(Data.AddressPart4);
            string address5 = RandomString(Data.AddressPart5);
            string address = $"{address1} {address2} {address3}, {address4}, {address5}";

            string imageUrl = $"https://picsum.photos/id/{rng.Next(1000, 1086)}/300/200";

            return new Place
            {
                id = id,
                Type = type,
                Name = name,
                Address = address,
                ImageUrl = imageUrl
            };
        }

        private string RandomString (List<string> stringList)
        {
            Random rng = new Random();
            return stringList[rng.Next(stringList.Count)];
        }

    }
}
