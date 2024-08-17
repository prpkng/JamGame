using System.Collections;
using UnityEngine;

namespace Game.Systems.Path
{
    using System.Linq;
#if UNITY_EDITOR

    using UnityEditor;

    [CustomEditor(typeof(PathContainer))]
    public class PathContainerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var obj = ((PathContainer) target);

            if (GUILayout.Button("Invert Path"))
            {
                obj.path.UpdatePoints(obj.path.Points.Reverse<Vector3>().ToList());
            }
        }

        protected virtual void OnSceneGUI()
        {
            var obj = ((PathContainer) target);
            var path = obj.path;


            Vector3 cameraToTarget = Camera.current.transform.position - obj.transform.position;
            Quaternion billboardOrientation = Quaternion.LookRotation(cameraToTarget, Camera.current.transform.up);

            var newPath = path.Points;

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.D && Event.current.shift)
            {
                Event.current.Use();
                Undo.RecordObject(obj, $"Added path element");
                newPath.Add(path.Points.Last() + Vector3.forward);
            }


            for (int i = 0; i < path.Points.Count; i++)
            {
                Handles.color = Color.yellow;
                if (i + 1 < path.Points.Count)
                    Handles.DrawLine(path.Points[i], path.Points[i + 1], 5f);

                EditorGUI.BeginChangeCheck();
                var newPos = Handles.PositionHandle(path.Points[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(obj, $"Change Path element {i}");
                    newPath[i] = newPos;
                }
            }
            path.UpdatePoints(newPath);
        }
        void OnEnable()
        {
            Tools.hidden = true;
        }

        void OnDisable()
        {
            Tools.hidden = false;
        }
    }

#endif

    public class PathContainer : MonoBehaviour
    {
        public Path path = new(new Vector3(0, 0, 1), new Vector3(0, 0, -1));

        private void Start()
        {
            // Force recalculate distances
            path.CalculateDistances();
        }
    }
}