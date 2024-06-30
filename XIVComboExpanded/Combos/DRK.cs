using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class DRK
    {
        public const byte JobID = 32;

        public const uint
            HardSlash = 3617,
            Unleash = 3621,
            BloodWeapon = 3625,
            CarveAndSpit = 3643,
            SyphonStrike = 3623,
            Souleater = 3632,
            Quietus = 7391,
            Bloodspiller = 7392,
            FloodOfDarkness = 16466,
            EdgeOfDarkness = 16467,
            StalwartSoul = 16468,
            FloodOfShadow = 16469,
            EdgeOfShadow = 16470,
            LivingShadow = 16472,
            Shadowbringer = 25757,
            AbyssalDrain = 3641,
            Unmend = 3624,
            TheBlackestNight = 7393,
            Oblation = 25754;

        public static class Buffs
        {
            public const ushort
                BloodWeapon = 742,
                Delirium = 1972,
                TheBlackestnight = 1178,
                Oblation = 2682;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                SyphonStrike = 2,
                Souleater = 26,
                FloodOfDarkness = 30,
                EdgeOfDarkness = 40,
                StalwartSoul = 40,
                CarveAndSpit = 60,
                Bloodpiller = 62,
                Quietus = 64,
                Delirium = 68,
                Shadow = 74,
                LivingShadow = 80;
        }
    }

    internal class DarkSouleaterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkSouleaterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.Souleater)
            {
                if (IsEnabled(CustomComboPreset.DarkSoulmendFeature))
                {
                    if (CanUseAction(DRK.Unmend) && !InMeleeRange())
                        return DRK.Unmend;
                }

                if (comboTime > 0)
                {
                    if (lastComboMove == DRK.HardSlash && level >= DRK.Levels.SyphonStrike)
                        return DRK.SyphonStrike;

                    if (lastComboMove == DRK.SyphonStrike && level >= DRK.Levels.Souleater)
                        return DRK.Souleater;
                }

                return DRK.HardSlash;
            }

            return actionID;
        }
    }

    internal class DarkStalwartSoulCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkStalwartSoulCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == (IsEnabled(CustomComboPreset.DarkEvilStalwartSoulCombo) ? DRK.Unleash : DRK.StalwartSoul))
            {
                var gauge = GetJobGauge<DRKGauge>();

                if (IsEnabled(CustomComboPreset.DarkStalsteemFeature) && OriginalHook(DRK.LivingShadow) != DRK.LivingShadow && CanUseAction(OriginalHook(DRK.LivingShadow)))
                    return OriginalHook(DRK.LivingShadow);

                if (comboTime > 0 && lastComboMove == DRK.Unleash && level >= DRK.Levels.StalwartSoul)
                {
                    if (IsEnabled(CustomComboPreset.DRKOvercapFeature) && gauge.Blood >= 90)
                        return OriginalHook(DRK.Quietus);
                    return DRK.StalwartSoul;
                }

                return DRK.Unleash;
            }

            return actionID;
        }
    }

    internal class DarkDeliriumFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkDeliriumFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsActionOffCooldown(OriginalHook(DRK.BloodWeapon)) || !CanUseAction(DRK.CarveAndSpit))
                return OriginalHook(DRK.BloodWeapon);

            return actionID;
        }
    }

    internal class DarkLivingShadowFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkLivingShadowFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<DRKGauge>();
            if (IsActionOffCooldown(DRK.LivingShadow) && level >= DRK.Levels.LivingShadow && gauge.Blood >= 50)
                return DRK.LivingShadow;

            return actionID;
        }
    }

    internal class DarkEdgeToFloodFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkEdgeToFloodFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.EdgeOfShadow || actionID == DRK.EdgeOfDarkness)
            {
                if ((lastComboMove == DRK.Unleash || lastComboMove == DRK.StalwartSoul) ||
                    (!CanUseAction(OriginalHook(DRK.EdgeOfDarkness))))
                    return OriginalHook(DRK.FloodOfDarkness);
            }

            return actionID;
        }
    }

    internal class DarkBloodspillerToQuietusFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkBloodspillerToQuietusFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.Bloodspiller)
            {
                if ((lastComboMove == DRK.Unleash || lastComboMove == DRK.StalwartSoul) && level >= DRK.Levels.Quietus)
                    return OriginalHook(DRK.Quietus);
            }

            return actionID;
        }
    }

    internal class DarkCarvetoDrainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkCarveToDrainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.CarveAndSpit)
            {
                if (lastComboMove == DRK.Unleash || lastComboMove == DRK.StalwartSoul || !CanUseAction(DRK.CarveAndSpit))
                    return DRK.AbyssalDrain;
            }

            return actionID;
        }
    }

    internal class DarkTBNToOblationFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkTBNToOblationFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.TheBlackestNight)
            {
                if (CanUseAction(DRK.Oblation) && !IsActionOffCooldown(DRK.TheBlackestNight) && GetCooldown(DRK.Oblation).CooldownRemaining < 60)
                {
                    var oblationStatus = FindEffectAny(DRK.Buffs.Oblation);

                    if (oblationStatus == null || oblationStatus.RemainingTime < 2)
                        return DRK.Oblation;
                }
            }

            return actionID;
        }
    }
}
