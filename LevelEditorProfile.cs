using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BigBrainIndie.XtremeLabSafety.Classes;

/*******************************
 * File name:     LevelCreator.cs
 * Creation date: 14/10/2019
 * Author:        Erlend Samstad
 * 
 * Description:
 * This script will contain all
 * the levels and will be used
 * as a visual for the level
 * editor
 * *****************************/

namespace BigBrainIndie.XtremeLabSafety.Profiles {
    public class LevelEditorProfile : ScriptableObject {

        [HideInInspector] public List<Level> levels = new List<Level>();

        public void AddLevel () {
            Level _level = new Level();
            _level.SetID(levels.Count);
            _level.SetLevelName("Level_" + _level.ID.ToString());

            levels.Add(_level);
        }

        public void InsertLevel (int _index) {
            levels.Insert(_index, new Level());

            for (int i = 0; i < levels.Count; i++) {
                levels[i].SetID(i);
            }
        }

        public void RemoveLevel (int _index) {
            levels.RemoveAt(_index);

            for (int i = 0; i < levels.Count; i++) {
                levels[i].SetID(i);
            }
        }

        public void Changed () {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public List<Level> Levels => levels;
    }
}