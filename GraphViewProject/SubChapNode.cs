using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Text;
using System;

namespace GV
{
    public class SubChapNode : BaseNode
    {  
        public string Contact;
        public string TimeIndicator;
        public string UnlockInstaPostsAccount;
        public List<int> UnlockPosts;
        public List<TextMessageNode> TextList = new List<TextMessageNode>();
        public List<ResponseNode> Responses = new List<ResponseNode>();

        public SubChapNode(GraphView graphView) : base(graphView)
        {
            title = "SubChapter";

            // Create input ports for Chapter and output ports for TextMessages and Responses
            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(ChapterNode));
            inputPort.portName = "Parent Chapter";
            inputContainer.Add(inputPort);

            var textMessageOutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(TextMessageNode));
            textMessageOutputPort.portName = "Text Messages";
            outputContainer.Add(textMessageOutputPort);

            var responseOutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ResponseNode));
            responseOutputPort.portName = "Responses";
            outputContainer.Add(responseOutputPort);

            // Other fields...

            VisualElement CustomDataContainer = new VisualElement();

            Foldout Foldout = new Foldout(){
                text = "Sub Chapter Content"
            };

            TextField ContactTextField = new TextField() {
                value = Contact,
                label = "Contact"
            };

            TextField TimeIndicatorTextField = new TextField() {
                value = TimeIndicator,
                label = "Time Indicator"
            };

            TextField UnlockInstaPostsAccountTextField = new TextField() {
                value = UnlockInstaPostsAccount,
                label = "Unlock InstaPosts Account"
            };

            TextField UnlockListTextField = new TextField() {
                label = "Unlock Posts List"
            };

            // UnlockListTextField.RegisterValueChangedCallback(evt => UnlockPosts = int.Parse(Regex.Match(evt.newValue, @"\d+").Value));
            UnlockListTextField.RegisterValueChangedCallback(evt => {
                var UnlockList = (from Match m in Regex.Matches(evt.newValue, @"\d+") select m.Value).ToList();
                GetUnlockPostList(UnlockList);
            });

            Foldout.Add(ContactTextField);
            Foldout.Add(TimeIndicatorTextField);
            Foldout.Add(UnlockInstaPostsAccountTextField);
            Foldout.Add(UnlockListTextField);
            CustomDataContainer.Add(Foldout);
            extensionContainer.Add(CustomDataContainer);

            RefreshExpandedState();
            RefreshPorts();
        }

        private void GetUnlockPostList(List<string> UnlockList) {
            UnlockPosts = new List<int>();
            foreach(string post in UnlockList) {
                UnlockPosts.Add(Convert.ToInt32(post));
            }
        }

        public override BaseNode InstantiateNodeCopy()
        {
            return new SubChapNode(graphView);
        }
    }
}



