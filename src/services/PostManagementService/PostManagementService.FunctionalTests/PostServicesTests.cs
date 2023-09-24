using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace PostManagementService.FunctionalTests;

public class PostServiceTests
{
    [Fact]
    public async Task Create_post_check_created()
    {
        
        var serviceUrl = "http://localhost:5429/api/posts";

        var http = new HttpClient();
        var content = new StringContent(BuildPost(), UTF8Encoding.UTF8, "application/json")
            {
                Headers = { { "x-requestid", Guid.NewGuid().ToString() } },
                
            };

        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.PostAsync(serviceUrl, content);
        var s = await response.Content.ReadAsStringAsync();
        var post = JsonSerializer.Deserialize<Post>(s);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(post);
        Assert.NotNull(post.postId);

    }

    [Fact]
    public async Task Get_post_by_id_not_found()
    {
        var fakeId = Guid.NewGuid().ToString();

        var serviceUrl = $"http://localhost:5429/api/posts/{fakeId}";

        var http = new HttpClient();
        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.GetAsync(serviceUrl);
        var s = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task Get_post_by_creating_post_ok_response()
    {
        var post = await CreatePost();
        Assert.NotNull(post);

        var serviceUrl = $"http://localhost:5429/api/posts/{post.postId}";

        var http = new HttpClient();
        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.GetAsync(serviceUrl);
        var s = await response.Content.ReadAsStringAsync();
        var getPost = JsonSerializer.Deserialize<PostVM>(s);
        System.Console.WriteLine(s); 
        
        Assert.NotNull(getPost);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(post.postId, getPost.postId);
    }

    [Fact]
    public async Task Delete_post_ok_response()
    {
        var post = await CreatePost();
        Assert.NotNull(post);

        var serviceUrl = $"http://localhost:5429/api/posts";

        var http = new HttpClient();
        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var request = new HttpRequestMessage {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(serviceUrl),
            Content = new StringContent(JsonSerializer.Serialize(new {
                PostId = post.postId
            }), UTF8Encoding.UTF8, "application/json")
        };

        var response = await http.SendAsync(request);
        var s = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    async Task<Post> CreatePost()
    {
        var serviceUrl = "http://localhost:5429/api/posts";

        var http = new HttpClient();
        var content = new StringContent(BuildPost(), UTF8Encoding.UTF8, "application/json")
            {
                Headers = { { "x-requestid", Guid.NewGuid().ToString() } },
                
            };

        http.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await http.PostAsync(serviceUrl, content);
        var s = await response.Content.ReadAsStringAsync();
        var post = JsonSerializer.Deserialize<Post>(s);
        return post;
    }

    string BuildPost()
    {
        var post = new {
            Name = "Test post",
            Content = "Test comment"
        };

        return JsonSerializer.Serialize(post);
    }
}

public class Post
{
    public int id { get; set; }
    public string postId { get; set; }
    public string name { get; set; }
    public string content { get; set; }
    public DateTime created { get; set; }
    public DateTime? updated { get; set; }

}

public class PostVM
{

    public string postId { get; set; }
    public string name { get; set; }
    public string content { get; set; }
    public List<string> comments { get; set; }
}