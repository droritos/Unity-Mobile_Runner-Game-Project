#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ResetScriptableObjectOnBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        // Reference to your ScriptableObject asset
        PlayerStatsConfig playerStatsConfig = AssetDatabase.LoadAssetAtPath<PlayerStatsConfig>("Assets/ScriptableObjects/Player Stats.asset");

        if (playerStatsConfig != null)
        {
            playerStatsConfig.ResetStats();  // Reset the ScriptableObject values before the build
            EditorUtility.SetDirty(playerStatsConfig);  // Mark it as modified
            AssetDatabase.SaveAssets();  // Save the changes
            Debug.Log("PlayerStatsConfig has been reset before build!");
        }
    }
}
#endif
