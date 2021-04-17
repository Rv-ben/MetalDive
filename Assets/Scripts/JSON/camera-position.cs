using UnityEngine;

[System.Serializable]
public class CameraInfo
{
    public float x, y, z;

    public static CameraInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<CameraInfo>(jsonString);
    }

    public Vector3 GetCameraOffset()
    {
        Debug.Log(this.x + " " + this.y + " " + this.z);
        var wtf = new Vector3(this.x, this.y, this.z);
        Debug.Log(wtf);
        return wtf;
    }

    public string ToString()
    {
        return this.x + " " + this.y + " " + this.z;
    }

    // Given JSON input:
    // { "x": 0.05, "y": 0.19, "z": -0.15 }
    // this example will return a CameraInfo object with populated values
}