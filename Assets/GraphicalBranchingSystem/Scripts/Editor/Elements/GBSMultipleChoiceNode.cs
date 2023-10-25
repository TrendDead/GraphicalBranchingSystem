using UnityEngine;

namespace GBS.Elements
{
    using Enumerations;
    using Utility;
    using Windows;
    using UnityEngine.UIElements;

    public class GBSMultipleChoiceNode : GBSNode
    {
        public override void Draw()
        {
            base.Draw();

            Button addChiseButton = GBSElementUtility.CreateButton("Add Choice", () =>
            {
                this.CreateOutputPort("New Out Port", true);
                Choices.Add("New Out Port");
            });

            addChiseButton.AddToClassList(".ds-node__button");

            mainContainer.Insert(1, addChiseButton);

            foreach (var choice in Choices)
            {
                this.CreateOutputPort(choice, true);
            }
        }

        public override void Init(GBSGraphView graphView, Vector2 position)
        {
            base.Init(graphView, position);

            EventType = GBSEventsType.MultipleChoice;

            Choices.Add("New Choise");
            //AddOutputPort("Out Port");
        }
    }
}
