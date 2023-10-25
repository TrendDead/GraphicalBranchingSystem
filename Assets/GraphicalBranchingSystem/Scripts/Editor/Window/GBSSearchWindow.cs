using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GBS.Windows
{
    using Elements;
    using Enumerations;
    using GBS.Utility;

    public class GBSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        GBSGraphView _graphView;

        public void Init(GBSGraphView graphView)
        {
            _graphView = graphView;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntryes = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create element")),
                new SearchTreeGroupEntry(new GUIContent("Dialoge Node"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice"))
                {
                    level = 2,
                    userData = GBSEventsType.SingleChoice
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice"))
                {
                    level = 2,
                    userData = GBSEventsType.MultipleChoice
                },
                new SearchTreeGroupEntry(new GUIContent("Dialoge Group"), 1),
                new SearchTreeEntry(new GUIContent("Single Group"))
                {
                    level = 2,
                    userData = new Group()
                }
            };

            return searchTreeEntryes;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousPosition = _graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch (SearchTreeEntry.userData)
            {
                case GBSEventsType.SingleChoice:
                    {
                        GBSSingleChoiceNode singleChoiceNode = (GBSSingleChoiceNode)_graphView.CreateNode(GBSEventsType.SingleChoice, localMousPosition);

                        _graphView.AddElement(singleChoiceNode);
                        return true;
                    }
                case GBSEventsType.MultipleChoice:
                    {
                        GBSMultipleChoiceNode multtipleChoiceNode = (GBSMultipleChoiceNode)_graphView.CreateNode(GBSEventsType.MultipleChoice, localMousPosition);

                        _graphView.AddElement(multtipleChoiceNode);
                        return true;
                    }

                case Group _:
                    {
                        Group group = _graphView.CreateGroup("EventGroup", localMousPosition);

                        _graphView.AddElement(group);

                        return true;
                    }
                default: return false;
            }
        }
    }
}
