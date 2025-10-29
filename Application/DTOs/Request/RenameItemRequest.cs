namespace Application.DTOs.Request
{
    public class RenameItemRequest
    {
        public string OldPath { get; set; } = string.Empty;
        public string NewName { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
    }
}

