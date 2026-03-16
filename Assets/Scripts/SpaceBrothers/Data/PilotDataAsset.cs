using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBrothers.Data
{
    /// <summary>
    /// Unity ScriptableObject that holds pilot definitions.
    /// Create via Assets ▶ Create ▶ SpaceBrothers ▶ PilotDataAsset.
    /// Follows the same pattern as TriTailorShop.GameCharacter.PlayerDataAsset.
    /// </summary>
    [CreateAssetMenu(menuName = "SpaceBrothers/PilotDataAsset")]
    public class PilotDataAsset : ScriptableObject
    {
        [Tooltip("All pilot archetypes available in the game.")]
        public List<PilotMasterData> pilots = new List<PilotMasterData>();

        [NonSerialized]
        private Dictionary<string, PilotMasterData> m_lookup;

        /// <summary>Returns the master data for the pilot with the given id, or null if not found.</summary>
        public PilotMasterData GetPilotData(string id)
        {
            if (m_lookup == null)
                BuildLookup();

            m_lookup.TryGetValue(id, out PilotMasterData result);
            return result;
        }

        private void BuildLookup()
        {
            m_lookup = new Dictionary<string, PilotMasterData>(pilots.Count);
            for (int i = 0; i < pilots.Count; i++)
            {
                if (pilots[i] != null && !string.IsNullOrEmpty(pilots[i].id))
                    m_lookup[pilots[i].id] = pilots[i];
            }
        }

        // Invalidate the lookup cache whenever pilot data is changed in the editor.
        private void OnValidate()
        {
            m_lookup = null;
        }
    }
}
