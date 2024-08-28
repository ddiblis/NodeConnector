using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace JSONMapper {
    public class ChapterNode : BaseNode {
        public bool allowMidrolls = false;
        public List<SubChapNode> SubChaps = new List<SubChapNode>();

        public ChapterNode(GraphView graphView) : base(graphView) {
            title = "Chapter";

            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(SubChapNode));
            outputPort.portName = "SubChapters";
            inputContainer.Add(outputPort);

            var CustomDataContainer = new VisualElement();
            var Foldout = new Foldout() { text = "Chapter Content" };

            var allowMidrollsToggle = new Toggle("Allow Midrolls") { value = allowMidrolls };
            allowMidrollsToggle.RegisterValueChangedCallback(evt => allowMidrolls = evt.newValue);

            Foldout.Add(allowMidrollsToggle);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public Chapter ToChapterData() {
            return new Chapter {
                AllowMidrolls = this.allowMidrolls,
                SubChaps = this.SubChaps.ConvertAll(subChapNode => subChapNode.ToSubChapData())
            };
        }

        public override BaseNode InstantiateNodeCopy() {
            return new ChapterNode(graphView);
        }
    }
}

