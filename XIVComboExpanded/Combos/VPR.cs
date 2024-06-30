using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class VPR
    {
        public const byte JobID = 41;

        public const uint
            Placeholder = 0;

        public static class Buffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Placeholder = 0;
        }
    }

    /*internal class BlackUmbralSoulTransposeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackUmbralSoulTransposeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.UmbralSoul)
            {
                if (!GetJobGauge<BLMGauge>().InUmbralIce || level < BLM.Levels.UmbralSoul)
                    return BLM.Transpose;
            }

            return actionID;
        }
    }*/
}