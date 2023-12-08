// -----------------------------------------------------------------------
// <copyright file="InteractingLockerEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Player
{
    using System;

    using API.Features;
    using Exiled.API.Features.Lockers;
    using Interfaces;
    using MapGeneration.Distributors;

    /// <summary>
    /// Contains all information before a player interacts with a locker.
    /// </summary>
    public class InteractingLockerEventArgs : IPlayerEvent, IDeniableEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractingLockerEventArgs" /> class.
        /// </summary>
        /// <param name="player">
        /// <inheritdoc cref="Player" />
        /// </param>
        /// <param name="locker">
        /// <inheritdoc cref="Locker" />
        /// </param>
        /// <param name="lockerChamber">
        /// <inheritdoc cref="Chamber" />
        /// </param>
        /// <param name="chamberId">
        /// <inheritdoc cref="ChamberId" />
        /// </param>
        /// <param name="isAllowed">
        /// <inheritdoc cref="IsAllowed" />
        /// </param>
        public InteractingLockerEventArgs(Player player, MapGeneration.Distributors.Locker locker, LockerChamber lockerChamber, byte chamberId, bool isAllowed)
        {
            Player = player;
            LockerChamber = API.Features.Lockers.Chamber.Get(lockerChamber);
            ChamberId = chamberId;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the <see cref="MapGeneration.Distributors.Locker" /> instance.
        /// </summary>
        [Obsolete("Use LockerChamber::Locker instead.")]
        public MapGeneration.Distributors.Locker Locker => LockerChamber.Locker.Base;

        /// <summary>
        /// Gets the interacting chamber.
        /// </summary>
        [Obsolete("Use LockerChamber instead.")]
        public LockerChamber Chamber => LockerChamber.Base;

        /// <summary>
        /// Gets the interacting chamber.
        /// </summary>
        public Chamber LockerChamber { get; }

        /// <summary>
        /// Gets the chamber id.
        /// </summary>
        public byte ChamberId { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the player can interact with the locker.
        /// </summary>
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets the player who's interacting with the locker.
        /// </summary>
        public Player Player { get; }
    }
}