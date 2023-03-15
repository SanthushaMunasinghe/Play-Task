using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class GetRequest
{
    public static IEnumerator SendRequest(string url, Dictionary<string, string> headers, Action<string, string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Set headers
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error sending GET request to {url}: {request.error}");
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
