// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace STRIPES.Services.Endpoints.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class NotamDto : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The airports property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Airports { get; set; }
#nullable restore
#else
        public List<string> Airports { get; set; }
#endif
        /// <summary>The centers property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Centers { get; set; }
#nullable restore
#else
        public List<string> Centers { get; set; }
#endif
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The endTime property</summary>
        public double? EndTime { get; set; }
        /// <summary>The id property</summary>
        public double? Id { get; set; }
        /// <summary>The military property</summary>
        public bool? Military { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The regionMap property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::STRIPES.Services.Endpoints.Models.BaseRegionMapDto>? RegionMap { get; set; }
#nullable restore
#else
        public List<global::STRIPES.Services.Endpoints.Models.BaseRegionMapDto> RegionMap { get; set; }
#endif
        /// <summary>The regionMapPolygon property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public UntypedNode? RegionMapPolygon { get; set; }
#nullable restore
#else
        public UntypedNode RegionMapPolygon { get; set; }
#endif
        /// <summary>The specialAreaId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SpecialAreaId { get; set; }
#nullable restore
#else
        public string SpecialAreaId { get; set; }
#endif
        /// <summary>The startTime property</summary>
        public double? StartTime { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.Models.NotamDto"/> and sets the default values.
        /// </summary>
        public NotamDto()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.NotamDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::STRIPES.Services.Endpoints.Models.NotamDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::STRIPES.Services.Endpoints.Models.NotamDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "airports", n => { Airports = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "centers", n => { Centers = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "endTime", n => { EndTime = n.GetDoubleValue(); } },
                { "id", n => { Id = n.GetDoubleValue(); } },
                { "military", n => { Military = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "regionMap", n => { RegionMap = n.GetCollectionOfObjectValues<global::STRIPES.Services.Endpoints.Models.BaseRegionMapDto>(global::STRIPES.Services.Endpoints.Models.BaseRegionMapDto.CreateFromDiscriminatorValue)?.AsList(); } },
                { "regionMapPolygon", n => { RegionMapPolygon = n.GetObjectValue<UntypedNode>(UntypedNode.CreateFromDiscriminatorValue); } },
                { "specialAreaId", n => { SpecialAreaId = n.GetStringValue(); } },
                { "startTime", n => { StartTime = n.GetDoubleValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("airports", Airports);
            writer.WriteCollectionOfPrimitiveValues<string>("centers", Centers);
            writer.WriteStringValue("description", Description);
            writer.WriteDoubleValue("endTime", EndTime);
            writer.WriteDoubleValue("id", Id);
            writer.WriteBoolValue("military", Military);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::STRIPES.Services.Endpoints.Models.BaseRegionMapDto>("regionMap", RegionMap);
            writer.WriteObjectValue<UntypedNode>("regionMapPolygon", RegionMapPolygon);
            writer.WriteStringValue("specialAreaId", SpecialAreaId);
            writer.WriteDoubleValue("startTime", StartTime);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
