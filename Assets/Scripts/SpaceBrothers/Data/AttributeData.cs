using UnityEngine.Events;

namespace SpaceBrothers.Data
{
    /// <summary>
    /// Runtime data for a single S.P.A.C.E attribute.
    /// Tracks the current value, enforces min/max bounds, and fires change events.
    /// Follows the same pattern as TriTailorShop.Data.ResourceData.
    /// </summary>
    public class AttributeData
    {
        public const int MinValue = 1;
        public const int MaxValue = 10;

        private readonly PilotAttribute m_attribute;
        private int m_value;
        private int m_stars; // growth potential: 0 – 3 stars (Battle Brothers-inspired)

        public PilotAttribute Attribute => m_attribute;
        public int Value => m_value;

        /// <summary>Stars represent the pilot's growth potential for this attribute (0–3).</summary>
        public int Stars => m_stars;

        /// <summary>Fired with (oldValue, newValue) whenever the attribute value changes.</summary>
        public readonly UnityEvent<int, int> onValueChanged = new UnityEvent<int, int>();

        public AttributeData(PilotAttribute attribute, int initialValue = MinValue, int stars = 0)
        {
            m_attribute = attribute;
            m_value = Clamp(initialValue);
            m_stars = ClampStars(stars);
        }

        /// <summary>
        /// Increases the attribute value by <paramref name="amount"/> up to MaxValue.
        /// </summary>
        public void Add(int amount)
        {
            if (amount <= 0) return;
            int oldValue = m_value;
            m_value = Clamp(m_value + amount);
            if (m_value != oldValue)
                onValueChanged.Invoke(oldValue, m_value);
        }

        /// <summary>
        /// Decreases the attribute value by <paramref name="amount"/> down to MinValue.
        /// Returns true if the reduction was possible.
        /// </summary>
        public bool TryReduce(int amount)
        {
            if (amount <= 0) return false;
            int newValue = m_value - amount;
            if (newValue < MinValue) return false;
            int oldValue = m_value;
            m_value = newValue;
            onValueChanged.Invoke(oldValue, m_value);
            return true;
        }

        /// <summary>Sets the growth potential stars (clamped to 0–3).</summary>
        public void SetStars(int stars)
        {
            m_stars = ClampStars(stars);
        }

        private static int Clamp(int value)
        {
            if (value < MinValue) return MinValue;
            if (value > MaxValue) return MaxValue;
            return value;
        }

        private static int ClampStars(int stars)
        {
            if (stars < 0) return 0;
            if (stars > 3) return 3;
            return stars;
        }
    }
}
