using System.ComponentModel.DataAnnotations;

namespace BackShopCore.Dto
{
    public class CustomerDtoRequest
    {
        [Required(ErrorMessage = "FirstName is Required")]
        [MaxLength(40, ErrorMessage = "Must have a maximum of 40 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]
        [MaxLength(40, ErrorMessage = "Must have a maximum of 40 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
    }
}