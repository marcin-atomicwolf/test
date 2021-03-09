using System.IO;
using UnityEditor;
using UnityEngine;

class MyEditorScript
{
    [MenuItem("Test/Build")]
    public static void PerformBuild()
    {
        string locationPathName = Application.dataPath + "/../testBuild";
        if (Directory.Exists(locationPathName))
        {
            Directory.Delete(locationPathName, true);
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new [] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = locationPathName + "/test.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}