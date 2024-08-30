using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JSONMapper {
    public class ResponseNode : BaseNode {
        List<string> TypeOptions = new List<string>{ "Type of Text", "Sent Text 0", "Sent Image 2", "Sent Emoji 4" };

        public bool RespTree = false;
        public string TextContent;
        public int SubChapNum;
        public int Type;

        private TextField TextMessageField;
        private Toggle ResponseTreeToggle;
        private IntegerField NextSubChapField;
        private DropdownField TypeDropDown;
        public Port ParentSubChapPort;
        public Port NextSubChapterNodePort;

        public ResponseNode(GraphView graphView) : base(graphView) {
            title = "Response";

            ParentSubChapPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            ParentSubChapPort.portName = "Parent SubChap";
            inputContainer.Add(ParentSubChapPort);

            NextSubChapterNodePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(SubChapNode));
            NextSubChapterNodePort.portName = "Next SubChap";
            inputContainer.Add(NextSubChapterNodePort);

            var CustomDataContainer = new VisualElement();
            CustomDataContainer.AddToClassList("jm-node__custom-data-container");

            var Foldout = new Foldout() { text = "Response Content" };

            TextMessageField = new TextField("Response Text", 256, false, false, 'a') { value = TextContent };
            TextMessageField.RegisterValueChangedCallback(evt => TextContent = evt.newValue);
            
            TypeDropDown = new DropdownField("Text Type", TypeOptions, 0);
            TypeDropDown.RegisterValueChangedCallback(evt => Type = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));

            NextSubChapField = new IntegerField("Next Sub Chapter") { value = SubChapNum };
            NextSubChapField.RegisterValueChangedCallback(evt => SubChapNum = evt.newValue);

            ResponseTreeToggle = new Toggle("Response Tree") { value = RespTree };
            ResponseTreeToggle.RegisterValueChangedCallback(evt => RespTree = evt.newValue);

            TextMessageField.AddClasses(
                "jm-node__textfield",
                "jm-node__quote-textfield"
            );
            ResponseTreeToggle.AddClasses(
                "jm-node__textfield",
                "jm-node__quote-textfield"
            );
            TypeDropDown.AddClasses(
                "jm-node__textfield",
                "jm-node__quote-textfield"
            );
            NextSubChapField.AddClasses(
                "jm-node__textfield",
                "jm-node__quote-textfield"
            );

            Foldout.Add(ResponseTreeToggle);
            Foldout.Add(TextMessageField);
            Foldout.Add(TypeDropDown);
            Foldout.Add(NextSubChapField);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public void UpdateFields() {
            int TypeIndex = TypeOptions.FindIndex(x => x.Contains("" + Type));
            TextMessageField.value = TextContent;
            ResponseTreeToggle.value = RespTree;
            NextSubChapField.value = SubChapNum;
            TypeDropDown.value = TypeOptions[TypeIndex >= 0 ? TypeIndex : 0];
        }

        public ResponseData ToResponseNodeData() {
            Rect rect = this.GetPosition();
            return new ResponseData {
                RespTree = this.RespTree,
                TextContent = this.TextContent,
                SubChapNum = this.SubChapNum,
                Type = this.Type,
                location = new Location {
                    x = rect.x,
                    y = rect.y,
                    Width = rect.width,
                    Height = rect.height
                }
            };
        }

        public Response ToResponseData() {
            return new Response {
                RespTree = this.RespTree,
                TextContent = this.TextContent,
                SubChapNum = this.SubChapNum,
                Type = this.Type
            };
        }

        public override BaseNode InstantiateNodeCopy() {
            return new ResponseNode(graphView);
        }
    }
}
