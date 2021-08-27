using FluentAssertions;
using Movies.Application.Movies.Queries.Movieslist;
using Movies.Tests.API.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Tests.API.Movies
{
    public class MoviesTests : TestBase
    {
        [Fact]
        public async Task GetAllMoviesTest()
        {
            await AuthenticateAsync();
            var response = await TestClient.GetAsync($"api/movies/all");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MoviesListVm>(contentResult);
            result.Should().BeOfType<MoviesListVm>();
            result.Movies.Items.Should().HaveCount(4);
        }       

        [Fact]
        public async Task RateMovieTest()
        {
            await AuthenticateAsync();

            var response= await TestClient.PostAsync($"api/movies/rate/2", new StringContent(
                JsonConvert.SerializeObject(2), Encoding.UTF8, "application/json"
            ));
            
            var result = await response.Content.ReadAsStringAsync();

            var obj= JsonConvert.DeserializeObject<int>(result);
            obj.Should().Be(2);
        }
    }
}
