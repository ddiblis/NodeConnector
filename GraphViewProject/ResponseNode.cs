using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GV
{
    public class ResponseNode : BaseNode
    {
        public bool RespTree = false;
        public string TextContent;
        public int SubChapNum;
        public int Type;
        public ResponseNode(GraphView graphView) : base(graphView)
        {
            title = "Response";

            // Create an input port for SubChap
            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            inputPort.portName = "Parent SubChap";
            inputContainer.Add(inputPort);

            // Other fields...
            VisualElement CustomDataContainer = new VisualElement();

            Foldout Foldout = new Foldout(){
                text = "Response Content"
            };

            TextField TextMessageField = new TextField() {
                label = "Response Text",
                value = TextContent
            };

            List<string> TypeOptions = new List<string>{
                "Type of Text", "Sent Text 0", "Sent Image 2", "Sent Emoji 4"
            };
            DropdownField TypeDropDown = new DropdownField("Type Of Text", TypeOptions, 0) {
                label = "Text Type"
            };
            
            TypeDropDown.RegisterValueChangedCallback(evt => Type = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));

            IntegerField NextSubChapField = new IntegerField(){
                label = "Next Sub Chapter (int)"
            };

            Toggle ResponseTreeToggle = new Toggle() {
                value = RespTree,
                label = "Response Tree"
            };

            Foldout.Add(ResponseTreeToggle);
            Foldout.Add(TextMessageField);
            Foldout.Add(TypeDropDown);
            Foldout.Add(NextSubChapField);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new ResponseNode(graphView);
        }
    }
}
