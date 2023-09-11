namespace Blog.Web.Requests
{
    public class PostQueryParametersRequest
    {
        public string? Title { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}