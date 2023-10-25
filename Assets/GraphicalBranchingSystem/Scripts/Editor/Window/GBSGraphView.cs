using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace GBS.Windows
{
    using Elements;
    using Enumerations;
    using UnityEngine;
    using Utility;
    using Data.Error;

    public class GBSGraphView : GraphView
    {
        private GBSEditorWindow _editorWindow;
        private GBSSearchWindow _searchWindow;

        private SerializableDictionary<string, GBSNodeErrorData> _ungroupedNodes;

        public GBSGraphView(GBSEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            _ungroupedNodes = new SerializableDictionary<string, GBSNodeErrorData>();

            AddManipulators();
            AddSearchWindow();
            AddGridBackground();

            OnElementsDeleted();

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

        private void AddSearchWindow()
        {
            if(_searchWindow == null)
            {
                _searchWindow = ScriptableObject.CreateInstance<GBSSearchWindow>();

                _searchWindow.Init(this);
            }

            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }

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
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Event Group", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
                );

            return contextualMenuManipulator;
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, GBSEventsType eventType)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(eventType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
                );

            return contextualMenuManipulator;
        }

        #endregion

        #region Create Elements

        public Group CreateGroup(string title, Vector2 localMousePosition)
        {
            Group group = new Group()
            {
                title = title
            };

            group.SetPosition(new Rect(localMousePosition, Vector2.zero));

            return group;
        }

        public GBSNode CreateNode(GBSEventsType eventType, Vector2 position)
        {
            Type nodeType = Type.GetType($"GBS.Elements.GBS{eventType}Node");

            GBSNode node = (GBSNode) Activator.CreateInstance(nodeType);

            node.Init(this, position);
            node.Draw();

            AddUngroupedNode(node);
            
            return node;
        }

        #endregion

        #region Callbacks

        private void OnElementsDeleted()
        {
            deleteSelection = (operationName, askUser) =>
            {
                List<GBSNode> nodeToDelete = new List<GBSNode>();

                foreach (GraphElement element in selection)
                {
                    if (element is GBSNode node)
                    {
                        nodeToDelete.Add(node);

                        continue;
                    }
                }

                foreach (var node in nodeToDelete)
                {
                    RemoveUngroupedNode(node);

                    RemoveElement(node);
                }
            };
        }

        #endregion

        #region Repeated Elements
        public void AddUngroupedNode(GBSNode node)
        {
            string nodeName = node.EventName;

            if (!_ungroupedNodes.ContainsKey(nodeName))
            {
                GBSNodeErrorData errorData = new GBSNodeErrorData();

                errorData.Nodes.Add(node);

                _ungroupedNodes.Add(nodeName, errorData);

                return;
            }

            List<GBSNode> ungroupdNodeList = _ungroupedNodes[nodeName].Nodes;

            ungroupdNodeList.Add(node);

            Color errorColor = _ungroupedNodes[nodeName].ErrorData.Color;

            node.SetErrorStyle(errorColor);

            if(ungroupdNodeList.Count == 2)
            {
                ungroupdNodeList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveUngroupedNode(GBSNode node)
        {
            string nodeName = node.EventName;
            List<GBSNode> ungroupdNodeList = _ungroupedNodes[nodeName].Nodes;

            ungroupdNodeList.Remove(node);

            node.ResetStyle();

            if (ungroupdNodeList.Count == 1)
            {
                ungroupdNodeList[0].ResetStyle();

                return;
            }


            if (ungroupdNodeList.Count == 0)
            {
                _ungroupedNodes.Remove(nodeName);
            }
        }
        #endregion

        #region Utilites

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition -= _editorWindow.position.position;
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }

        #endregion
    }
}
