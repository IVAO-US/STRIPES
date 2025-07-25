// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using STRIPES.Services.Endpoints.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace STRIPES.Services.Endpoints.V2.Aircrafts.All
{
    /// <summary>
    /// Builds and executes requests for operations under \v2\aircrafts\all
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AllRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AllRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/aircrafts/all{?description*,hasBaseModels*,manufactureId*,page*,perPage*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AllRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/aircrafts/all{?description*,hasBaseModels*,manufactureId*,page*,perPage*}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.AircraftDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto">When receiving a 401 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::STRIPES.Services.Endpoints.Models.AircraftDto?> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder.AllRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::STRIPES.Services.Endpoints.Models.AircraftDto> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder.AllRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::STRIPES.Services.Endpoints.Models.AircraftDto>(requestInfo, global::STRIPES.Services.Endpoints.Models.AircraftDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder.AllRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder.AllRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder WithUrl(string rawUrl)
        {
            return new global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class AllRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
            /// <summary>String to find in aircrafts description</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("description")]
            public string? Description { get; set; }
#nullable restore
#else
            [QueryParameter("description")]
            public string Description { get; set; }
#endif
            /// <summary>Check if the simulator version has baseModels</summary>
            [QueryParameter("hasBaseModels")]
            public bool? HasBaseModels { get; set; }
            /// <summary>Manufacture of the aircraft</summary>
            [QueryParameter("manufactureId")]
            public double? ManufactureId { get; set; }
            /// <summary>The number of the page</summary>
            [QueryParameter("page")]
            public double? Page { get; set; }
            /// <summary>The number of elements per page</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("perPage")]
            public string? PerPage { get; set; }
#nullable restore
#else
            [QueryParameter("perPage")]
            public string PerPage { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class AllRequestBuilderGetRequestConfiguration : RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Aircrafts.All.AllRequestBuilder.AllRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
