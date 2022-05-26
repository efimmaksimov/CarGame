using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private WaveConfig config;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text[] enemyCounters;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text reward;
    [SerializeField] private Text doubleReward;
    [SerializeField] private Text currentWave;


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
            int quantity = config.GetWaveData(WaveProgress.Instance.CurrentWave).enemiesQuantity[i];
            if (quantity == 0)
            {
                enemyCounters[i].transform.GetComponentInParent<LayoutElement>().gameObject.SetActive(false);
            }
            else
            {
                enemyCounters[i].text = quantity.ToString();
            }
            
        }
        currentWave.text = $"Волна {WaveProgress.Instance.CurrentWave + 1}";
    }

    public void SetReward(int coins)
    {
        reward.text = coins.ToString();
        if (coins == 0)
        {
            doubleReward.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            doubleReward.text = (coins * 2).ToString();
        }
        
    }

    public void ChangeGameOverText()
    {
        gameOverText.text = "Игра окончена";
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
