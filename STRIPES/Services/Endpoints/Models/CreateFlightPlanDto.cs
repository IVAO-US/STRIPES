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
    public partial class CreateFlightPlanDto : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aircraftEquipments property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AircraftEquipments { get; set; }
#nullable restore
#else
        public List<string> AircraftEquipments { get; set; }
#endif
        /// <summary>The aircraftId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AircraftId { get; set; }
#nullable restore
#else
        public string AircraftId { get; set; }
#endif
        /// <summary>The aircraftNumber property</summary>
        public double? AircraftNumber { get; set; }
        /// <summary>The aircraftTransponderTypes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AircraftTransponderTypes { get; set; }
#nullable restore
#else
        public List<string> AircraftTransponderTypes { get; set; }
#endif
        /// <summary>The aircraftWakeTurbulence property</summary>
        public global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_aircraftWakeTurbulence? AircraftWakeTurbulence { get; set; }
        /// <summary>The alternative2Id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Alternative2Id { get; set; }
#nullable restore
#else
        public string Alternative2Id { get; set; }
#endif
        /// <summary>The alternativeId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AlternativeId { get; set; }
#nullable restore
#else
        public string AlternativeId { get; set; }
#endif
        /// <summary>null, if and only if, altitude type is set to VFR</summary>
        public double? Altitude { get; set; }
        /// <summary>The altitudeType property</summary>
        public global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_altitudeType? AltitudeType { get; set; }
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
        /// <summary>The cruisingSpeed property</summary>
        public double? CruisingSpeed { get; set; }
        /// <summary>The cruisingSpeedType property</summary>
        public global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_cruisingSpeedType? CruisingSpeedType { get; set; }
        /// <summary>The departureId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DepartureId { get; set; }
#nullable restore
#else
        public string DepartureId { get; set; }
#endif
        /// <summary>Seconds since 0000UTC</summary>
        public double? DepartureTime { get; set; }
        /// <summary>time in seconds</summary>
        public double? Eet { get; set; }
        /// <summary>time in seconds</summary>
        public double? Endurance { get; set; }
        /// <summary>The flightRules property</summary>
        public global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightRules? FlightRules { get; set; }
        /// <summary>The flightType property</summary>
        public global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightType? FlightType { get; set; }
        /// <summary>The pob property</summary>
        public double? Pob { get; set; }
        /// <summary>The remarks property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Remarks { get; set; }
#nullable restore
#else
        public string Remarks { get; set; }
#endif
        /// <summary>The route property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Route { get; set; }
#nullable restore
#else
        public string Route { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto"/> and sets the default values.
        /// </summary>
        public CreateFlightPlanDto()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aircraftEquipments", n => { AircraftEquipments = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "aircraftId", n => { AircraftId = n.GetStringValue(); } },
                { "aircraftNumber", n => { AircraftNumber = n.GetDoubleValue(); } },
                { "aircraftTransponderTypes", n => { AircraftTransponderTypes = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "aircraftWakeTurbulence", n => { AircraftWakeTurbulence = n.GetEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_aircraftWakeTurbulence>(); } },
                { "alternative2Id", n => { Alternative2Id = n.GetStringValue(); } },
                { "alternativeId", n => { AlternativeId = n.GetStringValue(); } },
                { "altitude", n => { Altitude = n.GetDoubleValue(); } },
                { "altitudeType", n => { AltitudeType = n.GetEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_altitudeType>(); } },
                { "arrivalId", n => { ArrivalId = n.GetStringValue(); } },
                { "callsign", n => { Callsign = n.GetStringValue(); } },
                { "cruisingSpeed", n => { CruisingSpeed = n.GetDoubleValue(); } },
                { "cruisingSpeedType", n => { CruisingSpeedType = n.GetEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_cruisingSpeedType>(); } },
                { "departureId", n => { DepartureId = n.GetStringValue(); } },
                { "departureTime", n => { DepartureTime = n.GetDoubleValue(); } },
                { "eet", n => { Eet = n.GetDoubleValue(); } },
                { "endurance", n => { Endurance = n.GetDoubleValue(); } },
                { "flightRules", n => { FlightRules = n.GetEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightRules>(); } },
                { "flightType", n => { FlightType = n.GetEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightType>(); } },
                { "pob", n => { Pob = n.GetDoubleValue(); } },
                { "remarks", n => { Remarks = n.GetStringValue(); } },
                { "route", n => { Route = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("aircraftEquipments", AircraftEquipments);
            writer.WriteStringValue("aircraftId", AircraftId);
            writer.WriteDoubleValue("aircraftNumber", AircraftNumber);
            writer.WriteCollectionOfPrimitiveValues<string>("aircraftTransponderTypes", AircraftTransponderTypes);
            writer.WriteEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_aircraftWakeTurbulence>("aircraftWakeTurbulence", AircraftWakeTurbulence);
            writer.WriteStringValue("alternative2Id", Alternative2Id);
            writer.WriteStringValue("alternativeId", AlternativeId);
            writer.WriteDoubleValue("altitude", Altitude);
            writer.WriteEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_altitudeType>("altitudeType", AltitudeType);
            writer.WriteStringValue("arrivalId", ArrivalId);
            writer.WriteStringValue("callsign", Callsign);
            writer.WriteDoubleValue("cruisingSpeed", CruisingSpeed);
            writer.WriteEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_cruisingSpeedType>("cruisingSpeedType", CruisingSpeedType);
            writer.WriteStringValue("departureId", DepartureId);
            writer.WriteDoubleValue("departureTime", DepartureTime);
            writer.WriteDoubleValue("eet", Eet);
            writer.WriteDoubleValue("endurance", Endurance);
            writer.WriteEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightRules>("flightRules", FlightRules);
            writer.WriteEnumValue<global::STRIPES.Services.Endpoints.Models.CreateFlightPlanDto_flightType>("flightType", FlightType);
            writer.WriteDoubleValue("pob", Pob);
            writer.WriteStringValue("remarks", Remarks);
            writer.WriteStringValue("route", Route);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
