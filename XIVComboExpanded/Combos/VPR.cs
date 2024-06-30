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

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class VPR
    {
        public const byte JobID = 41;

        public const uint
            SteelFangs = 34606,
            DreadFangs = 34607,
            HuntersSting = 34608,
            SwiftskinsSting = 34609,
            FlankstingStrike = 34610,
            FlanksbaneFang = 34611,
            HindstingStrike = 34612,
            HindsbaneFang = 34613,
            SteelMaw = 34614,
            DreadMaw = 34615,
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
            Reawaken = 34626,
            FirstGeneration = 34627,
            SecondGeneration = 34628,
            ThirdGeneration = 34629,
            FourthGeneration = 34630,
            Ouroboros = 34631,
            WrithingSnap = 34632,
            Slither = 34646;

        public static class Buffs
        {
            public const ushort
                Placeholder = 0;
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

    /*internal class ViperOuroborosFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperOuroborosFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(VPR.Ouroboros) && lastComboMove == VPR.FourthGeneration)
            {
                if (actionID == VPR.SwiftskinsCoil || actionID == VPR.SwiftskinsDen)
                    return VPR.Ouroboros;
                if (IsEnabled(CustomComboPreset.ViperCoilAwakenedOption))
                {
                    if (actionID == VPR.DreadFangs || actionID == VPR.DreadMaw)
                        return VPR.Ouroboros;
                }
            }

            return actionID;
        }
    }*/

    internal class ViperSteelFangRangedFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperSteelFangRangedFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (!InMeleeRange())
                return VPR.WrithingSnap;

            return actionID;
        }
    }

    internal class ViperDreadFangDashFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ViperDreadFangDashFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (!InMeleeRange() || CurrentTarget?.ObjectKind == Dalamud.Game.ClientState.Objects.Enums.ObjectKind.Player)
                return VPR.Slither;

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
                    return VPR.DreadFangs;
                case VPR.JaggedMaw:
                    return VPR.SteelMaw;
                case VPR.BloodiedMaw:
                    return VPR.DreadMaw;
            }

            return lastComboMove;
        }

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(VPR.SerpentsTail) != VPR.SerpentsTail)
            {
                if (OriginalHook(VPR.SteelFangs) == VPR.FirstGeneration)
                {
                    /*if (IsEnabled(CustomComboPreset.ViperCoilAwakenedOption))
                    {
                        if ((actionID == VPR.SteelFangs || actionID == VPR.SteelMaw) && lastComboMove == VPR.ThirdGeneration)
                            return OriginalHook(VPR.SerpentsTail);
                        if ((actionID == VPR.DreadFangs || actionID == VPR.DreadMaw) && lastComboMove == VPR.FourthGeneration)
                            return OriginalHook(VPR.SerpentsTail);
                    }

                    if (OriginalHook(actionID) == lastComboMove)
                        return OriginalHook(VPR.SerpentsTail);*/
                    return OriginalHook(VPR.SerpentsTail);
                }
                else if (actionID == ReturnParentCombo(lastComboMove))
                {
                    return OriginalHook(VPR.SerpentsTail);
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
                if (new[] { VPR.SteelFangs, VPR.SteelMaw, VPR.HuntersCoil, VPR.HuntersDen }.Contains(actionID))
                    return OriginalHook(VPR.Twinfang);
                else
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
                if (actionID == VPR.DreadMaw)
                    return OriginalHook(VPR.SwiftskinsCoil);
            }

            if (actionID == VPR.SteelFangs || actionID == VPR.DreadFangs)
                if (CanUseAction(VPR.HuntersCoil) || CanUseAction(VPR.SwiftskinsCoil))
                    return actionID == VPR.SteelFangs ? VPR.HuntersCoil : VPR.SwiftskinsCoil;

            if (actionID == VPR.SteelMaw || actionID == VPR.DreadMaw)
                if (CanUseAction(VPR.HuntersDen) || CanUseAction(VPR.SwiftskinsDen))
                    return actionID == VPR.SteelMaw ? VPR.HuntersDen : VPR.SwiftskinsDen;

            return actionID;
        }
    }
}