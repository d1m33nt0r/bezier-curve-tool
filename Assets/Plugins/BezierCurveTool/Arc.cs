using System;
using UnityEngine;

namespace BezierCurveTool
{
    [Serializable]
    public class Arc
    {
        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 TangentA;
        public Vector3 TangentB;
    }
}