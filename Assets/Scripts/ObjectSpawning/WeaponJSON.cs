using UnityEngine;

[System.Serializable]
public class WeaponJSON
{
    public GameObject weapon;
    public Vector3 localPosition;

    public WeaponJSON(GameObject weapon, Vector3 localPosition)
    {
        this.weapon = weapon;
        this.localPosition = localPosition;
    }

    public string CreateJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public static WeaponJSON CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<WeaponJSON>(json);
    }
}
