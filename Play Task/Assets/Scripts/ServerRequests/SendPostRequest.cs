using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SendPostRequest : MonoBehaviour
{
    //Send Post Data
    public void SendPostData(string url, string method, Dictionary<string, string> headers, string payload, Label errorMsg)
    {
        headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        StartCoroutine(PostRequest.SendRequest(url, method, headers, payload, (responseBody) =>
        {
            // Parse the response body as JSON
            JObject responseJson = JObject.Parse(responseBody);

            // Check for errors in the response
            if (responseJson["message"] != null)
            {
                GlobalMethods.DisplayError(errorMsg, responseJson["message"].Value<string>());
                return;
            }
            else if (responseJson["userid"] != null)
            {
                // Access the data in the response
                string id = responseJson["userid"].Value<string>();
                Debug.Log($"Get user: {id}");
            }
        }));
    }
}
