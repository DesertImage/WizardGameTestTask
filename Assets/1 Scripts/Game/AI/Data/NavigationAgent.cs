using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameCOP.AI
{
    public class NavigationAgent : Data
    {
        public NavMeshPath Path = new NavMeshPath();
        public Queue<Vector3> Points = new Queue<Vector3>();
        public Vector3 CurrentPoint;
    }
}