using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class PostRequest
{
    public static IEnumerator SendRequest(string url, string method, Dictionary<string, string> headers, string payload, Action<string, string> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            // Set headers
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            // Set payload for POST or PUT requests
            if (method == "POST" || method == "PUT")
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
                callback(null, request.error);
                //yield break;
            }

            // Check the response status code
            if (request.responseCode >= 400)
            {
                Debug.LogError($"Received error response with status code {request.responseCode} from {url}: {request.downloadHandler.text}");
                callback(null, request.downloadHandler.text);
                //yield break;
            }

            // Parse the response body as JSON
            string responseBody = request.downloadHandler.text;
            callback(responseBody, null);
        }
    }

}
