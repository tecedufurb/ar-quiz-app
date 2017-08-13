using System;

/// <summary>
/// Contains the question informations received by the server.
/// </summary>
[Serializable]
public class Question {

    public string _id;
    public string m_Question;
    public string m_AnswerA;
    public string m_AnswerB;
    public string m_AnswerC;
    public string m_AnswerD;
    public string m_RightAlternative;
    
}
