using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Conditions;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class PCT
    {
        public const byte JobID = 42;

        public const uint MaxPaint = 5;

        public const uint
            FireInRed = 34650,
            BlizzardInCyan = 34653,
            Fire2InRed = 34656,
            Blizzard2InCyan = 34659,
            SubtractivePalette = 34683,
            RainbowDrip = 34688,
            HolyInWhite = 34662,
            CometInBlack = 34663,
            CreatureMotif = 34689,
            WeaponMotif = 34690,
            LandscapeMotif = 34691,
            LivingMuse = 35347,
            SteelMuse = 35348,
            ScenicMuse = 35349,
            HammerStamp = 34678,
            StarPrism = 34681;

        public static class Buffs
        {
            public const ushort
                SubtractivePalette = 3674,
                MonochromeTones = 3691,
                AetherhuesII = 3676,
                RainbowBright = 3679,
                HammerTime = 3680,
                Starstruck = 3681;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                HolyInWhite = 80;
        }
    }

    internal class PictSubFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictSubFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var isAoE = actionID == PCT.FireInRed ? false : true;

            if (IsEnabled(CustomComboPreset.PictSubMovementOption) && IsMoving() && !HasEffect(All.Buffs.Swiftcast) && level >= PCT.Levels.HolyInWhite)
            {
                if (HasEffect(PCT.Buffs.MonochromeTones) && IsEnabled(CustomComboPreset.PictCometFeature))
                    return PCT.CometInBlack;
                return PCT.HolyInWhite;
            }

            if (IsEnabled(CustomComboPreset.PictSubHolyOption) || IsEnabled(CustomComboPreset.PictSubOvercapOption))
            {
                var gauge = GetJobGauge<PCTGauge>();
                var maxPalette = 100;

                if (IsEnabled(CustomComboPreset.PictSubHolyOption) && HasEffect(PCT.Buffs.AetherhuesII) && gauge.Paint == PCT.MaxPaint)
                {
                    if (HasEffect(PCT.Buffs.MonochromeTones) && IsEnabled(CustomComboPreset.PictCometFeature))
                        return PCT.CometInBlack;
                    return PCT.HolyInWhite;
                }

                if (IsEnabled(CustomComboPreset.PictSubOvercapOption) && HasEffect(PCT.Buffs.AetherhuesII) && !HasEffect(PCT.Buffs.SubtractivePalette) && gauge.PalleteGauge == maxPalette)
                    return PCT.SubtractivePalette;
            }

            if (HasEffect(PCT.Buffs.SubtractivePalette))
               return isAoE ? OriginalHook(PCT.Blizzard2InCyan) : OriginalHook(PCT.BlizzardInCyan);

            return actionID;
        }
    }

    internal class PictCometFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictCometFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PCT.HolyInWhite)
                if (HasEffect(PCT.Buffs.MonochromeTones))
                    return PCT.CometInBlack;

            return actionID;
        }
    }

    internal class PictMotifFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictMotifFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PCT.CreatureMotif)
                if (OriginalHook(PCT.LivingMuse) != PCT.LivingMuse)
                    return OriginalHook(PCT.LivingMuse);

            if (actionID == PCT.WeaponMotif)
            {
                if (HasEffect(PCT.Buffs.HammerTime) && IsEnabled(CustomComboPreset.PictHammerFeature))
                    return OriginalHook(PCT.HammerStamp);
                if (OriginalHook(PCT.SteelMuse) != PCT.SteelMuse)
                        return OriginalHook(PCT.SteelMuse);
            }

            if (actionID == PCT.LandscapeMotif)
            {
                if (HasEffect(PCT.Buffs.Starstruck) && IsEnabled(CustomComboPreset.PictStarFeature))
                    return PCT.StarPrism;
                if (OriginalHook(PCT.ScenicMuse) != PCT.ScenicMuse)
                    return OriginalHook(PCT.ScenicMuse);
            }

            return actionID;
        }
    }

    internal class PictHammerFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictHammerFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (HasEffect(PCT.Buffs.HammerTime))
                return OriginalHook(PCT.HammerStamp);

            return actionID;
        }
    }

    internal class PictStarFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictStarFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (HasEffect(PCT.Buffs.Starstruck))
                return PCT.StarPrism;

            return actionID;
        }
    }

    internal class PictDripFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictDripFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<PCTGauge>();

            if (gauge.Paint == PCT.MaxPaint && HasCondition(ConditionFlag.InCombat))
            {
                if (HasEffect(PCT.Buffs.MonochromeTones) && IsEnabled(CustomComboPreset.PictCometFeature))
                    return PCT.CometInBlack;
                return PCT.HolyInWhite;
            }

            return actionID;
        }
    }
}