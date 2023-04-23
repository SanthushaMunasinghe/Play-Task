using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : MonoBehaviour
{
    void Start()
    {
        List<ILevelData> projectDataList = JsonConvert.DeserializeObject<List<ILevelData>>(GlobalData.projectData);
    }
}
