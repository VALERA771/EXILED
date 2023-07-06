// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Handlers
{
    using Exiled.Events.EventArgs.Item;

    using Exiled.Events.Features;

    /// <summary>
    ///     Item related events.
    /// </summary>
    public static class Item
    {
        /// <summary>
        ///     Gets or sets invoked before the ammo of an firearm are changed.
        /// </summary>
        public static Event<ChangingAmmoEventArgs> ChangingAmmo { get; set; } = new ();

        /// <summary>
        ///     Gets or sets invoked before item attachments are changed.
        /// </summary>
        public static Event<ChangingAttachmentsEventArgs> ChangingAttachments { get; set; } = new();

        /// <summary>
        ///     Gets or sets invoked before receiving a preference.
        /// </summary>
        public static Event<ReceivingPreferenceEventArgs> ReceivingPreference { get; set; } = new();

        /// <summary>
        /// Gets or sets invoked before a keycard interacts with a door.
        /// </summary>
        public static Event<KeycardInteractingEventArgs> KeycardInteracting { get; set; } = new();

        /// <summary>
        /// Gets or sets invoked before a melee item is swung.
        /// </summary>
        public static Event<SwingingEventArgs> Swinging { get; set; } = new();

        /// <summary>
        /// Gets or sets invoked before a <see cref="API.Features.Items.Jailbird"/> is charged.
        /// </summary>
        public static Event<ChargingJailbirdEventArgs> ChargingJailbird { get; set; } = new();

        /// <summary>
        /// Called before the ammo of an firearm is changed.
        /// </summary>
        /// <param name="ev">The <see cref="ChangingAmmoEventArgs"/> instance.</param>
        public static void OnChangingAmmo(ChangingAmmoEventArgs ev) => ChangingAmmo.InvokeSafely(ev);

        /// <summary>
        ///     Called before item attachments are changed.
        /// </summary>
        /// <param name="ev">The <see cref="ChangingAttachmentsEventArgs" /> instance.</param>
        public static void OnChangingAttachments(ChangingAttachmentsEventArgs ev) => ChangingAttachments.InvokeSafely(ev);

        /// <summary>
        ///     Called before receiving a preference.
        /// </summary>
        /// <param name="ev">The <see cref="ReceivingPreferenceEventArgs" /> instance.</param>
        public static void OnReceivingPreference(ReceivingPreferenceEventArgs ev) => ReceivingPreference.InvokeSafely(ev);

        /// <summary>
        /// Called before keycard interacts with a door.
        /// </summary>
        /// <param name="ev">The <see cref="KeycardInteractingEventArgs"/> instance.</param>
        public static void OnKeycardInteracting(KeycardInteractingEventArgs ev) => KeycardInteracting.InvokeSafely(ev);

        /// <summary>
        /// Called before a melee item is swung.
        /// </summary>
        /// <param name="ev">The <see cref="SwingingEventArgs"/> instance.</param>
        public static void OnSwinging(SwingingEventArgs ev) => Swinging.InvokeSafely(ev);

        /// <summary>
        /// Called before a <see cref="API.Features.Items.Jailbird"/> is charged.
        /// </summary>
        /// <param name="ev">The <see cref="ChargingJailbirdEventArgs"/> instance.</param>
        public static void OnChargingJailbird(ChargingJailbirdEventArgs ev) => ChargingJailbird.InvokeSafely(ev);
    }
}