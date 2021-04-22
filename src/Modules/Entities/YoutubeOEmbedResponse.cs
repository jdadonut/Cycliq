namespace Cycliq.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class YoutubeOEmbedResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public Uri AuthorUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("provider_url")]
        public Uri ProviderUrl { get; set; }

        [JsonProperty("thumbnail_height")]
        public long ThumbnailHeight { get; set; }

        [JsonProperty("thumbnail_width")]
        public long ThumbnailWidth { get; set; }

        [JsonProperty("thumbnail_url")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }
    }

    public partial class YoutubeOEmbedResponse
    {
        public static YoutubeOEmbedResponse FromJson(string json) => JsonConvert.DeserializeObject<YoutubeOEmbedResponse>(json, Cycliq.Entities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this YoutubeOEmbedResponse self) => JsonConvert.SerializeObject(self, Cycliq.Entities.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
