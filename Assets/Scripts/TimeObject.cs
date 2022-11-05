using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    public PointInTime currentPointInTime;

    [Space(20)]
    [Header("References")]
    [SerializeField]
    private SpriteRenderer visuals;
    [SerializeField]
    private SpriteRenderer oldVisuals;

    [Space(20)]
    [Header("SnapshotData")]
    [SerializeField]
    private bool editMode;

    [Space(20)]
    [SerializeField]
    public bool SnapshotIsActiveAtTimeStamp;
    [SerializeField]
    public Sprite SnapshotSprite;
    [SerializeField]
    public Vector3 SnapshotPosition;

    [ExecuteInEditMode]
    private void OnValidate()
    {
        if (editMode)
        {
            visuals.sprite = SnapshotSprite;
            SnapshotPosition = this.transform.position;
        }
        else
        {
            currentPointInTime = pointsInTime[0];
            currentPointInTime.snapshotData.isActiveAtTimeStamp = pointsInTime[0].snapshotData.isActiveAtTimeStamp;
            visuals.sprite = pointsInTime[0].snapshotData.sprite;
            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 0);
            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 1);
            transform.position = pointsInTime[0].snapshotData.goalPosition;
        }

        if (pointsInTime.Count > 0)
        {
            pointsInTime = pointsInTime.OrderBy(point => point.timeStamp).ToList();
        }
    }

    private void Awake()
    {
        LeanTween.alpha(oldVisuals.gameObject, 0, 0);
        LeanTween.alpha(visuals.gameObject, 1, 0);

        foreach (var point in pointsInTime)
        {
            if (point.snapshotData.goalPosition == Vector3.zero)
            {
                point.snapshotData.goalPosition = transform.position;
            }
            if (point.snapshotData.isActiveAtTimeStamp && !point.snapshotData.sprite)
            {
                point.snapshotData.sprite = visuals.sprite;
            }
        }

        if (pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
        }
    }

    public void UpdateTimeObject()
    {
        LeanTween.cancel(gameObject);
        if (currentPointInTime.snapshotData.isActiveAtTimeStamp)
        {
            oldVisuals.sprite = visuals.sprite;
            visuals.sprite = currentPointInTime.snapshotData.sprite;

            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 1);
            LeanTween.alpha(oldVisuals.gameObject, 0, .2f);

            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 0);
            LeanTween.alpha(visuals.gameObject, 1, .2f);
        }
        else
        {
            LeanTween.alpha(visuals.gameObject, 0, .2f);
        }

        LeanTween.move(gameObject, currentPointInTime.snapshotData.goalPosition, .5f);
    }
}
