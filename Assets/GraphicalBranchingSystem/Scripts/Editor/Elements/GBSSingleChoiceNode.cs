using UnityEngine;

namespace GBS.Elements
{
    using Enumerations;
    using GBS.Utility;

    public class GBSSingleChoiceNode : GBSNode
    {
        public override void Draw()
        {
            base.Draw();

            this.CreateOutputPort("Out Port"); 

            RefreshExpandedState();
            RefreshPorts();
        }

        public override void Init(Vector2 position)
        {
            base.Init(position);

            EventType = GBSEventsType.SingleChoice;

            Choices.Add("New Choise");
            //AddOutputPort("Out Port");
            //Events.Add("Next Event");
        }
    }
}
