using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handle the UI components of the Menu scene.
/// </summary>
public class MainMenu : MonoBehaviour {

    [SerializeField] private AudioManager m_AudioManager;

    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_InstructionsPanel;
    [SerializeField] private GameObject m_DiffucultyPanel;
    [SerializeField] private GameObject m_ConfigurationsPanel;
    [SerializeField] private GameObject m_QuestionnairePanel;
    [SerializeField] private GameObject m_MessegePanel;
    [SerializeField] private InputField m_QuestionnaireCode;

    [SerializeField] private Text m_QuestionnaireCodeText;
    [SerializeField] private Text m_Messege;
    [SerializeField] private Text m_QuestionnairePanelTitleText;
    [SerializeField] private Text m_DifficultyPanelTitleText;

    [SerializeField] private GameObject m_SendIcon;
    [SerializeField] private Sprite m_Confirm;
    [SerializeField] private Sprite m_Error;

    /// <summary>
    /// Enables or disables the Instructions Panel.
    /// </summary>
    /// <param name="active">True to enable or False to disable the panel.</param>
    public void EnableInstructionsPanel(bool active) {
        m_ConfigurationsPanel.SetActive(!active);
        m_InstructionsPanel.SetActive(active);
    }

    /// <summary>
    /// Enables or disables the Difficulty Panel.
    /// </summary>
    /// <param name="active">True to enable or False to disable the panel.</param>
    public void EnableDifficultyPanel(bool active) {
        m_MainMenuPanel.SetActive(!active);
        m_DiffucultyPanel.SetActive(active);
        m_DifficultyPanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
    }

    /// <summary>
    /// Enables or disables the Questionnaire Panel.
    /// </summary>
    /// <param name="active">True to enable or False to disable the panel.</param>
    public void EnableQuestionnairePanel(bool active) {
        m_ConfigurationsPanel.SetActive(!active);
        m_QuestionnairePanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
        m_QuestionnairePanel.SetActive(active);
    }

    /// <summary>
    /// Enables or disables the Configurations Panel.
    /// </summary>
    /// <param name="active">True to enable or False to disable the panel.</param>
    public void EnableConfigurationsPanel(bool active) {
        m_MainMenuPanel.SetActive(!active);
        m_ConfigurationsPanel.SetActive(active);
    }


    /// <summary>
    /// Enables or disables the Messege Panel but doesn't set the messege.
    /// </summary>
    /// <param name="active">True to enable or False to disable the panel.</param>
    public void EnableMessegePanel(bool active) {
        m_QuestionnairePanel.SetActive(!active);
        m_MessegePanel.SetActive(active);
    }

    /// <summary>
    /// Enables or disables the Messege Panel and set the messege.
    /// </summary>
    /// <param name="messege">Messege that will be show on the screen.</param>
    /// <param name="active">True to enable or False to disable the panel.</param>
    private void EnableMessegePanel(string messege, bool active) {
        m_QuestionnairePanel.SetActive(!active);
        m_Messege.text = messege;
        m_MessegePanel.SetActive(active);
    }

    /// <summary>
    /// Enables or disables the Questionnaire Panel icon of success or error.
    /// </summary>
    public void EnableSendIcon() {
        m_SendIcon.gameObject.SetActive(false);
    }


    /// <summary>
    /// Sets the difficulty level based on the clicked button.
    /// </summary>
    /// <param name="bt">The button that will be clicked.</param>
    public void ChooseDifficultyButton(Button bt) {
        switch (bt.name) {
            case "EasyButton":
                QuestionSingleTon.Instance.m_Difficulty = Difficulty.EASY;
                break;
            case "NormalButton":
                QuestionSingleTon.Instance.m_Difficulty = Difficulty.NORMAL;
                break;
            case "HardButton":
                QuestionSingleTon.Instance.m_Difficulty = Difficulty.HARD;
                break;
        }
    }

    /// <summary>
    /// Open an external url.
    /// </summary>
    /// <param name="url">The url that will be open.</param>
    public void OpenLink(string url) {
        Application.OpenURL(url);
    }
   
    /// <summary>
    /// Calls the RequestQuestion() coroutine of ServerConnecton class to send the questionnaire code.
    /// </summary>
    public void SendCodeQuestionnaire() {
        if (m_QuestionnaireCodeText.text == "") {
            EnableMessegePanel("Informe algum código!", true);
            m_AudioManager.PlayWrongAnswerAudio();
        } else {
            StartCoroutine(ServerConnection.RequestQuestion(m_QuestionnaireCodeText.text, CallBackRequestQuestion));
        }
    }

    /// <summary>
    /// Saves the request result in the PlayerPrefs and populate the m_Questions list 
    /// of QuestionSingleton class with the new questions.
    /// </summary>
    /// <param name="err">Server request error.</param>
    /// <param name="resultStr">Server request result.</param>
    /// <returns>0</returns>
    private int CallBackRequestQuestion(string err, string resultStr) {
        if (err == null) {
            PlayerPrefs.SetString("Questionnaire", "{\"m_Questionnaire\":" + resultStr + "}"); //precisa fazer isso para ficar no formato certo para gerar o objeto
            QuestionSingleTon.Instance.PopulateQuestionsFromQuestionnaireJson();

            m_SendIcon.GetComponent<Image>().sprite = m_Confirm;
            m_SendIcon.gameObject.SetActive(true);
            EnableMessegePanel("Questionário substituído com sucesso!", true);
            m_AudioManager.PlayRightAnswerAudio();
            m_QuestionnairePanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
        }else {
            Debug.Log(err);
            m_SendIcon.GetComponent<Image>().sprite = m_Error;
            m_SendIcon.gameObject.SetActive(true);
            EnableMessegePanel("Código inválido. Tente novamente!", true);
            m_AudioManager.PlayWrongAnswerAudio();
        }
        return 0;
    }

    /// <summary>
    /// Close the game.
    /// </summary>
    public void Quit() {
        Application.Quit();
    }
}
