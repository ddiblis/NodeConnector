using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GV
{
    public class SubChapNode : BaseNode
    {
        public SubChapNode(GraphView graphView) : base(graphView)
        {
            title = "SubChap";

            // Create an output port for TextMessages
            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(TextMessageNode));
            outputPort.portName = "Text Messages";
            outputPort.AddManipulator(new EdgeConnector<Edge>(new CustomEdgeConnectorListener()));
            outputContainer.Add(outputPort);

            // Add other fields here...
            var contactField = new TextField("Contact");
            var timeIndicatorField = new TextField("TimeIndicator");
            var unlockInstaPostsAccountField = new TextField("UnlockInstaPostsAccount");

            mainContainer.Add(contactField);
            mainContainer.Add(timeIndicatorField);
            mainContainer.Add(unlockInstaPostsAccountField);

            RefreshExpandedState();
            RefreshPorts();
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new SubChapNode(graphView);
        }
    }
}




