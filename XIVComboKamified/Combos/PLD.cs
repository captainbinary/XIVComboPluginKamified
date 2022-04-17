using Dalamud.Game.ClientState.Statuses;

namespace XIVComboKamifiedPlugin.Combos
{
    internal static class PLD
    {
        public const byte ClassID = 1;
        public const byte JobID = 19;

        public const uint
            FastBlade = 9,
            RiotBlade = 15,
            ShieldBash = 16,
            RageOfHalone = 21,
            GoringBlade = 3538,
            RoyalAuthority = 3539,
            LowBlow = 7540,
            TotalEclipse = 7381,
            Requiescat = 7383,
            HolySpirit = 7384,
            Prominence = 16457,
            HolyCircle = 16458,
            Confiteor = 16459,
            Atonement = 16460,
            BladeOfFaith = 25748,
            BladeOfTruth = 25749,
            BladeOfValor = 25750,
            SpiritsWithin = 29,
            Expiacion = 25747,
            CircleOfScorn = 23,
            ShieldLob = 24;

        public static class Buffs
        {
            public const ushort
                FightOrFlight = 76,
                Requiescat = 1368,
                SwordOath = 1902;
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
                GoringBlade = 54,
                RoyalAuthority = 60,
                HolyCircle = 72,
                Atonement = 76,
                Confiteor = 80,
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

    internal class PaladinGoringBladeAtonementFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinGoringBladeAtonementFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return HasEffect(PLD.Buffs.SwordOath) && lastComboMove != PLD.FastBlade && lastComboMove != PLD.RiotBlade ? PLD.Atonement : actionID;
        }
    }

    internal class PaladinAtonementFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityAtonementFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.PaladinRoyalSpiritFeature))
            {
                if ((HasEffect(PLD.Buffs.Requiescat) && !HasEffect(PLD.Buffs.FightOrFlight))
                    ||
                    (IsEnabled(CustomComboPreset.PaladinConfiteorFeature) && OriginalHook(PLD.Confiteor) != PLD.Confiteor))
                return actionID;
            }

            return HasEffect(PLD.Buffs.SwordOath) && lastComboMove != PLD.FastBlade && lastComboMove != PLD.RiotBlade ? PLD.Atonement : actionID;
        }
    }

    internal class PaladinHolyCircleFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinHolyCircleFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (IsEnabled(CustomComboPreset.PaladinConfiteorFeature))
            {
                if (OriginalHook(PLD.Confiteor) != PLD.Confiteor)
                    return OriginalHook(PLD.Confiteor);
            }

            if (HasEffect(PLD.Buffs.Requiescat))
            {
                if (IsEnabled(CustomComboPreset.PaladinConfiteorFeature))
                {
                    if (FindEffect(PLD.Buffs.Requiescat)?.StackCount <= 1 && level >= PLD.Levels.Confiteor)
                    {
                        return OriginalHook(PLD.Confiteor);
                    }
                }

                return level >= PLD.Levels.HolyCircle ? PLD.HolyCircle : actionID;
            }

            return actionID;
        }
    }

    internal class PaladinGoringBladeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinGoringBladeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.GoringBlade)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
                        return PLD.RiotBlade;

                    if (lastComboMove == PLD.RiotBlade && CanUseAction(PLD.GoringBlade))
                        return PLD.GoringBlade;
                }

                return PLD.FastBlade;
            }

            return actionID;
        }
    }

    internal class PaladinRoyalAuthorityCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.RoyalAuthority || actionID == PLD.RageOfHalone)
            {
                if (IsEnabled(CustomComboPreset.PaladinRoyalSpiritFeature))
                {
                    if (IsEnabled(CustomComboPreset.PaladinConfiteorFeature))
                    {
                        if (OriginalHook(PLD.Confiteor) != PLD.Confiteor)
                            return OriginalHook(PLD.Confiteor);
                    }

                    Status? requiescat = FindEffect(PLD.Buffs.Requiescat);

                    if (requiescat != null && !HasEffect(PLD.Buffs.FightOrFlight) && LocalPlayer?.CurrentMp >= 1000)
                    {
                        if (requiescat.StackCount <= 1 && level >= PLD.Levels.Confiteor && IsEnabled(CustomComboPreset.PaladinConfiteorFeature))
                        {
                            return OriginalHook(PLD.Confiteor);
                        }

                        return PLD.HolySpirit;
                    }
                }

                if (IsEnabled(CustomComboPreset.PaladinRoyalLobFeature))
                {
                    if (CanUseAction(PLD.ShieldLob) && !InMeleeRange())
                        return PLD.ShieldLob;
                }

                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
                        return PLD.RiotBlade;

                    if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.RageOfHalone)
                        return OriginalHook(PLD.RageOfHalone);
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
                        return PLD.Prominence;
                }

                return PLD.TotalEclipse;
            }

            return actionID;
        }
    }

    internal class PaladinConfiteorFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinConfiteorFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.HolySpirit || actionID == PLD.HolyCircle)
            {
                if (OriginalHook(PLD.Confiteor) != PLD.Confiteor)
                    return OriginalHook(PLD.Confiteor);

                Status? requiescat = FindEffect(PLD.Buffs.Requiescat);

                if (requiescat != null)
                {
                    if (requiescat.StackCount <= 1 && level >= PLD.Levels.Confiteor)
                    {
                        return OriginalHook(PLD.Confiteor);
                    }
                }

                return actionID;
            }

            return actionID;
        }
    }

    internal class PaladinRequiescatCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRequiescatCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.Requiescat)
            {
                if ((HasEffect(PLD.Buffs.Requiescat) && level >= PLD.Levels.Confiteor) || OriginalHook(PLD.Confiteor) != PLD.Confiteor)
                    return OriginalHook(PLD.Confiteor);

                return PLD.Requiescat;
            }

            return actionID;
        }
    }

    internal class PaladinHolySpiritToHolyCircleFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinHolySpiritToHolyCircleFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.HolySpirit)
            {
                if ((this.FilteredLastComboMove == PLD.Prominence || this.FilteredLastComboMove == PLD.TotalEclipse) && level >= PLD.Levels.HolyCircle)
                    return PLD.HolyCircle;
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
