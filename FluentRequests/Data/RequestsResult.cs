﻿using System.Net;

namespace FluentRequests.Data
{
    public class RequestsResult<TResponse>
    {
        public string Url { get; } = "";
        public TResponse? Data { get; }
        public HttpStatusCode StatusCode { get; }
        public int TimeMs { get; }

        public void Retry()
        {
            throw new NotImplementedException();
        }
    }
}
