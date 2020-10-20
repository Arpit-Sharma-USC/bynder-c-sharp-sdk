﻿// Copyright (c) Bynder. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Bynder.Sdk.Api.RequestSender
{
    /// <summary>
    /// HTTP request sender. Used to send HTTP Requests.
    /// It eases unit testing of other components checking that correct requests are being sent.
    /// </summary>
    internal class HttpRequestSender : IHttpRequestSender
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public string UserAgent
        {
            get { return "bynder-c-sharp-sdk/" + Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// Sends the HTTP request and returns its response.
        /// </summary>
        /// <returns>The HTTP request response.</returns>
        /// <param name="httpRequest">HTTP request.</param>
        public async Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage httpRequest)
        {
            httpRequest.Headers.Add("User-Agent", UserAgent);
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="T:Sdk.Api.ApiRequestSender"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:Sdk.Api.ApiRequestSender"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:Sdk.Api.ApiRequestSender"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:Sdk.Api.ApiRequestSender"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:Sdk.Api.ApiRequestSender"/> was occupying.</remarks>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
