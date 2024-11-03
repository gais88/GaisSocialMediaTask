namespace GaisSocialMediaTask.Api.Responses
{
	public class DataResponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public DataResponse(bool success, T data, string message = "")
		{
			Success = success;
			Message = message;
			Data = data;
		}
	}

	public class DataResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		public DataResponse(bool success, string message = "")
		{
			Success = success;
			Message = message;
		}
	}

}
