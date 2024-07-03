using Dalamud.Game.ClientState.Statuses;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class PLD
    {
        public const byte ClassID = 1;
        public const byte JobID = 19;

        public const uint
            FastBlade = 9,
            RiotBlade = 15,
            ShieldBash = 16,
            NotNoMercy = 20,
            RageOfHalone = 21,
            NotSonicBreak = 3538,
            RoyalAuthority = 3539,
            LowBlow = 7540,
            TotalEclipse = 7381,
            Requiescat = 7383,
            Imperator = 36921,
            NotBurstStrike = 7384,
            Prominence = 16457,
            NotFatedCircle = 16458,
            NotGnashingFangCombo = 16459,
            Atonement = 16460,
            NotGnashingFang = 25748,
            NotSavageClaw = 25749,
            NotWickedTalon = 25750,
            SpiritsWithin = 29,
            Expiacion = 25747,
            CircleOfScorn = 23,
            ShieldLob = 24;

        public static class Buffs
        {
            public const ushort
                NotNoMercy = 76,
                Requiescat = 1368,
                SwordOath = 1902,
                DivineMight = 2673;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                RiotBlade = 4,
                RageOfHalone = 26,
                Prominence = 40,
                CircleOfScorn = 50,
                NotSonicBreak = 54,
                RoyalAuthority = 60,
                NotFatedCircle = 72,
                Atonement = 76,
                NotGnashingFangCombo = 80,
                Expiacion = 86;
        }
    }

    internal class PaladinLowBashFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinLowBashFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.AllTankInterruptFeature) && CanInterruptEnemy() && IsActionOffCooldown(All.Interject))
                return All.Interject;

            return IsActionOffCooldown(All.LowBlow) ? All.LowBlow : actionID;
        }
    }

    internal class PaladinShieldLobToNotBurstStrikeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinShieldLobToNotBurstStrikeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.PaladinShieldLobToNotBurstStrikeOption))
                return actionID == PLD.ShieldLob && CanUseAction(PLD.NotBurstStrike) && LocalPlayer?.CurrentMp >= 1000 && (HasEffect(PLD.Buffs.DivineMight) || !IsMoving()) ? PLD.NotBurstStrike : actionID;
            return actionID == PLD.ShieldLob && LocalPlayer?.CurrentMp >= 1000 && HasEffect(PLD.Buffs.DivineMight) ? PLD.NotBurstStrike : actionID;
        }
    }

    internal class PaladinNotNoMercyToRequiescatFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinNotNoMercyToRequiescat;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.NotNoMercy)
            {
                if (HasEffect(PLD.Buffs.NotNoMercy))
                {
                    bool currentlyNotSonicBreak = OriginalHook(PLD.NotNoMercy) == PLD.NotNoMercy;
                    if (IsActionOffCooldown(OriginalHook(PLD.Requiescat)) && CanUseAction(OriginalHook(PLD.Requiescat)))
                    {
                        if (!currentlyNotSonicBreak)
                            return GetCooldown(PLD.FastBlade).CooldownRemaining >= 0.5 ? OriginalHook(PLD.Requiescat) : actionID;
                        return OriginalHook(PLD.Requiescat);
                    }

                    if (OriginalHook(PLD.Requiescat) != PLD.Requiescat)
                        return OriginalHook(PLD.Requiescat);

                    if (!currentlyNotSonicBreak && CanUseAction(OriginalHook(PLD.NotGnashingFangCombo)) && IsEnabled(CustomComboPreset.PaladinRequiescatCombo))
                        return OriginalHook(PLD.NotGnashingFangCombo);
                    if (!currentlyNotSonicBreak && HasEffect(PLD.Buffs.Requiescat) && IsEnabled(CustomComboPreset.PaladinRequiescatComboSpirit))
                        return IsEnabled(CustomComboPreset.PaladinHolySpiritToHolyCircleFeature) && (this.FilteredLastComboMove == PLD.Prominence || this.FilteredLastComboMove == PLD.TotalEclipse) && level >= PLD.Levels.NotFatedCircle ? PLD.NotFatedCircle : PLD.NotBurstStrike;
                }
            }

            return actionID;
        }
    }

/*    internal class PaladinNotNoMercyToNotSonicBreakFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinNotNoMercyToNotSonicBreak;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.NotNoMercy)
            {
                if (HasEffect(PLD.Buffs.NotNoMercy))
                {
                    if (IsActionOffCooldown(PLD.NotSonicBreak) && CanUseAction(PLD.NotSonicBreak))
                        return PLD.NotSonicBreak;
                }
            }

            return actionID;
        }
    }*/

    internal class PaladinRoyalAuthorityAtonementFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityAtonementFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return CanUseAction(OriginalHook(PLD.Atonement)) && (!IsEnabled(CustomComboPreset.PaladinRoyalLobFeature) || InMeleeRange()) ? OriginalHook(PLD.Atonement) : actionID;
        }
    }

    /*internal class PaladinAtonementFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinNotBurstStrikeToAtonement;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == PLD.NotBurstStrike && ((!HasEffect(PLD.Buffs.DivineMight) && !HasEffect(PLD.Buffs.Requiescat)) || LocalPlayer?.CurrentMp < 1000) && HasEffect(PLD.Buffs.SwordOath) && InMeleeRange() ? PLD.Atonement : actionID;
        }
    }*/

    internal class PaladinConfiteorFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinConfiteorFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.NotBurstStrike || actionID == PLD.NotFatedCircle)
            {
                if (CanUseAction(OriginalHook(PLD.NotGnashingFangCombo)))
                    return OriginalHook(PLD.NotGnashingFangCombo);

                if (OriginalHook(PLD.Requiescat) != PLD.Requiescat && OriginalHook(PLD.Requiescat) != PLD.Imperator)
                    return OriginalHook(PLD.Requiescat);
            }

            return actionID;
        }
    }

    internal class PaladinHolyCircleFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinNotFatedCircleFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if ((actionID == PLD.TotalEclipse || actionID == PLD.Prominence) && HasEffect(PLD.Buffs.Requiescat) && LocalPlayer?.CurrentMp >= 1000)
            {
                if (level >= PLD.Levels.NotGnashingFangCombo || OriginalHook(PLD.NotGnashingFangCombo) != PLD.NotGnashingFangCombo)
                    return OriginalHook(PLD.NotGnashingFangCombo);

                if (OriginalHook(PLD.Requiescat) != PLD.Requiescat && OriginalHook(PLD.Requiescat) != PLD.Imperator)
                    return OriginalHook(PLD.Requiescat);

                if (IsEnabled(CustomComboPreset.PaladinRequiescatComboSpirit))
                {
                    if (CanUseAction(PLD.NotBurstStrike) && !CanUseAction(OriginalHook(PLD.NotGnashingFangCombo)))
                        return (this.FilteredLastComboMove == PLD.Prominence || this.FilteredLastComboMove == PLD.TotalEclipse) && level >= PLD.Levels.NotFatedCircle ? PLD.NotFatedCircle : PLD.NotBurstStrike;
                }
            }

            return actionID;
        }
    }

    internal class PaladinRoyalAuthorityNotBurstStrikeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityNotBurstStrikeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return !IsEnabled(CustomComboPreset.PaladinRoyalAuthorityCombo) && (actionID == PLD.RageOfHalone || actionID == PLD.RoyalAuthority) && HasEffect(PLD.Buffs.DivineMight) && LocalPlayer?.CurrentMp >= 1000 ? PLD.NotBurstStrike : actionID;
        }
    }

    internal class PaladinRoyalAuthorityCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.RoyalAuthority || actionID == PLD.RageOfHalone)
            {
                if (IsEnabled(CustomComboPreset.PaladinRoyalLobFeature))
                {
                    if (CanUseAction(PLD.ShieldLob) && !InMeleeRange()
                        && !(IsEnabled(CustomComboPreset.PaladinRoyalAuthorityNotBurstStrikeFeature) && HasEffect(PLD.Buffs.DivineMight) && LocalPlayer?.CurrentMp >= 1000 && lastComboMove == PLD.RiotBlade))
                        return IsEnabled(CustomComboPreset.PaladinShieldLobToNotBurstStrikeFeature) && LocalPlayer?.CurrentMp >= 1000 && CanUseAction(PLD.NotBurstStrike)
                            && (HasEffect(PLD.Buffs.DivineMight) || (IsEnabled(CustomComboPreset.PaladinShieldLobToNotBurstStrikeOption) && !IsMoving())) ? PLD.NotBurstStrike : PLD.ShieldLob;
                }

                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
                        return PLD.RiotBlade;

                    if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.RageOfHalone)
                        return IsEnabled(CustomComboPreset.PaladinRoyalAuthorityNotBurstStrikeFeature) && HasEffect(PLD.Buffs.DivineMight) && LocalPlayer?.CurrentMp >= 1000 ? PLD.NotBurstStrike : OriginalHook(PLD.RageOfHalone);
                }

                return PLD.FastBlade;
            }

            return actionID;
        }
    }

    internal class PaladinProminenceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinProminenceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == (IsEnabled(CustomComboPreset.PaladinEvilProminenceCombo) ? PLD.TotalEclipse : PLD.Prominence))
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.TotalEclipse && CanUseAction(PLD.Prominence))
                        return IsEnabled(CustomComboPreset.PaladinNotFatedCircleOvercapFeature) && HasEffect(PLD.Buffs.DivineMight) && level >= PLD.Levels.NotFatedCircle && LocalPlayer?.CurrentMp >= 1000 ? PLD.NotFatedCircle : PLD.Prominence;
                }

                return PLD.TotalEclipse;
            }

            return actionID;
        }
    }

    internal class PaladinRequiescatCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRequiescatCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.Requiescat || actionID == PLD.Imperator)
            {
                if (CanUseAction(PLD.NotGnashingFangCombo) || OriginalHook(PLD.NotGnashingFangCombo) != PLD.NotGnashingFangCombo)
                    return OriginalHook(PLD.NotGnashingFangCombo);

                if (IsEnabled(CustomComboPreset.PaladinRequiescatComboSpirit) && HasEffect(PLD.Buffs.Requiescat))
                {
                    if (CanUseAction(PLD.NotBurstStrike) && !CanUseAction(OriginalHook(PLD.NotGnashingFangCombo)))
                        return IsEnabled(CustomComboPreset.PaladinHolySpiritToHolyCircleFeature) && (this.FilteredLastComboMove == PLD.Prominence || this.FilteredLastComboMove == PLD.TotalEclipse) && level >= PLD.Levels.NotFatedCircle ? PLD.NotFatedCircle : PLD.NotBurstStrike;
                }
            }

            return actionID;
        }
    }

    internal class PaladinHolySpiritToHolyCircleFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinHolySpiritToHolyCircleFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.NotBurstStrike)
            {
                if ((this.FilteredLastComboMove == PLD.Prominence || this.FilteredLastComboMove == PLD.TotalEclipse) && level >= PLD.Levels.NotFatedCircle)
                    return PLD.NotFatedCircle;
            }

            return actionID;
        }
    }

    internal class PaladinScornfulSpiritsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinScornfulSpiritsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (level < PLD.Levels.CircleOfScorn)
                return PLD.SpiritsWithin;

            var oppositeActionID = actionID == PLD.SpiritsWithin || actionID == PLD.Expiacion ? PLD.CircleOfScorn : PLD.SpiritsWithin;

            if (!IsActionOffCooldown(actionID) && IsActionOffCooldown(oppositeActionID))
                return OriginalHook(oppositeActionID);

            return actionID;
        }
    }
}
