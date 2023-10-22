using System;
using UnityEngine.UIElements;

namespace GBS.Utility
{
    using CodiceApp.EventTracking;
    using CodiceApp.EventTracking.Plastic;
    using Elements;
    using GBS.Events;
    using Unity.VisualScripting.YamlDotNet.Core.Events;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;

    public static class GBSElementUtility
    {
        public static Port CreateOutputPort(this GBSNode node, string name, bool addCloseButton = false, Port.Capacity capacity = Port.Capacity.Multi, bool addEvent = true)
        {
            Port outputPort = node.InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));
            outputPort.portName = string.Empty;

            TextField choiceTextField = new TextField()
            {
                value = name
                //“ут поидее им€ можно запихать value = name . но при этом в portName передовать пустую строку
            };

            choiceTextField.AddClasses(
                ".ds-node__text-field",
                ".ds-node__choice-text-field",
                ".ds-node__text-field__hidden"
                );


            outputPort.Add(choiceTextField);

            if (addCloseButton)
            {
                Button deleteChoiceButton = GBSElementUtility.CreateButton("X");

                deleteChoiceButton.AddToClassList(".ds-node__button");

                outputPort.Add(deleteChoiceButton);
            }

            node.outputContainer.Add(outputPort);

            /* ’з насколько рабоча€ иде€*/
            if (addEvent)
            {
                ObjectField eventPort = GetEventPort();
                node.Events.Add(eventPort.value as GBSEvent);

                node.outputContainer.Add(eventPort);
            }

            return outputPort;
        }

        public static TextField CreateTitle(this GBSNode node, string eventName)
        {
            TextField eventNameTextField = new TextField()
            {
                value = eventName
            };

            eventNameTextField.AddClasses(
                ".ds-node__text-field",
                ".ds-node__filename-text-field",
                ".ds-node__text-field__hidden"
                );

            node.titleContainer.Insert(0, eventNameTextField);

            return eventNameTextField;
        }

        public static Port CreateInputPort(this GBSNode node, string potName = "", bool addEvent = true)
        {
            Port inputPort = node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = potName;
            node.inputContainer.Add(inputPort);

            /* ’з насколько рабоча€ иде€*/
            if (addEvent)
            {
                ObjectField eventPort = GetEventPort();
                node.StartEvent = eventPort.value as GBSEvent;

                node.inputContainer.Add(eventPort);
            }

            return inputPort;
        }

        public static ObjectField GetEventPort()
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(GBSEvent),
                allowSceneObjects = false,
                value = null
            };

            objectField.RegisterValueChangedCallback(value =>
            {
                GBSEvent gbsEvent = objectField.value as GBSEvent;
            });
            //_objectField.SetValueWithoutNotify(_gbsEvent);

            return objectField;
        }

        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value
            };

            if(onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            return textField;
        }

        public static TextField CreateTextAria(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, onValueChanged);

            textArea.multiline = true;

            return textArea;
        }
    }
}
