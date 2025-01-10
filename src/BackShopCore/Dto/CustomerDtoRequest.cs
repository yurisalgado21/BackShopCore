using System.ComponentModel.DataAnnotations;
using BackShopCore.Utils;

namespace BackShopCore.Dto
{
    public class CustomerDtoRequest
    {
        [Required(ErrorMessage = ResponseMessages.FirstNameIsRequired)]
        [MaxLength(40, ErrorMessage = ResponseMessages.MaximumCharacters)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ResponseMessages.LastNameIsRequired)]
        [MaxLength(40, ErrorMessage = ResponseMessages.MaximumCharacters)]
        public string LastName { get; set; }
        [Required(ErrorMessage = ResponseMessages.EmailIsRequired)]
        [EmailAddress(ErrorMessage = ResponseMessages.EmailFieldIsNotAValid)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}