using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace JSONMapper {
    public class ChapterNode : BaseNode {
        public bool allowMidrolls;
        private Toggle allowMidrollsToggle;
        public List<SubChapNode> SubChaps = new List<SubChapNode>();

        public ChapterNode(GraphView graphView) : base(graphView) {
            title = "Chapter";

            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(SubChapNode));
            outputPort.portName = "SubChapters";
            inputContainer.Add(outputPort);

            var CustomDataContainer = new VisualElement();
            CustomDataContainer.AddToClassList("jm-node__custom-data-container");

            var Foldout = new Foldout() { text = "Chapter Content" };

            allowMidrollsToggle = new Toggle("Allow Midrolls") { value = allowMidrolls };
            allowMidrollsToggle.RegisterValueChangedCallback(evt => allowMidrolls = evt.newValue);

            allowMidrollsToggle.AddClasses(
                "jm-node__textfield",
                "jm-node__quote-textfield"
            );

            Foldout.Add(allowMidrollsToggle);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public void UpdateFields() {
            allowMidrollsToggle.value = allowMidrolls;
        }
        
        public Chapter ToChapterAsset() {
            return new Chapter {
                AllowMidrolls = this.allowMidrolls,
            };
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

