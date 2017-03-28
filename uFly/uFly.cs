using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace uFly
{
    public class uFly : RocketPlugin
    {
        public List<UnturnedPlayer> playersFlying = new List<UnturnedPlayer>();

        protected override void Load()
        {
            UnturnedPlayerEvents.OnPlayerUpdateGesture += UnturnedPlayerEvents_OnPlayerUpdateGesture;
        }

        public void UnturnedPlayerEvents_OnPlayerUpdateGesture(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            if (gesture == UnturnedPlayerEvents.PlayerGesture.Point)
            {
                playersFlying.Add(player);
                player.Player.movement.gravity = 0;
            }

            if (gesture == UnturnedPlayerEvents.PlayerGesture.Wave)
            {
                playersFlying.Remove(player);
                player.Player.movement.gravity = 1;
            }
        }

        public void FixedUpdate()
        {
            foreach (UnturnedPlayer player in playersFlying)
            {
                if (player.Player.input.keys[0])
                {
                    player.Teleport(new Vector3(player.Position.x, player.Position.y, player.Position.z), player.Rotation);
                }

                if (player.Player.input.keys[5])
                {
                    player.Teleport(new Vector3(player.Position.x, player.Position.y - 1, player.Position.z), player.Rotation);
                }
            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerUpdateGesture -= UnturnedPlayerEvents_OnPlayerUpdateGesture;
        }
    }
}
