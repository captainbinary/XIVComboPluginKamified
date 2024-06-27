using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SCH
    {
        public const byte ClassID = 15;
        public const byte JobID = 28;

        public const uint
            FeyBless = 16543,
            Consolation = 16546,
            EnergyDrain = 167,
            Aetherflow = 166,
            Indomitability = 3583,
            Lustrate = 189,
            Excogitation = 7434,
            SacredSoil = 188,
            SummonEos = 17215,
            SummonSelene = 17216,
            WhisperingDawn = 16537,
            FeyIllumination = 16538,
            Dissipation = 3587,
            Aetherpact = 7437,
            SummonSeraph = 16545,
            ChainStratagem = 7436,
            Recitation = 16542,
            EmergencyTactics = 3586,
            DeploymentTactics = 3585,
            Ruin = 17869,
            Ruin2 = 17870,
            Broil = 3584,
            Broil2 = 7435,
            Broil3 = 16541,
            Broil4 = 25865,
            Adloquium = 185,
            Physick = 190;

        public static class Buffs
        {
            public const ushort
                Aetherflow = 304,
                Dissipation = 791,
                Recitation = 1896;
        }

        public static class Debuffs
        {
            public const ushort
                ChainStratagem = 1221;
        }

        public static class Levels
        {
            public const byte
                Adloquium = 30,
                ChainStratagem = 66,
                Recitation = 74;
        }
    }

    internal class ScholarAdloPhysickSyncFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarAdloPhysickSyncFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == SCH.Adloquium && level < SCH.Levels.Adloquium ? SCH.Physick : actionID;
        }
    }

    internal class ScholarFairyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarFairyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<SCHGauge>();
            if (Service.BuddyList.PetBuddy == null && gauge.SeraphTimer == 0 && !HasEffect(SCH.Buffs.Dissipation))
                return SCH.SummonEos;

            return actionID;
        }
    }

    internal class ScholarRuinChainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarRuinChainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(SCH.ChainStratagem) != SCH.ChainStratagem)
                return OriginalHook(SCH.ChainStratagem);
            if (IsEnabled(CustomComboPreset.ScholarChainLockoutFeature) && TargetHasEffectAny(SCH.Debuffs.ChainStratagem) && FindTargetEffectAny(SCH.Debuffs.ChainStratagem)?.RemainingTime > 3)
                return actionID;
            if (GetCooldown(PLD.FastBlade).CooldownRemaining >= 0.5 && IsActionOffCooldown(SCH.ChainStratagem))
                return SCH.ChainStratagem;
            return actionID;
        }
    }

    internal class ScholarChainLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarChainLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == SCH.ChainStratagem && IsActionOffCooldown(SCH.ChainStratagem) && TargetHasEffectAny(SCH.Debuffs.ChainStratagem) && FindTargetEffectAny(SCH.Debuffs.ChainStratagem)?.RemainingTime > 3 ? SMN.Physick : actionID;
        }
    }

    internal class ScholarRuin2MovementFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarRuin2MovementFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.Ruin || actionID == SCH.Broil || actionID == SCH.Broil2 || actionID == SCH.Broil3 || actionID == SCH.Broil4)
            {
                if (IsMoving())
                    return SCH.Ruin2;
            }

            return actionID;
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

    internal class ScholarExcogRecitationFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarExcogRecitationFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return IsActionOffCooldown(SCH.Recitation) && level >= SCH.Levels.Recitation ? SCH.Recitation : actionID;
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
                // if (gauge.Aetherflow == 0)
                if (!HasEffect(SCH.Buffs.Aetherflow))
                    return SCH.Aetherflow;
            }

            if (IsEnabled(CustomComboPreset.ScholarEverythingFeature))
            {
                if (HasEffect(SCH.Buffs.Recitation) && (actionID == SCH.Indomitability || actionID == SCH.Excogitation))
                    return actionID;
                var gauge = GetJobGauge<SCHGauge>();
                // if (gauge.Aetherflow == 0)
                if (!HasEffect(SCH.Buffs.Aetherflow))
                    return SCH.Aetherflow;
            }

            return actionID;
        }
    }

    internal class ScholarLucidReminderFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarLucidReminderFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) != actionID)
                return OriginalHook(actionID);
            return IsActionOffCooldown(All.LucidDreaming) && HasCondition(ConditionFlag.InCombat) && !IsActionOffCooldown(actionID) && LocalPlayer?.CurrentMp <= 9000 && CanUseAction(All.LucidDreaming) ? All.LucidDreaming : actionID;
        }
    }
}
