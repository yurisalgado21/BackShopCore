using BackShopCore.Dto;

namespace BackShopCore.Models
{
    public class CustomerWithMessageError
    {
        public CustomerDtoRequest Customer { get; set; }
        public string ErrorMessage { get; set; }
    }
}