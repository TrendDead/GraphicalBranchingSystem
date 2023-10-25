using GBS.Events;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace GBS.Elements
{
    using CodiceApp.EventTracking.Plastic;
    using Enumerations;
    using GBS.Windows;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Utility;

    public class GBSNode : Node
    {
        /* TEST */

        private GBSEvent _gbsEvent;
        private ObjectField _objectField;

        /* TEST */

        //public event UnityAction Start;
        //public event UnityAction End;
        public string EventName { get; set; }
        public List<string> Choices { get; set; }
        public List<GBSEvent> Events { get; set; }
        public GBSEvent StartEvent { get; set; }
        public GBSEventsType EventType { get; set; }
        public GBSGraphView GraphView { get; private set; }

        private Color defaulBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

        public virtual void Init(GBSGraphView graphView, Vector2 position)
        {
            EventName = "EventName";
            Events = new List<GBSEvent>();
            StartEvent = null;
            Choices = new List<string>();
            GraphView = graphView;

            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList(".ds-node__main-container");
            extensionContainer.AddToClassList(".gbs-node_extension-container");   

            //StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/GraphicalBranchingSystem/Scripts/Editor Default Resources/GBSNodeStyles.uss");
            //styleSheets.Add(styleSheet);
        }

        public virtual void Draw()
        {
            /* Title Container */
            this.CreateTitle(EventName);

            //TextField eventNameTextField = GBSElementUtility.CreateTextField(EventName, null, callback =>
            //{
            //    GraphView.RemoveUngroupedNode(this);

            //    EventName = callback.newValue;

            //    GraphView.AddUngroupedNode(this);
            //});

            //eventNameTextField.AddClasses(
            //    ".ds-node__text-field",
            //    ".ds-node__filename-text-field",
            //    ".ds-node__text-field__hidden"
            //    );

            //titleContainer.Insert(0, eventNameTextField);

            /* Input Port Container */
            this.CreateInputPort("Event Connection");
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void ResetStyle()
        {
            mainContainer.style.backgroundColor = defaulBackgroundColor;
        }
    }
}
