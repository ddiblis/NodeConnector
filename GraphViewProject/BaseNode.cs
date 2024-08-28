using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GV
{
    public abstract class BaseNode : Node
    {
        protected GraphView graphView;

        public BaseNode(GraphView graphView)
        {
            this.graphView = graphView;

            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);

            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            outputPort.portName = "Output";
            outputContainer.Add(outputPort);

            this.AddManipulator(new ContextualMenuManipulator(evt => evt.menu.AppendAction("Duplicate Node", action =>
            {
                var duplicate = this.InstantiateNodeCopy();
                duplicate.SetPosition(new Rect(this.GetPosition().position + new Vector2(20, 20), this.GetPosition().size));
                this.graphView.AddElement(duplicate);
            })));

            RefreshExpandedState();
            RefreshPorts();
        }

        public abstract BaseNode InstantiateNodeCopy();
    }
}
