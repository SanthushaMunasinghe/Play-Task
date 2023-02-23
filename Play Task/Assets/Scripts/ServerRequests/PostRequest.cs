using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class PostRequest
{
    //Post
    public static IEnumerator SendRequest(string url, string method, Dictionary<string, string> headers, string payload, Action<string> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            // Set headers
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            // Set payload for POST requests
            if (method == "POST")
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
            }

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error sending {method} request to {url}: {request.error}");
                //yield break;
            }

            // Parse the response body as JSON
            string responseBody = request.downloadHandler.text;
            callback(responseBody);
        }
    }
}
