using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace GV
{
    public class ChapterNode : BaseNode
    {
        public ChapterNode(GraphView graphView) : base(graphView)
        {
            title = "Chapter";
            var allowMidrollsField = new Toggle("AllowMidrolls");

            mainContainer.Add(allowMidrollsField);
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new ChapterNode(graphView);
        }
    }
}
