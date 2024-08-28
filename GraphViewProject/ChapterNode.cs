using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace GV
{
    public class ChapterNode : BaseNode
    {
        public bool allowMidrolls = false;
        public List<SubChapNode> SubChaps = new List<SubChapNode>();

        public ChapterNode(GraphView graphView) : base(graphView)
        {
            title = "Chapter";

            // Create an input port for SubChaps
            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(SubChapNode));
            outputPort.portName = "SubChapters";
            inputContainer.Add(outputPort);

            // Other fields...
            VisualElement CustomDataContainer = new VisualElement();

            Foldout Foldout = new Foldout(){
                text = "Chapter Content"
            };

            Toggle allowMidrollsToggle = new Toggle() {
                value = allowMidrolls,
                label = "Allow Midrolls"
            };

            Foldout.Add(allowMidrollsToggle);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new ChapterNode(graphView);
        }
    }
}

