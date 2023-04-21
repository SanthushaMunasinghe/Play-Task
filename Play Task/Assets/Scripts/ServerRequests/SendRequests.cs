using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SendRequests : MonoBehaviour
{
    //Send Post Or Put Request
    public void SendPostPutRequest(string url, string method, Dictionary<string, string> headers, string payload, Label label, Action<JObject> callback)
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

    //Send Get Request
    public void SendGetRequest(string url, Dictionary<string, string> headers, Label label, Action<JObject> callback)
    {
        StartCoroutine(GetRequest.SendRequest(url, headers, (responseBody, error) =>
        {
            // Handle errors
            if (error != null)
            {
                Debug.LogError($"Error sending Get request to {url}: {error}");
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

    //Get Array
    public void GetArray(string url, Dictionary<string, string> headers, Label label, Action<JArray> callback)
    {
        StartCoroutine(GetRequest.SendRequest(url, headers, (responseBody, error) =>
        {
            // Handle errors
            if (error != null)
            {
                Debug.LogError($"Error sending Get request to {url}: {error}");
                GlobalMethods.DisplayMessage(label, "Something Went Wrong", true);
                return;
            }

            // Parse the response body as JSON
            JArray responseJson = JArray.Parse(responseBody);

            //// Check for errors in the response
            //if (JObject.Parse(responseBody)["message"] != null)
            //{
            //    GlobalMethods.DisplayMessage(label, responseJson["message"].Value<string>(), true);
            //    return;
            //}

            callback(responseJson);
        }));
    }
}
