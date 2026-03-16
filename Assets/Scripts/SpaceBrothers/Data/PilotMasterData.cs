using System;
using UnityEngine;

namespace SpaceBrothers.Data
{
    /// <summary>
    /// Serializable master-data definition for a Space Brothers pilot.
    /// Configured in the Unity editor via <see cref="PilotDataAsset"/> and shared
    /// across all runtime instances of the same pilot archetype.
    /// Follows the same pattern as TriTailorShop.Data.ItemMasterData.
    /// </summary>
    [Serializable]
    public class PilotMasterData
    {
        public string id;
        public string pilotName;

        [Tooltip("Portrait sprite shown in the UI.")]
        public Sprite portrait;

        // --- S.P.A.C.E base attribute values (1–10) ---

        [Range(AttributeData.MinValue, AttributeData.MaxValue)]
        public int staminaBase     = 5;

        [Range(AttributeData.MinValue, AttributeData.MaxValue)]
        public int perceptionBase  = 5;

        [Range(AttributeData.MinValue, AttributeData.MaxValue)]
        public int agilityBase     = 5;

        [Range(AttributeData.MinValue, AttributeData.MaxValue)]
        public int courageBase     = 5;

        [Range(AttributeData.MinValue, AttributeData.MaxValue)]
        public int expertiseBase   = 5;

        // --- S.P.A.C.E growth potential (0–3 stars) ---

        [Range(0, 3)]
        public int staminaStars    = 1;

        [Range(0, 3)]
        public int perceptionStars = 1;

        [Range(0, 3)]
        public int agilityStars    = 1;

        [Range(0, 3)]
        public int courageStars    = 1;

        [Range(0, 3)]
        public int expertiseStars  = 1;
    }
}
