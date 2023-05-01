using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : MonoBehaviour
{
    public List<ILevelData> currentLevels;

    public void GetGameData()
    {
        currentLevels = JsonConvert.DeserializeObject<List<ILevelData>>(GlobalData.projectData);
    }
}
