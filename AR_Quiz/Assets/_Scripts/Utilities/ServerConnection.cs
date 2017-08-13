using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for create a connection with the server and make the requests.
/// </summary>
public class ServerConnection : MonoBehaviour {

    //private const string URL_SERVER = "http://201.54.204.8:3000";
    private const string URL_SERVER = "http://10.9.30.85:3000";

    /// <summary>
    /// Send the player's informations to the server.
    /// </summary>
    /// <param name="user">Player object json to be send.</param>
    /// <param name="CallBackSaveScore">Function that will handle the request's errors and result.</param>
    /// <returns></returns>
    public static IEnumerator SaveScore(string user, Func<string, string, int> CallBackSaveScore) {

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers["Content-Type"] = "application/json";
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(user.ToCharArray());

        WWW www = new WWW(URL_SERVER + "/api/player/save-points", pData, headers);

        yield return www;

        Debug.Log(www.error);
        Debug.Log(www.text);

        CallBackSaveScore(www.error, www.text);
    }

    /// <summary>
    /// Send a code to the server and returns a json of questionnaire.
    /// </summary>
    /// <param name="codigo">Code to be send.</param>
    /// <param name="CallBackRequestQuestion">Function that will handle the request's errors and result.</param>
    /// <returns></returns>
    public static IEnumerator RequestQuestion(string codigo, Func<string, string, int> CallBackRequestQuestion) {

        WWW www = new WWW(URL_SERVER + "/api/questionnaire/code/" + codigo);

        yield return www;

        Debug.Log(www.text);
        Debug.Log(www.error);
        CallBackRequestQuestion(www.error, www.text);
    }
}
