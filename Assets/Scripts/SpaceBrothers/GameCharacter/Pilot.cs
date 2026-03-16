using SpaceBrothers.Data;
using TriTailorShop.GameCharacter;
using UnityEngine.Events;

namespace SpaceBrothers.GameCharacter
{
    /// <summary>
    /// A Space Brothers pilot.  Inherits the base inventory from
    /// <see cref="TriTailorShop.GameCharacter.GameCharacter"/> and adds the
    /// S.P.A.C.E attribute system.
    /// </summary>
    public class Pilot : TriTailorShop.GameCharacter.GameCharacter
    {
        // ── S.P.A.C.E attributes ─────────────────────────────────────────────
        public AttributeData Stamina    { get; } = new AttributeData(PilotAttribute.Stamina);
        public AttributeData Perception { get; } = new AttributeData(PilotAttribute.Perception);
        public AttributeData Agility    { get; } = new AttributeData(PilotAttribute.Agility);
        public AttributeData Courage    { get; } = new AttributeData(PilotAttribute.Courage);
        public AttributeData Expertise  { get; } = new AttributeData(PilotAttribute.Expertise);

        // ── Identity ──────────────────────────────────────────────────────────
        public PilotMasterData MasterData { get; private set; }

        /// <summary>Fired whenever any S.P.A.C.E attribute value changes.</summary>
        public readonly UnityEvent<PilotAttribute, int, int> onAttributeChanged
            = new UnityEvent<PilotAttribute, int, int>();

        public Pilot()
        {
            SubscribeAttributeEvents();
        }

        // ── Initialisation ────────────────────────────────────────────────────

        /// <summary>
        /// Loads base attribute values and growth stars from a
        /// <see cref="PilotMasterData"/> definition.
        /// </summary>
        public void LoadFromMasterData(PilotMasterData masterData)
        {
            MasterData = masterData;

            Stamina.Add(masterData.staminaBase - AttributeData.MinValue);
            Stamina.SetStars(masterData.staminaStars);

            Perception.Add(masterData.perceptionBase - AttributeData.MinValue);
            Perception.SetStars(masterData.perceptionStars);

            Agility.Add(masterData.agilityBase - AttributeData.MinValue);
            Agility.SetStars(masterData.agilityStars);

            Courage.Add(masterData.courageBase - AttributeData.MinValue);
            Courage.SetStars(masterData.courageStars);

            Expertise.Add(masterData.expertiseBase - AttributeData.MinValue);
            Expertise.SetStars(masterData.expertiseStars);
        }

        // ── Attribute access ──────────────────────────────────────────────────

        /// <summary>Returns the <see cref="AttributeData"/> for the given S.P.A.C.E attribute.</summary>
        public AttributeData GetAttribute(PilotAttribute attribute)
        {
            switch (attribute)
            {
                case PilotAttribute.Stamina:    return Stamina;
                case PilotAttribute.Perception: return Perception;
                case PilotAttribute.Agility:    return Agility;
                case PilotAttribute.Courage:    return Courage;
                case PilotAttribute.Expertise:  return Expertise;
                default:                        return null;
            }
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private void SubscribeAttributeEvents()
        {
            SubscribeAttribute(Stamina,    PilotAttribute.Stamina);
            SubscribeAttribute(Perception, PilotAttribute.Perception);
            SubscribeAttribute(Agility,    PilotAttribute.Agility);
            SubscribeAttribute(Courage,    PilotAttribute.Courage);
            SubscribeAttribute(Expertise,  PilotAttribute.Expertise);
        }

        private void SubscribeAttribute(AttributeData attr, PilotAttribute type)
        {
            attr.onValueChanged.AddListener((oldVal, newVal) =>
                onAttributeChanged.Invoke(type, oldVal, newVal));
        }
    }
}
