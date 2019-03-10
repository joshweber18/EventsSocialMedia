using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants
{
    public partial class Restaurant
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("average_cost_for_two")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AverageCostForTwo { get; set; }

        [JsonProperty("price_range")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PriceRange { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("thumb")]
        public Uri Thumb { get; set; }

        [JsonProperty("featured_image")]
        public Uri FeaturedImage { get; set; }

        [JsonProperty("photos_url")]
        public Uri PhotosUrl { get; set; }

        [JsonProperty("menu_url")]
        public Uri MenuUrl { get; set; }

        [JsonProperty("events_url")]
        public Uri EventsUrl { get; set; }

        [JsonProperty("user_rating")]
        public UserRating UserRating { get; set; }

        [JsonProperty("has_online_delivery")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long HasOnlineDelivery { get; set; }

        [JsonProperty("is_delivering_now")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IsDeliveringNow { get; set; }

        [JsonProperty("has_table_booking")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long HasTableBooking { get; set; }

        [JsonProperty("deeplink")]
        public string Deeplink { get; set; }

        [JsonProperty("cuisines")]
        public string Cuisines { get; set; }

        [JsonProperty("all_reviews_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AllReviewsCount { get; set; }

        [JsonProperty("photo_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PhotoCount { get; set; }

        [JsonProperty("phone_numbers")]
        public string PhoneNumbers { get; set; }

        [JsonProperty("photos")]
        public Photo[] Photos { get; set; }

        [JsonProperty("all_reviews")]
        public AllReview[] AllReviews { get; set; }
    }

    public partial class AllReview
    {
        [JsonProperty("rating")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rating { get; set; }

        [JsonProperty("review_text")]
        public string ReviewText { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("rating_color")]
        public string RatingColor { get; set; }

        [JsonProperty("review_time_friendly")]
        public string ReviewTimeFriendly { get; set; }

        [JsonProperty("rating_text")]
        public string RatingText { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Timestamp { get; set; }

        [JsonProperty("likes")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Likes { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("comments_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CommentsCount { get; set; }
    }

    public partial class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("zomato_handle")]
        public string ZomatoHandle { get; set; }

        [JsonProperty("foodie_level")]
        public string FoodieLevel { get; set; }

        [JsonProperty("foodie_level_num")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FoodieLevelNum { get; set; }

        [JsonProperty("foodie_color")]
        public string FoodieColor { get; set; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("profile_deeplink")]
        public string ProfileDeeplink { get; set; }

        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("zipcode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zipcode { get; set; }

        [JsonProperty("country_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CountryId { get; set; }
    }

    public partial class Photo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("thumb_url")]
        public Uri ThumbUrl { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("res_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ResId { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Timestamp { get; set; }

        [JsonProperty("friendly_time")]
        public string FriendlyTime { get; set; }

        [JsonProperty("width")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Width { get; set; }

        [JsonProperty("height")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Height { get; set; }

        [JsonProperty("comments_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CommentsCount { get; set; }

        [JsonProperty("likes_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LikesCount { get; set; }
    }

    public partial class UserRating
    {
        [JsonProperty("aggregate_rating")]
        public string AggregateRating { get; set; }

        [JsonProperty("rating_text")]
        public string RatingText { get; set; }

        [JsonProperty("rating_color")]
        public string RatingColor { get; set; }

        [JsonProperty("votes")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Votes { get; set; }
    }

    public partial class Restaurant
    {
        public static Restaurant FromJson(string json) => JsonConvert.DeserializeObject<Restaurant>(json, Restaurants.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Restaurant self) => JsonConvert.SerializeObject(self, Restaurants.Converter.Settings);
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }

            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
