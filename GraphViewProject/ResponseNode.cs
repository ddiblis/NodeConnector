using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace JSONMapper {
    public class ResponseNode : BaseNode {
        public bool RespTree = false;
        public string TextContent;
        public int SubChapNum;
        public int Type;
        public ResponseNode(GraphView graphView) : base(graphView) {
            title = "Response";

            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            inputPort.portName = "Parent SubChap";
            inputContainer.Add(inputPort);

            var CustomDataContainer = new VisualElement();
            var Foldout = new Foldout() { text = "Response Content" };

            var TextMessageField = new TextField("Response Text", 256, false, false, 'a') { value = TextContent };
            TextMessageField.RegisterValueChangedCallback(evt => TextContent = evt.newValue);
            
            var TypeOptions = new List<string>{ "Type of Text", "Sent Text 0", "Sent Image 2", "Sent Emoji 4" };
            var TypeDropDown = new DropdownField("Text Type", TypeOptions, 0);
            TypeDropDown.RegisterValueChangedCallback(evt => Type = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));

            var NextSubChapField = new IntegerField("Next Sub Chapter") { value = SubChapNum };
            NextSubChapField.RegisterValueChangedCallback(evt => SubChapNum = evt.newValue);

            var ResponseTreeToggle = new Toggle("Response Tree") { value = RespTree };
            ResponseTreeToggle.RegisterValueChangedCallback(evt => RespTree = evt.newValue);

            Foldout.Add(ResponseTreeToggle);
            Foldout.Add(TextMessageField);
            Foldout.Add(TypeDropDown);
            Foldout.Add(NextSubChapField);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
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
