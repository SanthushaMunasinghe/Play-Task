using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : MonoBehaviour
{
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

        GlobalMethods.LoadScene("PlayerResult");
    }
}
