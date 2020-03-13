using System.Collections.Generic;
using BigBrainIndie.XtremeLabSafety.Controllers;
using BigBrainIndie.XtremeLabSafety.Classes;

/*******************************
 * File name:     BeakerControllerExtensions.cs
 * Creation date: 21/10/2019
 * Author:        Erlend Samstad
 * 
 * Description:
 * Extension methods for
 * BeakerController.cs. Is just
 * used for checking the mix in
 * the beaker.
 * *****************************/

namespace BigBrainIndie.XtremeLabSafety.Extensions {
    public static class BeakerControllerExtensions {

        public static string[,] _chemData; // Contains all the different combinations of the chemical mixes
        public static int _chemDataAmount = 0;

        public static string[,] _compoundData; // Contains all the different combinations of compound mixes
        public static int _compoundDataAmount = 0;

        /// <summary>
        /// Call this to check the mix
        /// </summary>
        /// <param name="_b">The beaker you want to check</param>
        public static void CheckMix (this BeakerController _b) {
            // Extract Beaker data
            List<Chemical> _chemical = _b.chemicalMix;
            int _parts = _b.parts;
            string _mix = string.Empty;

            bool _hasCompound = false;
            foreach (var _c in _chemical) {
                if (_c.Compound) {
                    _hasCompound = true;
                    break;
                }
            }

            // Checks all the chemicals
            for (int i = 0; i < _chemical.Count; i++) {
                if (!_chemical[i].Compound) {
                    switch (_chemical[i].Trait) {
                        case Chemical.Traits.caustic:
                            _mix += "Cau";
                            break;
                        case Chemical.Traits.flammable:
                            _mix += "Flam";
                            break;
                        case Chemical.Traits.flashbang:
                            _mix += "Flash";
                            break;
                        case Chemical.Traits.foaming:
                            _mix += "Foam";
                            break;
                        case Chemical.Traits.foggy:
                            _mix += "Fog";
                            break;
                        case Chemical.Traits.toxic:
                            _mix += "Tox";
                            break;
                        case Chemical.Traits.explosive:
                            _mix += "Vol";
                            break;
                        case Chemical.Traits.water:
                            _mix += "Wat";
                            break;
                        default:
                            _mix += "NA";
                            break;
                    }
                } else {
                    // Can't use switch because colors can't be constants
                    Chemical _c = _chemical[i];
                    //Debug.Log("_c.Color: " + _c.Color.ToString());

                    if (_c.Color == ChemicalColor.DarkCyan)
                        _mix += "DCyan";
                    else if (_c.Color == ChemicalColor.DarkGreen)
                        _mix += "DGreen";
                    else if (_c.Color == ChemicalColor.DarkBlue)
                        _mix += "DBlue";
                    else if (_c.Color == ChemicalColor.Purple)
                        _mix += "P";
                    else if (_c.Color == ChemicalColor.DarkGray)
                        _mix += "DGrey";
                    else if (_c.Color == ChemicalColor.DarkRed)
                        _mix += "DRed";
                }

                if (i < _chemical.Count - 1) {
                    _mix += " + ";
                }
            }

            if (!_hasCompound) {
                // Compares the mix with the interaction-sheet mixes
                for (int i = 0; i < _chemDataAmount; i++) {
                    //Debug.LogFormat("_chemData[{0}, 2]: {1}", i, _chemData[i, 2]);
                    if (_chemData[i, 1].Contains(_mix)) {
                        bool _heat = bool.Parse(_chemData[i, 3]);

                        if (_heat && _b.heatingState != BeakerController.HeatingStates.ready)
                            break;

                        _b.stopReactionInstance = _b.StartCoroutine(_b.StartReaction(_chemData[i, 2], 100));
                        break;
                    }
                }
            } else {
                // Compares the mix with the compound interaction-sheet mixes
                for (int i = 0; i < _compoundDataAmount; i++) {
                    //Debug.LogFormat("_compoundData[{0}, 1]: {1}", i, _compoundData[i, 1]);
                    if (_compoundData[i, 1].Contains(_mix)) {

                        _b.stopReactionInstance = _b.StartCoroutine(_b.StartReaction(_compoundData[i, 2], 100));
                        break;
                    }
                }
            }
        }
    }
}