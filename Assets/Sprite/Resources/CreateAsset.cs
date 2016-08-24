using UnityEngine;
using System.Collections;
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
