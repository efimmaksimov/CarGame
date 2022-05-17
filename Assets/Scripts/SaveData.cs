using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    public int coins;
    public int health;
    //public int[] hasCar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //hasCar = new int[3];
    }

    public void DataFromJSON(string data)
    {
        if (data != null)
        {
            Save save = JsonUtility.FromJson<Save>(data);
            coins = save.coins;
            health = save.health;
            //hasCar = new int[3];
            //hasCar[0] = save.oldCar;
            //hasCar[1] = save.policeCar;
            //hasCar[2] = save.millitaryCar;
        }
    }

    public string DataToJSON()
    {
        Save save = new Save();
        save.coins = coins;
        save.health = health;
        //save.oldCar = hasCar[0];
        //save.policeCar = hasCar[1];
        //save.millitaryCar = hasCar[2];
        string json = JsonUtility.ToJson(save);
        return json;
    }
}

[System.Serializable]
public struct Save
{
    public int coins;
    public int health;
}
