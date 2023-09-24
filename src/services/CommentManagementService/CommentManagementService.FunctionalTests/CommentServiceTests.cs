using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
namespace CommentManagementService.FunctionalTests;

public class CommentServiceTests
{
    [Fact]
    public async Task Create_comment_accepted_response()
    {
        var serviceUrl = "http://localhost:5055/api/comments";

        var http = new HttpClient();
        var content = new StringContent(BuildComment(), UTF8Encoding.UTF8, "application/json")
            {
                Headers = { { "x-requestid", Guid.NewGuid().ToString() } },
                
            };

        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.PostAsync(serviceUrl, content);
        var s = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }

    [Fact]
    public async Task Get_comment_ok_response()
    {

        var serviceUrl = "http://localhost:5055/api/comments/9da8f3a7-a367-4c5e-a5f8-6497aae64331";

        var http = new HttpClient();
        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.GetAsync(serviceUrl);
        var s = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    string BuildComment()
    {
        var comment = new {
            PostId = Guid.NewGuid().ToString(),
            Comment = "Test comment"
        };

        return JsonSerializer.Serialize(comment);
    }
}