using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;

namespace StaminaRegen
{
    public class StaminaRegenMod : Mod
    {
        public override string Name
        {
            get { return "Stamina Regen"; }
        }

        public override string Authour
        {
            get { return "Natfoth"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Description
        {
            get { return "Slow Regenerates Stamina every 3s"; }
        }

        public override void Entry()
        {
            Events.UpdateTick += Events_UpdateTick;
        }

        private const float RegenTime = 3.0f;

        private double prevTime = 0;

        void Events_UpdateTick()
        {
            if (Game1.player == null || !Game1.hasLoadedGame)
                return;

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds < prevTime + RegenTime)
                return;

            prevTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

            ++Game1.player.Stamina;

        }
    }
}
