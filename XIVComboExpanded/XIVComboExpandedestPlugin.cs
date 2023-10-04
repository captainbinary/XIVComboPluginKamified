using System;
using System.Linq;

using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace XIVComboExpandedestPlugin
{
    /// <summary>
    /// Main plugin implementation.
    /// </summary>
    public sealed partial class XIVComboExpandedestPlugin : IDalamudPlugin
    {
        private const string Command = "/pcombo";

        private readonly WindowSystem windowSystem;
        private readonly ConfigWindow configWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="XIVComboExpandedestPlugin"/> class.
        /// </summary>
        /// <param name="pluginInterface">Dalamud plugin interface.</param>
        public XIVComboExpandedestPlugin(DalamudPluginInterface pluginInterface)
        {
            FFXIVClientStructs.Interop.Resolver.GetInstance.SetupSearchSpace();
            FFXIVClientStructs.Interop.Resolver.GetInstance.Resolve();

            pluginInterface.Create<Service>();

            Service.Configuration = pluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            Service.Address = new PluginAddressResolver();
            Service.Address.Setup(Service.SigScanner);

            Service.IconReplacer = new IconReplacer();

            if (Service.Configuration.Version == 4)
                this.UpgradeConfig4();

            this.configWindow = new();
            this.windowSystem = new("XIVComboExpanded");
            this.windowSystem.AddWindow(this.configWindow);

            Service.Interface.UiBuilder.OpenConfigUi += this.OnOpenConfigUi;
            Service.Interface.UiBuilder.Draw += this.windowSystem.Draw;

            Service.CommandManager.AddHandler(Command, new CommandInfo(this.OnCommand)
            {
                HelpMessage = "Open a window to edit custom combo settings.",
                ShowInHelp = true,
            });
        }

        [PluginService]
        public static IFramework Framework { get; private set; } = null!;

        /// <inheritdoc/>
        public string Name => "XIV Combo Expandedest";

        /// <inheritdoc/>
        public void Dispose()
        {
            Service.CommandManager.RemoveHandler(Command);

            Service.Interface.UiBuilder.OpenConfigUi -= this.OnOpenConfigUi;
            Service.Interface.UiBuilder.Draw -= this.windowSystem.Draw;

            Service.IconReplacer.Dispose();
        }

        private void OnOpenConfigUi()
            => this.configWindow.IsOpen = true;

        private void OnCommand(string command, string arguments)
        {
            var argumentsParts = arguments.Split();

            switch (argumentsParts[0])
            {
                case "setall":
                    {
                        foreach (var preset in Enum.GetValues<CustomComboPreset>())
                        {
                            Service.Configuration.EnabledActions.Add(preset);
                        }

                        Service.ChatGui.Print("All SET");
                        Service.Configuration.Save();
                        break;
                    }

                case "unsetall":
                    {
                        foreach (var preset in Enum.GetValues<CustomComboPreset>())
                        {
                            Service.Configuration.EnabledActions.Remove(preset);
                        }

                        Service.ChatGui.Print("All UNSET");
                        Service.Configuration.Save();
                        break;
                    }

                case "set":
                    {
                        var targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (var preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            Service.Configuration.EnabledActions.Add(preset);
                            Service.ChatGui.Print($"{preset} SET");
                        }

                        Service.Configuration.Save();
                        break;
                    }

                case "secrets":
                    {
                        Service.Configuration.EnableSecretCombos = !Service.Configuration.EnableSecretCombos;

                        Service.ChatGui.Print(Service.Configuration.EnableSecretCombos
                            ? $"Secret combos are now shown"
                            : $"Secret combos are now hidden");

                        Service.Configuration.Save();
                        break;
                    }

                case "toggle":
                    {
                        var targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (var preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            if (Service.Configuration.EnabledActions.Contains(preset))
                            {
                                Service.Configuration.EnabledActions.Remove(preset);
                                Service.ChatGui.Print($"{preset} UNSET");
                            }
                            else
                            {
                                Service.Configuration.EnabledActions.Add(preset);
                                Service.ChatGui.Print($"{preset} SET");
                            }
                        }

                        Service.Configuration.Save();
                        break;
                    }

                case "unset":
                    {
                        var targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (var preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            Service.Configuration.EnabledActions.Remove(preset);
                            Service.ChatGui.Print($"{preset} UNSET");
                        }

                        Service.Configuration.Save();
                        break;
                    }

                case "list":
                    {
                        var filter = argumentsParts.Length > 1
                            ? argumentsParts[1].ToLowerInvariant()
                            : "all";

                        if (filter == "set")
                        {
                            foreach (var preset in Enum.GetValues<CustomComboPreset>()
                                .Select(preset => Service.Configuration.IsEnabled(preset)))
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                        else if (filter == "unset")
                        {
                            foreach (var preset in Enum.GetValues<CustomComboPreset>()
                                .Select(preset => !Service.Configuration.IsEnabled(preset)))
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                        else if (filter == "all")
                        {
                            foreach (var preset in Enum.GetValues<CustomComboPreset>())
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                        else
                        {
                            Service.ChatGui.PrintError("Available list filters: set, unset, all");
                        }

                        break;
                    }

                default:
                    this.configWindow.Toggle();
                    break;
            }

            Service.Configuration.Save();
        }

        private void UpgradeConfig4()
        {
            Service.Configuration.Version = 5;
            Service.Configuration.EnabledActions = Service.Configuration.EnabledActions4
                .Select(preset => (int)preset switch
                {
                    94 => 9001,
                    27 => 3301,
                    75 => 3302,
                    62 => 3303,
                    25 => 2501,
                    107 => 2502,
                    26 => 2503,
                    56 => 2504,
                    87 => 2505,
                    88 => 2506,
                    100 => 2507,
                    41 => 2301,
                    42 => 2302,
                    84 => 2303,
                    85 => 2304,
                    108 => 2305,
                    33 => 3801,
                    31 => 3802,
                    34 => 3803,
                    43 => 3804,
                    50 => 3805,
                    109 => 3806,
                    64 => 3807,
                    44 => 2201,
                    0 => 2202,
                    1 => 2203,
                    2 => 2204,
                    3 => 3201,
                    4 => 3202,
                    71 => 3203,
                    20 => 3701,
                    21 => 3702,
                    52 => 3703,
                    103 => 3704,
                    22 => 3705,
                    30 => 3706,
                    70 => 3707,
                    66 => 3708,
                    23 => 3101,
                    24 => 3102,
                    47 => 3103,
                    58 => 3104,
                    95 => 3105,
                    105 => 3106,
                    54 => 2001,
                    65 => 2002,
                    110 => 2003,
                    17 => 3001,
                    18 => 3002,
                    19 => 3003,
                    81 => 3004,
                    82 => 3005,
                    89 => 3006,
                    90 => 3007,
                    97 => 3008,
                    98 => 3009,
                    111 => 3010,
                    112 => 3011,
                    5 => 1901,
                    6 => 1902,
                    59 => 1903,
                    7 => 1904,
                    55 => 1905,
                    68 => 1906,
                    113 => 3901,
                    114 => 3902,
                    115 => 3903,
                    119 => 3904,
                    121 => 3905,
                    124 => 3906,
                    120 => 3907,
                    122 => 3908,
                    48 => 3501,
                    49 => 3502,
                    78 => 3503,
                    53 => 3504,
                    79 => 3505,
                    104 => 3506,
                    80 => 3507,
                    123 => 4001,
                    11 => 3401,
                    12 => 3402,
                    13 => 3403,
                    14 => 3404,
                    15 => 3405,
                    91 => 3406,
                    74 => 3407,
                    101 => 3408,
                    102 => 3409,
                    116 => 3410,
                    117 => 3411,
                    29 => 2801,
                    37 => 2802,
                    28 => 2701,
                    39 => 2702,
                    40 => 2703,
                    125 => 2704,
                    8 => 2101,
                    9 => 2102,
                    10 => 2103,
                    99 => 2104,
                    67 => 2105,
                    69 => 2106,
                    96 => 2107,
                    118 => 2108,
                    35 => 2401,
                    36 => 2402,
                    60 => 2403,
                    61 => 2404,
                    _ => 0,
                })
                .Where(id => id != 0)
                .Select(id => (CustomComboPreset)id)
                .ToHashSet();
            Service.Configuration.EnabledActions4 = new();
            Service.Configuration.Save();
        }
    }
}