// -----------------------------------------------------------------------
// <copyright file="SpawningRagdollEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Player
{
    using API.Features;
    using Exiled.API.Features.DamageHandlers;
    using Interfaces;
    using PlayerRoles;
    using PlayerRoles.Ragdolls;
    using UnityEngine;

    /// <summary>
    /// Contains all information before spawning a player ragdoll.
    /// </summary>
    public class SpawningRagdollEventArgs : IPlayerEvent, IDeniableEvent
    {
        private DamageHandler damageHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpawningRagdollEventArgs" /> class.
        /// </summary>
        /// <param name="info">
        /// <inheritdoc cref="Info" />
        /// </param>
        /// <param name="isAllowed">
        /// <inheritdoc cref="IsAllowed" />
        /// </param>
        public SpawningRagdollEventArgs(RagdollData info, bool isAllowed = true)
        {
            Info = info;
            Player = Player.Get(info.OwnerHub);
            Scale = Player.Scale;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets or sets the spawning position of the ragdoll.
        /// </summary>
        public Vector3 Position
        {
            get => Info.StartPosition;
            set => Info = new RagdollData(Player.ReferenceHub, DamageHandlerBase, value, Rotation);
        }

        /// <summary>
        /// Gets or sets the ragdoll's rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get => Info.StartRotation;
            set => Info = new RagdollData(Player.ReferenceHub, DamageHandlerBase, Position, value);
        }

        /// <summary>
        /// Gets or sets the ragdoll's scale.
        /// </summary>
        public Vector3 Scale { get; set; }

        /// <summary>
        /// Gets or sets the ragdoll's <see cref="RoleTypeId" />.
        /// </summary>
        public RoleTypeId Role
        {
            get => Info.RoleType;
            set => Info = new RagdollData(Player.ReferenceHub, DamageHandlerBase, value, Position, Rotation, Nickname, CreationTime);
        }

        /// <summary>
        /// Gets the ragdoll's creation time.
        /// </summary>
        public double CreationTime => Info.CreationTime;

        /// <summary>
        /// Gets or sets the ragdoll's nickname.
        /// </summary>
        public string Nickname
        {
            get => Info.Nickname;
            set => Info = new RagdollData(Player.ReferenceHub, DamageHandlerBase, Role, Position, Rotation, value, CreationTime);
        }

        /// <summary>
        /// Gets or sets the ragdoll's <see cref="RagdollData" />.
        /// </summary>
        public RagdollData Info { get; set; }

        /// <summary>
        /// Gets or sets the ragdoll's <see cref="DamageHandler"/>.
        /// </summary>
        public DamageHandler DamageHandlerBase
        {
            get => damageHandler ??= new DamageHandler(Player, Info.Handler);
            set
            {
                Info = new RagdollData(Player.ReferenceHub, value, Role, Position, Rotation, Nickname, CreationTime);
                damageHandler = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the ragdoll can be spawned.
        /// </summary>
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets the <see cref="Player">Owner</see> of the ragdoll.
        /// </summary>
        public Player Player { get; }
    }
}