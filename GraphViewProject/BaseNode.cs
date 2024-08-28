using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JSONMapper {
    public abstract class BaseNode : Node {
        protected GraphView graphView;

        public BaseNode(GraphView graphView) {
            this.graphView = graphView;

            this.AddManipulator(new ContextualMenuManipulator(evt => evt.menu.AppendAction("Duplicate Node", action => {
                var duplicate = this.InstantiateNodeCopy();
                duplicate.SetPosition(new Rect(this.GetPosition().position + new Vector2(20, 20), this.GetPosition().size));
                this.graphView.AddElement(duplicate);
            })));

            RefreshExpandedState();
            RefreshPorts();
        }

        // This method is called when an edge is connected to this node
        public virtual void OnConnected(Port port, Edge edge) {
            if (port.direction == Direction.Output) {
                // Example for handling TextMessage connections in SubChapNode
                if (this is SubChapNode subChapNode) {
                    if (port.portName == "Text Messages" && edge.input.node is TextMessageNode textMessageNode) {
                        subChapNode.TextList.Add(textMessageNode);
                    }
                    else if (port.portName == "Responses" && edge.input.node is ResponseNode responseNode) {
                        subChapNode.Responses.Add(responseNode);
                    }
                }
                else if (this is ChapterNode chapterNode && port.portName == "SubChapters" && edge.input.node is SubChapNode subChap) {
                    chapterNode.SubChaps.Add(subChap);
                }
            }
        }

        // This method is called when an edge is disconnected from this node
        public virtual void OnDisconnected(Port port, Edge edge) {
            if (port.direction == Direction.Output) {
                // Example for handling TextMessage disconnections in SubChapNode
                if (this is SubChapNode subChapNode) {
                    if (port.portName == "Text Messages" && edge.input.node is TextMessageNode textMessageNode) {
                        subChapNode.TextList.Remove(textMessageNode);
                    }
                    else if (port.portName == "Responses" && edge.input.node is ResponseNode responseNode) {
                        subChapNode.Responses.Remove(responseNode);
                    }
                }
                else if (this is ChapterNode chapterNode && port.portName == "SubChapters" && edge.input.node is SubChapNode subChap) {
                    chapterNode.SubChaps.Remove(subChap);
                }
            }
        }

        public abstract BaseNode InstantiateNodeCopy();
    }
}
