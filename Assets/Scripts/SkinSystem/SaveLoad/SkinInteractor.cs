using UnityEngine;
public class SkinInteractor : MonoBehaviour
{
    private SkinRepository repository;
    private SkinManager skinManager;
    private void Start()
    {
        repository = new SkinRepository();
        skinManager = ServiceLocator.GetService<SkinManager>();
        repository.LoadSkins(OnLoadSkins);
        GameSaver.Instance.AddListener(OnSaveGame);
        DontDestroyOnLoad(gameObject);
    }

    private void OnLoadSkins(SkinSaveData[] skins)
    {
        if (skins != null)
        {
            for (int i = 0; i < skins.Length; i++)
            {
                var data = skins[i];
                var skin = skinManager.GetSkin(data.id);
                skin.Setup(data.purchased);
            }
        }  
    }

    private void OnSaveGame()
    {
        ISkin[] skins = skinManager.GetAllSkins();
        SkinSaveData[] skinSaveDatas = new SkinSaveData[skins.Length];
        for (int i = 0; i < skins.Length; i++)
        {
            var skin = skins[i];
            var data = new SkinSaveData
            {
                id = skin.Id,
                purchased = skin.Purchased

            };
            skinSaveDatas[i] = data;
        }
        repository.SaveSkins(skinSaveDatas);
    }
}
