using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private WaveConfig config;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text[] enemyCounters;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text reward;
    [SerializeField] private Text doubleReward;


    private void Awake()
    {
        Messenger<EnemyType>.AddListener(GameEvents.enemyDeath, OnEnemyDeath);
    }

    private void OnDestroy()
    {
        Messenger<EnemyType>.RemoveListener(GameEvents.enemyDeath, OnEnemyDeath);
    }

    private void Start()
    {
        for (int i = 0; i < enemyCounters.Length; i++)
        {
            enemyCounters[i].text = config.waveDatas[WaveProgress.instance.CurrentWave].enemiesQuantity[i].ToString();
        }
    }

    public void SetReward(int coins)
    {
        reward.text = coins.ToString();
        doubleReward.text = (coins * 2).ToString();
    }

    public void ChangeGameOverText()
    {
        gameOverText.text = "Игра окончена";
    }

    public void ToGarage()
    {
        SceneManager.LoadScene(0);
    }

    #region events

    private void OnEnemyDeath(EnemyType type)
    {
        int counter = int.Parse(enemyCounters[((int)type)].text);
        counter--;
        enemyCounters[((int)type)].text = counter.ToString();
    }

    public void OnGameOver()
    {
        gameOver.SetActive(true);
    }
    #endregion
}
