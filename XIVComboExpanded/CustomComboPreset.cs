using XIVComboKamifiedPlugin.Attributes;
using XIVComboKamifiedPlugin.Combos;

namespace XIVComboKamifiedPlugin
{
    /// <summary>
    /// Combo presets.
    /// </summary>
    public enum CustomComboPreset
    {
        // ====================================================================================
        #region ALL CLASSES

        [OrderedEnum]
        [CustomComboInfo("Raise to Swiftcast Feature", "Replaces the respective raise on RDM/SMN/SCH/WHM/AST/SGE with Swiftcast when it is off cooldown (and Dualcast isn't up).", All.JobID, All.Raise, All.Resurrection, All.Ascend, All.Verraise, All.Egeiro)]
        AllSwiftcastFeature = 9001,

        #endregion

        #region ASTROLOGIAN

        [OrderedEnum]
        [CustomComboInfo("Draw on Play", "Play turns into Draw when no card is drawn, as well as the usual Play behavior.", AST.JobID, AST.Play)]
        AstrologianCardsOnDrawFeature = 3301,

        [OrderedEnum]
        [CustomComboInfo("Minor Arcana to Crown Play", "Changes Minor Arcana to Crown Play when a card is not drawn.", AST.JobID, AST.MinorArcana)]
        AstrologianMinorArcanaPlayFeature = 3302,

        [OrderedEnum]
        [CustomComboInfo("Benefic 2 to Benefic Level Sync", "Changes Benefic 2 to Benefic when below level 26 in synced content.", AST.JobID, AST.Benefic2)]
        AstrologianBeneficFeature = 3303,

        #endregion
        // ====================================================================================
        #region BLACK MAGE

        [OrderedEnum]
        [CustomComboInfo("Blizzard 4/Fire 4 Switcher", "Change Fire 4 or Blizzard 4 to whichever action you can currently use.", BLM.JobID, BLM.Blizzard4, BLM.Fire4)]
        BlackEnochianFeature = 2501,

        [OrderedEnum]
        [CustomComboInfo("Freeze/Flare Switcher", "Change Freeze or Flare to whichever action you can currently use.", BLM.JobID, BLM.Freeze, BLM.Flare)]
        BlackFreezeFlareFeature = 2502,

        [OrderedEnum]
        [CustomComboInfo("Umbral Soul/Transpose Switcher", "Change Transpose into Umbral Soul when Umbral Soul is usable.", BLM.JobID, BLM.Transpose)]
        BlackManaFeature = 2503,

        [OrderedEnum]
        [CustomComboInfo("(Between the) Ley Lines", "Change Ley Lines into BTL when Ley Lines is active.", BLM.JobID, BLM.LeyLines)]
        BlackLeyLinesFeature = 2504,

        [OrderedEnum]
        [CustomComboInfo("Fire 1/3 Feature", "Fire 1 becomes Fire 3 outside of Astral Fire, and when Firestarter proc is up.", BLM.JobID, BLM.Fire)]
        BlackFireFeature = 2505,

        [OrderedEnum]
        [CustomComboInfo("Blizzard 1/3 Feature", "Blizzard 1 becomes Blizzard 3 when out of Umbral Ice.", BLM.JobID, BLM.Blizzard)]
        BlackBlizzardFeature = 2506,

        [OrderedEnum]
        [CustomComboInfo("Scathe/Xenoglossy Feature", "Scathe becomes Xenoglossy when available.", BLM.JobID, BLM.Scathe)]
        BlackScatheFeature = 2507,

        #endregion
        // ====================================================================================
        #region BARD

        [OrderedEnum]
        [CustomComboInfo("Wanderer's into Pitch Perfect", "Replaces Wanderer's Minuet with Pitch Perfect while in WM.", BRD.JobID, BRD.WanderersMinuet)]
        BardWanderersPitchPerfectFeature = 2301,

        [OrderedEnum]
        [CustomComboInfo("Heavy Shot into Straight Shot", "Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced.", BRD.JobID, BRD.HeavyShot, BRD.BurstShot)]
        BardStraightShotUpgradeFeature = 2302,

        [OrderedEnum]
        [CustomComboInfo("Iron Jaws Feature", "Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up.\nAlternates between the two if Iron Jaws isn't available.", BRD.JobID, BRD.IronJaws)]
        BardIronJawsFeature = 2303,

        [OrderedEnum]
        [CustomComboInfo("Burst Shot/Quick Nock/Ladonsbite into Apex Arrow", "Replaces Burst Shot and Quick Nock with Apex Arrow when gauge is full.", BRD.JobID, BRD.BurstShot, BRD.QuickNock, BRD.Ladonsbite)]
        BardApexFeature = 2304,

        [OrderedEnum]
        [CustomComboInfo("Quick Nock/Ladonsbite into Shadowbite", "Replaces Quick Nock/Ladonsbite with Shadowbite when it is ready.", BRD.JobID, BRD.QuickNock, BRD.Ladonsbite)]
        BardShadowbiteFeature = 2305,

        #endregion
        // ====================================================================================
        #region DANCER

        [OrderedEnum]
        [CustomComboInfo("Fan Dance Combos", "Change Fan Dance and Fan Dance 2 into Fan Dance 3 while flourishing.", DNC.JobID, DNC.FanDance1, DNC.FanDance2)]
        DancerFanDanceCombo = 3801,

        [OrderedEnum]
        [SecretCustomCombo]
        [ConflictingCombos(DancerDanceComboCompatibility)]
        [CustomComboInfo("Dance Step Combo", "Change Standard Step and Technical Step into each dance step while dancing.", DNC.JobID, DNC.StandardStep, DNC.TechnicalStep)]
        DancerDanceStepCombo = 3802,

        [OrderedEnum]
        [CustomComboInfo("Flourish Proc Saver", "Change Flourish into any available procs before using.", DNC.JobID, DNC.Flourish)]
        DancerFlourishFeature = 3803,

        [OrderedEnum]
        [CustomComboInfo("Single Target Multibutton", "Change Cascade into procs and combos as available.", DNC.JobID, DNC.Cascade)]
        DancerSingleTargetMultibutton = 3804,

        [OrderedEnum]
        [CustomComboInfo("AoE Multibutton", "Change Windmill into procs and combos as available.", DNC.JobID, DNC.Windmill)]
        DancerAoeMultibutton = 3805,

        [OrderedEnum]
        [CustomComboInfo("Devilment Feature", "Change Devilment into Starfall Dance after use.", DNC.JobID, DNC.Devilment)]
        DancerDevilmentFeature = 3806,

        [OrderedEnum]
        [ConflictingCombos(DancerDanceStepCombo)]
        [CustomComboInfo(
            "Dance Step Feature",
            "Change actions into dance steps while dancing." +
            "\nThis helps ensure you can still dance with combos on, without using auto dance." +
            "\nYou can change the respective actions by inputting action IDs below for each dance step." +
            "\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions." +
            "\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog.",
            DNC.JobID)]
        DancerDanceComboCompatibility = 3807,

        #endregion
        // ====================================================================================
        #region DRAGOON

        [OrderedEnum]
        [CustomComboInfo("Jump + Mirage Dive", "Replace (High) Jump with Mirage Dive when Dive Ready.", DRG.JobID, DRG.Jump, DRG.HighJump)]
        DragoonJumpFeature = 2201,

        [OrderedEnum]
        [CustomComboInfo("Coerthan Torment Combo", "Replace Coerthan Torment with its combo chain.", DRG.JobID, DRG.CoerthanTorment)]
        DragoonCoerthanTormentCombo = 2202,

        [OrderedEnum]
        [CustomComboInfo("Chaos Thrust Combo", "Replace Chaos Thrust with its combo chain.", DRG.JobID, DRG.ChaosThrust)]
        DragoonChaosThrustCombo = 2203,

        [OrderedEnum]
        [CustomComboInfo("Full Thrust Combo", "Replace Full Thrust with its combo chain.", DRG.JobID, DRG.FullThrust)]
        DragoonFullThrustCombo = 2204,

        #endregion
        // ====================================================================================
        #region DARK KNIGHT

        [OrderedEnum]
        [CustomComboInfo("Souleater Combo", "Replace Souleater with its combo chain.", DRK.JobID, DRK.Souleater)]
        DarkSouleaterCombo = 3201,

        [OrderedEnum]
        [CustomComboInfo("Stalwart Soul Combo", "Replace Stalwart Soul with its combo chain.", DRK.JobID, DRK.StalwartSoul)]
        DarkStalwartSoulCombo = 3202,

        [OrderedEnum]
        [CustomComboInfo("Dark Knight Gauge Overcap Feature", "Replace AoE combo with gauge spender if you are about to overcap.", DRK.JobID, DRK.StalwartSoul)]
        DRKOvercapFeature = 3203,

        [OrderedEnum]
        [CustomComboInfo("Delirium Feature", "Replace Souleater and Stalwart Soul with Bloodspiller and Quietus when Delirium is active.", DRK.JobID, DRK.Souleater, DRK.StalwartSoul)]
        DarkDeliriumFeature = 3204,

        #endregion
        // ====================================================================================
        #region GUNBREAKER

        [OrderedEnum]
        [CustomComboInfo("Solid Barrel Combo", "Replace Solid Barrel with its combo chain.", GNB.JobID, GNB.SolidBarrel)]
        GunbreakerSolidBarrelCombo = 3701,

        [OrderedEnum]
        [CustomComboInfo("Gnashing Fang Continuation", "Replace Gnashing Fang with Continuation when appropriate.", GNB.JobID, GNB.GnashingFang)]
        GunbreakerGnashingFangContinuation = 3702,

        [OrderedEnum]
        [CustomComboInfo("Burst Strike Continuation", "Replace Burst Strike with Continuation when appropriate.", GNB.JobID, GNB.BurstStrike)]
        GunbreakerBurstStrikeCont = 3703,

        [OrderedEnum]
        [SecretCustomCombo]
        [CustomComboInfo("Bow Shock / Sonic Break Feature", "Replace Bow Shock and Sonic Break with one or the other depending on which is on cooldown.", GNB.JobID, GNB.BowShock, GNB.SonicBreak)]
        GunbreakerBowShockSonicBreakFeature = 3704,

        [OrderedEnum]
        [CustomComboInfo("Demon Slaughter Combo", "Replace Demon Slaughter with its combo chain.", GNB.JobID, GNB.DemonSlaughter)]
        GunbreakerDemonSlaughterCombo = 3705,

        [OrderedEnum]
        [CustomComboInfo("Fated Circle Feature", "In addition to the Demon Slaughter combo, add Fated Circle when charges are full.", GNB.JobID, GNB.DemonSlaughter)]
        GunbreakerFatedCircleFeature = 3706,

        [OrderedEnum]
        [CustomComboInfo("Burst Strike/Fated Circle to Bloodfest Feature", "Replace Burst Strike and Fated Circle with Bloodfest if you have no powder gauge.", GNB.JobID, GNB.BurstStrike, GNB.FatedCircle)]
        GunbreakerBloodfestOvercapFeature = 3707,

        [OrderedEnum]
        [CustomComboInfo("No Mercy Feature", "Replace No Mercy with Bow Shock, and then Sonic Break, while No Mercy is active.", GNB.JobID, GNB.NoMercy)]
        GunbreakerNoMercyFeature = 3708,

        #endregion
        // ====================================================================================
        #region MACHINIST

        [OrderedEnum]
        [CustomComboInfo("(Heated) Shot Combo", "Replace either form of Clean Shot with its combo chain.", MCH.JobID, MCH.CleanShot, MCH.HeatedCleanShot)]
        MachinistMainCombo = 3101,

        [OrderedEnum]
        [CustomComboInfo("Spread Shot Heat", "Replace Spread Shot with Auto Crossbow when overheated.", MCH.JobID, MCH.SpreadShot)]
        MachinistSpreadShotFeature = 3102,

        [OrderedEnum]
        [CustomComboInfo("Hypercharge Feature", "Replace Heat Blast and Auto Crossbow with Hypercharge when not overheated.", MCH.JobID, MCH.HeatBlast, MCH.AutoCrossbow)]
        MachinistOverheatFeature = 3103,

        [OrderedEnum]
        [CustomComboInfo("Overdrive Feature", "Replace Rook Autoturret and Automaton Queen with Overdrive while active.", MCH.JobID, MCH.RookAutoturret, MCH.AutomatonQueen)]
        MachinistOverdriveFeature = 3104,

        [OrderedEnum]
        [SecretCustomCombo]
        [CustomComboInfo("Gauss Round / Ricochet Feature", "Replace Gauss Round and Ricochet with one or the other depending on which has more charges.", MCH.JobID, MCH.GaussRound, MCH.Ricochet)]
        MachinistGaussRoundRicochetFeature = 3105,

        [OrderedEnum]
        [SecretCustomCombo]
        [CustomComboInfo("Drill / Air Anchor (Hot Shot) Feature", "Replace Drill and Air Anchor (Hot Shot) with one or the other (or Chainsaw) depending on which is on cooldown.", MCH.JobID, MCH.Drill, MCH.HotShot, MCH.AirAnchor)]
        MachinistHotShotDrillChainsawFeature = 3106,

        #endregion
        // ====================================================================================
        #region MONK

        [OrderedEnum]
        [CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker with the AoE combo chain, or Rockbreaker when Perfect Balance is active.", MNK.JobID, MNK.Rockbreaker)]
        MonkAoECombo = 2001,

        [OrderedEnum]
        [CustomComboInfo("Monk Bootshine Feature", "Replaces Dragon Kick with Bootshine if both a form and Leaden Fist are up.", MNK.JobID, MNK.DragonKick)]
        MnkBootshineFeature = 2002,

        [OrderedEnum]
        [CustomComboInfo("Howling Fist / Meditation Feature", "Howling Fist with Meditation when the Fifth Chakra is not open.", MNK.JobID, MNK.HowlingFist)]
        MonkHowlingFistMeditationFeature = 2003,

        #endregion
        // ====================================================================================
        #region NINJA

        [OrderedEnum]
        [CustomComboInfo("Armor Crush Combo", "Replace Armor Crush with its combo chain.", NIN.JobID, NIN.ArmorCrush)]
        NinjaArmorCrushCombo = 3001,

        [OrderedEnum]
        [CustomComboInfo("Aeolian Edge Combo", "Replace Aeolian Edge with its combo chain.", NIN.JobID, NIN.AeolianEdge)]
        NinjaAeolianEdgeCombo = 3002,

        [OrderedEnum]
        [CustomComboInfo("Hakke Mujinsatsu Combo", "Replace Hakke Mujinsatsu with its combo chain.", NIN.JobID, NIN.HakkeMujinsatsu)]
        NinjaHakkeMujinsatsuCombo = 3003,

        [OrderedEnum]
        [CustomComboInfo("Kassatsu to Trick", "Replaces Kassatsu with Trick Attack while Suiton or Hidden is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.Kassatsu)]
        NinjaKassatsuTrickFeature = 3004,

        [OrderedEnum]
        [CustomComboInfo("Ten Chi Jin to Meisui", "Replaces Ten Chi Jin (the move) with Meisui while Suiton is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.TenChiJin)]
        NinjaTCJMeisuiFeature = 3005,

        [OrderedEnum]
        [CustomComboInfo("Kassatsu Chi/Jin Feature", "Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu.", NIN.JobID, NIN.Chi)]
        NinjaKassatsuChiJinFeature = 3006,

        [OrderedEnum]
        [CustomComboInfo("Hide to Mug", "Replaces Hide with Mug while in combat.", NIN.JobID, NIN.Hide)]
        NinjaHideMugFeature = 3007,

        [OrderedEnum]
        [ConflictingCombos(NinjaGCDNinjutsuFeature)]
        [CustomComboInfo("Aeolian to Ninjutsu Feature", "Replaces Aeolian Edge (combo) with Ninjutsu if any Mudra are used.", NIN.JobID, NIN.AeolianEdge)]
        NinjaNinjutsuFeature = 3008,

        [OrderedEnum]
        [ConflictingCombos(NinjaNinjutsuFeature)]
        [CustomComboInfo("GCDs to Ninjutsu Feature", "Every GCD combo becomes Ninjutsu while Mudras are being used.", NIN.JobID, NIN.AeolianEdge, NIN.ArmorCrush, NIN.HakkeMujinsatsu)]
        NinjaGCDNinjutsuFeature = 3009,

        [OrderedEnum]
        [CustomComboInfo("Bunshin / Kamaitachi Feature", "Replaces Bunshin with Phantom Kamaitachi after usage.", NIN.JobID, NIN.Bunshin)]
        NinjaBunshinKamaitachiFeature = 3010,

        [OrderedEnum]
        [ParentCombo(NinjaArmorCrushCombo)]
        [CustomComboInfo("Armor Crush / Raiju Feature", "Replaces the Armor Crush combo with Forked and Fleeting Raiju when available.", NIN.JobID, NIN.ArmorCrush)]
        NinjaArmorCrushRaijuFeature = 3012,

        [OrderedEnum]
        [ParentCombo(NinjaAeolianEdgeCombo)]
        [CustomComboInfo("Aeolian Edge / Raiju Feature", "Replaces the Aeolian Edge combo with Forked and Fleeting Raiju when available.", NIN.JobID, NIN.AeolianEdge)]
        NinjaAeolianEdgeRaijuFeature = 3013,

        [OrderedEnum]
        [CustomComboInfo("Huraijin / Raiju Feature", "Replaces Huraijin with Forked and Fleeting Raiju when available.", NIN.JobID, NIN.Huraijin)]
        NinjaHuraijinRaijuFeature = 3011,

        #endregion
        // ====================================================================================
        #region PALADIN

        [OrderedEnum]
        [CustomComboInfo("Goring Blade Combo", "Replace Goring Blade with its combo chain.", PLD.JobID, PLD.GoringBlade)]
        PaladinGoringBladeCombo = 1901,

        [OrderedEnum]
        [CustomComboInfo("Royal Authority Combo", "Replace Royal Authority/Rage of Halone with its combo chain.", PLD.JobID, PLD.RoyalAuthority, PLD.RageOfHalone)]
        PaladinRoyalAuthorityCombo = 1902,

        [OrderedEnum]
        [CustomComboInfo("Atonement Feature", "Replace Royal Authority with Atonement when under the effect of Sword Oath.", PLD.JobID, PLD.RoyalAuthority)]
        PaladinAtonementFeature = 1903,

        [OrderedEnum]
        [CustomComboInfo("Prominence Combo", "Replace Prominence with its combo chain.", PLD.JobID, PLD.Prominence)]
        PaladinProminenceCombo = 1904,

        [OrderedEnum]
        [CustomComboInfo("Requiescat Confiteor", "Replace Requiescat with Confiter while under the effect of Requiescat. Includes Confiteor combo.", PLD.JobID, PLD.Requiescat)]
        PaladinRequiescatCombo = 1905,

        [OrderedEnum]
        [SecretCustomCombo]
        [CustomComboInfo("Confiteor Feature", "Replace Holy Spirit/Circle with Confiteor when Requiescat has one stack left. Includes Confiteor combo.", PLD.JobID, PLD.HolySpirit, PLD.HolyCircle)]
        PaladinConfiteorFeature = 1906,

        #endregion
        // ====================================================================================
        #region REAPER

        [OrderedEnum]
        [CustomComboInfo("Slice Combo", "Replace Slice with its combo chain.", RPR.JobID, RPR.Slice)]
        ReaperSliceCombo = 3901,

        [OrderedEnum]
        [CustomComboInfo("Scythe Combo", "Replace Spinning Scythe with its combo chain.", RPR.JobID, RPR.SpinningScythe)]
        ReaperScytheCombo = 3902,

        [OrderedEnum]
        [CustomComboInfo("Enshroud Communio Feature", "Replace Enshroud with Communio when Enshrouded.", RPR.JobID, RPR.Enshroud)]
        ReaperEnshroudCommunioFeature = 3903,

        [OrderedEnum]
        [ParentCombo(ReaperSliceCombo)]
        [CustomComboInfo("Gibbets and Gallows Feature", "Slice and Shadow of Death are replaced with Gibbet and Gallows while Soul Reaver or Shroud is active.", RPR.JobID, RPR.Slice, RPR.ShadowOfDeath)]
        ReaperGibbetGallowsFeature = 3904,

        [OrderedEnum]
        [ParentCombo(ReaperScytheCombo)]
        [CustomComboInfo("Guillotine Feature", "Spinning Scythe's combo gets replaced with Guillotine while Soul Reaver or Shroud is active.", RPR.JobID, RPR.SpinningScythe)]
        ReaperGuillotineFeature = 3909,

        [OrderedEnum]
        [ParentCombo(ReaperGibbetGallowsFeature)]
        [CustomComboInfo("GG Gallows Option", "Slice now turns into Gallows when Gallows is Enhanced, and removes it from Shadow of Death.", RPR.JobID, RPR.Slice)]
        ReaperGibbetGallowsOption = 3905,

        /*[OrderedEnum]
        [SecretCustomCombo]
        [CustomComboInfo("Communio Feature", "When one stack is left of Shroud, Communio replaces the Slice and Spinning Scythe combo.\nRequires their respective combos and features.", RPR.JobID, RPR.Slice)]
        ReaperGibbetGallowsCommunioFeature = 3906,*/

        [OrderedEnum]
        [CustomComboInfo("Arcane Circle Harvest Feature", "Replace Arcane Circle with Plentiful Harvest when you have stacks of Immortal Sacrifice.", RPR.JobID, RPR.ArcaneCircle)]
        ReaperHarvestFeature = 3907,

        [OrderedEnum]
        [CustomComboInfo("Regress Feature", "Both Hell's Ingress and Hell's Egress turn into Regress when Threshold is active, instead of just the opposite of the one you used.", RPR.JobID, RPR.HellsIngress, RPR.HellsEgress)]
        ReaperRegressFeature = 3908,

        #endregion
        // ====================================================================================
        #region RED MAGE

        [OrderedEnum]
        [CustomComboInfo("Red Mage AoE Combo", "Replaces Veraero/Verthunder 2 with Impact when Dualcast or Swiftcast are active.", RDM.JobID, RDM.Veraero2, RDM.Verthunder2)]
        RedMageAoECombo = 3501,

        [OrderedEnum]
        [CustomComboInfo("Redoublement Combo", "Replaces Redoublement with its combo chain, following enchantment rules.", RDM.JobID, RDM.Redoublement, RDM.Moulinet)]
        RedMageMeleeCombo = 3502,

        [OrderedEnum]
        [SecretCustomCombo]
        [ParentCombo(RedMageMeleeCombo)]
        [CustomComboInfo("Redoublement Combo Plus", "Replaces Redoublement/Moulinet with Verflare/Verholy after 3 stacks, whichever is more appropriate.", RDM.JobID, RDM.Redoublement, RDM.Moulinet)]
        RedMageMeleeComboPlus = 3503,

        [OrderedEnum]
        [CustomComboInfo("Verproc into Jolt", "Replaces Verstone/Verfire with Jolt/Scorch when no proc is available.", RDM.JobID, RDM.Verstone, RDM.Verfire)]
        RedMageVerprocCombo = 3504,

        [OrderedEnum]
        [ParentCombo(RedMageVerprocCombo)]
        [CustomComboInfo("Verproc into Jolt Plus", "Additionally replaces Verstone/Verfire with Veraero/Verthunder if Dualcast, Swiftcast, or Lost Chainspell are up.", RDM.JobID, RDM.Verstone, RDM.Verfire)]
        RedMageVerprocComboPlus = 3505,

        [OrderedEnum]
        [ParentCombo(RedMageVerprocComboPlus)]
        [CustomComboInfo("Verproc into Jolt Plus Opener Feature (Stone)", "Turns Verstone into Veraero when out of combat.", RDM.JobID, RDM.Verstone)]
        RedMageVerprocOpenerFeatureStone = 3506,

        [OrderedEnum]
        [ParentCombo(RedMageVerprocComboPlus)]
        [CustomComboInfo("Verproc into Jolt Plus Opener Feature (Fire)", "Turns Verfire into Verthunder when out of combat.", RDM.JobID, RDM.Verfire)]
        RedMageVerprocOpenerFeatureFire = 3507,

        #endregion
        // ====================================================================================
        #region SAGE

        [OrderedEnum]
        [CustomComboInfo("Kardia Into Soteria", "Kardia turns Soteria when active and Soteria is off-cooldown.", SGE.JobID, SGE.Kardia)]
        SageKardiaFeature = 4001,

        #endregion
        // ====================================================================================
        #region SAMURAI

        [OrderedEnum]
        [CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain.", SAM.JobID, SAM.Yukikaze)]
        SamuraiYukikazeCombo = 3401,

        [OrderedEnum]
        [CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain.", SAM.JobID, SAM.Gekko)]
        SamuraiGekkoCombo = 3402,

        [OrderedEnum]
        [CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain.", SAM.JobID, SAM.Kasha)]
        SamuraiKashaCombo = 3403,

        [OrderedEnum]
        [CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain.", SAM.JobID, SAM.Mangetsu)]
        SamuraiMangetsuCombo = 3404,

        [OrderedEnum]
        [CustomComboInfo("Oka Combo", "Replace Oka with its combo chain.", SAM.JobID, SAM.Oka)]
        SamuraiOkaCombo = 3405,

        [OrderedEnum]
        [ConflictingCombos(SamuraiIaijutsuTsubameGaeshiFeature)]
        [CustomComboInfo("Tsubame-gaeshi to Iaijutsu", "Replace Tsubame-gaeshi with Iaijutsu when Sen is empty.", SAM.JobID, SAM.TsubameGaeshi)]
        SamuraiTsubameGaeshiIaijutsuFeature = 3406,

        [OrderedEnum]
        [ConflictingCombos(SamuraiIaijutsuShohaFeature)]
        [CustomComboInfo("Tsubame-gaeshi to Shoha", "Replace Tsubame-gaeshi with Shoha when meditation is 3 and an Iaijutsu wasn't just used.", SAM.JobID, SAM.TsubameGaeshi)]
        SamuraiTsubameGaeshiShohaFeature = 3407,

        [OrderedEnum]
        [ConflictingCombos(SamuraiTsubameGaeshiIaijutsuFeature)]
        [CustomComboInfo("Iaijutsu to Tsubame-gaeshi", "Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty.", SAM.JobID, SAM.Iaijutsu)]
        SamuraiIaijutsuTsubameGaeshiFeature = 3408,

        [OrderedEnum]
        [ConflictingCombos(SamuraiTsubameGaeshiShohaFeature)]
        [CustomComboInfo("Iaijutsu to Shoha", "Replace Iaijutsu with Shoha when meditation is 3 and an Iaijutsu wasn't just used.", SAM.JobID, SAM.Iaijutsu)]
        SamuraiIaijutsuShohaFeature = 3409,

        [OrderedEnum]
        [CustomComboInfo("Shinten to Shoha", "Replace Hissatsu: Shinten with Shoha when Meditation is full.", SAM.JobID, SAM.Shinten)]
        SamuraiShohaFeature = 3412,

        [OrderedEnum]
        [CustomComboInfo("Kyuten to Shoha II", "Replace Hissatsu: Kyuten with Shoha II when Meditation is full.", SAM.JobID, SAM.Kyuten)]
        SamuraiShoha2Feature = 3410,

        [OrderedEnum]
        [CustomComboInfo("Ikishoten Namikiri Feature", "Replace Ikishoten with Ogi Namikiri and then Kaeshi Namikiri when available.", SAM.JobID, SAM.Ikishoten)]
        SamuraiIkishotenNamikiriFeature = 3411,

        #endregion
        // ====================================================================================
        #region SCHOLAR

        [OrderedEnum]
        [CustomComboInfo("Seraph Fey Blessing/Consolation", "Change Fey Blessing into Consolation when Seraph is out.", SCH.JobID, SCH.FeyBless)]
        ScholarSeraphConsolationFeature = 2801,

        [OrderedEnum]
        [CustomComboInfo("ED Aetherflow", "Change Energy Drain into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID, SCH.EnergyDrain)]
        ScholarEnergyDrainFeature = 2802,

        #endregion
        // ====================================================================================
        #region SUMMONER

        [OrderedEnum]
        [CustomComboInfo("Enkindle/Summon Switch", "When Bahamut/Phoenix are summoned, Enkindle Bahamut/Phoenix will replace Summon Bahamut/Phoenix", SMN.JobID, SMN.SummonBahamut, SMN.SummonPhoenix, SMN.DreadwyrmTrance, SMN.Aethercharge)]
        SummonerDemiCombo = 2701,

        [OrderedEnum]
        [CustomComboInfo("Shiny Enkindle Feature", "When Bahamut/Phoenix are summoned, changes Gemshine and Precious Brilliance with Enkindle.", SMN.JobID, SMN.Gemshine, SMN.PreciousBrilliance)]
        SummonerShinyDemiCombo = 2708,

        [OrderedEnum]
        [CustomComboInfo("ED Fester", "Change Fester into Energy Drain when out of Aetherflow stacks", SMN.JobID, SMN.Fester)]
        SummonerEDFesterCombo = 2702,

        [OrderedEnum]
        [CustomComboInfo("ES Painflare", "Change Painflare into Energy Syphon when out of Aetherflow stacks", SMN.JobID, SMN.Painflare)]
        SummonerESPainflareCombo = 2703,

        [OrderedEnum]
        [CustomComboInfo("Shiny Ruin Feature", "Change Ruin into Gemburst when attuned.\nThis can and WILL mess up your movement options, you have been warned.", SMN.JobID, SMN.Ruin1, SMN.Ruin2, SMN.Ruin3)]
        SummonerShinyRuinFeature = 2709,

        [OrderedEnum]
        [CustomComboInfo("Further Ruin Feature", "Change Ruin into Ruin4 when available and appropriate.\nThis can and WILL mess up your movement options, you have been warned.", SMN.JobID, SMN.Ruin1, SMN.Ruin2, SMN.Ruin3)]
        SummonerFurtherRuinFeature = 2705,

        [OrderedEnum]
        [CustomComboInfo("Shiny Outburst Feature", "Change Outburst/Tri-disaster into Precious Brilliance when attuned.", SMN.JobID, SMN.Outburst, SMN.TriDisaster)]
        SummonerShinyOutburstFeature = 2706,

        [OrderedEnum]
        [CustomComboInfo("Further Outburst Feature", "Change Outburst/Tri-disaster into Ruin4 when available and appropriate.", SMN.JobID, SMN.Outburst, SMN.TriDisaster)]
        SummonerFurtherOutburstFeature = 2707,

        /*[OrderedEnum]
        [CustomComboInfo("Carby Feature", "Summon Carbuncle becomes Radiant Aegis while Carbuncle is summoned.", SMN.JobID, SMN.SummonCarbuncle)]
        SummonerCarbyFeature = 2704,*/

        #endregion
        // ====================================================================================
        #region WARRIOR

        [OrderedEnum]
        [CustomComboInfo("Storms Path Combo", "Replace Storms Path with its combo chain", WAR.JobID, WAR.StormsPath)]
        WarriorStormsPathCombo = 2101,

        [OrderedEnum]
        [CustomComboInfo("Storms Eye Combo", "Replace Storms Eye with its combo chain", WAR.JobID, WAR.StormsEye)]
        WarriorStormsEyeCombo = 2102,

        [OrderedEnum]
        [CustomComboInfo("Mythril Tempest Combo", "Replace Mythril Tempest with its combo chain", WAR.JobID, WAR.MythrilTempest)]
        WarriorMythrilTempestCombo = 2103,

        [OrderedEnum]
        [CustomComboInfo("Overpower Combo", "Replace Overpower with its combo chain (so that you can still use Mythril Tempest by itself in pulls)", WAR.JobID, WAR.Overpower)]
        WarriorOverpowerCombo = 2104,

        [OrderedEnum]
        [CustomComboInfo("Warrior Gauge Overcap Feature", "Replace Single-target or AoE combo with gauge spender if you are about to overcap and are before a step of a combo that would generate beast gauge", WAR.JobID, WAR.MythrilTempest, WAR.StormsEye, WAR.StormsPath)]
        WarriorGaugeOvercapFeature = 2105,

        [OrderedEnum]
        [CustomComboInfo("Inner Release Feature", "Replace Single-target and AoE combo with Fell Cleave/Decimate during Inner Release", WAR.JobID, WAR.MythrilTempest, WAR.StormsPath)]
        WarriorInnerReleaseFeature = 2106,

        [OrderedEnum]
        [CustomComboInfo("Nascent Flash Feature", "Replace Nascent Flash with Raw intuition when level synced below 76", WAR.JobID, WAR.NascentFlash)]
        WarriorNascentFlashFeature = 2107,

        [OrderedEnum]
        [CustomComboInfo("Primal Rend Feature", "Replace Inner Beast and Steel Cyclone with Primal Rend when available", WAR.JobID, WAR.InnerBeast, WAR.SteelCyclone)]
        WarriorPrimalRendFeature = 2108,

        #endregion
        // ====================================================================================
        #region WHITE MAGE

        [OrderedEnum]
        [CustomComboInfo("Solace into Misery", "Replaces Afflatus Solace with Afflatus Misery when Misery is ready to be used", WHM.JobID, WHM.AfflatusSolace)]
        WhiteMageSolaceMiseryFeature = 2401,

        [OrderedEnum]
        [CustomComboInfo("Rapture into Misery", "Replaces Afflatus Rapture with Afflatus Misery when Misery is ready to be used", WHM.JobID, WHM.AfflatusRapture)]
        WhiteMageRaptureMiseryFeature = 2402,

        [OrderedEnum]
        [CustomComboInfo("Cure 2 to Cure Level Sync", "Changes Cure 2 to Cure when below level 30 in synced content.", WHM.JobID, WHM.Cure2)]
        WhiteMageCureFeature = 2403,

        [OrderedEnum]
        [CustomComboInfo("Afflatus Feature", "Changes Cure 2 into Afflatus Solace, and Medica into Afflatus Rapture, when lilies are up.", WHM.JobID, WHM.Cure2, WHM.Medica)]
        WhiteMageAfflatusFeature = 2404,

        #endregion
        // ====================================================================================
    }
}
