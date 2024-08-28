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
            AddStyles();
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets(
                "JSONMapperStyles/JMVariables.uss"
            );
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

            rootVisualElement.Add(toolbar);
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
