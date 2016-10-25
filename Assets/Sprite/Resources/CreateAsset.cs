using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

public class CreateAsset : MonoBehaviour {

    [MenuItem("Assets/Create/AchievementDataBase")]
    public static void Create()
    {
        AchievementsData asset = ScriptableObject.CreateInstance<AchievementsData>();
        AssetDatabase.CreateAsset(asset, "Assets/AchievementData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}

#endif