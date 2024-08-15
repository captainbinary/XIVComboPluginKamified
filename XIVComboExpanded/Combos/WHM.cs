using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class WHM
    {
        public const byte ClassID = 6;
        public const byte JobID = 24;

        public const uint
            Cure = 120,
            Medica = 124,
            Cure2 = 135,
            Holy = 139,
            Holyga = 25860,
            AfflatusSolace = 16531,
            AfflatusRapture = 16534,
            AfflatusMisery = 16535,
            PresenceOfMind = 136,
            Assize = 3571,
            Temperance = 16536,
            PlenaryIndulgence = 7433,
            Asylum = 3569,
            Tetragrammaton = 3570,
            Benediction = 140,
            Aquaveil = 25861,
            LiturgyOfTheBell = 25862,
            Glareja = 37009;

        public static class Buffs
        {
            public const ushort
                Zagadoo = 155,
                SacredSight = 3879;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Cure2 = 30,
                AfflatusSolace = 52,
                AfflatusRapture = 76;
        }
    }

    internal class WhiteMageSolaceMiseryFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageSolaceMiseryFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.AfflatusSolace)
            {
                var gauge = GetJobGauge<WHMGauge>();

                if (gauge.BloodLily == 3)
                    return WHM.AfflatusMisery;
            }

            return actionID;
        }
    }

    internal class WhiteMageRaptureMiseryFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageRaptureMiseryFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.AfflatusRapture)
            {
                var gauge = GetJobGauge<WHMGauge>();

                if (gauge.BloodLily == 3)
                    return WHM.AfflatusMisery;
            }

            return actionID;
        }
    }

    internal class WhiteMageHolyMiseryFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageHolyMiseryFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<WHMGauge>();

            if (gauge.BloodLily == 3 && CurrentTarget is not null)
                return WHM.AfflatusMisery;

            return actionID;
        }
    }

    internal class WhiteMageHolyGlareFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageHolyGlareFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (HasEffect(WHM.Buffs.SacredSight) && CurrentTarget is not null)
                return WHM.Glareja;

            return actionID;
        }
    }

    internal class WhiteMageCureFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageCureFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.Cure2)
            {
                if (level < WHM.Levels.Cure2)
                    return WHM.Cure;
            }

            return actionID;
        }
    }

    internal class WhiteMageLucidReminderFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageLucidReminderFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) != actionID)
                return OriginalHook(actionID);
            return IsActionOffCooldown(All.LucidDreaming) && HasCondition(ConditionFlag.InCombat) && GetCooldown(actionID).CooldownRemaining > Service.Configuration.LucidCooldownOffset && LocalPlayer?.CurrentMp <= Service.Configuration.LucidMPThreshold && CanUseAction(All.LucidDreaming) ? All.LucidDreaming : actionID;
        }
    }

    internal class WhiteMageAfflatusFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WhiteMageAfflatusFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.Cure2)
            {
                var gauge = GetJobGauge<WHMGauge>();
                if (IsEnabled(CustomComboPreset.WhiteMageSolaceMiseryFeature) && gauge.BloodLily == 3)
                    return WHM.AfflatusMisery;
                // Unholy line of cursed code that must NEVER be mentioned, under any circumstances
                // Feel free to remove it in any forks for absolutely no consequence, I assure you
                if (HasEffect(WHM.Buffs.Zagadoo)) return actionID;
                if (level >= WHM.Levels.AfflatusSolace && gauge.Lily > 0)
                    return WHM.AfflatusSolace;
            }

            if (actionID == WHM.Medica)
            {
                var gauge = GetJobGauge<WHMGauge>();
                if (IsEnabled(CustomComboPreset.WhiteMageRaptureMiseryFeature) && gauge.BloodLily == 3 && level >= WHM.Levels.AfflatusRapture && CurrentTarget is not null)
                    return WHM.AfflatusMisery;
                if (level >= WHM.Levels.AfflatusRapture && gauge.Lily > 0)
                    return WHM.AfflatusRapture;
            }

            return actionID;
        }
    }
}
