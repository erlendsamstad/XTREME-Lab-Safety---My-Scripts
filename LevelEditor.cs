using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BigBrainIndie.XtremeLabSafety.Classes;
using BigBrainIndie.XtremeLabSafety.Profiles;

/*******************************
 * File name:     LevelEditor.cs
 * Creation date: 14/10/2019
 * Author:        Erlend Samstad
 * 
 * Description:
 * Sets up the visuals for the
 * level editor profile.
 * *****************************/

namespace BigBrainIndie.XtremeLabSafety.Editors {
    [CustomEditor(typeof(LevelEditorProfile))]
    public class LevelEditor : Editor {

        static LevelEditorProfile _lep = null;

        public override void OnInspectorGUI () {
            _lep = (LevelEditorProfile)target;

            // Goes through all the levels
            for (int i = 0; i < _lep.Levels.Count; i++) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Level ID: ", _lep.Levels[i].ID.ToString());

                if (GUILayout.Button("Insert Level"))
                    _lep.InsertLevel(i);
                else if (GUILayout.Button("Delete Level"))
                    _lep.RemoveLevel(i);

                EditorGUILayout.EndHorizontal();

                _lep.Levels[i].SetLevelName(EditorGUILayout.TextField("Level Name:", _lep.Levels[i].LevelName));
                _lep.Levels[i].SetUsingGoggles(EditorGUILayout.Toggle("Use Goggles:", _lep.Levels[i].UseGoggles));
                _lep.Levels[i].SetUsingGloves(EditorGUILayout.Toggle("Use Gloves:", _lep.Levels[i].UsingGloves));
                _lep.Levels[i].SetUsingBurner(EditorGUILayout.Toggle("Use Burner:", _lep.Levels[i].UsingBurner));

                _lep.Levels[i].mixBeaker = EditorGUILayout.IntField("Mix beaker unit: ", _lep.Levels[i].MixBeaker);

                _lep.Levels[i].targetChemical = (ChemicalColor.ColorSelect)EditorGUILayout.EnumPopup("Target Chemical: ", _lep.Levels[i].targetChemical);

                GUILayout.Label(new GUIContent("Row 1", "The output chemical is automatically set based on the chemical reaction sheet"));
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Add chemical") && _lep.Levels[i].Instruct1.Count < 3)
                    _lep.Levels[i].instruct1.Add(new Chemical.Traits());
                else if (GUILayout.Button("Remove chemical") && _lep.Levels[i].Instruct1.Count > 0)
                    _lep.Levels[i].instruct1.RemoveAt(_lep.Levels[i].Instruct1.Count - 1);

                EditorGUILayout.EndHorizontal();

                for (int j = 0; j < _lep.Levels[i].Instruct1.Count; j++)
                    _lep.Levels[i].instruct1[j] = (Chemical.Traits)EditorGUILayout.EnumPopup("Chemical #" + (j + 1).ToString(), _lep.Levels[i].Instruct1[j]);

                GUILayout.Label(new GUIContent("Row 2", "Chemical #1 is automatically set to the output chemical from previous row"));
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Add chemical") && _lep.Levels[i].Instruct2.Count < 2)
                    _lep.Levels[i].instruct2.Add(new Chemical.Traits());
                else if (GUILayout.Button("Remove chemical") && _lep.Levels[i].Instruct2.Count > 0)
                    _lep.Levels[i].instruct2.RemoveAt(_lep.Levels[i].Instruct2.Count - 1);

                EditorGUILayout.EndHorizontal();

                for (int j = 0; j < _lep.Levels[i].Instruct2.Count; j++)
                    _lep.Levels[i].instruct2[j] = (Chemical.Traits)EditorGUILayout.EnumPopup("Chemical #" + (j + 2).ToString(), _lep.Levels[i].Instruct2[j]);

                GUILayout.Label(new GUIContent("Row 3", "Chemical #1 is automatically set to the output chemical from previous row"));
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Add chemical") && _lep.Levels[i].Instruct3.Count < 2)
                    _lep.Levels[i].instruct3.Add(new Chemical.Traits());
                else if (GUILayout.Button("Remove chemical") && _lep.Levels[i].Instruct3.Count > 0)
                    _lep.Levels[i].instruct3.RemoveAt(_lep.Levels[i].Instruct3.Count - 1);

                EditorGUILayout.EndHorizontal();

                for (int j = 0; j < _lep.Levels[i].Instruct3.Count; j++)
                    _lep.Levels[i].instruct3[j] = (Chemical.Traits)EditorGUILayout.EnumPopup("Chemical #" + (j + 2).ToString(), _lep.Levels[i].Instruct3[j]);
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Beaker groups:");

                if (GUILayout.Button("Add Group"))
                    _lep.Levels[i].ChemicalGroups.Add(new Chemical.Traits());
                else if (GUILayout.Button("Remove Group"))
                    _lep.Levels[i].ChemicalGroups.RemoveAt(_lep.Levels[i].chemicalGroups.Count - 1);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginVertical();

                for (int j = 0; j < _lep.Levels[i].ChemicalGroups.Count; j++)
                    _lep.Levels[i].chemicalGroups[j] = (Chemical.Traits)EditorGUILayout.EnumPopup("Group #" + (j + 1).ToString(), _lep.Levels[i].ChemicalGroups[j]);

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Custom Spawning:");

                if (GUILayout.Button("Add spawn")) {
                    _lep.Levels[i].CustomObjects.Add(null);
                    _lep.Levels[i].CustomSpawnPoints.Add(new Vector3());
                    _lep.Levels[i].CustomRotations.Add(Quaternion.identity);
                } else if (GUILayout.Button("Remove spawn")) {
                    _lep.Levels[i].CustomObjects.RemoveAt(_lep.Levels[i].CustomObjects.Count - 1);
                    _lep.Levels[i].CustomSpawnPoints.RemoveAt(_lep.Levels[i].CustomSpawnPoints.Count - 1);
                    _lep.Levels[i].CustomRotations.RemoveAt(_lep.Levels[i].CustomRotations.Count - 1);
                }

                EditorGUILayout.EndHorizontal();

                // Displays the custom objects in the level editor
                for (int j = 0; j < _lep.Levels[i].CustomObjects.Count; j++)
                    _lep.Levels[i].customObjects[j] = (GameObject)EditorGUILayout.ObjectField(_lep.Levels[i].CustomObjects[j], typeof(GameObject), false);

                EditorGUILayout.Space();

                Rect _lineRect = EditorGUILayout.GetControlRect(false, 3);
                _lineRect.height = 3;
                EditorGUI.DrawRect(_lineRect, Color.gray);

                EditorGUILayout.Space();
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add new Level"))
                _lep.AddLevel();

            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
                _lep.Changed();
        }

        [MenuItem("Assets/Create/Level Editor Profile", false, 1)]
        static void CreateLevelEditorProfile () {
            LevelEditorProfile _lep = CreateInstance<LevelEditorProfile>();
            AssetDatabase.CreateAsset(_lep, "Assets/Resources/LevelEditorProfile.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}