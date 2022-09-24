using Newtonsoft.Json;
using StackExchange.Redis;

namespace TvMaze.Api.Utils
{
  public static class Redis
  {
    public static async Task<T> GetAsync<T>(this IDatabase cache, string key)
    {
      try
      {
        var value = await cache.StringGetAsync(key);
        if (!value.IsNull)
          return JsonConvert.DeserializeObject<T>(value);
        else
        {
          return default(T);
        }
      }
      catch (Exception)
      {

        throw;
      }
    }

    public static async Task SetAsync(this IDatabase cache, string key, object value, TimeSpan experation)
    {
      await cache.StringSetAsync(key, JsonConvert.SerializeObject(value), experation);
    }
  }
}
