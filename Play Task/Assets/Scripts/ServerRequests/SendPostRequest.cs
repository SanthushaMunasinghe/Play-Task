using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SendPostRequest : MonoBehaviour
{
    //Send Post Data
    public void SendPostData(string url, string method, Dictionary<string, string> headers, string payload, Label label, Action<JObject> callback)
    {
        StartCoroutine(PostRequest.SendRequest(url, method, headers, payload, (responseBody, error) =>
        {
            // Handle errors
            if (error != null)
            {
                Debug.LogError($"Error sending {method} request to {url}: {error}");
                GlobalMethods.DisplayMessage(label, "Something Went Wrong", true);
                return;
            }

            // Parse the response body as JSON
            JObject responseJson = JObject.Parse(responseBody);

            // Check for errors in the response
            if (responseJson["message"] != null)
            {
                GlobalMethods.DisplayMessage(label, responseJson["message"].Value<string>(), true);
                return;
            }

            callback(responseJson);
        }));
    }
}
