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