using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Model
{
    public class Destination
    {
        public string Uri { get; set; } = "/";
        public bool RequiresAuthentication { get; set; } = false;
        static readonly HttpClient client = new HttpClient();
        HttpRequest request;

        public Destination(string uri, bool requiresAuthentication = false)
        {
            Uri = uri;
            RequiresAuthentication = requiresAuthentication;
        }
        private Destination()
        {
        }

        private string CreateDestinationUri()
        {
            var completeUrl = new StringBuilder(Uri);
            completeUrl.Append(CreateEndPoint(request.Path));
            completeUrl.Append(ClientQueryString(request.QueryString));
            return completeUrl.ToString();
        }
        private string CreateEndPoint(PathString webPath)
        {
            var endPointArray = (webPath.ToString()).Substring(1).Split('/');
            var endPoint = (endPointArray.Length > 1) ? endPointArray[1] : "";
            return endPoint;
        }
        private string ClientQueryString(QueryString clientQuery)
        {
            return clientQuery.ToString();
        }
        private string GetMessageString()
        {
            var streamResult = default(string);
            using (var streamReceiver = request.Body)
            {
                using (var streamReader = new StreamReader(streamReceiver, Encoding.UTF8))
                {
                    streamResult = streamReader.ReadToEnd();
                    return streamResult;
                }
            }
        }
        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            this.request = request;
            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri()))
            {
                newRequest.Content = new StringContent(GetMessageString(), Encoding.UTF8, request.ContentType);
                var response = await client.SendAsync(newRequest);
                return response;
            }
        }
    }
}
