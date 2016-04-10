using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace wpug.utility
{

    public class SlackMessage
    {
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("channel")]
        public string Channel;
        [JsonProperty("username")]
        public string Username;
        [JsonProperty("icon_emoji")]
        public string Icon;
    }

    public class SlackAttachmentMessage
    {
        [JsonProperty("attachments")]
        public List<SlackAttachment> Attachments;

        public SlackAttachmentMessage()
        {
            Attachments = new List<SlackAttachment>();
        }
    }

    public class SlackAttachment
    {
        [JsonProperty("fallback")]
        public string Fallback;
        [JsonProperty("color")]
        public string Colour;
        [JsonProperty("pretext")]
        public string PreText;
        [JsonProperty("author_name")]
        public string AuthorName;
        [JsonProperty("author_link")]
        public string AuthorLink;
        [JsonProperty("author_icon")]
        public string AuthorIcon;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("title_link")]
        public string Text;
        [JsonProperty("fields")]
        public List<SlackField> Fields;
        [JsonProperty("image_url")]
        public string ImageUrl;
        [JsonProperty("thumb_url")]
        public string ThumbUrl;

        public SlackAttachment()
        {
            Fields = new List<SlackField>();
        }
    }

    public class SlackField
    {
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("value")]
        public string Value;
        [JsonProperty("short")]
        public bool Short;
    }
}