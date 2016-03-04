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
            get { return "1.1"; }
        }

        public override string Description
        {
            get { return "Regenerates Stamina every set amount"; }
        }

        public override void Entry()
        {
            ReadConfig();
            Events.UpdateTick += Events_UpdateTick;
        }

        private float RegenTime = 3.0f;
        private int RegenAmount = 1;

        private double prevTime = 0;

        void Events_UpdateTick()
        {
            if (Game1.player == null || !Game1.hasLoadedGame)
                return;

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds < prevTime + RegenTime)
                return;

            prevTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

            Game1.player.Stamina += RegenAmount;

        }

        private void ReadConfig()
        {
            if (System.IO.File.Exists("StaminaRegen_Config.ini"))
            {
                var fileData = System.IO.File.ReadAllLines("StaminaRegen_Config.ini");
                if (fileData.Length > 1)
                {
                    //Load in TickRate
                    var regenTickRateString = fileData[0];
                    regenTickRateString = regenTickRateString.Replace("RegenTickRate:", "").Trim();
                    int newRate;
                    if (int.TryParse(regenTickRateString, out newRate))
                    {
                        RegenTime = Math.Max(1, newRate);
                    }

                    //Load in Amount
                    var regenAmountString = fileData[1];
                    regenAmountString = regenAmountString.Replace("RegenAmount:", "").Trim();
                    int newAmount;
                    if (int.TryParse(regenAmountString, out newAmount))
                    {
                        RegenAmount = newAmount;
                    }

                }
            }
            else
            {
                var dataToWrite = @"RegenTickRate: 3
RegenAmount: 1";

                System.IO.File.WriteAllText("StaminaRegen_Config.ini", dataToWrite);
            }
        }
    }
}
