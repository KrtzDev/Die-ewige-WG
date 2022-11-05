using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public float timeStamp;
    public bool isActiveAtTimeStamp = true;
    public Sprite sprite;
    public Vector3 goalPosition;
}