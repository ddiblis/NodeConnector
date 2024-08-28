using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GV
{
    public class TextMessageNode : BaseNode
    {
        public TextMessageNode(GraphView graphView) : base(graphView)
        {
            title = "TextMessage";

            // Create an input port for the parent SubChap
            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(SubChapNode));
            inputPort.portName = "Parent SubChap";
            inputPort.AddManipulator(new EdgeConnector<Edge>(new CustomEdgeConnectorListener()));
            inputContainer.Add(inputPort);

            // Add other fields here...
            var typeField = new IntegerField("Type");
            var textContentField = new TextField("TextContent");
            var textDelayField = new FloatField("TextDelay");

            mainContainer.Add(typeField);
            mainContainer.Add(textContentField);
            mainContainer.Add(textDelayField);

            RefreshExpandedState();
            RefreshPorts();
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new TextMessageNode(graphView);
        }
    }
}

