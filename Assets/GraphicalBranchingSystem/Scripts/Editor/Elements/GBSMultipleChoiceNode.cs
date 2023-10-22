using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBS.Elements
{
    using Enumerations;
    using GBS.Utility;
    using UnityEditor.Experimental.GraphView;
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

        public override void Init(Vector2 position)
        {
            base.Init(position);

            EventType = GBSEventsType.MultipleChoice;

            Choices.Add("New Choise");
            //AddOutputPort("Out Port");
        }
    }
}
