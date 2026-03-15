namespace ProductAPI;

public class GetNetworkService
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    public GetNetworkService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUserIP()
    {
        return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}
