using System.Text.Json;
using BlazorApp_Client.Models;

namespace BlazorApp_Client.Service;

public class UserService : IUserService
{
    private HttpClient httpClient;
    private String url = "http://localhost:8080";

    public UserService()
    {
        httpClient = new HttpClient();
        
    }

    public async Task<List<User>> GetAllUsers()
    {
        HttpResponseMessage responseMessage = await httpClient.GetAsync(url + "/users");
        if (responseMessage.IsSuccessStatusCode)
        {
            string result = await responseMessage.Content.ReadAsStringAsync(); 
            List<User> userList = JsonSerializer.Deserialize<List<User>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return userList;
        }
        else
        {
            Console.WriteLine($@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            return null;
        }

    }
}