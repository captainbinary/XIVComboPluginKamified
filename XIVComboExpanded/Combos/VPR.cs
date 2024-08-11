using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Game.ClientState.JobGauge.Types;
using FFXIVClientStructs.FFXIV.Client.Game.Gauge;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class VPR
    {
        public const byte JobID = 41;

        public const uint
            SteelFangs = 34606,
            ReavingFangs = 34607,
            HuntersSting = 34608,
            SwiftskinsSting = 34609,
            FlankstingStrike = 34610,
            FlanksbaneFang = 34611,
            HindstingStrike = 34612,
            HindsbaneFang = 34613,
            SteelMaw = 34614,
            ReavingMaw = 34615,
            HuntersBite = 34616,
            SwiftskinsBite = 34617,
            JaggedMaw = 34618,
            BloodiedMaw = 34619,
            HuntersCoil = 34621,
            SwiftskinsCoil = 34622,
            HuntersDen = 34624,
            SwiftskinsDen = 34625,
            SerpentsTail = 35920,
            Twinfang = 35921,
            Twinblood = 35922,
            TwinfangBite = 34636,
            TwinfangThresh = 34638,
            UncoiledTwinfang = 34644,
            UncoiledTwinblood = 34645,
            Reawaken = 34626,
            FirstGeneration = 34627,
            SecondGeneration = 34628,
            ThirdGeneration = 34629,
            FourthGeneration = 34630,
            Ouroboros = 34631,
            WrithingSnap = 34632,
            UncoiledFury = 34633,
            Slither = 34646;

        public static class Buffs
        {
            public const ushort
                FlankstungVenom = 3645,
                FlanksbaneVenom = 3646,
                HindstungVenom = 3647,
                HindsbaneVenom = 3648,
                GrimhuntersVenom = 3649,
                GrimskinsVenom = 3650,
                HuntersVenom = 3657,
                SwiftskinsVenom = 3658,
                FellhuntersVenom = 3659,
                FellskinsVenom = 3660,
                PoisedTwinfang = 3665,
                PoisedTwinblood = 3666,
                HonedSteel = 3672,
                HonedReavers = 3772;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Placeholder = 0;
        }
    }

    internal class ViperOuroborosFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperOuroborosFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<VPRGauge>();
            if (CanUseAction(VPR.Ouroboros) && gauge.AnguineTribute == 1 && (!IsEnabled(CustomComboPreset.ViperTailFeature) || !CanUseAction(OriginalHook(VPR.SerpentsTail))))
            {
                if (actionID == VPR.SwiftskinsCoil || actionID == VPR.SwiftskinsDen)
                    return VPR.Ouroboros;
                if (IsEnabled(CustomComboPreset.ViperCoilAwakenedOption))
                {
                    if (actionID == VPR.ReavingMaw)
                        return VPR.Ouroboros;
                }
            }

            return actionID;
        }
    }

    internal class ViperSteelFangRangedFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperSteelFangRangedFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (!InMeleeRange())
            {
                if (IsEnabled(CustomComboPreset.ViperBloodFangFeature) && OriginalHook(VPR.Twinblood) == VPR.UncoiledTwinblood)
                    return actionID;
                if (CanUseAction(VPR.UncoiledFury) && IsEnabled(CustomComboPreset.ViperSnapUncoiledFuryFeature))
                    return VPR.UncoiledFury;
                return VPR.WrithingSnap;
            }

            return actionID;
        }
    }

    internal class ViperDreadFangDashFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperDreadFangDashFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.ViperBloodFangFeature) && OriginalHook(VPR.Twinblood) == VPR.UncoiledTwinblood)
                return actionID;
            if (!InMeleeRange() || CurrentTarget?.ObjectKind == Dalamud.Game.ClientState.Objects.Enums.ObjectKind.Player)
                return VPR.Slither;

            return actionID;
        }
    }

    internal class ViperSnapUncoiledFuryFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperSnapUncoiledFuryFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(VPR.UncoiledFury))
                return VPR.UncoiledFury;

            return actionID;
        }
    }

    internal class ViperTailFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperTailFeature;

        public static uint ReturnParentCombo(uint lastComboMove)
        {
            switch (lastComboMove)
            {
                case VPR.FlankstingStrike:
                case VPR.HindstingStrike:
                    return VPR.SteelFangs;
                case VPR.FlanksbaneFang:
                case VPR.HindsbaneFang:
                    return VPR.ReavingFangs;
                case VPR.JaggedMaw:
                    return VPR.SteelMaw;
                case VPR.BloodiedMaw:
                    return VPR.ReavingMaw;
            }

            return lastComboMove;
        }

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(VPR.SerpentsTail) != VPR.SerpentsTail)
            {
                if (OriginalHook(VPR.SteelFangs) == VPR.FirstGeneration)
                {
                    return OriginalHook(VPR.SerpentsTail);
                }
                else if (actionID == ReturnParentCombo(lastComboMove) || IsEnabled(CustomComboPreset.ViperStrikingFangsFeature))
                {
                    return OriginalHook(VPR.SerpentsTail);
                }
            }

            return actionID;
        }
    }

    internal class ViperReawakenedFangsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperReawakenedFangsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {

            var gauge = GetJobGauge<VPRGauge>();
            var maxTribute = CanUseAction(VPR.Ouroboros) ? 5 : 4;
            var tribute = (int)gauge.AnguineTribute;

            if (tribute > 0)
            {
                switch (tribute)
                {
                    case var value when value == maxTribute:
                        return VPR.FirstGeneration;
                    case var value when value == maxTribute - 1:
                        return VPR.SecondGeneration;
                    case var value when value == maxTribute - 2:
                        return VPR.ThirdGeneration;
                    case var value when value == maxTribute - 3:
                        return VPR.FourthGeneration;
                    case var value when value == maxTribute - 4:
                        return VPR.Ouroboros;
                }
            }

            return actionID;
        }
    }

    internal class ViperBloodFangFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperBloodFangFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(OriginalHook(VPR.Twinfang)))
            {
                if (OriginalHook(VPR.Twinfang) == VPR.TwinfangBite && new[] { VPR.SteelMaw, VPR.ReavingMaw, VPR.HuntersDen, VPR.SwiftskinsDen }.Contains(actionID))
                    return actionID;
                if (OriginalHook(VPR.Twinfang) == VPR.TwinfangThresh && new[] { VPR.SteelFangs, VPR.ReavingFangs, VPR.HuntersCoil, VPR.SwiftskinsCoil }.Contains(actionID))
                    return actionID;

                if (IsEnabled(CustomComboPreset.ViperTwistedTwinsFeature))
                {
                    var fangBuffs = new[] { VPR.Buffs.PoisedTwinfang, VPR.Buffs.HuntersVenom, VPR.Buffs.FellhuntersVenom };

                    if (fangBuffs.Any(x => HasEffect(x)))
                        return OriginalHook(VPR.Twinfang);
                    else
                        return OriginalHook(VPR.Twinblood);
                }

                var isFang = new[] { VPR.SteelFangs, VPR.SteelMaw, VPR.HuntersCoil, VPR.HuntersDen }.Contains(actionID);

                if (isFang)
                    return OriginalHook(VPR.Twinfang);
                else
                    return OriginalHook(VPR.Twinblood);
            }

            return actionID;
        }
    }

    internal class ViperTwinTailsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperTwinTailsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(OriginalHook(VPR.SerpentsTail)))
                return OriginalHook(VPR.SerpentsTail);

            return actionID;
        }
    }

    internal class ViperTwistedTwinsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperTwistedTwinsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var fangBuffs = new[] { VPR.Buffs.PoisedTwinfang, VPR.Buffs.HuntersVenom, VPR.Buffs.FellhuntersVenom };

            if (CanUseAction(OriginalHook(VPR.Twinfang)))
            {
                if (fangBuffs.Any(x => HasEffect(x)))
                    return OriginalHook(VPR.Twinfang);
                else
                    return OriginalHook(VPR.Twinblood);
            }

            return actionID;
        }
    }

    internal class ViperTwinFuryFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperTwinFuryFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(OriginalHook(VPR.Twinfang)))
            {
                if (HasEffect(VPR.Buffs.PoisedTwinfang))
                    return OriginalHook(VPR.Twinfang);
                else if (HasEffect(VPR.Buffs.PoisedTwinblood))
                    return OriginalHook(VPR.Twinblood);
            }

            return actionID;
        }
    }

    internal class ViperCoilFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperCoilFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.ViperCoilAwakenedOption) && OriginalHook(VPR.SteelFangs) == VPR.FirstGeneration)
            {
                if (actionID == VPR.SteelMaw)
                    return OriginalHook(VPR.HuntersCoil);
                if (actionID == VPR.ReavingMaw)
                    return OriginalHook(VPR.SwiftskinsCoil);
            }

            bool swap = IsEnabled(CustomComboPreset.ViperCoilFeatureSwapOption);
            bool denSwap = IsEnabled(CustomComboPreset.ViperCoilFeatureDenSwapOption);

            if (actionID == VPR.SteelFangs || actionID == VPR.ReavingFangs)
                if (CanUseAction(VPR.HuntersCoil) || CanUseAction(VPR.SwiftskinsCoil))
                    return (actionID == VPR.SteelFangs && !swap) || (actionID == VPR.ReavingFangs && swap) ? VPR.HuntersCoil : VPR.SwiftskinsCoil;

            if (actionID == VPR.SteelMaw || actionID == VPR.ReavingMaw)
                if (CanUseAction(VPR.HuntersDen) || CanUseAction(VPR.SwiftskinsDen))
                    return (actionID == VPR.SteelMaw && !denSwap) || (actionID == VPR.ReavingMaw && denSwap) ? VPR.HuntersDen : VPR.SwiftskinsDen;

            return actionID;
        }
    }

    internal class ViperReavingSteelFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperReavingSteelFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) == actionID)
            {
                if (actionID == VPR.SteelFangs || actionID == VPR.ReavingFangs)
                {
                    if (HasEffect(VPR.Buffs.HonedReavers))
                        return VPR.ReavingFangs;
                    if (HasEffect(VPR.Buffs.HonedSteel))
                        return VPR.SteelFangs;
                }
                if (actionID == VPR.SteelMaw || actionID == VPR.ReavingMaw)
                {
                    if (HasEffect(VPR.Buffs.HonedReavers))
                        return VPR.ReavingMaw;
                    if (HasEffect(VPR.Buffs.HonedSteel))
                        return VPR.SteelMaw;
                }
            }

            return actionID;
        }
    }

    internal class ViperStrikingFangsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperStrikingFangsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (new[] { VPR.HuntersSting, VPR.SwiftskinsSting, VPR.HuntersBite, VPR.SwiftskinsBite }.Contains(lastComboMove) && GetJobGauge<VPRGauge>().AnguineTribute == 0)
            {
                if (actionID == VPR.SteelFangs || actionID == VPR.ReavingFangs)
                {
                    if (HasEffect(VPR.Buffs.FlankstungVenom))
                        return VPR.FlankstingStrike;
                    if (HasEffect(VPR.Buffs.FlanksbaneVenom))
                        return VPR.FlanksbaneFang;
                    if (HasEffect(VPR.Buffs.HindstungVenom))
                        return VPR.HindstingStrike;
                    if (HasEffect(VPR.Buffs.HindsbaneVenom))
                        return VPR.HindsbaneFang;
                }

                if (actionID == VPR.SteelMaw || actionID == VPR.ReavingMaw)
                {
                    if (HasEffect(VPR.Buffs.GrimhuntersVenom))
                        return VPR.JaggedMaw;
                    if (HasEffect(VPR.Buffs.GrimskinsVenom))
                        return VPR.BloodiedMaw;
                }
            }

            return actionID;
        }
    }
}