using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;
using Enums;
using Interfaces;

public static class FetchDataServiceClass
{
  public static async Task<IResponse> FetchData(
      string url,
      string token,
      string clientSessionId,
      object data,
      REQUEST_TYPES requestType,
      bool sendAsAnalytics = false,
      int[] skipRetryOnStatusCodes = null,
      int retries = 3, // Retry request 3 times on failure
      int backOff = 300)
  {
    Console.WriteLine("FetchData function called, {0}, {1}, {2}, {3}, {4}", url, token, clientSessionId, data, requestType);
    try
    {
      Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type", "application/json"},
                {"Session-Id", clientSessionId ?? Guid.NewGuid().ToString()},
                {"CurrentRetry", retries.ToString()},
                {"URL", url}
            };
      if (!string.IsNullOrEmpty(token))
      {
        headers.Add("Authorization", $"Bearer {token}");
      }

      //  ======= Testing Response =======
      // return await TestResponse(url);

      headers.Add("Client-source", Environment.OSVersion.Platform == PlatformID.Unix ? "Node" : "Browser");

      HttpRequestMessage request = new HttpRequestMessage()
      {
        Method = new HttpMethod(requestType.ToString()),
        RequestUri = new Uri(url),
        Headers = { }
      };

      foreach (var header in headers)
      {
        request.Headers.Add(header.Key, header.Value);
      }

      if (requestType == REQUEST_TYPES.POST || requestType == REQUEST_TYPES.PATCH)
      {
        string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
      }

      using (HttpClient client = new HttpClient())
      {
        HttpResponseMessage response = await client.SendAsync(request);
        string responseData = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
          return new Response()
          {
            status = true,
            data = responseData
          };
        }
        else
        {
          if (skipRetryOnStatusCodes != null && Array.IndexOf(skipRetryOnStatusCodes, (int)response.StatusCode) >= 0)
          {
            return new Response()
            {
              status = false,
              message = responseData
            };
          }

          if (retries > 0)
          {
            await Wait(backOff);
            return await FetchData(
                url,
                token,
                clientSessionId,
                data,
                requestType,
                sendAsAnalytics,
                skipRetryOnStatusCodes,
                retries - 1,
                backOff * 2);
          }
          else
          {
            return new Response()
            {
              status = false,
              message = responseData
            };
          }
        }
      }
    }
    catch (Exception error)
    {
      return new Response()
      {
        status = false,
        message = error.Message
      };
    }
  }

  public static async Task<IResponse> FetchDataService(
      string url,
      string token,
      string clientSessionId,
      object data,
      REQUEST_TYPES requestType,
      bool sendAsAnalytics = false,
      int[] skipRetryOnStatusCodes = null)
  {
    return await FetchData(
        url,
        token,
        clientSessionId,
        data,
        requestType,
        sendAsAnalytics,
        skipRetryOnStatusCodes);
  }

  private static async Task Wait(int ms)
  {
    await Task.Delay(ms);
  }

  public static async Task<IResponse> TestResponse(
    string url
  )
  {
    // ================================================
    // TESTING RESPONSE DATA

    if (url.EndsWith("init"))
    {
      return new Response()
      {
        status = true,
        data = new User
        {
          Id = "use503",
        }
      };
    }
    else if (url.EndsWith("assign"))
    {
      return new Response()
      {
        status = true,
        data = new List<object>
        {
          new
              {
                  target = "",
                  site = "add-point1",
                  experimentType = "Simple",
                  assignedCondition = new
                  {
                      id = "86326641-62d4-40b2-9811-ae66e55ea064",
                      conditionCode = "add-con2",
                      payload = (object)null,
                      experimentId = "3661678f-6701-49a7-973a-c1c27d5fe28b"
                  }
              }
          }

      };
    }
    else
    {
      {
        return new Response()
        {
          status = false,
          data = null,
        };
      }
    }
    // ==================================================
    // TESTTING COMPLETE
  }
}
