namespace Application.DTOs.Request
{
    public class DeleteItemRequest
    {
        public string Path { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
    }
}

