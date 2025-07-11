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
    public partial class AirportDto : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The atcCallsign property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AtcCallsign { get; set; }
#nullable restore
#else
        public string AtcCallsign { get; set; }
#endif
        /// <summary>The centerId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CenterId { get; set; }
#nullable restore
#else
        public string CenterId { get; set; }
#endif
        /// <summary>The city property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? City { get; set; }
#nullable restore
#else
        public string City { get; set; }
#endif
        /// <summary>The country property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::STRIPES.Services.Endpoints.Models.CountryDto? Country { get; set; }
#nullable restore
#else
        public global::STRIPES.Services.Endpoints.Models.CountryDto Country { get; set; }
#endif
        /// <summary>The countryId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CountryId { get; set; }
#nullable restore
#else
        public string CountryId { get; set; }
#endif
        /// <summary>The elevation property</summary>
        public double? Elevation { get; set; }
        /// <summary>The faaCode property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FaaCode { get; set; }
#nullable restore
#else
        public string FaaCode { get; set; }
#endif
        /// <summary>The firId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FirId { get; set; }
#nullable restore
#else
        public string FirId { get; set; }
#endif
        /// <summary>The iata property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Iata { get; set; }
#nullable restore
#else
        public string Iata { get; set; }
#endif
        /// <summary>The icao property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Icao { get; set; }
#nullable restore
#else
        public string Icao { get; set; }
#endif
        /// <summary>The latitude property</summary>
        public double? Latitude { get; set; }
        /// <summary>The longitude property</summary>
        public double? Longitude { get; set; }
        /// <summary>The magnetic property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Magnetic { get; set; }
#nullable restore
#else
        public string Magnetic { get; set; }
#endif
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
        /// <summary>The runways property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Runways { get; set; }
#nullable restore
#else
        public List<string> Runways { get; set; }
#endif
        /// <summary>The state property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? State { get; set; }
#nullable restore
#else
        public string State { get; set; }
#endif
        /// <summary>The transitionAltitude property</summary>
        public double? TransitionAltitude { get; set; }
        /// <summary>The web property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Web { get; set; }
#nullable restore
#else
        public string Web { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.Models.AirportDto"/> and sets the default values.
        /// </summary>
        public AirportDto()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.AirportDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::STRIPES.Services.Endpoints.Models.AirportDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::STRIPES.Services.Endpoints.Models.AirportDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "atcCallsign", n => { AtcCallsign = n.GetStringValue(); } },
                { "centerId", n => { CenterId = n.GetStringValue(); } },
                { "city", n => { City = n.GetStringValue(); } },
                { "country", n => { Country = n.GetObjectValue<global::STRIPES.Services.Endpoints.Models.CountryDto>(global::STRIPES.Services.Endpoints.Models.CountryDto.CreateFromDiscriminatorValue); } },
                { "countryId", n => { CountryId = n.GetStringValue(); } },
                { "elevation", n => { Elevation = n.GetDoubleValue(); } },
                { "faaCode", n => { FaaCode = n.GetStringValue(); } },
                { "firId", n => { FirId = n.GetStringValue(); } },
                { "iata", n => { Iata = n.GetStringValue(); } },
                { "icao", n => { Icao = n.GetStringValue(); } },
                { "latitude", n => { Latitude = n.GetDoubleValue(); } },
                { "longitude", n => { Longitude = n.GetDoubleValue(); } },
                { "magnetic", n => { Magnetic = n.GetStringValue(); } },
                { "military", n => { Military = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "runways", n => { Runways = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "state", n => { State = n.GetStringValue(); } },
                { "transitionAltitude", n => { TransitionAltitude = n.GetDoubleValue(); } },
                { "web", n => { Web = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("atcCallsign", AtcCallsign);
            writer.WriteStringValue("centerId", CenterId);
            writer.WriteStringValue("city", City);
            writer.WriteObjectValue<global::STRIPES.Services.Endpoints.Models.CountryDto>("country", Country);
            writer.WriteStringValue("countryId", CountryId);
            writer.WriteDoubleValue("elevation", Elevation);
            writer.WriteStringValue("faaCode", FaaCode);
            writer.WriteStringValue("firId", FirId);
            writer.WriteStringValue("iata", Iata);
            writer.WriteStringValue("icao", Icao);
            writer.WriteDoubleValue("latitude", Latitude);
            writer.WriteDoubleValue("longitude", Longitude);
            writer.WriteStringValue("magnetic", Magnetic);
            writer.WriteBoolValue("military", Military);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("runways", Runways);
            writer.WriteStringValue("state", State);
            writer.WriteDoubleValue("transitionAltitude", TransitionAltitude);
            writer.WriteStringValue("web", Web);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
