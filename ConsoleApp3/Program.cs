using Hotel_Booking_Application_Service.Services.AppServise;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
	static async Task Main(string[] args)
	{
		var httpClient = new HttpClient();
		var service = new PhoneVerificationService(httpClient);

		var fakeContext = new FakeHttpContext();

		Console.Write("Enter phone number: ");
		string phone = Console.ReadLine();

		string code = await service.GenerateAndSendCodeAsync(fakeContext, phone);
		Console.WriteLine($"Generated code: {code}");

		Console.Write("Enter code to validate: ");
		string input = Console.ReadLine();

		bool isValid = await service.ValidateCodeAsync(fakeContext, input);
		Console.WriteLine($"Validation result: {isValid}");
	}
}


public class FakeSession
{
	private Dictionary<string, string> _store = new();
	public void SetString(string key, string value) => _store[key] = value;
	public string GetString(string key) => _store.TryGetValue(key, out var value) ? value : null;
}

public class FakeHttpContext
{
	public FakeSession Session { get; } = new FakeSession();
}
