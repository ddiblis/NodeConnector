using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;


namespace JSONMapper {
    public class SchemaGraphWindow : EditorWindow {
        private SchemaGraphView graphView;

        [MenuItem("Window/JSONMapper")]
        public static void Open() {
            var window = GetWindow<SchemaGraphWindow>();
            window.titleContent = new GUIContent("JSONMapper");
        }

        private void OnEnable() {
            ConstructGraphView();
            GenerateToolbar();
        }

        private void ConstructGraphView() {
            graphView = new SchemaGraphView
            {
                name = "JSONMapper"
            };
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void GenerateToolbar() {
            var toolbar = new Toolbar();

            var saveButton = new Button(() => SaveGraphToJson())
            {
                text = "Save"
            };
            toolbar.Add(saveButton);

            var loadButton = new Button(() => LoadGraphFromJson()) {
                text = "Load"
            };
            toolbar.Add(loadButton);

            rootVisualElement.Add(toolbar);
        }

        private void LoadGraphFromJson() {
            string path = EditorUtility.OpenFilePanel("Load Graph", "", "json");
            if (string.IsNullOrEmpty(path)) return;

            string json = File.ReadAllText(path);
            Chapter chapterData = JsonUtility.FromJson<Chapter>(json);

            if (chapterData != null) {
                // Clear the existing nodes
                graphView.DeleteElements(graphView.nodes.ToList());

                // Define grid spacing
                float gridSpacing = 300f; // Adjust as needed

                // Keep track of current position
                Vector2 currentPosition = new Vector2(50, 50); // Starting position

                // Create a dictionary to keep track of created nodes by contact
                var createdNodes = new Dictionary<string, SubChapNode>();

                // Instantiate nodes from Chapter data
                foreach (var subChapData in chapterData.SubChaps) {
                    // Create SubChapNode
                    var subChapNode = new SubChapNode(graphView) {
                        Contact = subChapData.Contact,
                        TimeIndicator = subChapData.TimeIndicator,
                        UnlockInstaPostsAccount = subChapData.UnlockInstaPostsAccount,
                        UnlockPosts = subChapData.UnlockPosts
                    };
                    graphView.AddElement(subChapNode);
                    subChapNode.SetPosition(new Rect(currentPosition, new Vector2(200, 150)));

                    // Add SubChapNode to the dictionary
                    createdNodes[subChapData.Contact] = subChapNode;

                    // Move to the next position in the grid
                    currentPosition.x += gridSpacing;
                    if (currentPosition.x > 2000) { // Wrap to a new row
                        currentPosition.x = 50;
                        currentPosition.y += gridSpacing;
                    }

                    // Create TextMessageNodes
                    foreach (var textMessageData in subChapData.TextList) {
                        var textMessageNode = CreateTextMessageNode(textMessageData);
                        graphView.AddElement(textMessageNode);
                        textMessageNode.SetPosition(new Rect(currentPosition, new Vector2(200, 100)));

                        // Optional: Update currentPosition if you want to space out text message nodes
                        currentPosition.x += gridSpacing;
                    }

                    // Create ResponseNodes
                    foreach (var responseData in subChapData.Responses) {
                        var responseNode = CreateResponseNode(responseData);
                        graphView.AddElement(responseNode);
                        responseNode.SetPosition(new Rect(currentPosition, new Vector2(200, 100)));

                        // Optional: Update currentPosition if you want to space out response nodes
                        currentPosition.x += gridSpacing;
                    }
                }

                // Create ChapterNode
                var chapterNode = new ChapterNode(graphView) {
                    allowMidrolls = chapterData.AllowMidrolls,
                    SubChaps = chapterData.SubChaps.Select(data => createdNodes.ContainsKey(data.Contact) ? createdNodes[data.Contact] : null).ToList()
                };
                graphView.AddElement(chapterNode);

                // Position chapter node
                chapterNode.SetPosition(new Rect(new Vector2(50, currentPosition.y + 200), new Vector2(200, 150)));

                // Reconnect nodes if necessary
                ReconnectNodes();
            }
        }


        private SubChapNode FindSubChapNodeByContact(string contact) {
            return graphView.nodes.OfType<SubChapNode>().FirstOrDefault(node => node.Contact == contact);
        }

        private TextMessageNode CreateTextMessageNode(TextMessage data) {
            var textMessageNode = new TextMessageNode(graphView) {
                Type = data.Type,
                TextContent = data.TextContent,
                TextDelay = data.TextDelay
            };
            graphView.AddElement(textMessageNode);
            return textMessageNode;
        }

        private ResponseNode CreateResponseNode(Response data) {
            var responseNode = new ResponseNode(graphView) {
                RespTree = data.RespTree,
                TextContent = data.TextContent,
                SubChapNum = data.SubChapNum,
                Type = data.Type
            };
            graphView.AddElement(responseNode);
            return responseNode;
        }

        private void ReconnectNodes() {
            // You may need to implement logic to reconnect nodes based on the data structure and how you saved connections.
        }



        private void SaveGraphToJson() {
            Chapter Chapter = new Chapter();

            foreach (var node in graphView.nodes)
            {
                if (node is ChapterNode chapterNode)
                {
                    Chapter = chapterNode.ToChapterData();
                }
            }

            string json = JsonUtility.ToJson(Chapter, true);
            string path = EditorUtility.SaveFilePanel("Save Graph", "", "GraphData.json", "json");
            if (!string.IsNullOrEmpty(path)) {
                File.WriteAllText(path, json);
                Debug.Log("Graph saved to " + path);
            }
        }
    }
}
