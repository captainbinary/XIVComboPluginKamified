using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboKamifiedPlugin.Combos
{
    internal static class BRD
    {
        public const byte ClassID = 5;
        public const byte JobID = 23;

        public const uint
            HeavyShot = 97,
            StraightShot = 98,
            VenomousBite = 100,
            QuickNock = 106,
            Bloodletter = 110,
            Windbite = 113,
            EmpyrealArrow = 3558,
            WanderersMinuet = 3559,
            IronJaws = 3560,
            SideWinder = 3562,
            PitchPerfect = 7404,
            CausticBite = 7406,
            Stormbite = 7407,
            RefulgentArrow = 7409,
            Shadowbite = 16494,
            BurstShot = 16495,
            ApexArrow = 16496,
            Ladonsbite = 25783;

        public static class Buffs
        {
            public const ushort
                StraightShotReady = 122,
                ShadowbiteReady = 3002,
                WanderersMinuet = 2216;
        }

        public static class Debuffs
        {
            public const ushort
                VenomousBite = 124,
                Windbite = 129,
                CausticBite = 1200,
                Stormbite = 1201;
        }

        public static class Levels
        {
            public const byte
                VenomousBite = 6,
                Bloodletter = 12,
                Windbite = 30,
                EmpyrealArrow = 54,
                IronJaws = 56,
                Sidewinder = 60,
                BiteUpgrade = 64,
                RefulgentArrow = 70,
                BurstShot = 76;
        }
    }

    internal class BardWanderersPitchPerfectFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardWanderersPitchPerfectFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.WanderersMinuet)
            {
                if (HasEffect(BRD.Buffs.WanderersMinuet))
                    return BRD.PitchPerfect;
            }

            return actionID;
        }
    }

    internal class BardStraightShotUpgradeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardStraightShotUpgradeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.HeavyShot || actionID == BRD.BurstShot)
            {
                var gauge = GetJobGauge<BRDGauge>();
                if (IsEnabled(CustomComboPreset.BardApexFeature) && (gauge.SoulVoice == 100 || OriginalHook(BRD.ApexArrow) != BRD.ApexArrow))
                    return OriginalHook(BRD.ApexArrow);

                if (HasEffect(BRD.Buffs.StraightShotReady))
                    return OriginalHook(BRD.StraightShot);
            }

            return actionID;
        }
    }

    internal class BardIronJawsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardIronJawsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.IronJaws)
            {
                if (level < BRD.Levels.IronJaws)
                {
                    var venomous = FindTargetEffect(BRD.Debuffs.VenomousBite);
                    var windbite = FindTargetEffect(BRD.Debuffs.Windbite);
                    if (venomous is not null && windbite is not null)
                    {
                        if (venomous?.RemainingTime < windbite?.RemainingTime)
                            return BRD.VenomousBite;
                        return BRD.Windbite;
                    }
                    else if (windbite is not null || level < BRD.Levels.Windbite)
                    {
                        return BRD.VenomousBite;
                    }

                    return BRD.Windbite;
                }

                if (level < BRD.Levels.BiteUpgrade)
                {
                    var venomous = TargetHasEffect(BRD.Debuffs.VenomousBite);
                    var windbite = TargetHasEffect(BRD.Debuffs.Windbite);

                    if (venomous && windbite)
                        return BRD.IronJaws;

                    if (windbite)
                        return BRD.VenomousBite;

                    return BRD.Windbite;
                }

                var caustic = TargetHasEffect(BRD.Debuffs.CausticBite);
                var stormbite = TargetHasEffect(BRD.Debuffs.Stormbite);

                if (caustic && stormbite)
                    return BRD.IronJaws;

                if (stormbite)
                    return BRD.CausticBite;

                return BRD.Stormbite;
            }

            return actionID;
        }
    }

    internal class BardApexFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardApexFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.QuickNock || actionID == BRD.Ladonsbite)
            {
                var gauge = GetJobGauge<BRDGauge>();
                if (gauge.SoulVoice == 100 || OriginalHook(BRD.ApexArrow) != BRD.ApexArrow)
                    return OriginalHook(BRD.ApexArrow);
            }

            return actionID;
        }
    }

    internal class BardShadowbiteFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardShadowbiteFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.QuickNock || actionID == BRD.Ladonsbite)
            {
                if (HasEffect(BRD.Buffs.ShadowbiteReady))
                    return OriginalHook(BRD.Shadowbite);
            }

            return actionID;
        }
    }

    internal class BardSingleTargetCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardSingleTargetCombo;

        protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
        {
            if (actionID == BRD.HeavyShot)
            {
                // Sidewinder
                if (level > BRD.Levels.Sidewinder)
                {
                    var cooldown = GetCooldown(BRD.SideWinder);
                    if (cooldown.CooldownRemaining == 0)
                        return BRD.SideWinder;
                }

                // EmpyrealArrow
                if (level > BRD.Levels.EmpyrealArrow)
                {
                    var cooldown = GetCooldown(BRD.EmpyrealArrow);
                    if (cooldown.CooldownRemaining == 0)
                        return BRD.EmpyrealArrow;
                }

                // Straight Shot Calc
                if (HasEffect(BRD.Buffs.StraightShotReady))
                {
                    if (level > BRD.Levels.RefulgentArrow)
                        return BRD.RefulgentArrow;

                    return BRD.StraightShot;
                }

                // Bloodletter
                if (level > BRD.Levels.Bloodletter)
                {
                    var cooldown = GetCooldown(BRD.Bloodletter);
                    if (cooldown.CooldownRemaining == 0)
                        return BRD.Bloodletter;
                }
            }

            return actionID;
        }
    }
}
