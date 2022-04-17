using System;
using System.Linq;

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
            RagingStrikes = 101,
            QuickNock = 106,
            Barrage = 107,
            RainOfDeath = 117,
            Bloodletter = 110,
            Windbite = 113,
            BattleVoice = 118,
            WanderersMinuet = 3559,
            Peloton = 7557,
            IronJaws = 3560,
            Sidewinder = 3562,
            EmpyrealArrow = 3558,
            PitchPerfect = 7404,
            CausticBite = 7406,
            Stormbite = 7407,
            RefulgentArrow = 7409,
            Shadowbite = 16494,
            BurstShot = 16495,
            ApexArrow = 16496,
            Ladonsbite = 25783,
            BlastArrow = 25784,
            RadiantFinale = 25785;

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
                Windbite = 30,
                RainOfDeath = 45,
                BattleVoice = 50,
                IronJaws = 56,
                Sidewinder = 60,
                BiteUpgrade = 64,
                RefulgentArrow = 70,
                BurstShot = 76,
                RadiantFinale = 90;
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
                // if (IsEnabled(CustomComboPreset.BardApexFeature) && (gauge.SoulVoice == 100 || OriginalHook(BRD.ApexArrow) != BRD.ApexArrow))
                //    return OriginalHook(BRD.ApexArrow);

                if (HasEffect(BRD.Buffs.StraightShotReady))
                    return OriginalHook(BRD.StraightShot);
            }

            return actionID;
        }
    }

    internal class BardIronJawsFeaturePlus : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardIronJawsFeaturePlus;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.IronJaws)
            {
                if (!CanUseAction(BRD.IronJaws))
                {
                    var venomous = FindTargetEffect(BRD.Debuffs.VenomousBite);
                    var windbite = FindTargetEffect(BRD.Debuffs.Windbite);
                    if (venomous is not null && windbite is not null)
                    {
                        if (venomous?.RemainingTime < windbite?.RemainingTime)
                            return BRD.VenomousBite;
                        return BRD.Windbite;
                    }
                    else if (windbite is not null || !CanUseAction(BRD.Windbite))
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

    internal class BardWanderersPitchPerfectFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardWanderersPitchPerfectFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.Peloton && CurrentTarget is not null)
            {
                var gauge = GetJobGauge<BRDGauge>();
                if (gauge.Song == Song.WANDERER)
                    return BRD.PitchPerfect;
                return BRD.WanderersMinuet;
            }

            return actionID;
        }
    }

    internal class BardIronJawsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardIronJawsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return !CanUseAction(BRD.IronJaws) || (!TargetHasEffect(BRD.Debuffs.Stormbite) && !TargetHasEffect(BRD.Debuffs.Windbite)) ? (!CanUseAction(OriginalHook(BRD.Windbite)) ? BRD.VenomousBite : OriginalHook(BRD.Stormbite)) : actionID;
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
                if (gauge.SoulVoice >= 80 || OriginalHook(BRD.ApexArrow) != BRD.ApexArrow)
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

    internal class BardSidewinderFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardSidewinderFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return (IsActionOffCooldown(BRD.Sidewinder) && !IsActionOffCooldown(BRD.EmpyrealArrow) && CanUseAction(BRD.Sidewinder)) ? BRD.Sidewinder : BRD.EmpyrealArrow;
        }
    }

    internal class BardRadiantStrikesFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardRadiantStrikesFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return IsActionOffCooldown(BRD.RagingStrikes) || !CanUseAction(BRD.BattleVoice) || (IsEnabled(CustomComboPreset.BardRadiantFeature) && !IsActionOffCooldown(BRD.BattleVoice) && level < BRD.Levels.RadiantFinale) ? BRD.RagingStrikes : actionID;
        }
    }

    internal class BardRadiantFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardRadiantFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return (IsActionOffCooldown(BRD.BattleVoice) || level < BRD.Levels.RadiantFinale) ? BRD.BattleVoice : actionID;
        }
    }

    internal class BardBarrageFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardBarrageFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return HasEffect(BRD.Buffs.StraightShotReady) && !HasEffect(BRD.Buffs.ShadowbiteReady) ? OriginalHook(BRD.StraightShot) : BRD.Barrage;
        }
    }

    internal class BardRainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BardRainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            // return !TargetHasEffect(BRD.Debuffs.CausticBite) && !TargetHasEffect(BRD.Debuffs.Stormbite) && !TargetHasEffect(BRD.Debuffs.Windbite) && !TargetHasEffect(BRD.Debuffs.VenomousBite) && CanUseAction(BRD.RainOfDeath) ? BRD.RainOfDeath : BRD.Bloodletter;
            return (this.FilteredLastComboMove == OriginalHook(BRD.QuickNock) || this.FilteredLastComboMove == OriginalHook(BRD.Shadowbite)) && CanUseAction(BRD.RainOfDeath) ? BRD.RainOfDeath : BRD.Bloodletter;
        }
    }
}
