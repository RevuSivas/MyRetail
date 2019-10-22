namespace MyRetail.Rest.Entities
{
    public class PriceEntity
    {
        public long ProductId { get; set; }

        public decimal Value { get; set; }

        public string CurrencyCode { get; set; }
    }
}
