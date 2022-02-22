using UnityEditor;
using UnityEngine;

namespace BezierCurveTool.Editor
{
   
    [CustomEditor(typeof(BezierCurve))]
    public class BezierSceneGUI : UnityEditor.Editor 
    {
        [MenuItem("GameObject/Create Other/Bezier Curve")]
        static void CreateBezierCurve()
        {
            var gameObject = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
            gameObject.name = "Bezier Curve";
            gameObject.AddComponent<BezierCurve>();
            gameObject.GetComponent<BezierCurve>().arcs.Add(new Arc
            {
                PointA = new Vector3(-1, 0, 0),
                PointB = new Vector3(1, 0, 0),
                TangentA = new Vector3(-0.5f, 0, 1),
                TangentB = new Vector3(0.5f, 0, -1)
            });
        }
        
        public override void OnInspectorGUI()
        {
            var script = (BezierCurve) target;

            if (script.arcs.Count == 0) EditorGUILayout.LabelField("Points is empty");
            
            foreach (var arc in script.arcs)
            {
                arc.PointA = EditorGUILayout.Vector3Field("Point_A", arc.PointA);
                arc.PointB = EditorGUILayout.Vector3Field("Point_B", arc.PointB);
                arc.TangentA = EditorGUILayout.Vector3Field("Tangent_A", arc.TangentA);
                arc.TangentB = EditorGUILayout.Vector3Field("Tangent_B", arc.TangentB);
            }

            if (GUILayout.Button("Create Arc"))
            {
                if (script.arcs.Count > 0)
                {
                    var last = script.arcs[script.arcs.Count - 1];
                    
                    script.arcs.Add(new Arc
                    {
                        PointA = new Vector3(last.PointB.x, last.PointB.y, last.PointB.z),
                        PointB = new Vector3(last.PointB.x + 2, last.PointB.y, last.PointB.z),
                        TangentA = new Vector3(last.TangentA.x + 2, last.TangentA.y, last.TangentA.z),
                        TangentB = new Vector3(last.TangentB.x + 2, last.TangentB.y, last.TangentB.z)
                    });
                }
            }
        }
        
        void OnSceneGUI() 
        {
            var script = (BezierCurve) target;

            foreach (var arc in script.arcs)
            {
                arc.PointA = Handles.PositionHandle(arc.PointA, Quaternion.identity);
                arc.PointB = Handles.PositionHandle(arc.PointB, Quaternion.identity);
                arc.TangentA = Handles.PositionHandle(arc.TangentA, Quaternion.identity);
                arc.TangentB = Handles.PositionHandle(arc.TangentB, Quaternion.identity);
            
                Handles.DrawBezier(arc.PointA, arc.PointB, arc.TangentA, 
                    arc.TangentB, Color.red, null, 5);
            }
        }
    }
}