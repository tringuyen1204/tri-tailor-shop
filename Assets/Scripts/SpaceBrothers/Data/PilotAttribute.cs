namespace SpaceBrothers.Data
{
    /// <summary>
    /// The S.P.A.C.E attribute system for Space Brothers pilots.
    /// Each letter represents a core pilot trait loosely inspired by Battle Brothers.
    /// </summary>
    public enum PilotAttribute
    {
        Stamina    = 1 << 0,  // S – Physical endurance; governs health pool and fatigue resistance
        Perception = 1 << 1,  // P – Situational awareness; governs targeting accuracy and threat detection
        Agility    = 1 << 2,  // A – Speed and reflexes; governs dodge chance and action order
        Courage    = 1 << 3,  // C – Mental fortitude; governs morale checks and stress resistance
        Expertise  = 1 << 4,  // E – Technical skill; governs equipment effectiveness and mission success rate
    }
}
