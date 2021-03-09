using System.IO;
using UnityEditor;
using UnityEngine;

class EditorBuild
{
    static string contentBuilderPath = "C:/Users/AtomicWolf/Documents/steamworks_sdk_151/tools/ContentBuilder/";

    [MenuItem("Build/Windows Build")]
    public static void WindowsBuild()
    {
        string path = "Assets/SteamData.vdf";

        StreamReader reader = new StreamReader(path);
        File.WriteAllLines(contentBuilderPath + "scripts/SteamData.vdf", new[] { reader.ReadToEnd().Replace("@@@@", "Version " + Application.version + " Date " + System.DateTime.Now.ToString("yyyy/MM/dd")) });
        reader.Close();

        string locationPathName = contentBuilderPath + "content/windows_content/";
        if (Directory.Exists(locationPathName))
        {
            Directory.Delete(locationPathName, true);
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new [] { "Assets/Scenes/SampleScene.unity" };
        buildPlayerOptions.locationPathName = locationPathName + Application.productName + ".exe";
        
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }//
}