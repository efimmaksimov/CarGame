using System;
using InstantGamesBridge;

public class SkinRepository
{
    private string KEY = "skins";
    public void LoadSkins(Action<SkinSaveData[]> onComplete)
    {
        SkinSaveData[] skins;
        Bridge.game.GetData(KEY, (succes, data) => {
            if (succes && data != null)
            {
                skins = JsonHelper.FromJson<SkinSaveData>(data);
            }
            else
            {
                skins = null;
            }
            onComplete?.Invoke(skins);
        });
    }
    public void SaveSkins(SkinSaveData[] skins)
    {
        Bridge.game.SetData(KEY, JsonHelper.ToJson(skins));
    }
}
