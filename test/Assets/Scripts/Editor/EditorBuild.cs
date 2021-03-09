using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

class EditorBuild
{
    static string contentBuilderPath = "C:/Users/AtomicWolf/Documents/steamworks_sdk_151/tools/ContentBuilder/";

    [MenuItem("Build/Windows Build")]
    public static void WindowsBuild()
    {
        string path = "Assets/SteamData.vdf";

        StreamReader reader = new StreamReader(path);
        File.WriteAllLines(contentBuilderPath + "scripts/SteamData.vdf", new[] { reader.ReadToEnd().Replace("@@@@", "Version " + Application.version + " Date " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm")) });
        reader.Close();

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        string locationPathName = contentBuilderPath + "content/windows_content/";
        if (Directory.Exists(locationPathName))
        {
            Directory.Delete(locationPathName, true);
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = locationPathName + Application.productName + ".exe";
        
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}