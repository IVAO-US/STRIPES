// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using STRIPES.Services.Endpoints.Models;
using STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item;
using STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Latest;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace STRIPES.Services.Endpoints.V2.Sectors.Item.Files
{
    /// <summary>
    /// Builds and executes requests for operations under \v2\sectors\{-id}\files
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class FilesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The latest property</summary>
        public global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Latest.LatestRequestBuilder Latest
        {
            get => new global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Latest.LatestRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the STRIPES.Services.Endpoints.v2.sectors.item.files.item collection</summary>
        /// <param name="position">SectorFile ID</param>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder"/></returns>
        public global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder this[double position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>Gets an item from the STRIPES.Services.Endpoints.v2.sectors.item.files.item collection</summary>
        /// <param name="position">SectorFile ID</param>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder"/></returns>
        [Obsolete("This indexer is deprecated and will be removed in the next major version. Use the one with the typed parameter instead.")]
        public global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                if (!string.IsNullOrWhiteSpace(position)) urlTplParams.Add("id", position);
                return new global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.Item.FilesItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.FilesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FilesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/sectors/{%2Did}/files", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.FilesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FilesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/sectors/{%2Did}/files", rawUrl)
        {
        }
        /// <returns>A List&lt;global::STRIPES.Services.Endpoints.Models.SectorFileDto&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto">When receiving a 401 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::STRIPES.Services.Endpoints.Models.SectorFileDto>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::STRIPES.Services.Endpoints.Models.SectorFileDto>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto.CreateFromDiscriminatorValue },
            };
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::STRIPES.Services.Endpoints.Models.SectorFileDto>(requestInfo, global::STRIPES.Services.Endpoints.Models.SectorFileDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.FilesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.FilesRequestBuilder WithUrl(string rawUrl)
        {
            return new global::STRIPES.Services.Endpoints.V2.Sectors.Item.Files.FilesRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FilesRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
