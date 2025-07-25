// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using STRIPES.Services.Endpoints.Models;
using STRIPES.Services.Endpoints.V2.Creators.All;
using STRIPES.Services.Endpoints.V2.Creators.Item;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace STRIPES.Services.Endpoints.V2.Creators
{
    /// <summary>
    /// Builds and executes requests for operations under \v2\creators
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CreatorsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The all property</summary>
        public global::STRIPES.Services.Endpoints.V2.Creators.All.AllRequestBuilder All
        {
            get => new global::STRIPES.Services.Endpoints.V2.Creators.All.AllRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the STRIPES.Services.Endpoints.v2.creators.item collection</summary>
        /// <param name="position">User VID or &apos;me&apos;</param>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Creators.Item.WithVItemRequestBuilder"/></returns>
        public global::STRIPES.Services.Endpoints.V2.Creators.Item.WithVItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("vid", position);
                return new global::STRIPES.Services.Endpoints.V2.Creators.Item.WithVItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CreatorsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/creators{?divisionId*,page*,perPage*,rating*,tier*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CreatorsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/creators{?divisionId*,page*,perPage*,rating*,tier*}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.PaginatedCreatorDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::STRIPES.Services.Endpoints.Models.PaginatedCreatorDto?> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder.CreatorsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::STRIPES.Services.Endpoints.Models.PaginatedCreatorDto> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder.CreatorsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::STRIPES.Services.Endpoints.Models.PaginatedCreatorDto>(requestInfo, global::STRIPES.Services.Endpoints.Models.PaginatedCreatorDto.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder.CreatorsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder.CreatorsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class CreatorsRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
            /// <summary>Division in which the creator is registered</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("divisionId")]
            public string? DivisionId { get; set; }
#nullable restore
#else
            [QueryParameter("divisionId")]
            public string DivisionId { get; set; }
#endif
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
            /// <summary>Include creator&apos;s rating in response</summary>
            [QueryParameter("rating")]
            public bool? Rating { get; set; }
            /// <summary>Filter by tier</summary>
            [QueryParameter("tier")]
            public double? Tier { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class CreatorsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::STRIPES.Services.Endpoints.V2.Creators.CreatorsRequestBuilder.CreatorsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
