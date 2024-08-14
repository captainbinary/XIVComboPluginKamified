using System.Collections.Generic;

using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using Microsoft.VisualBasic;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class All
    {
        public const byte JobID = 0;

        public const uint
            Swiftcast = 7561,
            Resurrection = 173,
            Verraise = 7523,
            Raise = 125,
            Ascend = 3603,
            Egeiro = 24287,
            AngelWhisper = 18317,
            SolidReason = 232,
            AgelessWords = 215,
            WiseToTheWorldMIN = 26521,
            WiseToTheWorldBTN = 26522,
            LowBlow = 7540,
            Interject = 7538,
            Reprisal = 7535,
            LucidDreaming = 7562,
            Cast = 289,
            Hook = 296,
            CastLight = 2135,
            Snagging = 4100,
            Chum = 4104,
            PrecisionHookset = 4179,
            PowerfulHookset = 4103,
            SurfaceSlap = 4595,
            Gig = 7632,
            VeteranTrade = 7906,
            NaturesBounty = 7909,
            Salvage = 7910,
            ElectricCurrent = 26872,
            PrizeCatch = 26806,
            Feint = 7549,
            Addle = 7560,
            Troubadour = 7405,
            Tactician = 16889,
            ShieldSamba = 16012,
            Sprint = 3,
            IsleSprint = 31314;

        public static class Buffs
        {
            public const ushort
                Swiftcast = 167,
                EurekaMoment = 2765,
                Troubadour = 1934,
                Tactician = 1951,
                ShieldSamba = 1826,
                Chum = 763;
        }

        public static class Debuffs
        {
            public const ushort
                Reprisal = 1193,
                Feint = 1195,
                Addle = 1203,
                Amnesia = 5,
                Amnesia2 = 1092,
                InefficientHooking = 764;
        }

        public static class Levels
        {
            public const byte
                Cast = 1,
                Hook = 1,
                Raise = 12,
                Snagging = 36,
                Gig = 61,
                Salvage = 67,
                VeteranTrade = 63,
                NaturesBounty = 69,
                SurfaceSlap = 71,
                PrizeCatch = 81;
        }
    }

    internal class AllSwiftcastFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllSwiftcastFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Raise || actionID == All.Resurrection || actionID == All.Ascend || actionID == All.Verraise || actionID == All.Egeiro || actionID == All.AngelWhisper)
            {
                if ((IsActionOffCooldown(All.Swiftcast) && !HasEffect(RDM.Buffs.Dualcast) && !HasEffect(All.Debuffs.Amnesia) && !HasEffect(All.Debuffs.Amnesia2))
                    || (level <= All.Levels.Raise && actionID != All.AngelWhisper)
                    || (level <= RDM.Levels.Verraise && actionID == All.Verraise))
                    return All.Swiftcast;
            }

            return actionID;
        }
    }

    internal class AllIsleSprintFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllIsleSprintFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (Service.ClientState.TerritoryType == 1055) return All.IsleSprint;

            return actionID;
        }
    }

    internal class AllEurekaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllEurekaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.SolidReason || actionID == All.AgelessWords)
            {
                if (HasEffect(All.Buffs.EurekaMoment))
                {
                    if (actionID == All.SolidReason)
                        return All.WiseToTheWorldMIN;
                    return All.WiseToTheWorldBTN;
                }
            }

            return actionID;
        }
    }

    internal class DoLCastHookFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastHookFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Cast)
            {
                if (HasCondition(ConditionFlag.Fishing))
                    return All.Hook;
            }

            return actionID;
        }
    }

    internal class DoLCastGigFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastGigFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Cast)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.Gig;
            }

            return actionID;
        }
    }

    internal class DoLSurfaceTradeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolSurfaceTradeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.SurfaceSlap)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.VeteranTrade;
            }

            return actionID;
        }
    }

    internal class DoLPrizeBountyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolPrizeBountyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.PrizeCatch)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.NaturesBounty;
            }

            return actionID;
        }
    }

    internal class DoLSnaggingSalvageFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolSnaggingSalvageFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Snagging)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.Salvage;
            }

            return actionID;
        }
    }

    internal class DoLCastLightElectricCurrentFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolCastLightElectricCurrentFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.CastLight)
            {
                if (HasCondition(ConditionFlag.Diving))
                    return All.ElectricCurrent;
            }

            return actionID;
        }
    }

    internal class DoLHookingHooksetsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolHookingHooksetsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.PrecisionHookset || actionID == All.PowerfulHookset)
            {
                if (CanUseAction(All.Hook) && !HasEffect(All.Debuffs.InefficientHooking))
                    return All.Hook;
                if (HasCondition(ConditionFlag.Diving) && IsEnabled(CustomComboPreset.DolCastGigFeature))
                    return All.Gig;
            }

            return actionID;
        }
    }

    internal class DoLChummyCastFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DolChummyCastFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.Chum && LocalPlayer is not null)
            {
                if (HasCondition(ConditionFlag.Fishing) && IsEnabled(CustomComboPreset.DolCastHookFeature))
                    return All.Hook;
                if (HasCondition(ConditionFlag.Diving) && IsEnabled(CustomComboPreset.DolCastGigFeature))
                    return All.Gig;

                if (LocalPlayer.CurrentGp < 100 ||
                    (LocalPlayer.CurrentGp < 150 && HasEffect(All.Debuffs.InefficientHooking)) ||
                    HasEffect(All.Buffs.Chum))
                {
                    return All.Cast;
                }
            }

            return actionID;
        }
    }

    internal class AllTankInterruptFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllTankInterruptFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == All.LowBlow)
            {
                if (CanInterruptEnemy() && IsActionOffCooldown(All.Interject) && CanUseAction(All.Interject))
                    return All.Interject;
            }

            return actionID;
        }
    }

    internal class AllReprisalLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllReprisalLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (new List<uint> { All.Reprisal, All.Addle, All.Feint }.Contains(actionID))
            {
                ushort debuffID = 0;

                switch (actionID)
                {
                    case All.Reprisal:
                        debuffID = All.Debuffs.Reprisal; break;
                    case All.Addle:
                        debuffID = All.Debuffs.Addle; break;
                    case All.Feint:
                        debuffID = All.Debuffs.Feint; break;

                }

                var debuff = FindTargetEffectAny(debuffID);
                if (debuff != null && debuff.RemainingTime > 3 && IsActionOffCooldown(actionID)) return SCH.Physick;
            }

            return actionID;
        }
    }

    internal class AllRangedDefenseCooldownLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AllRangedDefenseCooldownLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (new List<uint> { All.Troubadour, All.Tactician, All.ShieldSamba }.Contains(actionID))
            {
                var buffs = new List<ushort>() { All.Buffs.Troubadour, All.Buffs.Tactician, All.Buffs.ShieldSamba };
                ushort buffID = 0;

                foreach (var buff in buffs)
                {
                    if (HasEffectAny(buff))
                    {
                        buffID = buff;
                        break;
                    }
                }

                if (buffID == 0) return actionID;

                var buffStatus = FindEffectAny(buffID);
                if (buffStatus != null && buffStatus.RemainingTime > 3 && IsActionOffCooldown(actionID)) return SCH.Physick;
            }

            return actionID;
        }
    }
}