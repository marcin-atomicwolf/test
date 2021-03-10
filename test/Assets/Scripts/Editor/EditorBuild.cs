using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

class EditorBuild
{
    static string contentBuilderPath = "C:/Users/AtomicWolf/Documents/steamworks_sdk_151/tools/ContentBuilder/";

    public static void WindowsBuild()
    {
        string path = "Assets/SteamData.vdf";

        StreamReader reader = new StreamReader(path);
        string buildInfo = "Version: " + Application.version + " Date: " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        File.WriteAllLines(contentBuilderPath + "scripts/SteamData.vdf", new[] { reader.ReadToEnd().Replace("@@@@", buildInfo) });
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
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        string summary = buildInfo + " Result: " + report.summary.result + " Duration: " + report.summary.totalTime
            + " Size: " + report.summary.totalSize + " Errors: " + report.summary.totalErrors + " Warnings: " + report.summary.totalWarnings;

        File.WriteAllLines("C:/Users/AtomicWolf/Documents/summary.txt", new[] { summary } );

        WWWForm form = new WWWForm();
        form.AddField("key", "67b98259c7ee67f217189ea8a1d07b31");
        form.AddField("token", "957046bfcbd17cd1b47785725d6604afe89df46a2f39d665eaa7873148f38930");
        form.AddField("text", summary);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.trello.com/1/cards/6048975d92f6b145a06f6dd5/actions/comments", form))
        {
            www.SendWebRequest();

            while (www.result == UnityWebRequest.Result.InProgress) ;
        }
    }
    //trello api key 67b98259c7ee67f217189ea8a1d07b31
    //trello api secret e12d7c55e0fe559e9cb2b7f3fc94c59d0b262c12c6a75a3f52412fbc6790b5d7
    //trello api token 957046bfcbd17cd1b47785725d6604afe89df46a2f39d665eaa7873148f38930
    //using (UnityWebRequest www = UnityWebRequest.Get("...key=67b98259c7ee67f217189ea8a1d07b31&token=957046bfcbd17cd1b47785725d6604afe89df46a2f39d665eaa7873148f38930"))
    //https://api.trello.com/1/members/me/boards? get board id 5ff6d7fce903cd3e7ec21b09
    //https://api.trello.com/1/boards/5ff6d7fce903cd3e7ec21b09/lists? get list id 60488bb4e441f52ab55e70e9
    //https://api.trello.com/1/lists/60488bb4e441f52ab55e70e9/cards? get card id 6048975d92f6b145a06f6dd5
}