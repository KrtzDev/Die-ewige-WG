using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public float timeStamp;
    public SnapshotData snapshotData;

    public PointInTime(float timestamp, SnapshotData snapshotData)
    {
        this.timeStamp = timestamp;
        this.snapshotData = snapshotData;
    }
}