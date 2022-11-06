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

        if (thisTimeObject.editMode)
        {
            if (GUILayout.Button("Take Snapshot"))
            {
                SnapshotData newSnapShotData = CreateInstance<SnapshotData>();
                newSnapShotData.isActiveAtTimeStamp = thisTimeObject.SnapshotIsActiveAtTimeStamp;
                newSnapShotData.sprite = thisTimeObject.SnapshotSprite;
                newSnapShotData.goalPosition = thisTimeObject.transform.position;

                float currentTimeStamp = FindObjectOfType<TESF.TimeLineManager>().timeSlider.value;
                if (!AssetDatabase.IsValidFolder($"Assets/ScriptableObjects/" + thisTimeObject.gameObject.name))
                {
                    AssetDatabase.CreateFolder("Assets/ScriptableObjects", thisTimeObject.gameObject.name);
                }
                string path = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/" + thisTimeObject.gameObject.name + "/Snapshot " + thisTimeObject.gameObject.name + " " + currentTimeStamp + ".asset");
                AssetDatabase.CreateAsset(newSnapShotData, path);

                PointInTime newPointInTime = new PointInTime(currentTimeStamp, newSnapShotData);

                thisTimeObject.pointsInTime.Add(newPointInTime);
                EditorUtility.SetDirty(thisTimeObject);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");
        EditorGUILayout.Space(20);
    }

    private void OnSceneGUI()
    {
        if (thisTimeObject.editMode)
        {
            thisTimeObject.SnapshotPosition = thisTimeObject.transform.position;
        }
    }
}
