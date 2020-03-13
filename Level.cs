using System;
using System.Collections.Generic;
using UnityEngine;

/*******************************
 * File name:     Level.cs
 * Creation date: 06/10/2019
 * Author:        Erlend Samstad
 * 
 * Description:
 * Contains all the data needed
 * for each level
 * *****************************/

namespace BigBrainIndie.XtremeLabSafety.Classes {
    [Serializable]
    public class Level {

        public int id;
        public string levelName;

        public bool useGoggles;
        public bool usingGloves;
        public bool usingBurner;

        public List<Chemical.Traits> instruct1 = new List<Chemical.Traits>();
        public List<Chemical.Traits> instruct2 = new List<Chemical.Traits>();
        public List<Chemical.Traits> instruct3 = new List<Chemical.Traits>();

        public int mixBeaker;
        [Tooltip("Set what chemical you want delivered")] public ChemicalColor.ColorSelect targetChemical;
        public List<string> warningLabelsNames = new List<string>();

        public float loadWait = 0;

        public List<Chemical.Traits> chemicalGroups = new List<Chemical.Traits>();

        public List<GameObject> customObjects = new List<GameObject>();
        public List<Vector3> customSpawnPoints = new List<Vector3>();
        public List<Quaternion> customRotations = new List<Quaternion>();

        /// <summary>
        /// An empty level with the standard values set
        /// </summary>
        public Level () { }

        // Setters
        // All setters are referenced in the LevelEditor
        public void SetID (int _id) {
            this.id = _id;
        }

        public void SetLevelName (string _levelName) {
            this.levelName = _levelName;
        }

        public void SetUsingGoggles (bool _usingGoggles) {
            this.useGoggles = _usingGoggles;
        }

        public void SetUsingGloves (bool _usingGloves) {
            this.usingGloves = _usingGloves;
        }

        public void SetUsingBurner (bool _usingBurner) {
            this.usingBurner = _usingBurner;
        }

        // Getters
        public int ID => id;
        public string LevelName => levelName;

        public bool UseGoggles => useGoggles;
        public bool UsingGloves => usingGloves;
        public bool UsingBurner => usingBurner;

        public List<Chemical.Traits> Instruct1 => instruct1; 
        public List<Chemical.Traits> Instruct2 => instruct2;
        public List<Chemical.Traits> Instruct3 => instruct3; 
        public int MixBeaker => mixBeaker;
        /// <summary>
        /// What Chemical you want delivered
        /// </summary>
        public ChemicalColor.ColorSelect TargetChemical => targetChemical;

        public List<string> WarningLabelsNames => warningLabelsNames;

        public float LoadWait => loadWait;
        public List<Chemical.Traits> ChemicalGroups => chemicalGroups;
        public List<GameObject> CustomObjects => customObjects;
        public List<Vector3> CustomSpawnPoints => customSpawnPoints;
        public List<Quaternion> CustomRotations => customRotations;
    }
}