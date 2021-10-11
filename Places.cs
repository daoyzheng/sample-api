using System;

namespace sample_api
{
    public class Place
    {
        public Guid id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
