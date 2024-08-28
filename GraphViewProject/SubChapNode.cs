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

            var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(ChapterNode));
            inputPort.portName = "Parent Chapter";
            inputContainer.Add(inputPort);

            var textMessageOutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(TextMessageNode));
            textMessageOutputPort.portName = "Text Messages";
            outputContainer.Add(textMessageOutputPort);

            var responseOutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(ResponseNode));
            responseOutputPort.portName = "Responses";
            outputContainer.Add(responseOutputPort);

            var CustomDataContainer = new VisualElement();
            var Foldout = new Foldout() { text = "Sub Chapter Content" };

            var ContactTextField = new TextField("Contact") { value = Contact };
            ContactTextField.RegisterValueChangedCallback(evt => Contact = evt.newValue);

            var TimeIndicatorTextField = new TextField("Time Indicator") { value = TimeIndicator };
            TimeIndicatorTextField.RegisterValueChangedCallback(evt => TimeIndicator = evt.newValue);

            var UnlockInstaPostsAccountTextField = new TextField("Unlock InstaPosts Account") { value = UnlockInstaPostsAccount };
            UnlockInstaPostsAccountTextField.RegisterValueChangedCallback(evt => UnlockInstaPostsAccount = evt.newValue);

            var UnlockListTextField = new TextField("Unlock Posts List");
            UnlockListTextField.RegisterValueChangedCallback(evt => {
                var UnlockList = (from Match m in Regex.Matches(evt.newValue, @"\d+") select m.Value).ToList();
                UnlockPosts = UnlockList.ConvertAll(int.Parse);
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

        public SubChap ToSubChapData()
        {
            return new SubChap
            {
                Contact = this.Contact,
                TimeIndicator = this.TimeIndicator,
                UnlockInstaPostsAccount = this.UnlockInstaPostsAccount,
                UnlockPosts = this.UnlockPosts,
                TextList = this.TextList.ConvertAll(textNode => textNode.ToTextMessageData()),
                Responses = this.Responses.ConvertAll(responseNode => responseNode.ToResponseData())
            };
        }


        public override BaseNode InstantiateNodeCopy()
        {
            return new SubChapNode(graphView);
        }
    }
}
