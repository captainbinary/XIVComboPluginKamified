using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SAM
    {
        public const byte JobID = 34;

        public const uint
            Hakaze = 7477,
            Yukikaze = 7480,
            Gekko = 7481,
            Jinpu = 7478,
            Kasha = 7482,
            Shifu = 7479,
            Mangetsu = 7484,
            Fuga = 7483,
            Oka = 7485,
            Shinten = 7490,
            Kyuten = 7491,
            Guren = 7496,
            Senei = 16481,
            MeikyoShisui = 7499,
            Seigan = 7501,
            Iaijutsu = 7867,
            TsubameGaeshi = 16483,
            Shoha = 16487,
            Ikishoten = 16482,
            Fuko = 25780,
            OgiNamikiri = 25781,
            KaeshiNamikiri = 25782,
            Enpi = 7486,
            Meditate = 7497;

        public static class Buffs
        {
            public const ushort
                MeikyoShisui = 1233,
                EyesOpen = 1252,
                Jinpu = 1298,
                Shifu = 1299,
                OgiNamikiriReady = 2959;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Jinpu = 4,
                Shifu = 18,
                Gekko = 30,
                Mangetsu = 35,
                Kasha = 40,
                Oka = 45,
                Yukikaze = 50,
                Kyuten = 62,
                Guren = 70,
                Senei = 72,
                TsubameGaeshi = 74,
                Shoha = 80,
                Hyosetsu = 86,
                OgiNamikiri = 90;
        }
    }

    internal class SamuraiYukikazeMeditateFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiYukikazeMeditateFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return CurrentTarget is null && CanUseAction(SAM.Meditate) ? SAM.Meditate : actionID;
        }
    }

    internal class SamuraiShintenToKyutenFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiShintenToKyutenFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Shinten && level >= SAM.Levels.Kyuten && (this.FilteredLastComboMove == SAM.Fuga || this.FilteredLastComboMove == SAM.Fuko || this.FilteredLastComboMove == SAM.Oka || this.FilteredLastComboMove == SAM.Mangetsu))
            {
                if (IsEnabled(CustomComboPreset.SamuraiGurenFeature) && IsActionOffCooldown(SAM.Guren) && CanUseAction(SAM.Guren) && CurrentTarget is not null)
                    return SAM.Guren;
                if (IsEnabled(CustomComboPreset.SamuraiShohaFeature) && CanUseAction(SAM.Shoha))
                    return SAM.Shoha;
                return SAM.Kyuten;
            }

            return actionID;
        }
    }

    internal class SamuraiYukikazeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiYukikazeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Yukikaze)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Yukikaze;

                if (comboTime > 0 && lastComboMove == OriginalHook(SAM.Hakaze) && level >= SAM.Levels.Yukikaze)
                    return SAM.Yukikaze;

                return OriginalHook(SAM.Hakaze);
            }

            return actionID;
        }
    }

    internal class SamuraiGekkoCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiGekkoCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Gekko)
            {
                if (IsEnabled(CustomComboPreset.SamuraiGekkoEnpiFeature))
                {
                    if (CanUseAction(SAM.Enpi) && !InMeleeRange())
                        return SAM.Enpi;
                }

                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Gekko;

                if (comboTime > 0)
                {
                    if (lastComboMove == OriginalHook(SAM.Hakaze) && level >= SAM.Levels.Jinpu)
                        return SAM.Jinpu;

                    if (lastComboMove == SAM.Jinpu && level >= SAM.Levels.Gekko)
                        return SAM.Gekko;
                }

                if (IsEnabled(CustomComboPreset.SamuraiGekkoOption))
                    return SAM.Jinpu;

                return OriginalHook(SAM.Hakaze);
            }

            return actionID;
        }
    }

    internal class SamuraiKashaCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiKashaCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Kasha)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Kasha;

                if (comboTime > 0)
                {
                    if (lastComboMove == OriginalHook(SAM.Hakaze) && level >= SAM.Levels.Shifu)
                        return SAM.Shifu;

                    if (lastComboMove == SAM.Shifu && level >= SAM.Levels.Kasha)
                        return SAM.Kasha;
                }

                if (IsEnabled(CustomComboPreset.SamuraiKashaOption))
                    return SAM.Shifu;

                return OriginalHook(SAM.Hakaze);
            }

            return actionID;
        }
    }

    internal class SamuraiMangetsuCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiMangetsuCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if ((!IsEnabled(CustomComboPreset.SamuraiEvilMangetsuCombo) && actionID == SAM.Mangetsu) || (IsEnabled(CustomComboPreset.SamuraiEvilMangetsuCombo) && (actionID == SAM.Fuga || actionID == SAM.Fuko)))
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Mangetsu;

                if (comboTime > 0 && (lastComboMove == SAM.Fuga || lastComboMove == SAM.Fuko) && level >= SAM.Levels.Mangetsu)
                    return SAM.Mangetsu;

                return OriginalHook(SAM.Fuga);
            }

            return actionID;
        }
    }

    internal class SamuraiOkaCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiOkaCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Oka)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Oka;

                if (comboTime > 0 && (lastComboMove == SAM.Fuga || lastComboMove == SAM.Fuko) && level >= SAM.Levels.Oka)
                    return SAM.Oka;

                return OriginalHook(SAM.Fuga);
            }

            return actionID;
        }
    }

    internal class SamuraiTsubameGaeshiShohaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiShohaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.TsubameGaeshi)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if ((gauge.Kaeshi != Kaeshi.NONE && gauge.Kaeshi != Kaeshi.NAMIKIRI) && IsEnabled(CustomComboPreset.SamuraiShohaBetweenOption))
                    return actionID;
                if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3 && (GCDClipCheck() || OriginalHook(SAM.TsubameGaeshi) == SAM.TsubameGaeshi || IsEnabled(CustomComboPreset.SamuraiShohaGCDOption)))
                {
                    return SAM.Shoha;
                }
            }

            return actionID;
        }
    }

    internal class SamuraiIaijutsuShohaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiIaijutsuShohaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Iaijutsu)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if ((gauge.Kaeshi != Kaeshi.NONE && gauge.Kaeshi != Kaeshi.NAMIKIRI) && IsEnabled(CustomComboPreset.SamuraiShohaBetweenOption))
                    return actionID;
                if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3 && (GCDClipCheck() || OriginalHook(SAM.TsubameGaeshi) == SAM.TsubameGaeshi || IsEnabled(CustomComboPreset.SamuraiShohaGCDOption)))
                {
                    return SAM.Shoha;
                }
            }

            return actionID;
        }
    }

    internal class SamuraiNamikiriShohaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiNamikiriShohaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.OgiNamikiri)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (gauge.Kaeshi != Kaeshi.NONE && IsEnabled(CustomComboPreset.SamuraiShohaBetweenOption))
                    return actionID;
                if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3 && (GCDClipCheck() || OriginalHook(SAM.OgiNamikiri) == SAM.OgiNamikiri || IsEnabled(CustomComboPreset.SamuraiShohaGCDOption)))
                {
                    return SAM.Shoha;
                }
            }

            return actionID;
        }
    }

    internal class SamuraiTsubameGaeshiIaijutsuFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiIaijutsuFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.TsubameGaeshi)
            {
                var gauge = GetJobGauge<SAMGauge>();

                return (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE) ? OriginalHook(SAM.TsubameGaeshi) : OriginalHook(SAM.Iaijutsu);
            }

            return actionID;
        }
    }

    internal class SamuraiIaijutsuTsubameGaeshiFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiIaijutsuTsubameGaeshiFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Iaijutsu)
            {
                var gauge = GetJobGauge<SAMGauge>();

                return (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE) ? OriginalHook(SAM.TsubameGaeshi) : OriginalHook(SAM.Iaijutsu);
            }

            return actionID;
        }
    }

    internal class SamuraiSeneiFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiSeneiFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Shinten)
            {
                if (IsActionOffCooldown(SAM.Senei) && level >= SAM.Levels.Senei)
                    return SAM.Senei;
                if (level < SAM.Levels.Senei && IsActionOffCooldown(SAM.Guren) && CanUseAction(SAM.Guren) && IsEnabled(CustomComboPreset.SamuraiSeneiGurenFeature))
                    return SAM.Guren;
            }

            return actionID;
        }
    }

    internal class SamuraiSeneiGurenFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiSeneiGurenFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == SAM.Senei && level < SAM.Levels.Senei ? SAM.Guren : actionID;
        }
    }

    internal class SamuraiShohaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiShohaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Shinten)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (gauge.MeditationStacks >= 3)
                    return SAM.Shoha;
            }

            return actionID;
        }
    }

    internal class SamuraiGurenFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiGurenFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Kyuten)
            {
                if (IsActionOffCooldown(SAM.Guren) && CanUseAction(SAM.Guren) && CurrentTarget is not null)
                    return SAM.Guren;
            }

            return actionID;
        }
    }

    internal class SamuraiIkishotenNamikiriFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiIkishotenNamikiriFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Ikishoten)
            {
                if (OriginalHook(SAM.Ikishoten) != SAM.Ikishoten)
                    return OriginalHook(SAM.Ikishoten);

                if (level >= SAM.Levels.OgiNamikiri)
                {
                    var gauge = GetJobGauge<SAMGauge>();
                    if (HasEffect(SAM.Buffs.OgiNamikiriReady))
                    {
                        if (gauge.MeditationStacks >= 3 && IsEnabled(CustomComboPreset.SamuraiNamikiriShohaFeature))
                        {
                            return SAM.Shoha;
                        }

                        return SAM.OgiNamikiri;
                    }

                    if (OriginalHook(SAM.OgiNamikiri) == SAM.KaeshiNamikiri)
                    {
                        if (gauge.MeditationStacks >= 3 && IsEnabled(CustomComboPreset.SamuraiNamikiriShohaFeature) && (GCDClipCheck() || IsEnabled(CustomComboPreset.SamuraiShohaGCDOption)) && !IsEnabled(CustomComboPreset.SamuraiShohaBetweenOption))
                        {
                            return SAM.Shoha;
                        }

                        return SAM.KaeshiNamikiri;
                    }
                }

                return SAM.Ikishoten;
            }

            return actionID;
        }
    }
}
