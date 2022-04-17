using System;

using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboKamifiedPlugin.Combos
{
    internal static class MCH
    {
        public const byte JobID = 31;

        public const uint
            // Single target
            CleanShot = 2873,
            HeatedCleanShot = 7413,
            SplitShot = 2866,
            HeatedSplitShot = 7411,
            SlugShot = 2868,
            HeatedSlugShot = 7412,
            // Charges
            GaussRound = 2874,
            Ricochet = 2890,
            // AoE
            SpreadShot = 2870,
            Scattergun = 25786,
            AutoCrossbow = 16497,
            // Rook
            RookAutoturret = 2864,
            RookOverdrive = 7415,
            AutomatonQueen = 16501,
            QueenOverdrive = 16502,
            // Other
            Hypercharge = 17209,
            Reassemble = 2876,
            Wildfire = 2878,
            HeatBlast = 7410,
            HotShot = 2872,
            Drill = 16498,
            AirAnchor = 16500,
            Chainsaw = 25788;

        public static class Buffs
        {
            public const ushort
                Reassemble = 851;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                SlugShot = 2,
                GaussRound = 15,
                CleanShot = 26,
                Hypercharge = 30,
                HeatBlast = 35,
                RookOverdrive = 40,
                Wildfire = 45,
                Ricochet = 50,
                AutoCrossbow = 52,
                HeatedSplitShot = 54,
                Drill = 58,
                HeatedSlugshot = 60,
                HeatedCleanShot = 64,
                ChargedActionMastery = 74,
                AirAnchor = 76,
                QueenOverdrive = 80,
                EnhancedReassemble = 84,
                Chainsaw = 90;
        }
    }

    internal class MachinistHypercomboFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistHypercomboFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<MCHGauge>();
            if (IsEnabled(CustomComboPreset.MachinistHypercomboOption))
            {
                if (gauge.IsOverheated && ((lastComboMove == MCH.SplitShot && (actionID == MCH.SlugShot || actionID == MCH.HeatedSlugShot)) ||
                    (lastComboMove == MCH.SlugShot && (actionID == MCH.CleanShot || actionID == MCH.HeatedCleanShot)) ||
                    (lastComboMove != MCH.SlugShot && lastComboMove != MCH.SplitShot && (actionID == MCH.SplitShot || actionID == MCH.HeatedSplitShot))))
                    return MCH.HeatBlast;
                return actionID;
            }

            return gauge.IsOverheated && CanUseAction(MCH.HeatBlast) ? MCH.HeatBlast : actionID;
        }
    }

    internal class MachinistMainCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistMainCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.CleanShot || actionID == MCH.HeatedCleanShot)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == MCH.SplitShot && CanUseAction(OriginalHook(MCH.SlugShot)))
                        return OriginalHook(MCH.SlugShot);

                    if (lastComboMove == MCH.SlugShot && CanUseAction(OriginalHook(MCH.CleanShot)))
                        return OriginalHook(MCH.CleanShot);
                }

                return OriginalHook(MCH.SplitShot);
            }

            return actionID;
        }
    }

    internal class MachinistGaussRoundRicochetFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistGaussRoundRicochetFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if ((actionID == MCH.GaussRound || actionID == MCH.Ricochet) && (!IsEnabled(CustomComboPreset.MachinistGaussRoundRicochetFeatureOption) || GetJobGauge<MCHGauge>().IsOverheated))
            {
                if (CanUseAction(MCH.Ricochet))
                    return CalcBestAction(actionID, MCH.GaussRound, MCH.Ricochet);

                return MCH.GaussRound;
            }

            return actionID;
        }
    }

    internal class MachinistHyperfireFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistHyperfireFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return ((IsActionOffCooldown(MCH.Wildfire) && CurrentTarget is not null) || (OriginalHook(MCH.Wildfire) != MCH.Wildfire && !IsActionOffCooldown(MCH.Hypercharge))) && CanUseAction(MCH.Wildfire) && actionID == MCH.Hypercharge ? OriginalHook(MCH.Wildfire) : actionID;
        }
    }

    internal class MachinistReassembleFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistReassembleFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var currentAction = !CanUseAction(MCH.Chainsaw) && IsEnabled(CustomComboPreset.MachinistReassembleOption) ? MCH.Drill : MCH.Chainsaw;
            var cooldownElapsed = GetCooldown(currentAction).CooldownElapsed;
            // This delay makes sure you don't fatfinger Reassemble twice if you are using it after it gets charges and are smashing the button.
            bool delay = !IsActionOffCooldown(currentAction) && cooldownElapsed < 1 && level >= MCH.Levels.EnhancedReassemble && GetCooldown(MCH.Reassemble).CooldownRemaining < 55 && !IsActionOffCooldown(MCH.Reassemble);
            return actionID == MCH.Reassemble && (HasEffect(MCH.Buffs.Reassemble) || delay) && CanUseAction(currentAction) ? currentAction : actionID;
        }
    }

    internal class MachinistOverheatFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistOverheatFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.HeatBlast || actionID == MCH.AutoCrossbow)
            {
                if (IsEnabled(CustomComboPreset.MachinistHyperfireFeature) && IsActionOffCooldown(MCH.Wildfire) && CanUseAction(MCH.Wildfire))
                    return MCH.Wildfire;
                var gauge = GetJobGauge<MCHGauge>();
                if (!gauge.IsOverheated && CanUseAction(MCH.Hypercharge))
                    return MCH.Hypercharge;

                if (!CanUseAction(MCH.AutoCrossbow))
                    return MCH.HeatBlast;
            }

            return actionID;
        }
    }

    internal class MachinistSpreadShotFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistSpreadShotFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.SpreadShot || actionID == MCH.Scattergun)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsOverheated && CanUseAction(MCH.AutoCrossbow))
                    return MCH.AutoCrossbow;

                return OriginalHook(MCH.SpreadShot);
            }

            return actionID;
        }
    }

    internal class MachinistOverdriveFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistOverdriveFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.RookAutoturret || actionID == MCH.AutomatonQueen)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsRobotActive)
                    // ROOKIE
                    return OriginalHook(MCH.QueenOverdrive);
            }

            return actionID;
        }
    }

    internal class MachinistDrillAirAnchorFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistHotShotDrillChainsawFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.Drill || actionID == MCH.HotShot || actionID == MCH.AirAnchor)
            {
                if (CanUseAction(MCH.Chainsaw))
                    return CalcBestAction(actionID, MCH.Chainsaw, MCH.AirAnchor, MCH.Drill);

                if (CanUseAction(MCH.AirAnchor))
                    return CalcBestAction(actionID, MCH.AirAnchor, MCH.Drill);

                if (CanUseAction(MCH.Drill))
                    return CalcBestAction(actionID, MCH.Drill, MCH.HotShot);

                return MCH.HotShot;
            }

            return actionID;
        }
    }
}
