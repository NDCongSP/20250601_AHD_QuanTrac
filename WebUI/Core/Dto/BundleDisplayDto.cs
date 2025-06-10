namespace UI.Core.Dto
{
    public class BundleDisplayDto
    {
        public required string ProductBundleCode { get; set; }
        public string? ProductBundleName { get; set; }

        public string ProductCodeOrigin
        {
            get
            {
                return ProductBundleCode.Length > 3 && ProductBundleCode[2] == '-' ? ProductBundleCode.Substring(3) : ProductBundleCode;
            }
        }
    }
}
