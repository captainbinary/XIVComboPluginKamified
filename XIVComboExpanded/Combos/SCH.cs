using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboKamifiedPlugin.Combos
{
    internal static class SCH
    {
        public const byte ClassID = 15;
        public const byte JobID = 28;

        public const uint
            Bio = 17869,
            FeyBless = 16543,
            Consolation = 16546,
            EnergyDrain = 167,
            Aetherflow = 166;

        public static class Buffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte Placeholder = 0;
        }
    }

    internal class ScholarSeraphConsolationFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarSeraphConsolationFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.FeyBless)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.SeraphTimer > 0)
                    return SCH.Consolation;
            }

            return actionID;
        }
    }

    internal class ScholarEnergyDrainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarEnergyDrainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.EnergyDrain)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.Aetherflow == 0)
                    return SCH.Aetherflow;
            }

            return actionID;
        }
    }
}
