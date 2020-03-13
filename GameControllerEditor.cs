using UnityEngine;
using UnityEditor;
using BigBrainIndie.XtremeLabSafety.Controllers;
using BigBrainIndie.XtremeLabSafety.Profiles;
using BigBrainIndie.XtremeLabSafety.Classes;

/*******************************
 * File name:     GameControllerEditor.cs
 * Creation date: 14/10/2019
 * Author:        Erlend Samstad
 * 
 * Description:
 * Shows the levels in the
 * inspector
 * *****************************/

namespace BigBrainIndie.XtremeLabSafety.Editors {
    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor {

        int showCustomObjectsIndex = 0;

        public override void OnInspectorGUI () {
            GameController _gc = (GameController)target;

            if (_gc.lep != null) { // lep is the LevelEditorProfile that is place in the GameController
                foreach (var level in _gc.lep.Levels) {
                    EditorGUILayout.LabelField("Level ID:", level.ID.ToString());
                    EditorGUILayout.LabelField("Level Name:", level.LevelName);

                    EditorGUILayout.LabelField("Required chemical:", level.TargetChemical.ToString());

                    EditorGUILayout.LabelField("Chemical Groups:");
                    for (int i = 0; i < level.ChemicalGroups.Count; i++)
                        EditorGUILayout.LabelField(" - Chemical Group #" + (i + 1).ToString() + ": " + level.ChemicalGroups[i]);

                    if (level.CustomObjects.Count > 0) {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("Custom objects:");
                        if (GUILayout.Button("Show Custom objects"))
                            showCustomObjectsIndex = _gc.lep.Levels.IndexOf(level);

                        EditorGUILayout.EndHorizontal();

                        foreach (var co in level.CustomObjects) {
                            if (co == null)
                                continue;

                            EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField(" - " + co.name);
                            if (GUILayout.Button("Reset Position"))
                                ResetPosition(level, level.CustomObjects.IndexOf(co));
                            else if (GUILayout.Button("Reset Rotation"))
                                ResetRotation(level, level.CustomObjects.IndexOf(co));

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    EditorGUILayout.Space();

                    Rect _lineRect = EditorGUILayout.GetControlRect(false, 3);
                    _lineRect.height = 3;
                    EditorGUI.DrawRect(_lineRect, Color.gray);

                    EditorGUILayout.Space();
                }
            } else {
                EditorGUILayout.HelpBox("No Level Editor Profile has been set!", MessageType.Error);
            }

            DrawDefaultInspector();
        }

        void OnSceneGUI () {
            GameController _gc = target as GameController;
            LevelEditorProfile _l = _gc.lep;

            if (_l != null) {
                for (int i = 0; i < _l.Levels.Count; i++) {
                    if (i != showCustomObjectsIndex)
                        continue;

                    // Displays the rotation and position handle of the custom object
                    for (int j = 0; j < _l.Levels[i].CustomSpawnPoints.Count; j++) {
                        _l.Levels[i].customSpawnPoints[j] = Handles.PositionHandle(_l.Levels[i].CustomSpawnPoints[j], _l.Levels[i].CustomRotations[j]);
                        _l.Levels[i].CustomRotations[j] = Handles.RotationHandle(_l.Levels[i].CustomRotations[j], _l.Levels[i].CustomSpawnPoints[j]);

                        // Adds a label to the object
                        Handles.Label(_l.Levels[i].CustomSpawnPoints[j] + new Vector3(0.01f, 0.05f), string.Format("Level #{0},\nObject: {1}\n x: {2}, y: {3}, z: {4}",
                                                                                                                  (i + 1).ToString(),
                                                                                                                  (_l.Levels[i].CustomObjects[j] != null) ? _l.Levels[i].CustomObjects[j].name : "null",
                                                                                                                  _l.Levels[i].CustomSpawnPoints[j].x.ToString("0.00"),
                                                                                                                  _l.Levels[i].CustomSpawnPoints[j].y.ToString("0.00"),
                                                                                                                  _l.Levels[i].CustomSpawnPoints[j].z.ToString("0.00")));

                    }
                }
            }
        }

        void ResetPosition (Level _level, int i) {
            _level.customSpawnPoints[i] = Vector3.zero;
        }

        void ResetRotation (Level _level, int i) {
            _level.customRotations[i] = Quaternion.identity;
        }
    }
}