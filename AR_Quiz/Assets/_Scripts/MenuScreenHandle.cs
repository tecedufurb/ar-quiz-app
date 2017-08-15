using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handle the UI components of the Menu scene.
/// </summary>
public class MenuScreenHandle : MonoBehaviour {

    #region PRIVATE_UI_VARIABLES
    [SerializeField] private AudioManager m_AudioManager;
    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_InstructionsPanel;
    [SerializeField] private GameObject m_DiffucultyPanel;
    [SerializeField] private GameObject m_ConfigurationsPanel;
    [SerializeField] private GameObject m_QuestionnairePanel;
    [SerializeField] private GameObject m_MessagePanel;
    [SerializeField] private InputField m_QuestionnaireCodeInputField;
    [SerializeField] private Text m_QuestionnaireCodeText;
    [SerializeField] private Text m_MessageText;
    [SerializeField] private Text m_QuestionnairePanelTitleText;
    [SerializeField] private Text m_DifficultyPanelTitleText;
    [SerializeField] private GameObject m_SendIcon;
    [SerializeField] private Sprite m_Confirm;
    [SerializeField] private Sprite m_Error;
    #endregion



    /// <summary>
    /// Enable or disable the instructions panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableInstructionsPanel(bool enable) {
        m_ConfigurationsPanel.SetActive(!enable);
        m_InstructionsPanel.SetActive(enable);
    }

    /// <summary>
    /// Enable or disable the difficulty panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableDifficultyPanel(bool enable) {
        m_MainMenuPanel.SetActive(!enable);
        m_DiffucultyPanel.SetActive(enable);
        m_DifficultyPanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
    }

    /// <summary>
    /// Enable or disable the questionnaire panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableQuestionnairePanel(bool enable) {
        m_ConfigurationsPanel.SetActive(!enable);
        m_QuestionnairePanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
        m_QuestionnairePanel.SetActive(enable);
    }

    /// <summary>
    /// Enable or disable the configurations panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableConfigurationsPanel(bool enable) {
        m_MainMenuPanel.SetActive(!enable);
        m_ConfigurationsPanel.SetActive(enable);
    }


    /// <summary>
    /// Enable or disable the message panel but doesn't set the message.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableMessagePanel(bool enable) {
        m_QuestionnairePanel.SetActive(!enable);
        m_MessagePanel.SetActive(enable);
    }

    /// <summary>
    /// Enable or disable the message panel and set the message.
    /// </summary>
    /// <param name="message">Message that will be show on the screen.</param>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    private void EnableMessagePanel(string message, bool enable) {
        m_QuestionnairePanel.SetActive(!enable);
        m_MessageText.text = message;
        m_MessagePanel.SetActive(enable);
    }

    /// <summary>
    /// Enable or disable the questionnaire panel icon of success or error.
    /// </summary>
    public void EnableSendIcon() {
        m_SendIcon.gameObject.SetActive(false);
    }


    /// <summary>
    /// Set the difficulty level based on the clicked button.
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
    /// Call the RequestQuestion() coroutine of ServerConnecton class to send the questionnaire code.
    /// </summary>
    public void SendCodeQuestionnaire() {
        if (m_QuestionnaireCodeText.text == "") {
            EnableMessagePanel("Informe algum código!", true);
            m_AudioManager.PlayWrongAnswerAudio();
        } else {
            StartCoroutine(ServerConnection.RequestQuestion(m_QuestionnaireCodeText.text, CallBackRequestQuestion));
        }
    }

    /// <summary>
    /// Save the request result in the PlayerPrefs and populate the m_Questions list 
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
            EnableMessagePanel("Questionário substituído com sucesso!", true);
            m_AudioManager.PlayRightAnswerAudio();
            m_QuestionnairePanelTitleText.text = QuestionSingleTon.Instance.m_JsonQuestions.m_Questionnaire.result.title;
        }else {
            Debug.Log(err);
            m_SendIcon.GetComponent<Image>().sprite = m_Error;
            m_SendIcon.gameObject.SetActive(true);
            EnableMessagePanel("Código inválido. Tente novamente!", true);
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
