using System.ComponentModel.DataAnnotations;

namespace GaisSocialMediaTask.Api.Dtos
{
	public class RegisterDto
	{
		[Required,MaxLength(50)]
		public string FirstName { get; set; } = string.Empty;
		[Required,MaxLength(50)]
		public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(100)]

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
