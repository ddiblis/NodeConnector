using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.IO;
using UnityEditor.UIElements;
using System.Linq;

namespace JSONMapper {
    [System.Serializable]
    public class GraphData : ScriptableObject {
        public List<Chapter> Chapters = new List<Chapter>();
        public List<SubChap> SubChapters = new List<SubChap>();
        public List<TextMessage> TextMessages = new List<TextMessage>();
        public List<Response> Responses = new List<Response>();

        public void CopyFrom(GraphData saved) {
            Chapters = new List<Chapter>(saved.Chapters);
            SubChapters = new List<SubChap>(saved.SubChapters);
            TextMessages = new List<TextMessage>(saved.TextMessages);
            Responses = new List<Response>(saved.Responses);
        }

        public void PopulateGraphView(GraphView graphView) {
            
            foreach (Chapter chapter in Chapters) {
                var newNode = new ChapterNode(graphView) {
                    allowMidrolls = chapter.AllowMidrolls
                };
                newNode.UpdateFields();
                newNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(newNode);
            }

            foreach (SubChap subChap in SubChapters) {
                var newNode = new SubChapNode(graphView) {
                    Contact = subChap.Contact,
                    TimeIndicator = subChap.TimeIndicator,
                    UnlockInstaPostsAccount = subChap.UnlockInstaPostsAccount,
                    UnlockPosts = subChap.UnlockPosts
                };
                newNode.UpdateFields();
                newNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(newNode);
            }

            foreach (TextMessage text in TextMessages) {
                var newNode = new TextMessageNode(graphView) {
                    TextContent = text.TextContent,
                    TextDelay = text.TextDelay,
                    Type = text.Type
                };
                newNode.UpdateFields();
                newNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(newNode);
            }
            
            foreach (Response resp in Responses) {
                var newNode = new ResponseNode(graphView) {
                    RespTree = resp.RespTree,
                    TextContent = resp.TextContent,
                    SubChapNum = resp.SubChapNum,
                    Type = resp.Type
                };
                newNode.UpdateFields();
                newNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(newNode);
            }
        }

        private void AddNode(GraphView graphView, BaseNode node, Vector2 position) {
            node.SetPosition(new Rect(position, new Vector2(200, 150)));
            graphView.AddElement(node);
        }
    }
}
