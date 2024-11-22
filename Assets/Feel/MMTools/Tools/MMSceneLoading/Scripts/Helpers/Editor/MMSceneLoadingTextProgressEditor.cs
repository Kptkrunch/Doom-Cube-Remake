using UnityEditor;

namespace MoreMountains.Tools.Editors
{
    [CustomEditor(typeof(MoreMountains.Tools.MMSceneLoadingTextProgress))]
    public class MMSceneLoadingTextProgressEditor : Editor
    {
        // Implement this function to make a custom inspector.
        // Inside this function you can add your own custom IMGUI based GUI for the inspector of a specific object class.
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
