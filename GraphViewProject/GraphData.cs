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

        public void CopyFrom(GraphData saved) {
            Chapters = new List<Chapter>(saved.Chapters);
        }

        public void PopulateGraphView(GraphView graphView) {
            var ChapterNode = new ChapterNode(graphView) {
                allowMidrolls = Chapters[0].AllowMidrolls
            };
            ChapterNode.UpdateFields();
            ChapterNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
            graphView.AddElement(ChapterNode);
            LoadSubChapNodes(Chapters[0], graphView);
        }

        private void LoadSubChapNodes(Chapter chapter, GraphView graphView) {
            foreach(SubChap subChap in chapter.SubChaps) {
                var SubChapNode = new SubChapNode(graphView) {
                Contact = subChap.Contact,
                TimeIndicator = subChap.TimeIndicator,
                UnlockInstaPostsAccount = subChap.UnlockInstaPostsAccount,
                UnlockPosts = subChap.UnlockPosts
                };
                SubChapNode.UpdateFields();
                SubChapNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(SubChapNode);
                LoadTextMessageNodes(subChap, graphView);
                LoadResponseNodes(subChap, graphView);
            }
        }

        private void LoadTextMessageNodes(SubChap subChap, GraphView graphView) {
            foreach (TextMessage text in subChap.TextList) {
                var TextMessageNode = new TextMessageNode(graphView) {
                    TextContent = text.TextContent,
                    TextDelay = text.TextDelay,
                    Type = text.Type
                };
                TextMessageNode.UpdateFields();
                TextMessageNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(TextMessageNode);
            }
        }

        private void LoadResponseNodes(SubChap subChap, GraphView graphView) {
            foreach (Response resp in subChap.Responses) {
                var ResponseNode = new ResponseNode(graphView) {
                    RespTree = resp.RespTree,
                    TextContent = resp.TextContent,
                    SubChapNum = resp.SubChapNum,
                    Type = resp.Type
                };
                ResponseNode.UpdateFields();
                ResponseNode.SetPosition(new Rect(new Vector2(200, 150), new Vector2(200, 150)));
                graphView.AddElement(ResponseNode);
            }
        }

    }
}
