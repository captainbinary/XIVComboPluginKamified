using System.Linq;

using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SMN
    {
        public const byte ClassID = 26;
        public const byte JobID = 27;

        public const uint
            Physick = 16230,
            Deathflare = 3582,
            Sunflare = 36996,
            EnkindlePhoenix = 16516,
            EnkindleBahamut = 7429,
            EnkindleSolarBahamut = 36998,
            DreadwyrmTrance = 3581,
            SummonBahamut = 7427,
            SummonPhoenix = 25831,
            SummonSolarBahamut = 36992,
            Aethercharge = 25800,
            Ruin1 = 163,
            Ruin2 = 172,
            Ruin3 = 3579,
            Ruin4 = 7426,
            BrandOfPurgatory = 16515,
            FountainOfFire = 16514,
            AstralImpulse = 25820,
            UmbralImpulse = 36994,
            Fester = 181,
            Necrotize = 36990,
            EnergyDrain = 16508,
            Painflare = 3578,
            EnergySyphon = 16510,
            SummonCarbuncle = 25798,
            RadiantAegis = 25799,
            SearingLight = 25801,
            Outburst = 16511,
            TriDisaster = 25826,
            Gemshine = 25883,
            PreciousBrilliance = 25884,
            AstralFlow = 25822,
            MountainBuster = 25836,
            SummonRuby = 25802,
            SummonIfrit = 25805,
            SummonIfrit2 = 25838,
            SummonTopaz = 25803,
            SummonTitan = 25806,
            SummonTitan2 = 25839,
            SummonEmerald = 25804,
            SummonGaruda = 25807,
            SummonGaruda2 = 25840,
            Rekindle = 25830;

        public static class Buffs
        {
            public const ushort
                Aetherflow = 304,
                FurtherRuin = 2701,
                SearingLight = 2703;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                RadiantAegis = 2,
                Gemshine = 6,
                EnergyDrain = 10,
                PreciousBrilliance = 26,
                Painflare = 40,
                EnergySyphon = 52,
                Ruin3 = 54,
                Ruin4 = 62,
                SearingLight = 66,
                EnkindleBahamut = 70,
                SummonBahamut = 70,
                Rekindle = 80,
                SummonPhoenix = 80;
        }
    }

    internal class SummonerCarbyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerCarbyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<SMNGauge>();
            if (Service.BuddyList.PetBuddy == null && gauge.SummonTimerRemaining == 0 && gauge.Attunement == 0)
                return SMN.SummonCarbuncle;

            return actionID;
        }
    }

    internal class SummonerSearingLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerSearingLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(SMN.SearingLight) != SMN.SearingLight)
                return actionID;
            return actionID == SMN.SearingLight && IsActionOffCooldown(SMN.SearingLight) && HasEffectAny(SMN.Buffs.SearingLight) && FindEffectAny(SMN.Buffs.SearingLight)?.RemainingTime > 3 ? SCH.Physick : actionID;
        }
    }

    internal class SummonerDemiCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerDemiCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            // Replace demi summons with enkindle
            if (actionID == SMN.SummonBahamut || actionID == SMN.SummonPhoenix || actionID == SMN.DreadwyrmTrance || actionID == SMN.Aethercharge)
            {
                if (OriginalHook(SMN.Ruin1) == SMN.AstralImpulse && CanUseAction(SMN.EnkindleBahamut) && IsActionOffCooldown(SMN.EnkindleBahamut))
                    return SMN.EnkindleBahamut;
                if (OriginalHook(SMN.Ruin1) == SMN.FountainOfFire && IsActionOffCooldown(SMN.EnkindlePhoenix))
                    return SMN.EnkindlePhoenix;
                if (OriginalHook(SMN.Ruin1) == SMN.UmbralImpulse && IsActionOffCooldown(SMN.EnkindleSolarBahamut))
                    return SMN.EnkindleSolarBahamut;
            }

            return actionID;
        }
    }

    internal class SummonerDemiFlowFeature : CustomCombo
    {
        private static readonly uint[] UsedIDs = { SMN.SummonBahamut, SMN.SummonPhoenix, SMN.DreadwyrmTrance, SMN.Aethercharge };

        protected override CustomComboPreset Preset => CustomComboPreset.SummonerDemiFlowFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (UsedIDs.Contains(actionID))
            {
                if (OriginalHook(SMN.AstralFlow) != SMN.AstralFlow)
                {
                    if (OriginalHook(SMN.AstralFlow) == SMN.Deathflare || OriginalHook(SMN.AstralFlow) == SMN.Rekindle || OriginalHook(SMN.AstralFlow) == SMN.Sunflare)
                        return IsActionOffCooldown(OriginalHook(SMN.AstralFlow)) ? OriginalHook(SMN.AstralFlow) : actionID;
                    return OriginalHook(SMN.AstralFlow);
                }
            }

            return actionID;
        }
    }

    internal class SummonerShinyDemiCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerShinyDemiCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            // Replace demi summons with enkindle
            if (actionID == SMN.Gemshine || actionID == SMN.PreciousBrilliance)
            {
                if (OriginalHook(SMN.Ruin1) == SMN.AstralImpulse)
                {
                    if (IsEnabled(CustomComboPreset.SummonerShinyFlowCombo) && (!IsActionOffCooldown(SMN.EnkindleBahamut) || !CanUseAction(SMN.EnkindleBahamut)) && CanUseAction(OriginalHook(SMN.AstralFlow)))
                        return OriginalHook(SMN.AstralFlow);
                    if (CanUseAction(SMN.EnkindleBahamut)) return SMN.EnkindleBahamut;
                }

                if (OriginalHook(SMN.Ruin1) == SMN.FountainOfFire)
                {
                    if (IsEnabled(CustomComboPreset.SummonerShinyFlowCombo) && !IsActionOffCooldown(SMN.EnkindlePhoenix))
                        return OriginalHook(SMN.AstralFlow);
                    return SMN.EnkindlePhoenix;
                }

                if (OriginalHook(SMN.Ruin1) == SMN.UmbralImpulse)
                {
                    if (IsEnabled(CustomComboPreset.SummonerShinyFlowCombo) && (!IsActionOffCooldown(SMN.EnkindleSolarBahamut) || !CanUseAction(SMN.EnkindleSolarBahamut)) && CanUseAction(OriginalHook(SMN.AstralFlow)))
                        return OriginalHook(SMN.AstralFlow);
                    if (CanUseAction(SMN.EnkindleSolarBahamut)) return SMN.EnkindleSolarBahamut;
                }
            }

            return actionID;
        }
    }

    internal class SummonerShinyFlowCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerShinyFlowCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            // Replace demi summons with enkindle
            if ((actionID == SMN.Gemshine || actionID == SMN.PreciousBrilliance) && !IsEnabled(CustomComboPreset.SummonerShinyDemiCombo))
            {
                if (OriginalHook(SMN.Ruin1) == SMN.AstralImpulse && CanUseAction(OriginalHook(SMN.AstralFlow)))
                    return OriginalHook(SMN.AstralFlow);
                if (OriginalHook(SMN.Ruin1) == SMN.FountainOfFire)
                    return OriginalHook(SMN.AstralFlow);
                if (OriginalHook(SMN.Ruin1) == SMN.UmbralImpulse && CanUseAction(OriginalHook(SMN.AstralFlow)))
                    return OriginalHook(SMN.AstralFlow);
            }

            return actionID;
        }
    }

    internal class SummonerEDFesterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerEDFesterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Fester)
            {
                var gauge = GetJobGauge<SMNGauge>();

                if (!gauge.HasAetherflowStacks && IsEnabled(CustomComboPreset.SummonerLucidReminderFeature) && IsActionOffCooldown(All.LucidDreaming) && !IsActionOffCooldown(SMN.EnergyDrain) && LocalPlayer?.CurrentMp <= 9000 && CanUseAction(All.LucidDreaming)) return All.LucidDreaming;

                if (!gauge.HasAetherflowStacks)
                    return SMN.EnergyDrain;
            }

            return actionID;
        }
    }

    internal class SummonerESPainflareCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerESPainflareCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Painflare)
            {
                var gauge = GetJobGauge<SMNGauge>();

                if (!gauge.HasAetherflowStacks && IsEnabled(CustomComboPreset.SummonerLucidReminderFeature) && IsActionOffCooldown(All.LucidDreaming) && !IsActionOffCooldown(SMN.EnergySyphon) && LocalPlayer?.CurrentMp <= 9000 && CanUseAction(All.LucidDreaming)) return All.LucidDreaming;

                if (!gauge.HasAetherflowStacks)
                    return IsEnabled(CustomComboPreset.SummonerEStoED) && level < SMN.Levels.EnergySyphon ? SMN.EnergyDrain : SMN.EnergySyphon;

                if (level >= SMN.Levels.Painflare)
                    return SMN.Painflare;

                return IsEnabled(CustomComboPreset.SummonerEStoED) && level < SMN.Levels.EnergySyphon ? SMN.EnergyDrain : SMN.EnergySyphon;
            }

            return actionID;
        }
    }

    internal class SummonerEStoED : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerEStoED;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == SMN.EnergySyphon && IsEnabled(CustomComboPreset.SummonerEStoED) && level < SMN.Levels.EnergySyphon ? SMN.EnergyDrain : actionID;
        }
    }

    internal class SummonerMountainBusterFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerMountainBusterFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Gemshine || actionID == SMN.PreciousBrilliance)
            {
                if (OriginalHook(SMN.AstralFlow) == SMN.MountainBuster)
                    return OriginalHook(SMN.AstralFlow);
            }

            return actionID;
        }
    }

    internal class SummonerSummoningFlowFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerSummoningFlowFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.SummonRuby || actionID == SMN.SummonIfrit || actionID == SMN.SummonIfrit2 || actionID == SMN.SummonTopaz || actionID == SMN.SummonTitan || actionID == SMN.SummonTitan2 || actionID == SMN.SummonEmerald || actionID == SMN.SummonGaruda || actionID == SMN.SummonGaruda2)
            {
                if (OriginalHook(SMN.AstralFlow) != SMN.AstralFlow)
                    return OriginalHook(SMN.AstralFlow);
            }

            return actionID;
        }
    }

    internal class SummonerFlowingRuinFeature : CustomCombo
    {
        private static readonly uint[] UsedIDs = { SMN.Ruin1, SMN.Ruin2, SMN.Ruin3 };

        protected override CustomComboPreset Preset => CustomComboPreset.SummonerFlowingRuinFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (UsedIDs.Contains(actionID) || (actionID == SMN.Gemshine && IsEnabled(CustomComboPreset.SummonerRuiningShineFeature)))
            {
                if (actionID != SMN.Gemshine && IsEnabled(CustomComboPreset.SummonerRuiningShineFeature)) return actionID;
                if (OriginalHook(SMN.AstralFlow) != SMN.AstralFlow && CanUseAction(OriginalHook(SMN.AstralFlow)))
                {
                    if (OriginalHook(SMN.AstralFlow) == SMN.Deathflare || OriginalHook(SMN.AstralFlow) == SMN.Rekindle || OriginalHook(SMN.AstralFlow) == SMN.Sunflare)
                        return IsActionOffCooldown(OriginalHook(SMN.AstralFlow)) ? OriginalHook(SMN.AstralFlow) : actionID;
                    return OriginalHook(SMN.AstralFlow);
                }
            }

            return actionID;
        }
    }

    internal class SummonerShinyRuinFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerShinyRuinFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Ruin1 || actionID == SMN.Ruin2 || actionID == SMN.Ruin3)
            {
                if (IsEnabled(CustomComboPreset.SummonerRuiningShineFeature)) return actionID;
                if (IsEnabled(CustomComboPreset.SummonerMountainBusterFeature) && OriginalHook(SMN.AstralFlow) == SMN.MountainBuster)
                    return OriginalHook(SMN.AstralFlow);
                if (OriginalHook(SMN.Gemshine) != SMN.Gemshine)
                    return OriginalHook(SMN.Gemshine);
            }

            return actionID;
        }
    }

    internal class SummonerFurtherRuinFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerFurtherRuinFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Ruin1 || actionID == SMN.Ruin2 || actionID == SMN.Ruin3 || (actionID == SMN.Gemshine && IsEnabled(CustomComboPreset.SummonerRuiningShineFeature)))
            {
                if (actionID != SMN.Gemshine && IsEnabled(CustomComboPreset.SummonerRuiningShineFeature)) return actionID;
                if (HasEffect(SMN.Buffs.FurtherRuin) && (OriginalHook(SMN.Ruin1) != SMN.AstralImpulse && OriginalHook(SMN.Ruin1) != SMN.FountainOfFire) && (OriginalHook(SMN.Ruin1) != SMN.UmbralImpulse))
                    return SMN.Ruin4;
            }

            return actionID;
        }
    }

    internal class SummonerRuiningShineFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerRuiningShineFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Gemshine)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (gauge.AttunmentTimerRemaining == 0)
                    return OriginalHook(SMN.Ruin3);
            }

            return actionID;
        }
    }

    internal class SummonerFlowingOutburstFeature : CustomCombo
    {
        private static readonly uint[] UsedIDs = { SMN.Outburst, SMN.TriDisaster };

        protected override CustomComboPreset Preset => CustomComboPreset.SummonerFlowingOutburstFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (UsedIDs.Contains(actionID) || (actionID == SMN.PreciousBrilliance && IsEnabled(CustomComboPreset.SummonerOutburstOfBrillianceFeature)))
            {
                if (actionID != SMN.PreciousBrilliance && IsEnabled(CustomComboPreset.SummonerOutburstOfBrillianceFeature)) return actionID;
                if (OriginalHook(SMN.AstralFlow) != SMN.AstralFlow && CanUseAction(OriginalHook(SMN.AstralFlow)))
                {
                    if (OriginalHook(SMN.AstralFlow) == SMN.Deathflare || OriginalHook(SMN.AstralFlow) == SMN.Rekindle || OriginalHook(SMN.AstralFlow) == SMN.Sunflare)
                        return IsActionOffCooldown(OriginalHook(SMN.AstralFlow)) ? OriginalHook(SMN.AstralFlow) : actionID;
                    return OriginalHook(SMN.AstralFlow);
                }
            }

            return actionID;
        }
    }

    internal class SummonerFurtherOutburstFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerFurtherOutburstFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Outburst || actionID == SMN.TriDisaster || (actionID == SMN.PreciousBrilliance && IsEnabled(CustomComboPreset.SummonerOutburstOfBrillianceFeature)))
            {
                if (actionID != SMN.PreciousBrilliance && IsEnabled(CustomComboPreset.SummonerOutburstOfBrillianceFeature)) return actionID;
                if (HasEffect(SMN.Buffs.FurtherRuin) && (OriginalHook(SMN.Ruin1) != SMN.AstralImpulse && OriginalHook(SMN.Ruin1) != SMN.FountainOfFire && (OriginalHook(SMN.Ruin1) != SMN.UmbralImpulse)))
                    return SMN.Ruin4;
            }

            return actionID;
        }
    }

    internal class SummonerShinyOutburstFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerShinyOutburstFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Outburst || actionID == SMN.TriDisaster)
            {
                if (actionID != SMN.PreciousBrilliance && IsEnabled(CustomComboPreset.SummonerOutburstOfBrillianceFeature)) return actionID;
                if (IsEnabled(CustomComboPreset.SummonerMountainBusterFeature) && OriginalHook(SMN.AstralFlow) == SMN.MountainBuster)
                    return OriginalHook(SMN.AstralFlow);
                if (OriginalHook(SMN.PreciousBrilliance) != SMN.PreciousBrilliance)
                    return OriginalHook(SMN.PreciousBrilliance);
            }

            return actionID;
        }
    }

    internal class SummonerOutburstOfBrillianceFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerOutburstOfBrillianceFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.PreciousBrilliance)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (gauge.AttunmentTimerRemaining == 0)
                    return OriginalHook(SMN.Outburst);
            }

            return actionID;
        }
    }

    internal class SummonerLucidReminderFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerLucidReminderFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) != actionID) return actionID;
            return IsActionOffCooldown(All.LucidDreaming) && HasCondition(ConditionFlag.InCombat) && !IsActionOffCooldown(actionID) && LocalPlayer?.CurrentMp <= 9000 && CanUseAction(All.LucidDreaming) ? All.LucidDreaming : actionID;
        }
    }
}
