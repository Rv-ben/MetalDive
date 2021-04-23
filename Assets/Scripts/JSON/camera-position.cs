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
        var wtf = new Vector3(this.x, this.y, this.z);
        return wtf;
    }

    // Given JSON input:
    // { "x": 0.05, "y": 0.19, "z": -0.15 }
    // this example will return a CameraInfo object with populated values
}