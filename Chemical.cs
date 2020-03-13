using System;
using UnityEngine;

/************************************
 * File name:       Chemical.cs
 * Creation date:   12/09/2019
 * Author:          Erlend Samstad
 * 
 * Description:
 * Contains all data necessary for a
 * chemical.
 * **********************************/

namespace BigBrainIndie.XtremeLabSafety.Classes {
    [Serializable]
    public class Chemical {

        [Serializable]
        public enum Traits {
            na,
            caustic,
            flammable,
            flashbang,
            foaming,
            foggy,
            toxic,
            explosive,
            water,
            inert
        }

        // Chemical data
        public string chemicalName;
        public Color color;
        public float amount;
        public Traits trait;
        public bool compound;

        // Getters
        public string ChemicalName => chemicalName;
        public Color Color => color;
        public float Amount => amount;
        public Traits Trait => trait;
        public bool Compound => compound;


        // Sets a standard undefined chemical when no data is set
        public Chemical () {
            this.chemicalName = "<Unnamed>";
            this.color = ChemicalColor.NA;
            this.amount = 0;
            this.trait = Traits.na;
        }

        public Chemical (string _name, Color _color) {
            this.chemicalName = _name;
            this.color = _color;
            this.amount = 100;
            this.trait = Traits.na;
        }

        // Sets the name and color of the chemical
        public Chemical (string _name, Color _color, Traits _trait) {
            this.chemicalName = _name;
            this.color = _color;
            this.amount = 100;
            this.trait = _trait;
        }

        public void ChangeName (string _name) {
            this.chemicalName = _name;
        }

        public void ChangeColor (Color _color) {
            this.color = _color;
        }

        public void SetAmount (float _amount) {
            this.amount = _amount;
            this.amount = Mathf.Clamp(this.amount, 0, 100);
        }

        public void AddAmount (float _amount) {
            this.amount += _amount;
            this.amount = Mathf.Clamp(this.amount, 0, 100);
        }

        public void ChangeTrait (Traits _trait) {
            this.trait = _trait;
        }

        public void IsCompound (bool _c) {
            this.compound = _c;
        }
    }

    public struct ChemicalColor {

        public enum ColorSelect {
            NA,
            Red,
            Green,
            DarkGreen,
            Blue,
            DarkBlue,
            Cyan,
            DarkCyan,
            Orange,
            Yellow,
            Purple,
            Magenta,
            Gray,
            DarkGray,
            AquaMarine,
            Black,
            Amber,
            DarkPurple,
            Brown,
            DarkYellow,
            DarkRed,
            Water
        }

        // Pre-set of colors
        public static readonly Color NA = Color.white;
        public static readonly Color Red = Color.red;
        public static readonly Color Green = Color.green;
        public static readonly Color DarkGreen = new Color(0.11f, 0.42f, 0.11f);
        public static readonly Color Blue = Color.blue;
        public static readonly Color DarkBlue = new Color(0.043f, 0.325f, 0.58f);
        public static readonly Color Cyan = Color.cyan;
        public static readonly Color DarkCyan = new Color(0, 0.663f, 0.439f);
        public static readonly Color Orange = new Color(1, 0.62f, 0);
        public static readonly Color Yellow = Color.yellow;
        public static readonly Color Purple = new Color(0.6f, 0, 1);
        public static readonly Color Magenta = Color.magenta;
        public static readonly Color Gray = new Color(0.7f, 0.7f, 0.7f);
        public static readonly Color DarkGray = new Color(0.369f, 0.369f, 0.369f);
        public static readonly Color AquaMarine = new Color(0.49f, 1, 0.83f);
        public static readonly Color Black = Color.black;
        public static readonly Color Amber = new Color(1, 0.74f, 0);
        public static readonly Color DarkPurple = new Color(0.314f, 0.043f, 0.686f);
        public static readonly Color Brown = new Color(0.271f, 0.051f, 0.051f);
        public static readonly Color DarkYellow = new Color(0.769f, 0.698f, 0.035f);
        public static readonly Color DarkRed = new Color(0.663f, 0.11f, 0);
        public static readonly Color Water = new Color(0.5647059f, 0.6841081f, 1);
    }
}