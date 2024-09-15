using System;
using UnityEngine;

namespace GameCOP.Combat
{
    [Serializable]
    public class Defense : Data
    {
        [Range(0f, 1f)] public float Value = 1f;
    }
}