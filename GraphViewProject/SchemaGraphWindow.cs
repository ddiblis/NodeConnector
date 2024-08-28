using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace GV
{
    public class SchemaGraphWindow : EditorWindow
    {
        private SchemaGraphView _graphView;

        [MenuItem("Window/Schema Graph")]
        public static void OpenSchemaGraphWindow()
        {
            var window = GetWindow<SchemaGraphWindow>();
            window.titleContent = new GUIContent("Schema Graph");
        }

        private void OnEnable()
        {
            // Initialize the graph view and add it to the root element.
            _graphView = new SchemaGraphView
            {
                name = "Schema Graph"
            };

            // Set the graph view to fill the entire window.
            _graphView.style.flexGrow = 1.0f;
            rootVisualElement.Add(_graphView);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }
    }
}
