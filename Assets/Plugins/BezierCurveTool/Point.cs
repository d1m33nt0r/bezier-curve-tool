using System;
using System.Collections.Generic;
using UnityEngine;

namespace BezierCurveTool
{
    [Serializable]
    public class Point
    {
        public bool isFirstPoint;
        public bool isLastPoint;
        public Vector3 position;
        public List<Vector3> handles = new List<Vector3>();
        public bool isUpArc;
        
        public Point(bool _isFirstPoint, bool _isLastPoint, Vector3 _position, List<Vector3> _handles, bool _isUpArc)
        {
            isFirstPoint = _isFirstPoint;
            isLastPoint = _isLastPoint;
            position = _position;
            isUpArc = _isUpArc;
            handles = _handles;
        }
    }
}