using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GV
{
    public class CustomEdgeConnectorListener : IEdgeConnectorListener
    {
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
            // Handle the case where an edge is dropped outside of any port, if needed.
        }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            // Ensure that only edges between compatible ports are added
            if (edge.input.portType == edge.output.portType && edge.input.node != edge.output.node)
            {
                graphView.AddElement(edge);
            }
            else
            {
                // Remove the edge if the connection is not valid
                edge.input.Disconnect(edge);
                edge.output.Disconnect(edge);
            }
        }
    }
}
