namespace FE.ViewModels;

public class BaseResponse<T>
{
	public T? Dado { get; set; }
	public EStatusResponse Status { get; set; }
}