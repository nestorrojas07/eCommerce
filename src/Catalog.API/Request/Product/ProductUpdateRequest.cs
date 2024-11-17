namespace Catalog.API.Request.Product
{
    public class ProductUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public float? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
