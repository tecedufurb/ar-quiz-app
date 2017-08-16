using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handle the UI components of the Game scene.
/// </summary>
/// Originally attached to the Canvas object.
public class GameScreenHandle : MonoBehaviour {

    public Text m_QuestionText;
    public Text m_AlternativeAText;
    public Text m_AlternativeBText;
    public Text m_AlternativeCText;
    public Text m_AlternativeDText;

    #region PRIVATE_UI_VARIABLES
    [SerializeField] private GameObject m_MainPanel;
    [SerializeField] private GameObject m_QuestionsPanel;
    [SerializeField] private GameObject m_FinalPanel;
    [SerializeField] private GameObject m_MessagePanel;
    [SerializeField] private GameObject m_PanelScoreText;
    [SerializeField] private GameObject m_TargetPanel;
    [SerializeField] private GameObject m_MobileSingleStickControlRig;
    [SerializeField] private Text m_AnswerMessageText;
    [SerializeField] private Text m_RightHitsText;
    [SerializeField] private Text m_MessageText;
    [SerializeField] private Text m_RightScoreText;
    [SerializeField] private Text m_WrongScoreText;
    [SerializeField] private Button m_SendScoreButton;
    [SerializeField] private Button m_RankingButton;
    [SerializeField] private InputField m_PlayerNameInputField;
    #endregion 
    
    [SerializeField] private AudioManager m_AudioManager;
    private float mTimer;

    void Update() {
        if (mTimer > 0) {
            mTimer -= Time.deltaTime;
            if (mTimer <= 0)
                EraseAnswerMessage();
        }
    }

    /// <summary>
    /// Disable all the panels, except the loading screen panel.
    /// </summary>
    public void BackMainMenu() {
        m_MainPanel.SetActive(false);
        m_QuestionsPanel.SetActive(false);
        m_MobileSingleStickControlRig.SetActive(false);
        m_FinalPanel.SetActive(false);
        m_TargetPanel.SetActive(false);
    }

    /// <summary>
    /// Disable the target panel and enable the main panel.
    /// </summary>
    public void DisableTargetPanel() {
        m_TargetPanel.SetActive(false);
        m_MainPanel.SetActive(true);
        m_MobileSingleStickControlRig.SetActive(true);
    }

    /// <summary>
    /// Enable or disable the main panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableMainPanel(bool enable) {
        m_MainPanel.SetActive(enable);
    }

    /// <summary>
    /// Enable the ranking button and disable the send button.
    /// </summary>
    public void EnableRankingButton() {
        m_SendScoreButton.gameObject.SetActive(false);
        m_PlayerNameInputField.gameObject.SetActive(false);
        m_RankingButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Enable or disable the question panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableQuestionPanel(bool enable) {
        m_QuestionsPanel.SetActive(enable);
    }

    /// <summary>
    /// Enable or disable the final panel.
    /// </summary>
    /// <param name="enable">True to enable or False to disable the panel.</param>
    public void EnableFinalPanel(bool enable) {
        m_MobileSingleStickControlRig.SetActive(!enable);
        m_MainPanel.SetActive(!enable);
        m_FinalPanel.SetActive(enable);
        m_AudioManager.PlayWinAudio();
    }

    /// <summary>
    /// Show a success message when the player answer is right.
    /// </summary>
    /// <param name="remainingQuestions">The amount of remaining questions.</param>
    public void ShowRightAnswerMessage(int remainingQuestions) {
        m_AnswerMessageText.color = new Color(18f / 255f, 218f / 255f, 0);
        m_AnswerMessageText.text = "Você acertou!\n Faltam " + remainingQuestions + " perguntas.";
        m_AudioManager.PlayRightAnswerAudio();
        mTimer = 5;
    }

    /// <summary>
    /// Show a failure message when the player answer is wrong.
    /// </summary>
    public void ShowWrongAnswerMessage() {
        m_AnswerMessageText.color = new Color(227f / 255f, 8f / 255f, 8f / 255f);
        m_AnswerMessageText.text = "Você errou!\n Tente novamente.";
        m_AudioManager.PlayWrongAnswerAudio();
        mTimer = 5;
    }

    /// <summary>
    /// Erase the message that appears when the player answers a question.
    /// </summary>
    public void EraseAnswerMessage() {
        m_AnswerMessageText.text = "";
    }

    /// <summary>
    /// Update the score text shown in the main panel.
    /// </summary>
    /// <param name="value">The new score.</param>
    public void ShowScore(int value) {
        m_RightHitsText.text = "Pontos: " + value.ToString();
    }

    /// <summary>
    /// Show the amount of right and wrong questions at the final panel.
    /// </summary>
    /// <param name="rightQuestionsCount">Amount of right questions.</param>
    /// <param name="wrongQuestionsCount">Amount of right questions.</param>
    public void ShowQuestionsScore(int rightQuestionsCount, int wrongQuestionsCount) {
        m_RightScoreText.text = rightQuestionsCount.ToString();
        m_WrongScoreText.text = wrongQuestionsCount.ToString();
    }

    /// <summary>
    /// Enable the message panel, set the messege and plays a sound.
    /// </summary>
    /// <param name="message">The message to be set.</param>
    /// <param name="right">Defines if will be a right or wrong sound.</param>
    public void EnableMessagePanel(string message, bool right) {
        m_FinalPanel.SetActive(false);
        m_MessageText.text = message;
        m_MessagePanel.SetActive(true);

        if (right)
            m_AudioManager.PlayRightAnswerAudio();
        else
            m_AudioManager.PlayWrongAnswerAudio();
    }

    /// <summary>
    /// Disable the message panel and enable the final panel.
    /// </summary>
    public void BackFinalPanel() {
        m_MessagePanel.SetActive(false);
        m_FinalPanel.SetActive(true);
    }

    /// <summary>
    /// Open an external url.
    /// </summary>
    /// <param name="url">The url that will be open.</param>
    public void OpenLink(string url) {
        Application.OpenURL(url);
    }

    /// <summary>
    /// Intantiate the score prefab that appears at the right corner when the answer is right.
    /// </summary>
    /// <param name="points">The points that the player won.</param>
    public void ShowAddScoreAnimation(int points) {
        ScoreTextManager.Instance.CreateText(m_PanelScoreText.transform.position, "+" + points.ToString() + " ", new Color(18f / 255f, 218f / 255f, 0));
    }

    /// <summary>
    /// Intantiate the score prefab that appears at the right corner when the answer is wrong.
    /// </summary>
    /// <param name="points">The points that the player lost.</param>
    public void ShowSubScoreAnimation(int points) {
        ScoreTextManager.Instance.CreateText(m_PanelScoreText.transform.position, "-" + points.ToString() + " ", new Color(227f / 255f, 8f / 255f, 8f / 255f));
    }

    /// <summary>
    /// Send the player score to the server.
    /// </summary>
    /// <param name="playerName">Name of the player that will be sent.</param>
    public void SendScoreButtom(InputField playerName) {
        if (playerName.text != "") {
            Quiz quiz = FindObjectOfType<Quiz>();
            Player player = new Player();
            player.points = quiz.Score;
            player.name = playerName.text;
            player.questCode = QuestionSingleTon.Instance.JsonQuestions.m_ServerResult.result.code;
            string json = JsonUtility.ToJson(player);
            Debug.Log(json);
            StartCoroutine(ServerConnection.SaveScore(json, CallBackSaveScore));
        } else {
            EnableMessagePanel("Nome inválido. Tente novamente!", false);
        }
    }

    /// <summary>
    /// Check if the request returned an error or not.
    /// </summary>
    /// <param name="err">Server request error</param>
    /// <param name="resultStr">Server request result</param>
    /// <returns>0</returns>
    public int CallBackSaveScore(string err, string resultStr) {
        if (err == null) {
            EnableMessagePanel("Pontuação enviada com sucesso!", true);
            EnableRankingButton();
        } else {
            EnableMessagePanel("Erro ao enviar a pontuação. Tente novamente.", false);
        }
        return 0;
    }

}
