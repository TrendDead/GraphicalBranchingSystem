using UnityEngine;

namespace GBS.Elements
{
    using Enumerations;
    using Utility;
    using Windows;

    public class GBSSingleChoiceNode : GBSNode
    {
        public override void Draw()
        {
            base.Draw();

            this.CreateOutputPort("Out Port"); 

            RefreshExpandedState();
            RefreshPorts();
        }

        public override void Init(GBSGraphView graphView, Vector2 position)
        {
            base.Init(graphView, position);

            EventType = GBSEventsType.SingleChoice;

            Choices.Add("New Choise");
            //AddOutputPort("Out Port");
            //Events.Add("Next Event");
        }
    }
}
