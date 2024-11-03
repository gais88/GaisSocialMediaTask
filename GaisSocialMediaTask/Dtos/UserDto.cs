namespace GaisSocialMediaTask.Api.Dtos
{
	public class UserDto
	{
		public string Id { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public bool IsAuthenticated { get; set; }
	}
}
