using System.Net.Http.Json;
using System.Text.Json;

namespace SafeBoda.Admin.Services
{
    public class AuthService
    {
        private readonly ApiClient _apiClient;
        public string? AuthToken { get; private set; }

        public AuthService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };

            var response = await _apiClient.HttpClient.PostAsJsonAsync("/api/Auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<LoginResult>(options);
                AuthToken = result?.Token;
                return null; 
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return $"Login failed: {response.StatusCode}. Details: {errorContent}";
        }
        
        public void Logout()
        {
            AuthToken = null;
        }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}