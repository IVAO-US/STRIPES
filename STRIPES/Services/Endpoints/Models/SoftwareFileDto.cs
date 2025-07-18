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
    public partial class SoftwareFileDto : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The changeLog property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ChangeLog { get; set; }
#nullable restore
#else
        public string ChangeLog { get; set; }
#endif
        /// <summary>The downloads property</summary>
        public double? Downloads { get; set; }
        /// <summary>The extension property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Extension { get; set; }
#nullable restore
#else
        public string Extension { get; set; }
#endif
        /// <summary>The id property</summary>
        public double? Id { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The softwareId property</summary>
        public double? SoftwareId { get; set; }
        /// <summary>The valid property</summary>
        public bool? Valid { get; set; }
        /// <summary>The version property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Version { get; set; }
#nullable restore
#else
        public string Version { get; set; }
#endif
        /// <summary>The versionSuffix property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? VersionSuffix { get; set; }
#nullable restore
#else
        public string VersionSuffix { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::STRIPES.Services.Endpoints.Models.SoftwareFileDto"/> and sets the default values.
        /// </summary>
        public SoftwareFileDto()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::STRIPES.Services.Endpoints.Models.SoftwareFileDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::STRIPES.Services.Endpoints.Models.SoftwareFileDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::STRIPES.Services.Endpoints.Models.SoftwareFileDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "changeLog", n => { ChangeLog = n.GetStringValue(); } },
                { "downloads", n => { Downloads = n.GetDoubleValue(); } },
                { "extension", n => { Extension = n.GetStringValue(); } },
                { "id", n => { Id = n.GetDoubleValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "softwareId", n => { SoftwareId = n.GetDoubleValue(); } },
                { "valid", n => { Valid = n.GetBoolValue(); } },
                { "version", n => { Version = n.GetStringValue(); } },
                { "versionSuffix", n => { VersionSuffix = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("changeLog", ChangeLog);
            writer.WriteDoubleValue("downloads", Downloads);
            writer.WriteStringValue("extension", Extension);
            writer.WriteDoubleValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteDoubleValue("softwareId", SoftwareId);
            writer.WriteBoolValue("valid", Valid);
            writer.WriteStringValue("version", Version);
            writer.WriteStringValue("versionSuffix", VersionSuffix);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
