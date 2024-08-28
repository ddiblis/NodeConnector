using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GV
{
    public class SchemaGraphView : GraphView
    {
        public SchemaGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            // Add background grid
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            // Enable basic manipulators
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            // Add contextual menu to create nodes
            this.AddManipulator(new ContextualMenuManipulator(BuildContextualMenu));

            // Add the custom edge connector listener
            this.AddManipulator(new EdgeConnector<Edge>(new CustomEdgeConnectorListener()));
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Add SubChap Node", action => AddNode("SubChap", evt.mousePosition));
            evt.menu.AppendAction("Add TextMessage Node", action => AddNode("TextMessage", evt.mousePosition));
            evt.menu.AppendAction("Add Response Node", action => AddNode("Response", evt.mousePosition));
            evt.menu.AppendAction("Add Chapter Node", action => AddNode("Chapter", evt.mousePosition));
        }

        private void AddNode(string type, Vector2 position)
        {
            BaseNode node = null;
            switch (type)
            {
                case "SubChap":
                    node = new SubChapNode(this);
                    break;
                case "TextMessage":
                    node = new TextMessageNode(this);
                    break;
                case "Response":
                    node = new ResponseNode(this);
                    break;
                case "Chapter":
                    node = new ChapterNode(this);
                    break;
            }

            if (node != null)
            {
                node.SetPosition(new Rect(position, new Vector2(200, 150)));
                AddElement(node);
            }
        }
    }
}
