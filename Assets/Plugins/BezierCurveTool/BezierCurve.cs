using System;
using System.Collections.Generic;
using UnityEngine;

namespace BezierCurveTool
{
    [ExecuteAlways][Serializable]
    public class BezierCurve : MonoBehaviour
    {
        public List<Arc> arcs = new List<Arc>();
        
        private Vector3 previousTransformPosition;

        private void OnDrawGizmosSelected()
        {
            foreach (var arc in arcs)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(arc.PointA, 0.1f);
            
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(arc.PointB, 0.1f);
            
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(arc.TangentA, 0.1f);
            
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(arc.TangentB, 0.1f);
            }
        }

        private void Update()
        {
            if (previousTransformPosition == transform.position) return;

            var bias = GetBias(previousTransformPosition, transform.position);

            foreach (var arc in arcs)
            {
                arc.PointA += bias;
                arc.PointB += bias;
                arc.TangentA += bias;
                arc.TangentB += bias;
            }
            
            previousTransformPosition = transform.position;
        }

        public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            var p01 = Vector3.Lerp(p0, p1, t);
            var p12 = Vector3.Lerp(p1, p2, t);
            var p23 = Vector3.Lerp(p2, p3, t);

            var p012 = Vector3.Lerp(p01, p12, t);
            var p123 = Vector3.Lerp(p12, p23, t);
            
            var p0123 = Vector3.Lerp(p012, p123, t);

            return p0123;
        }

        private Vector3 GetBias(Vector3 _previousPosition, Vector3 _currentPosition)
        {
            var absValue = new Vector3(Mathf.Abs(_previousPosition.x - _currentPosition.x), 
                Mathf.Abs(_previousPosition.y - _currentPosition.y), 
                Mathf.Abs(_previousPosition.z - _currentPosition.z));

            var result = new Vector3();
            
            if (_previousPosition.x > _currentPosition.x)
                result.x = absValue.x * -1;
            else
                result.x = absValue.x;
            
            if (_previousPosition.y > _currentPosition.y)
                result.y = absValue.y * -1;
            else
                result.y = absValue.y;
            
            if (_previousPosition.z > _currentPosition.z)
                result.z = absValue.z * -1;
            else
                result.z = absValue.z;
            
            return result;
        }
    }
}
