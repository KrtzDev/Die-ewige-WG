using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public float timestamp;
    public bool isActiveAtTimestamp = true;
    public Sprite sprite;
    public Vector3 goalPosition;
}
