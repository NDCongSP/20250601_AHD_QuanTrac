using System;

namespace Application.DTOs.Response
{
    public class FolderTreeItem
    {
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<FolderTreeItem> Children { get; set; } = new();
    }
}

