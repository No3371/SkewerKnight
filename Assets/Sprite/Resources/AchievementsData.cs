using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AchievementsData : ScriptableObject { 
    public List<Achievement> List;

    [System.Serializable]
    public struct Achievement
    {
        public string Name;
        public string Code;
    }

    public AchievementsData()
    {
        List = new List<Achievement>();
    }
}