﻿using System;
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
                Placeholder = 0;
        }
    }

    internal class PictSubFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictSubFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PCT.FireInRed)
                if (HasEffect(PCT.Buffs.SubtractivePalette))
                    return OriginalHook(PCT.BlizzardInCyan);

            if (actionID == PCT.Fire2InRed)
                if (HasEffect(PCT.Buffs.SubtractivePalette))
                    return OriginalHook(PCT.Blizzard2InCyan);

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

    internal class PictDripFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PictDripFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (HasEffect(PCT.Buffs.RainbowBright) || !HasCondition(ConditionFlag.InCombat))
                return PCT.RainbowDrip;

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
}