using System.Text;
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
            List<User> userList = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return userList;
        }
        else
        {
            Console.WriteLine($@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            return null;
        }

    }

    public async Task<bool> SaveUser(User user)
    {
        string jsonUser = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url + "/users", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User Added");
                return true;
            }
            else
            {
                Console.WriteLine($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
                return false;
            }
    }

    public async Task<bool> DeleteUser(string userId)
    {
        HttpResponseMessage responseMessage = await httpClient.DeleteAsync(url + "/user/" + userId);

        if (responseMessage.IsSuccessStatusCode)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            bool response = JsonSerializer.Deserialize<bool>(result,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return response;
        }
        else
        {
            Console.WriteLine($@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            return false;
        }
    }
}