using System.Collections.Generic;

namespace sample_api
{
    public class Response<T>
    {
        public Data<T> Data { get; set; }

        public string Message { get; set; }
        public string Code { get; set; }
        public string ExceptionName { get; set; }
        public string StackTrace { get; set; }
    }

    public class Data<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PerPage { get; set; }
    }
}
