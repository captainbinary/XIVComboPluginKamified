using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboKamifiedPlugin.Combos
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
            DragonSight = 7398,
            BattleLitany = 3557;

        public static class Buffs
        {
            public const ushort
                SharperFangAndClaw = 802,
                EnhancedWheelingThrust = 803,
                DiveReady = 1243;
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

    internal class DragoonFangToThrustFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonFangToThrustFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.FangAndClaw)
            {
                if (HasEffect(DRG.Buffs.EnhancedWheelingThrust)) return DRG.WheelingThrust;
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
                if (lastComboMove == DRG.Disembowel && comboTime > 0) return OriginalHook(DRG.ChaosThrust);
            }

            return actionID;
        }
    }

    internal class DragoonOppositewyrmwindFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonOppositeWyrmwindFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.Disembowel)
            {
                if (lastComboMove != DRG.TrueThrust && lastComboMove != DRG.RaidenThrust && CanUseAction(DRG.WyrmwindThrust)) return DRG.WyrmwindThrust;
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
                if (IsEnabled(CustomComboPreset.DragoonOppositeWyrmwindFeature) && IsEnabled(CustomComboPreset.DragoonChaosThrustCombo) && CanUseAction(DRG.WyrmwindThrust) &&
                    (lastComboMove == DRG.Disembowel ||
                    (this.FilteredLastComboMove == DRG.ChaoticSpring && !IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && (CanUseAction(DRG.FangAndClaw) || CanUseAction(DRG.WheelingThrust))) ||
                    (IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && !CanUseAction(DRG.FangAndClaw) && CanUseAction(DRG.WheelingThrust))))
                    return DRG.WyrmwindThrust;

                if (IsEnabled(CustomComboPreset.DragoonFullThrustTalonFeature))
                {
                    if (CanUseAction(DRG.PiercingTalon) && !InMeleeRange())
                        return DRG.PiercingTalon;
                }

                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.VorpalThrust)
                        return DRG.VorpalThrust;

                    if (lastComboMove == DRG.VorpalThrust && level >= DRG.Levels.FullThrust)
                        return OriginalHook(DRG.FullThrust);
                }

                if (IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && (HasEffect(DRG.Buffs.SharperFangAndClaw) || HasEffect(DRG.Buffs.EnhancedWheelingThrust)))
                    return DRG.FangAndClaw;

                if (HasEffect(DRG.Buffs.SharperFangAndClaw) && level >= DRG.Levels.FangAndClaw && (this.FilteredLastComboMove == DRG.FullThrust || this.FilteredLastComboMove == DRG.HeavensThrust || this.FilteredLastComboMove == 0 || !IsEnabled(CustomComboPreset.DragoonComboCosmeticOption)))
                    return DRG.FangAndClaw;

                if (HasEffect(DRG.Buffs.EnhancedWheelingThrust) && level >= DRG.Levels.WheelingThrust && (this.FilteredLastComboMove == DRG.FullThrust || this.FilteredLastComboMove == DRG.HeavensThrust || this.FilteredLastComboMove == 0 || !IsEnabled(CustomComboPreset.DragoonComboCosmeticOption)))
                    return DRG.WheelingThrust;

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
                if (IsEnabled(CustomComboPreset.DragoonOppositeWyrmwindFeature) && CanUseAction(DRG.WyrmwindThrust) &&
                    (lastComboMove == DRG.VorpalThrust ||
                    (this.FilteredLastComboMove == DRG.HeavensThrust && !IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && (CanUseAction(DRG.FangAndClaw) || CanUseAction(DRG.WheelingThrust))) ||
                    (IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && !CanUseAction(DRG.WheelingThrust) && CanUseAction(DRG.FangAndClaw))))
                    return DRG.WyrmwindThrust;
                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.Disembowel)
                        return DRG.Disembowel;

                    if (lastComboMove == DRG.Disembowel && level >= DRG.Levels.ChaosThrust)
                        return OriginalHook(DRG.ChaosThrust);
                }

                if (IsEnabled(CustomComboPreset.DragoonFangThrustFeature) && (HasEffect(DRG.Buffs.SharperFangAndClaw) || HasEffect(DRG.Buffs.EnhancedWheelingThrust)))
                    return DRG.WheelingThrust;

                if (HasEffect(DRG.Buffs.SharperFangAndClaw) && level >= DRG.Levels.FangAndClaw && (this.FilteredLastComboMove == DRG.Disembowel || this.FilteredLastComboMove == DRG.ChaosThrust || this.FilteredLastComboMove == DRG.ChaoticSpring || this.FilteredLastComboMove == 0 || !IsEnabled(CustomComboPreset.DragoonComboCosmeticOption)))
                    return DRG.FangAndClaw;

                if (HasEffect(DRG.Buffs.EnhancedWheelingThrust) && level >= DRG.Levels.WheelingThrust && (this.FilteredLastComboMove == DRG.Disembowel || this.FilteredLastComboMove == DRG.ChaosThrust || this.FilteredLastComboMove == DRG.ChaoticSpring || this.FilteredLastComboMove == 0 || !IsEnabled(CustomComboPreset.DragoonComboCosmeticOption)))
                    return DRG.WheelingThrust;

                if (IsEnabled(CustomComboPreset.DragoonChaosThrustComboOption))
                    return DRG.Disembowel;

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
            var gauge = GetJobGauge<DRGGauge>();
            return IsActionOffCooldown(DRG.Nastrond) || !gauge.IsLOTDActive || !IsActionOffCooldown(DRG.Stardiver) ? OriginalHook(DRG.Geirskogul) : DRG.Stardiver;
        }
    }

    internal class DragoonStarfireDiveFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonStarfireDiveFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<DRGGauge>();
            return (IsActionOffCooldown(DRG.DragonfireDive) && gauge.LOTDTimer > 7.5) || !gauge.IsLOTDActive || !IsActionOffCooldown(DRG.Stardiver) ? DRG.DragonfireDive : DRG.Stardiver;
        }
    }

    internal class DragoonLanceSightFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonLanceSightFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.LanceCharge)
            {
                if (CanUseAction(DRG.DragonSight) && IsActionOffCooldown(DRG.DragonSight) && !IsActionOffCooldown(DRG.LanceCharge)) return DRG.DragonSight;
            }

            return actionID;
        }
    }

    internal class DragoonLancetanyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonLancetanyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.LanceCharge)
            {
                if (CanUseAction(DRG.BattleLitany) && IsActionOffCooldown(DRG.BattleLitany) && !IsActionOffCooldown(DRG.LanceCharge)) return DRG.BattleLitany;
            }

            return actionID;
        }
    }
}
