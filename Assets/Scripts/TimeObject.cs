using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    public PointInTime currentPointInTime;

    [HideInInspector] public SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        pointsInTime = pointsInTime.OrderBy(point => point.timestamp).ToList();
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        foreach (var point in pointsInTime)
        {
            if (point.goalPosition == Vector3.zero)
            {
                point.goalPosition = transform.position;
            }
            if (point.isActiveAtTimestamp && !point.sprite)
            {
                point.sprite = spriteRenderer.sprite;
            }
        }

        if (pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
        }
    }

    public void UpdateTimeObject()
    {
        spriteRenderer.sprite = currentPointInTime.sprite;

        transform.position = currentPointInTime.goalPosition;
    }
}
