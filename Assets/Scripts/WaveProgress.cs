using InstantGamesBridge;

public class WaveProgress : Singleton<WaveProgress>
{
    private const string KEY = "waveProgress";
    public int CurrentWave { get; private set; }

    private void Start()
    {
        LoadWaveProgress();
    }
    public void CompleteWave()
    {
#if !UNITY_EDITOR
        YandexMetrica.EventWinWave(CurrentWave);
#endif
        CurrentWave++;
        SaveWaveProgress();
    }

    private void SaveWaveProgress()
    {
        Bridge.game.SetData(KEY, CurrentWave);
    }

    private void LoadWaveProgress()
    {
        int currentWave;
        Bridge.game.GetData(KEY, (succes, data) => {
            if (succes && data != null)
            {
                currentWave = int.Parse(data);
            }
            else
            {
                currentWave = 0;
            }
            CurrentWave = currentWave;
        });
    }

    //Debug
    public void SetProgress(int wave)
    {
        CurrentWave = wave;
    }
}
