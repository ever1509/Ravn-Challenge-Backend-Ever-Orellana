using Movies.API;
using Movies.Application.Common.Models.Requests;
using Movies.Application.Common.Models.Responses;
using Movies.Application.Movies.Commands.Create;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Tests.API.Base
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory>
    {
        protected readonly HttpClient TestClient;
        public TestBase()
        {
            var factory = new CustomWebApplicationFactory();
            TestClient = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<int> CreateMovieAsync()
        {
            var command = GetCreateMovieCommand();

            var response = await TestClient.PostAsync("api/movies/create", new StringContent(
                JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"
            ));
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<int>(result);
        }
        private async Task<string> GetJwtAsync()
        {

            var response = await TestClient.PostAsync("api/identity/register", new StringContent(
                JsonConvert.SerializeObject(new UserRegistrationRequest()
                {
                    Email = "orelle01@test.com",
                    Password = "Orelle01234!",
                    Role = null
                }), Encoding.UTF8, "application/json"
            ));

            var registrationResponse = await response.Content.ReadAsStringAsync();

            var value = JsonConvert.DeserializeObject<AuthSuccessResponse>(registrationResponse).Token;

            return value;
        }
        private CreateMovieCommand GetCreateMovieCommand()
        {
            return new CreateMovieCommand()
            {
                Title="Star Wars IV",
                Synopsis="The Sith revenge....",
                ReleaseDate= DateTime.Now,
                CategoryId=4,
                Image="http://starwarstest.png"
            };
        }
    }
}
