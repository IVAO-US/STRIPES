// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using STRIPES.Services.Endpoints.Models;
using STRIPES.Services.Endpoints.V2.Centers.Item.Notams;
using STRIPES.Services.Endpoints.V2.Centers.Item.SpecialAreas;
using STRIPES.Services.Endpoints.V2.Centers.Item.Squawks;
using STRIPES.Services.Endpoints.V2.Centers.Item.Subcenters;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace STRIPES.Services.Endpoints.V2.Centers.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \v2\centers\{id}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CentersItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The notams property</summary>
        public global::STRIPES.Services.Endpoints.V2.Centers.Item.Notams.NotamsRequestBuilder Notams
        {
            get => new global::STRIPES.Services.Endpoints.V2.Centers.Item.Notams.NotamsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The specialAreas property</summary>
        public global::STRIPES.Services.Endpoints.V2.Centers.Item.SpecialAreas.SpecialAreasRequestBuilder SpecialAreas
        {
            get => new global::STRIPES.Services.Endpoints.V2.Centers.Item.SpecialAreas.SpecialAreasRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The squawks property</summary>
        public global::STRIPES.Services.Endpoints.V2.Centers.Item.Squawks.SquawksRequestBuilder Squawks
        {
            get => new global::STRIPES.Services.Endpoints.V2.Centers.Item.Squawks.SquawksRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The subcenters property</summary>
        public global::STRIPES.Services.Endpoints.V2.Centers.Item.Subcenters.SubcentersRequestBuilder Subcenters
        {
            get => new global::STRIPES.Services.Endpoints.V2.Centers.Item.Subcenters.SubcentersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Centers.Item.CentersItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CentersItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/centers/{id}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.Centers.Item.CentersItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CentersItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/centers/{id}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.CenterDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto">When receiving a 401 status code</exception>
        /// <exception cref="global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto">When receiving a 404 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::STRIPES.Services.Endpoints.Models.CenterDto?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::STRIPES.Services.Endpoints.Models.CenterDto> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto.CreateFromDiscriminatorValue },
                { "404", global::STRIPES.Services.Endpoints.Models.SwaggerResponsesDto.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::STRIPES.Services.Endpoints.Models.CenterDto>(requestInfo, global::STRIPES.Services.Endpoints.Models.CenterDto.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
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
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.Centers.Item.CentersItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::STRIPES.Services.Endpoints.V2.Centers.Item.CentersItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::STRIPES.Services.Endpoints.V2.Centers.Item.CentersItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class CentersItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
