using System.Collections.Generic;

    [System.Serializable]
    public class Chapter
    {
        public bool AllowMidrolls;
        public List<SubChap> SubChaps;
    }

    [System.Serializable]
    public class SubChap
    {
        public string Contact;
        public string TimeIndicator;
        public List<TextMessage> TextList;
        public string UnlockInstaPostsAccount;
        public List<int> UnlockPosts;
        public List<Response> Responses;
    }

    [System.Serializable]
    public class TextMessage
    {
        public int Type;
        public string TextContent;
        public float TextDelay;
    }

    [System.Serializable]
    public class Response
    {
        public bool RespTree;
        public string TextContent;
        public int SubChapNum;
        public int Type;
    }

    [System.Serializable]
    public class Location {
        public float x;
        public float y;
        public float Width;
        public float Height;
    }

    [System.Serializable]
    public class ChapterData
    {
        public bool AllowMidrolls;
        public Location location;
        public List<SubChapData> SubChaps;
    }

    [System.Serializable]
    public class SubChapData
    {
        public string Contact;
        public string TimeIndicator;
        public List<TextMessageData> TextList;
        public string UnlockInstaPostsAccount;
        public List<int> UnlockPosts;
        public List<ResponseData> Responses;
        public Location location;

    }

    [System.Serializable]
    public class TextMessageData
    {
        public int Type;
        public string TextContent;
        public float TextDelay;
        public Location location;
    }

    [System.Serializable]
    public class ResponseData
    {
        public bool RespTree;
        public string TextContent;
        public int SubChapNum;
        public int Type;
        public Location location;

    }
