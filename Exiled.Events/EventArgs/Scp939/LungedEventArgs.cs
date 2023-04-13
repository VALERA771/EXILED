// -----------------------------------------------------------------------
// <copyright file="LungedEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Scp939
{
    using API.Features;
    using Interfaces;

    /// <summary>
    ///     Contains all information after SCP-939 uses its lunge ability.
    /// </summary>
    public class LungedEventArgs : IPlayerEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LungedEventArgs" /> class.
        /// </summary>
        /// <param name="player">
        ///     <inheritdoc cref="Player" />
        /// </param>
        public LungedEventArgs(ReferenceHub player)
        {
            Player = Player.Get(player);
        }

        /// <summary>
        ///     Gets the player who's controlling SCP-939.
        /// </summary>
        public Player Player { get; }
    }
}