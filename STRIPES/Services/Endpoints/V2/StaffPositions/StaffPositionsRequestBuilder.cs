// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using STRIPES.Services.Endpoints.Models;
using STRIPES.Services.Endpoints.V2.StaffPositions.Item;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace STRIPES.Services.Endpoints.V2.StaffPositions
{
    /// <summary>
    /// Builds and executes requests for operations under \v2\staffPositions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class StaffPositionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the STRIPES.Services.Endpoints.v2.staffPositions.item collection</summary>
        /// <param name="position">Staff Position Id</param>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.StaffPositions.Item.StaffPositionsItemRequestBuilder"/></returns>
        public global::STRIPES.Services.Endpoints.V2.StaffPositions.Item.StaffPositionsItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("id", position);
                return new global::STRIPES.Services.Endpoints.V2.StaffPositions.Item.StaffPositionsItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public StaffPositionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/staffPositions{?departmentId*,departmentTeamId*,page*,perPage*,type*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public StaffPositionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/v2/staffPositions{?departmentId*,departmentTeamId*,page*,perPage*,type*}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.PaginatedStaffPositionResponseDto"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::STRIPES.Services.Endpoints.Models.PaginatedStaffPositionResponseDto?> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder.StaffPositionsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::STRIPES.Services.Endpoints.Models.PaginatedStaffPositionResponseDto> GetAsync(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder.StaffPositionsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::STRIPES.Services.Endpoints.Models.PaginatedStaffPositionResponseDto>(requestInfo, global::STRIPES.Services.Endpoints.Models.PaginatedStaffPositionResponseDto.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder.StaffPositionsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder.StaffPositionsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class StaffPositionsRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
            /// <summary>ID of the department</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("departmentId")]
            public string? DepartmentId { get; set; }
#nullable restore
#else
            [QueryParameter("departmentId")]
            public string DepartmentId { get; set; }
#endif
            /// <summary>ID of the department team</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("departmentTeamId")]
            public string? DepartmentTeamId { get; set; }
#nullable restore
#else
            [QueryParameter("departmentTeamId")]
            public string DepartmentTeamId { get; set; }
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
            /// <summary>Staff Position Type</summary>
            [Obsolete("This property is deprecated, use TypeAsGetTypeQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("type")]
            public string? Type { get; set; }
#nullable restore
#else
            [QueryParameter("type")]
            public string Type { get; set; }
#endif
            /// <summary>Staff Position Type</summary>
            [QueryParameter("type")]
            public global::STRIPES.Services.Endpoints.V2.StaffPositions.GetTypeQueryParameterType? TypeAsGetTypeQueryParameterType { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class StaffPositionsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::STRIPES.Services.Endpoints.V2.StaffPositions.StaffPositionsRequestBuilder.StaffPositionsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
