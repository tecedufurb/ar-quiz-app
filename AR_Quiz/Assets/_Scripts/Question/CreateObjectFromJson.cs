using System;
using UnityEngine;

/// <summary>
/// Responsible for create the serverResult object.
/// </summary>
[Serializable]
public class CreateObjectFromJson {
    
    public ServerResult m_Questionnaire;

    public CreateObjectFromJson() { }

    /// <summary>
    /// Creates the ServerResult object from the json saved on PlayerPrefs and stores it in the m_Questionnaire variable.
    /// </summary>
    /// <returns></returns>
    public static CreateObjectFromJson CreateQuestionnaireFromJson() {
        string assets = PlayerPrefs.GetString("Questionnaire");
        return JsonUtility.FromJson<CreateObjectFromJson>(assets);
    }
    
    /*
    public static void LoadPlayerPrefs() {
        string test = PlayerPrefs.GetString("Questionnaire", "");
        if (test == "") {
            string json = Resources.Load("QuestionnaireJson").ToString();
            PlayerPrefs.SetString("Questionnaire", json);
        }
    }
    */
}
