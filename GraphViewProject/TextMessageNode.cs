using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace JSONMapper {
    public class TextMessageNode : BaseNode {
        public int Type;
        public string TextContent;
        public float TextDelay;
        public TextMessageNode(GraphView graphView) : base(graphView) {
            title = "TextMessage";

            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            inputPort.portName = "Parent SubChap";
            inputContainer.Add(inputPort);

            var CustomDataContainer = new VisualElement();
            var Foldout = new Foldout() { text = "Text Message Content" };

            var TextMessageField = new TextField("Text message") { value = TextContent };
            TextMessageField.RegisterValueChangedCallback(evt => TextContent = evt.newValue);

            var TypeOptions = new List<string>{ "Type of Text", "Recieved Text 1", "Recieved Image 3", "Recieved Emoji 5", "Chapter end 6" };
            var TypeDropDown = new DropdownField("Text Type", TypeOptions, 0);
            TypeDropDown.RegisterValueChangedCallback(evt => Type = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));

            var DelayOptions = new List<string>{ "Delay Options", "Very Fast 0.5", "Fast 1.0", "Medium 2.0", "Slow 2.7", "Very slow 3.5", "Dramatic Pause 5.0" };
            var DelayDropDown = new DropdownField("Text Delay", DelayOptions, 0);
            DelayDropDown.RegisterValueChangedCallback(evt => TextDelay = float.Parse(Regex.Match(evt.newValue, @"\d+[.][^2]").Value));

            Foldout.Add(TextMessageField);
            Foldout.Add(TypeDropDown);
            Foldout.Add(DelayDropDown);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        public TextMessage ToTextMessageData() {
            return new TextMessage {
                Type = this.Type,
                TextContent = this.TextContent,
                TextDelay = this.TextDelay
            };
        }

        public override BaseNode InstantiateNodeCopy() {
            return new TextMessageNode(graphView);
        }
    }
}
