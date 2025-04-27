namespace RO.DevTest.WebApp.Services;

public class ConfirmationService
{
	public event Func<string, string, string, string, Task<bool>>? OnConfirmationRequest;

	public Task<bool> Show(string title, string message, string confirmText = "Confirmar", string cancelText = "Cancelar")
	{
		if (OnConfirmationRequest is not null)
		{
			return OnConfirmationRequest.Invoke(title, message, confirmText, cancelText);
		}

		return Task.FromResult(false);
	}
}
