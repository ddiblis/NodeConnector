using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GV
{
    public class ResponseNode : BaseNode
    {
        public ResponseNode(GraphView graphView) : base(graphView)
        {
            title = "Response";
            var respTreeField = new Toggle("RespTree");
            var textContentField = new TextField("TextContent");
            var subChapNumField = new IntegerField("SubChapNum");
            var typeField = new IntegerField("Type");

            mainContainer.Add(respTreeField);
            mainContainer.Add(textContentField);
            mainContainer.Add(subChapNumField);
            mainContainer.Add(typeField);
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new ResponseNode(graphView);
        }
    }
}
