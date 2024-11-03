namespace GaisSocialMediaTask.Api.Responses
{
	public class ListDataResponse<T>
	{
		public bool Success { get; set; }
		public int Total { get; set; }
		public List<T> Data { get; set; }


		public ListDataResponse(int total, List<T> data)
		{
			Success = true;
			Total = total;
			Data = data;
		}

	}
}
