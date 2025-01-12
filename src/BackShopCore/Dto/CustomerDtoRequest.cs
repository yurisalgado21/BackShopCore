using System.ComponentModel.DataAnnotations;
using BackShopCore.Utils;

namespace BackShopCore.Dto
{
    public class CustomerDtoRequest
    {
        [Required(ErrorMessage = ResponseMessages.FirstNameIsRequired, AllowEmptyStrings = false)]
        [MaxLength(40, ErrorMessage = ResponseMessages.MaximumCharacters)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ResponseMessages.LastNameIsRequired, AllowEmptyStrings = false)]
        [MaxLength(40, ErrorMessage = ResponseMessages.MaximumCharacters)]
        public string LastName { get; set; }
        [Required(ErrorMessage = ResponseMessages.EmailIsRequired, AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = ResponseMessages.EmailFieldIsNotAValid)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}