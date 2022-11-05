using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TimeObject))]
public class TimeObjectEditor : Editor
{
    TimeObject thisTimeObject;

    private void OnEnable()
    {
        thisTimeObject = serializedObject.targetObject as TimeObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");
        EditorGUILayout.Space(20);

        if (GUILayout.Button("Take Snapshot"))
        {
            SnapshotData newSnapShotData = CreateInstance<SnapshotData>();
            newSnapShotData.isActiveAtTimeStamp = true;
            newSnapShotData.sprite = thisTimeObject.SnapshotSprite;
            newSnapShotData.goalPosition = thisTimeObject.transform.position;
            PointInTime newPointInTime = new PointInTime(0,newSnapShotData);

            thisTimeObject.pointsInTime.Add(newPointInTime);
        }
    }
}
