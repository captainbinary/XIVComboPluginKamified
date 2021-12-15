using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboKamifiedPlugin.Combos
{
    internal static class All
    {
        public const byte JobID = 0;

        public const uint
            LucidDreaming = 7562,
            Blizzard = 142,
            Jolt = 7503,
            Ruin = 163,
            Stone = 119,
            Malefic = 3596,
            Dosis = 24283,
            Swiftcast = 7561,
            Resurrection = 173,
            Verraise = 7523,
            Raise = 125,
            Ascend = 3603,
            Egeiro = 24287;

        public static class Buffs
        {
            public const ushort
                Swiftcast = 167;
        }

        public static class Levels
        {
            public const byte
                LucidDreaming = 24,
                Raise = 12;
        }
    }

    internal class AllSwiftcastFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllSwiftcastFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Raise || actionID == All.Resurrection || actionID == All.Ascend || actionID == All.Verraise || actionID == All.Egeiro)
            {
                var swiftCD = GetCooldown(All.Swiftcast);
                if ((swiftCD.CooldownRemaining == 0 && !HasEffect(RDM.Buffs.Dualcast))
                    || level <= All.Levels.Raise
                    || (level <= RDM.Levels.Verraise && actionID == All.Verraise))
                    return All.Swiftcast;
            }

            return actionID;
        }
    }

    internal class AllLucidDreaming : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllLucidDreaming;

        protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
        {
            if (actionID == All.Blizzard || actionID == All.Jolt || actionID == All.Ruin || actionID == All.Stone || actionID == All.Malefic || actionID == All.Dosis)
            {
                var cooldown = GetCooldown(All.LucidDreaming);
                if (level >= All.Levels.LucidDreaming && cooldown.CooldownRemaining == 0)
                    return All.LucidDreaming;
            }

            return actionID;
        }
    }
}