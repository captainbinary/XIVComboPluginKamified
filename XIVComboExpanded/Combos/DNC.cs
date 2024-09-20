using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class DNC
    {
        public const byte JobID = 38;

        public const uint
            // Single Target
            Cascade = 15989,
            Fountain = 15990,
            ReverseCascade = 15991,
            Fountainfall = 15992,
            // AoE
            Windmill = 15993,
            Bladeshower = 15994,
            RisingWindmill = 15995,
            Bloodshower = 15996,
            // Dancing
            StandardStep = 15997,
            TechnicalStep = 15998,
            Tillana = 25790,
            LastDance = 36983,
            // Fans
            FanDance1 = 16007,
            FanDance2 = 16008,
            FanDance3 = 16009,
            FanDance4 = 25791,
            // Other
            Jete = 16001,
            SaberDance = 16005,
            EnAvant = 16010,
            Devilment = 16011,
            Flourish = 16013,
            Improvisation = 16014,
            StarfallDance = 25792;

        public static class Buffs
        {
            public const ushort
                Devilment = 1825,
                FlourishingSymmetry = 3017,
                SilkenSymmetry = 2693,
                FlourishingFlow = 3018,
                SilkenFlow = 2694,
                FlourishingFinish = 2698,
                FlourishingStarfall = 2700,
                StandardStep = 1818,
                TechnicalStep = 1819,
                ThreefoldFanDance = 1820,
                FourfoldFanDance = 2699,
                TechnicalFinish = 1822;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Cascade = 1,
                Fountain = 2,
                Windmill = 15,
                StandardStep = 15,
                ReverseCascade = 20,
                Bladeshower = 25,
                RisingWindmill = 35,
                Fountainfall = 40,
                Bloodshower = 45,
                FanDance2 = 50,
                FanDance3 = 66,
                TechnicalStep = 70,
                Tillana = 82,
                FanDance4 = 86,
                StarfallDance = 90;
        }
    }

    internal class DancerDanceComboCompatibility : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDanceComboCompatibility;

        protected override uint[] ActionIDs => Service.Configuration.DancerDanceCompatActionIDs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<DNCGauge>();
            if (gauge.IsDancing)
            {
                var actionIDs = Service.Configuration.DancerDanceCompatActionIDs;

                if (actionID == actionIDs[0] || (actionIDs[0] == 0 && actionID == DNC.Cascade))
                    return OriginalHook(DNC.Cascade);

                if (actionID == actionIDs[1] || (actionIDs[1] == 0 && actionID == DNC.Flourish))
                    return OriginalHook(DNC.Fountain);

                if (actionID == actionIDs[2] || (actionIDs[2] == 0 && actionID == DNC.FanDance1))
                    return OriginalHook(DNC.ReverseCascade);

                if (actionID == actionIDs[3] || (actionIDs[3] == 0 && actionID == DNC.FanDance2))
                    return OriginalHook(DNC.Fountainfall);
            }

            return actionID;
        }
    }

    internal class DancerJeteShenanigans : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerJeteShenanigans;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(DNC.Jete)) return DNC.Jete;

            return actionID;
        }
    }

    internal class DancerFanDanceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFanDanceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.FanDance1 || actionID == DNC.FanDance2)
            {
                if (level >= DNC.Levels.FanDance4 && HasEffect(DNC.Buffs.FourfoldFanDance) && IsEnabled(CustomComboPreset.DancerFanDance4Combo))
                    return DNC.FanDance4;

                if (level >= DNC.Levels.FanDance3 && HasEffect(DNC.Buffs.ThreefoldFanDance))
                    return DNC.FanDance3;
            }

            return actionID;
        }
    }

    internal class DancerCombosToFanDance : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerCombosToFanDance;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var fanDance = actionID == DNC.Windmill ? DNC.FanDance2 : DNC.FanDance1;

            if (IsEnabled(CustomComboPreset.DancerFanDance4Combo) && GCDClipCheck() && CanUseAction(DNC.FanDance4))
                return DNC.FanDance4;

            if (IsEnabled(CustomComboPreset.DancerFanDanceCombo) && GCDClipCheck() && CanUseAction(DNC.FanDance3))
                return DNC.FanDance3;

            if (GetJobGauge<DNCGauge>().Feathers == 4 && GCDClipCheck())
                return fanDance;

            return actionID;
        }
    }

    internal class DancerCombosToSaberDanceOvercap : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerCombosToSaberDanceOvercap;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (GetJobGauge<DNCGauge>().Esprit >= 85)
                return OriginalHook(DNC.SaberDance);

            return actionID;
        }
    }

    internal class DancerFanDance1to2 : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFanDance1to2;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.FanDance1 && level >= DNC.Levels.FanDance2)
            {
                if (this.FilteredLastComboMove == DNC.Windmill || this.FilteredLastComboMove == DNC.Bladeshower)
                {
                    return DNC.FanDance2;
                }
            }

            return actionID;
        }
    }

    internal class DancerDanceStepCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDanceStepCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.StandardStep)
            {
                var gauge = GetJobGauge<DNCGauge>();
                if (gauge.IsDancing && level >= DNC.Levels.StandardStep && HasEffect(DNC.Buffs.StandardStep))
                {
                    if (gauge.CompletedSteps < 2)
                        return gauge.NextStep;

                    return OriginalHook(DNC.StandardStep);
                }

                return DNC.StandardStep;
            }

            if (actionID == DNC.TechnicalStep)
            {
                var gauge = GetJobGauge<DNCGauge>();
                if (gauge.IsDancing && level >= DNC.Levels.TechnicalStep && HasEffect(DNC.Buffs.TechnicalStep))
                {
                    if (gauge.CompletedSteps < 4)
                        return gauge.NextStep;
                }

                return OriginalHook(DNC.TechnicalStep);
            }

            return actionID;
        }
    }

    internal class DancerStandardechnicalStepFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerStandardechnicalStepFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(DNC.TechnicalStep) != DNC.TechnicalStep)
                return OriginalHook(DNC.TechnicalStep);

            if (IsEnabled(CustomComboPreset.DancerTechnicalLockoutFeature) && HasEffectAny(DNC.Buffs.TechnicalFinish) && FindEffectAny(DNC.Buffs.TechnicalFinish)?.RemainingTime > 8)
                return actionID;

            if (CanUseAction(DNC.TechnicalStep) && IsActionOffCooldown(DNC.TechnicalStep) && !IsActionOffCooldown(DNC.StandardStep))
                return DNC.TechnicalStep;

            return actionID;
        }
    }

    internal class DancerLastDanceFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerLastDanceFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (CanUseAction(DNC.LastDance))
                return DNC.LastDance;

            return actionID;
        }
    }

    internal class DancerFlourishFanDance3Feature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFlourishFanDance3Feature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Flourish)
            {
                if (level >= DNC.Levels.FanDance3 && HasEffect(DNC.Buffs.ThreefoldFanDance))
                    return DNC.FanDance3;
            }

            return actionID;
        }
    }

    internal class DancerFlourishFanDance4Feature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerFlourishFanDance4Feature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Flourish)
            {
                if (level >= DNC.Levels.FanDance4 && HasEffect(DNC.Buffs.FourfoldFanDance))
                    return DNC.FanDance4;
            }

            return actionID;
        }
    }

    internal class DancerSingleTargetMultibutton : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargetMultibutton;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Cascade)
            {
                if (OriginalHook(actionID) != actionID)
                    return actionID;
                if (IsEnabled(CustomComboPreset.DancerSingleTargetMultibuttonSaber) && HasEffect(DNC.Buffs.Devilment) && GetJobGauge<DNCGauge>().Esprit >= 50)
                    return OriginalHook(DNC.SaberDance);
                if (!IsEnabled(CustomComboPreset.DancerSingleTargetMultibuttonNoProcs))
                {
                    // From Fountain
                    if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                        return DNC.Fountainfall;
                    // From Cascade
                    if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                        return DNC.ReverseCascade;
                }

                // Cascade Combo
                if (lastComboMove == DNC.Cascade && level >= DNC.Levels.Fountain)
                    return DNC.Fountain;

                return DNC.Cascade;
            }

            return actionID;
        }
    }

    internal class DancerReverseCascadeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerReverseCascadeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.ReverseCascade)
            {
                if (OriginalHook(actionID) != actionID)
                    return actionID;
                if ((this.FilteredLastComboMove == DNC.Windmill || this.FilteredLastComboMove == DNC.Bladeshower) && IsEnabled(CustomComboPreset.DancerReverseCascadeComboAoE))
                {
                    if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                        return DNC.Bloodshower;
                    if (level >= DNC.Levels.RisingWindmill)
                        return DNC.RisingWindmill;
                }

                if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;
            }
            return actionID;
        }
    }

    internal class DancerSingleTargetProcs : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerSingleTargetProcs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) != actionID)
                return actionID;
            if (actionID == DNC.Cascade)
                if (level >= DNC.Levels.ReverseCascade && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.ReverseCascade;

            if (actionID == DNC.Fountain)
                if (level >= DNC.Levels.Fountainfall && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Fountainfall;

            return actionID;
        }
    }

    internal class DancerAoeMultibutton : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerAoeMultibutton;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Windmill)
            {
                if (OriginalHook(actionID) != actionID)
                    return actionID;
                if (IsEnabled(CustomComboPreset.DancerAoeMultibuttonSaber) && HasEffect(DNC.Buffs.Devilment) && GetJobGauge<DNCGauge>().Esprit >= 50)
                    return OriginalHook(DNC.SaberDance);
                if (!IsEnabled(CustomComboPreset.DancerAoeMultibuttonNoProcs))
                {
                    // From Bladeshower
                    if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                        return DNC.Bloodshower;

                    // From Windmill
                    if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                        return DNC.RisingWindmill;
                }

                // Windmill Combo
                if (lastComboMove == DNC.Windmill && level >= DNC.Levels.Bladeshower)
                    return DNC.Bladeshower;

                return DNC.Windmill;
            }

            return actionID;
        }
    }

    internal class DancerRisingWindmillCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerRisingWindmillCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.RisingWindmill)
            {
                if (OriginalHook(actionID) != actionID)
                    return actionID;
                if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;
            }

            return actionID;
        }
    }

    internal class DancerAoEProcs : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerAoeProcs;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (OriginalHook(actionID) != actionID)
                return actionID;
            if (actionID == DNC.Windmill)
                if (level >= DNC.Levels.RisingWindmill && (HasEffect(DNC.Buffs.FlourishingSymmetry) || HasEffect(DNC.Buffs.SilkenSymmetry)))
                    return DNC.RisingWindmill;

            if (actionID == DNC.Bladeshower)
                if (level >= DNC.Levels.Bloodshower && (HasEffect(DNC.Buffs.FlourishingFlow) || HasEffect(DNC.Buffs.SilkenFlow)))
                    return DNC.Bloodshower;

            return actionID;
        }
    }

    internal class DancerDevilmentFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerDevilmentFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DNC.Devilment)
            {
                if (level >= DNC.Levels.StarfallDance && HasEffect(DNC.Buffs.FlourishingStarfall))
                    return DNC.StarfallDance;

                return DNC.Devilment;
            }

            return actionID;
        }
    }

    internal class DancerTechnicalLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DancerTechnicalLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == DNC.TechnicalStep && OriginalHook(DNC.TechnicalStep) == DNC.TechnicalStep && IsActionOffCooldown(DNC.TechnicalStep) && HasEffectAny(DNC.Buffs.TechnicalFinish) && FindEffectAny(DNC.Buffs.TechnicalFinish)?.RemainingTime > 8 ? SMN.Physick : actionID;
        }
    }
}
