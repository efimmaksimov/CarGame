using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private class ActiveText
    {
        public Text textUI;
        public float maxTime;
        public float timer;
        public Vector3 unitPosition;

        public void MoveText(Camera camera)
        {
            float delta = 1f - (timer / maxTime);
            Vector3 pos = unitPosition + new Vector3(delta, delta, 0);
            pos = camera.WorldToScreenPoint(pos);
            pos.z = 0;

            textUI.transform.position = pos;
        }
    }
    public static DamageUI Instance { get; private set;}
    private const int POOL_SIZE = 64;

    [SerializeField] private Text textPrefab;
    [SerializeField] private Color enemyColor;
    [SerializeField] private Color playerColor;
    [SerializeField] private float maxTime;

    private Queue<Text> pool = new Queue<Text>();
    private List<ActiveText> activeTexts = new List<ActiveText>();
    private Camera _camera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        for (int i = 0; i < POOL_SIZE; i++)
        {
            Text text = Instantiate(textPrefab, transform);
            text.gameObject.SetActive(false);
            pool.Enqueue(text);
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeTexts.Count; i++)
        {
            ActiveText activeText = activeTexts[i];
            activeText.timer -= Time.deltaTime;
            if (activeText.timer <= 0)
            {
                activeText.textUI.gameObject.SetActive(false);
                pool.Enqueue(activeText.textUI);
                activeTexts.RemoveAt(i);
                i--;
            }
            else
            {
                Color color = activeText.textUI.color;
                color.a = activeText.timer / activeText.maxTime;
                activeText.textUI.color = color;

                activeText.MoveText(_camera);
            }
        }
    }

    public void AddText(int amount, Vector3 unitPosition, bool isEnemy)
    {
        Text text = pool.Dequeue();
        text.gameObject.SetActive(true);
        text.text = amount.ToString();
        text.color = isEnemy ? enemyColor : playerColor;

        ActiveText activeText = new ActiveText();
        activeText.textUI = text;
        activeText.maxTime = this.maxTime;
        activeText.timer = maxTime;
        activeText.unitPosition = unitPosition + Vector3.up;
        activeText.MoveText(_camera);
        activeTexts.Add(activeText);
    }

}
