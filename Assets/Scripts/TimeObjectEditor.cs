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

        if (GUILayout.Button("Take Snapshot"))
        {
            SnapshotData newSnapShotData = CreateInstance<SnapshotData>();
            newSnapShotData.isActiveAtTimeStamp = true;
            newSnapShotData.sprite = thisTimeObject.SnapshotSprite;
            newSnapShotData.goalPosition = thisTimeObject.transform.position;

            float currentTimeStamp = FindObjectOfType<TESF.TimeLineManager>().timeSlider.value;

            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/Snapshot " + thisTimeObject.gameObject.name + " " + currentTimeStamp + ".asset");
            AssetDatabase.CreateAsset(newSnapShotData, path);

            PointInTime newPointInTime = new PointInTime(currentTimeStamp, newSnapShotData);


            thisTimeObject.pointsInTime.Add(newPointInTime);
        }

        EditorGUILayout.LabelField("-------------------------------------------------------------------------");
        EditorGUILayout.Space(20);
    }

    private void OnSceneGUI()
    {
        thisTimeObject.SnapshotPosition = thisTimeObject.transform.position;
    }
}
