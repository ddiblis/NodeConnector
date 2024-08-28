using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GV
{
    public class TextMessageNode : BaseNode
    {
        public int Type;
        public string TextContent;
        public float TextDelay;
        public TextMessageNode(GraphView graphView) : base(graphView)
        {
            
            title = "TextMessage";
            
            // Create an input port for SubChap
            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            inputPort.portName = "Parent SubChap";
            inputContainer.Add(inputPort);

            // Other fields...
            VisualElement CustomDataContainer = new VisualElement();

            Foldout Foldout = new Foldout(){
                text = "Text Message Content"
            };

            TextField TextMessageField = new TextField() {
                value = TextContent,
                label = "Text message"
            };

            List<string> TypeOptions = new List<string>{
                "Type of Text", "Recieved Text 1", "Recieved Image 3", "Recieved Emoji 5", "Chapter end 6"
            };
            DropdownField TypeDropDown = new DropdownField("Type Of Text", TypeOptions, 0) {
                label = "Text Type"
            };

            TypeDropDown.RegisterValueChangedCallback(evt => Type = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));

            List<string> DelayOptions = new List<string>{
                "Delay Options", "Very Fast 0.5", "Fast 1.0", "Medium 2.0", "Slow 2.7", "Very slow 3.5", "Dramatic Pause 5.0"
            };
            DropdownField DelayDropDown = new DropdownField("Delay amount", DelayOptions, 0) {
                label = "Text Delay"
            };

            DelayDropDown.RegisterValueChangedCallback(evt => TextDelay = float.Parse(Regex.Match(evt.newValue, @"\d+[.][^2]").Value));

            TextMessageField.label = "Text message";
            TypeDropDown.label = "Text Type";
            DelayDropDown.label = "Text Delay";

            Foldout.Add(TextMessageField);
            Foldout.Add(TypeDropDown);
            Foldout.Add(DelayDropDown);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new TextMessageNode(graphView);
        }
    }
}
