using GBS.Events;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
namespace GBS.Elements
{
    using CodiceApp.EventTracking;
    using Enumerations;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.Events;
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

        public virtual void Init(Vector2 position)
        {
            EventName = "EventName";
            Events = new List<GBSEvent>();
            StartEvent = null;
            Choices = new List<string>();

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

            /* Input Port Container */
            this.CreateInputPort("Event Connection");
        }


    }
}
