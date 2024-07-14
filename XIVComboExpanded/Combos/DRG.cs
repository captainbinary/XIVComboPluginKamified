using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class DRG
    {
        public const byte ClassID = 4;
        public const byte JobID = 22;

        public const uint
            // Single Target
            TrueThrust = 75,
            VorpalThrust = 78,
            Disembowel = 87,
            FullThrust = 84,
            ChaosThrust = 88,
            HeavensThrust = 25771,
            ChaoticSpring = 25772,
            WheelingThrust = 3556,
            FangAndClaw = 3554,
            Drakesbane = 36952,
            RaidenThrust = 16479,
            PiercingTalon = 90,
            // AoE
            DoomSpike = 86,
            SonicThrust = 7397,
            CoerthanTorment = 16477,
            DraconianFury = 25770,
            // Combined
            Geirskogul = 3555,
            Nastrond = 7400,
            // Jumps
            Jump = 92,
            HighJump = 16478,
            MirageDive = 7399,
            DragonfireDive = 96,
            // Dragon
            Stardiver = 16480,
            WyrmwindThrust = 25773,
            // Buffs
            LanceCharge = 85,
            BattleLitany = 3557;

        public static class Buffs
        {
            public const ushort
                BattleLitany = 786,
                SharperFangAndClaw = 802,
                EnhancedWheelingThrust = 803,
                DiveReady = 1243,
                NastrondReady = 3844;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                VorpalThrust = 4,
                Disembowel = 18,
                FullThrust = 26,
                ChaosThrust = 50,
                HeavensThrust = 86,
                ChaoticSpring = 86,
                FangAndClaw = 56,
                WheelingThrust = 58,
                SonicThrust = 62,
                MirageDive = 68,
                CoerthanTorment = 72,
                HighJump = 74,
                RaidenThrust = 76,
                Stardiver = 80;
        }
    }

    internal class DragoonCoerthanTormentCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonCoerthanTormentCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.CoerthanTorment)
            {
                var gauge = GetJobGauge<DRGGauge>();
                if (gauge.FirstmindsFocusCount == 2 && IsEnabled(CustomComboPreset.DragoonWyrmwindFeature))
                    return DRG.WyrmwindThrust;

                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.DoomSpike || lastComboMove == DRG.DraconianFury) && level >= DRG.Levels.SonicThrust)
                        return DRG.SonicThrust;

                    if (lastComboMove == DRG.SonicThrust && level >= DRG.Levels.CoerthanTorment)
                        return DRG.CoerthanTorment;
                }

                return OriginalHook(DRG.DoomSpike);
            }

            return actionID;
        }
    }

    internal class DragoonRaidenWyrmwindFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonRaidenWyrmwindFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.TrueThrust)
            {
                if (CanUseAction(DRG.WyrmwindThrust)) return DRG.WyrmwindThrust;
            }

            return actionID;
        }
    }

    internal class DragoonFullChaosFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonFullChaosFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.FullThrust || actionID == DRG.HeavensThrust)
            {
                if (lastComboMove == OriginalHook(DRG.Disembowel) && comboTime > 0) return OriginalHook(DRG.ChaosThrust);

                if (lastComboMove == OriginalHook(DRG.ChaosThrust) && CanUseAction(DRG.WheelingThrust))
                    return DRG.WheelingThrust;

                if (lastComboMove == DRG.WheelingThrust && CanUseAction(DRG.Drakesbane))
                    return DRG.Drakesbane;
            }

            return actionID;
        }
    }

    internal class DragoonFullThrustCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonFullThrustCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.FullThrust || actionID == DRG.HeavensThrust)
            {
                if (IsEnabled(CustomComboPreset.DragoonFullThrustTalonFeature))
                {
                    if (CanUseAction(DRG.PiercingTalon) && !InMeleeRange())
                        return DRG.PiercingTalon;
                }

                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.VorpalThrust)
                        return OriginalHook(DRG.VorpalThrust);

                    if (lastComboMove == OriginalHook(DRG.VorpalThrust) && level >= DRG.Levels.FullThrust)
                        return OriginalHook(DRG.FullThrust);

                    if (lastComboMove == OriginalHook(DRG.FullThrust) && CanUseAction(DRG.FangAndClaw))
                        return DRG.FangAndClaw;

                    if (lastComboMove == DRG.FangAndClaw && CanUseAction(DRG.Drakesbane))
                        return DRG.Drakesbane;
                }

                if (IsEnabled(CustomComboPreset.DragoonFullThrustComboOption))
                    return DRG.VorpalThrust;

                return IsEnabled(CustomComboPreset.DragoonRaidenWyrmwindFeature) && CanUseAction(DRG.WyrmwindThrust) && IsEnabled(CustomComboPreset.DragoonChaosThrustCombo) ? DRG.WyrmwindThrust : OriginalHook(DRG.TrueThrust);
            }

            return actionID;
        }
    }

    internal class DragoonChaosThrustCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonChaosThrustCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.ChaosThrust || actionID == DRG.ChaoticSpring)
            {
                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.Disembowel)
                        return OriginalHook(DRG.Disembowel);

                    if (lastComboMove == OriginalHook(DRG.Disembowel) && level >= DRG.Levels.ChaosThrust)
                        return OriginalHook(DRG.ChaosThrust);

                    if (lastComboMove == OriginalHook(DRG.ChaosThrust) && CanUseAction(DRG.WheelingThrust))
                        return DRG.WheelingThrust;

                    if (lastComboMove == DRG.WheelingThrust && CanUseAction(DRG.Drakesbane))
                        return DRG.Drakesbane;
                }

                if (IsEnabled(CustomComboPreset.DragoonChaosThrustComboOption))
                    return OriginalHook(DRG.Disembowel);

                return IsEnabled(CustomComboPreset.DragoonRaidenWyrmwindFeature) && CanUseAction(DRG.WyrmwindThrust) ? DRG.WyrmwindThrust : OriginalHook(DRG.TrueThrust);
            }

            return actionID;
        }
    }

    internal class DragoonNastrondFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonNastrondFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(DRG.Stardiver) != DRG.Stardiver) return OriginalHook(DRG.Stardiver);

            var gauge = GetJobGauge<DRGGauge>();

            return !IsActionOffCooldown(DRG.Stardiver) || !CanUseAction(DRG.Stardiver) ? OriginalHook(DRG.Geirskogul) : DRG.Stardiver;
        }
    }

    internal class DragoonStarfireDiveFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonStarfireDiveFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(DRG.DragonfireDive) != DRG.DragonfireDive && !IsActionOffCooldown(DRG.Stardiver) && OriginalHook(DRG.Stardiver) == DRG.Stardiver) return OriginalHook(DRG.DragonfireDive);

            return !IsActionOffCooldown(DRG.DragonfireDive) && CanUseAction(DRG.Stardiver) ? actionID : DRG.DragonfireDive;
        }
    }

    internal class DragoonLancetanyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonLancetanyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.LanceCharge)
            {
                if ((CanUseAction(DRG.BattleLitany) && IsActionOffCooldown(DRG.BattleLitany) && !IsActionOffCooldown(DRG.LanceCharge))
                    && !(IsEnabled(CustomComboPreset.DragoonLitanyLockoutFeature) && HasEffectAny(DRG.Buffs.BattleLitany) && FindEffectAny(DRG.Buffs.BattleLitany)?.RemainingTime > 3)) return DRG.BattleLitany;
            }

            return actionID;
        }
    }

    internal class DragoonLitanyLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonLitanyLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == DRG.BattleLitany && IsActionOffCooldown(DRG.BattleLitany) && HasEffectAny(DRG.Buffs.BattleLitany) && FindEffectAny(DRG.Buffs.BattleLitany)?.RemainingTime > 3 ? SMN.Physick : actionID;
        }
    }
}
