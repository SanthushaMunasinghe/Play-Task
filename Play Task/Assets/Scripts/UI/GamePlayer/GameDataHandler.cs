using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDataHandler : MonoBehaviour
{
    [SerializeField] private SendRequests sendRequests;

    public List<ILevelData> currentLevels;

    public GameplayData gameplayData = new GameplayData();

    public void GetGameData()
    {
        currentLevels = JsonConvert.DeserializeObject<List<ILevelData>>(GlobalData.projectData);
    }

    public void SetGameplayData()
    {
        //Assign to Global Gameplay Data
        GlobalData.currentGameplayData = gameplayData;
        Debug.Log(gameplayData.FinalScore);

        //Convert gameplay level data
        List<GameplayLevelData> gLvljDataList = gameplayData.GameLevelData;
        string json = JsonConvert.SerializeObject(gLvljDataList, Formatting.Indented);

        //Save Data
        if (GlobalData.gameMode != "Test")
        {
            SaveGame(gameplayData.StartDateTime, gameplayData.EndDateTime, gameplayData.FinalScore, gameplayData.Duration, json);
        }
        else
        {
            GlobalMethods.LoadScene("PlayerResult");
        }
    }

    private void SaveGame(string sDate, string eDate, float fScore, float dura, string gLvlData)
    {
        // Define headers, and payload for the request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");
        string payload = $"{{\"starttime\":\"{sDate}\",\"endtime\":\"{eDate}\",\"duration\":\"{dura}\",\"score\":\"{fScore}\",\"levelscore\":{JsonConvert.SerializeObject(gLvlData)},\"student\":\"{GlobalUser.userData.UserID}\",\"subtopic\":\"{GlobalData.gameSubtopicID}\"}}";

        Label label = new Label();

        GlobalMethods.DisplayMessage(label, "Please Wait...");
        sendRequests.SendPostPutRequest(GlobalData.url + "/createattempt", GlobalData.methodPost, headers, payload, label, (responseJson) =>
        {
            Debug.Log(responseJson["success"].Value<string>());
            GlobalMethods.LoadScene("PlayerResult");
        });
    }
}
