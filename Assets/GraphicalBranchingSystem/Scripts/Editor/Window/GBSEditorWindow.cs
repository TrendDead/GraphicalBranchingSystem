using UnityEditor;
using UnityEngine.UIElements;

namespace GBS.Windows
{
    using Utility;

    public class GBSEditorWindow : EditorWindow
    {
        [MenuItem("UnityDev/GBS/Open Graph")]
        public static void Open()
        {
            GetWindow<GBSEditorWindow>("GBS Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddStyles();
        }
        private void AddGraphView()
        {
            GBSGraphView graphView = new GBSGraphView(this);

            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            rootVisualElement.AddVisualElement(
                "Assets/GraphicalBranchingSystem/Scripts/Editor Default Resources/GBSVariables.uss"
                );
        }

    }
}
