namespace BackShopCore.Models
{
    public class Bulk2ImportCustomersResponse
    {
        public int SuccessCustomersCount { get; set; }
        public int FailureCustomersCount { get; set; }
        public List<Customer>? Success { get; set; }
        public List<CustomerWithMessageError>? Failure { get; set; }
    }
}