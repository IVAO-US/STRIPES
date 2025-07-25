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
    public partial class FlightPlanListDto : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aircraftId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AircraftId { get; set; }
#nullable restore
#else
        public string AircraftId { get; set; }
#endif
        /// <summary>The arrivalId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ArrivalId { get; set; }
#nullable restore
#else
        public string ArrivalId { get; set; }
#endif
        /// <summary>The callsign property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Callsign { get; set; }
#nullable restore
#else
        public string Callsign { get; set; }
#endif
        /// <summary>The departureId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DepartureId { get; set; }
#nullable restore
#else
        public string DepartureId { get; set; }
#endif
        /// <summary>The eobt property</summary>
        public DateTimeOffset? Eobt { get; set; }
        /// <summary>The id property</summary>
        public double? Id { get; set; }
        /// <summary>The isArchived property</summary>
        public bool? IsArchived { get; set; }
        /// <summary>The userId property</summary>
        public double? UserId { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.Models.FlightPlanListDto"/> and sets the default values.
        /// </summary>
        public FlightPlanListDto()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.FlightPlanListDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::STRIPES.Services.Endpoints.Models.FlightPlanListDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::STRIPES.Services.Endpoints.Models.FlightPlanListDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aircraftId", n => { AircraftId = n.GetStringValue(); } },
                { "arrivalId", n => { ArrivalId = n.GetStringValue(); } },
                { "callsign", n => { Callsign = n.GetStringValue(); } },
                { "departureId", n => { DepartureId = n.GetStringValue(); } },
                { "eobt", n => { Eobt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetDoubleValue(); } },
                { "isArchived", n => { IsArchived = n.GetBoolValue(); } },
                { "userId", n => { UserId = n.GetDoubleValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("aircraftId", AircraftId);
            writer.WriteStringValue("arrivalId", ArrivalId);
            writer.WriteStringValue("callsign", Callsign);
            writer.WriteStringValue("departureId", DepartureId);
            writer.WriteDateTimeOffsetValue("eobt", Eobt);
            writer.WriteDoubleValue("id", Id);
            writer.WriteBoolValue("isArchived", IsArchived);
            writer.WriteDoubleValue("userId", UserId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
