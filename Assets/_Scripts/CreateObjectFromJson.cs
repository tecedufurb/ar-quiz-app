using System;
using UnityEngine;

/// <summary>
/// Responsible for create the ServerResult object.
/// </summary>
[Serializable]
public class CreateObjectFromJson {
    
    public ServerResult m_ServerResult;

    /// <summary>
    /// Creates the ServerResult object from the json saved on PlayerPrefs and stores it 
    /// in the m_ServerResult variable.
    /// </summary>
    /// <returns></returns>
    public static CreateObjectFromJson CreateServerResultFromJson() {
        string assets = PlayerPrefs.GetString("ServerResult");
        return JsonUtility.FromJson<CreateObjectFromJson>(assets);
    }

    /// <summary>
    /// If the PlayerPrefs wasn't set yet, creates the ServerResult object from 
    /// the QuestionnaireJson file on the Resources folder.
    /// </summary>
    public static void SetPlayerPrefs() {
        string test = PlayerPrefs.GetString("ServerResult", "");
        if (test == "") {
            string json = Resources.Load("QuestionnaireJson").ToString();
            PlayerPrefs.SetString("ServerResult", json);
        }
    }
}
