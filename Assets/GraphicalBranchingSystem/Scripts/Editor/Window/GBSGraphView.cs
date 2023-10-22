using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace GBS.Windows
{
    using Elements;
    using Enumerations;
    using UnityEngine;
    using Utility;

    public class GBSGraphView : GraphView
    {
        public GBSGraphView()
        {
            AddManipulators();
            AddGridBackground();
            AddStyles();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if(startPort == port)
                {
                    return;
                }

                if(startPort.node == port.node)
                {
                    return;
                }

                if(startPort.direction == port.direction)
                {
                    return;
                }

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        #region Adding Elements

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            //Не работате
            this.AddVisualElement(
                "Assets/GraphicalBranchingSystem/Scripts/Editor Default Resources/GraphViewStyles.uss",
                "Assets/GraphicalBranchingSystem/Scripts/Editor Default Resources/GBSNodeStyles.uss"
                );
        }

        #endregion

        #region Manipulators

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector()); 

            this.AddManipulator(CreateNodeContextualMenu("Add Bode (Single Choice)", GBSEventsType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Bode (Multiple Choice)", GBSEventsType.MultipleChoice));

            this.AddManipulator(CreateGroupContextMenu());
        }

        private IManipulator CreateGroupContextMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Event Group", actionEvent.eventInfo.localMousePosition)))
                );

            return contextualMenuManipulator;
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, GBSEventsType eventType)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(eventType, actionEvent.eventInfo.localMousePosition)))
                );

            return contextualMenuManipulator;
        }

        #endregion

        #region Create Group

        private Group CreateGroup(string title, Vector2 localMousePosition)
        {
            Group group = new Group()
            {
                title = title
            };

            group.SetPosition(new Rect(localMousePosition, Vector2.zero));

            return group;
        }

        private GBSNode CreateNode(GBSEventsType eventType, Vector2 position)
        {
            Type nodeType = Type.GetType($"GBS.Elements.GBS{eventType}Node");

            GBSNode node = (GBSNode) Activator.CreateInstance(nodeType);

            node.Init(position);
            node.Draw();
            
            return node;
        }

        #endregion
    }
}
