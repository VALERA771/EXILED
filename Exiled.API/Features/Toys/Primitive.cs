// -----------------------------------------------------------------------
// <copyright file="Primitive.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features.Toys
{
    using System.Linq;

    using AdminToys;
    using Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Interfaces;
    using Exiled.API.Structs;
    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// A wrapper class for <see cref="PrimitiveObjectToy"/>.
    /// </summary>
    public class Primitive : AdminToy, IWrapper<PrimitiveObjectToy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <param name="toyAdminToyBase">The <see cref="PrimitiveObjectToy"/> of the toy.</param>
        internal Primitive(PrimitiveObjectToy toyAdminToyBase)
            : base(toyAdminToyBase, AdminToyType.PrimitiveObject) => Base = toyAdminToyBase;

        /// <summary>
        /// Gets the light prefab's type.
        /// </summary>
        public static PrefabType PrefabType => PrefabType.PrimitiveObjectToy;

        /// <summary>
        /// Gets the light prefab's object.
        /// </summary>
        public static GameObject PrefabObject => PrefabHelper.PrefabToGameObject[PrefabType];

        /// <summary>
        /// Gets the base <see cref="PrimitiveObjectToy"/>.
        /// </summary>
        public PrimitiveObjectToy Base { get; }

        /// <summary>
        /// Gets or sets the type of the primitive.
        /// </summary>
        public PrimitiveType Type
        {
            get => Base.NetworkPrimitiveType;
            set => Base.NetworkPrimitiveType = value;
        }

        /// <summary>
        /// Gets or sets the material color of the primitive.
        /// </summary>
        public Color Color
        {
            get => Base.NetworkMaterialColor;
            set => Base.NetworkMaterialColor = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the primitive can be collided with.
        /// </summary>
        public bool Collidable
        {
            get => Flags.HasFlag(PrimitiveFlags.Collidable);
            set => Flags = Flags.ModifyFlags(value, PrimitiveFlags.Collidable);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the primitive is visible.
        /// </summary>
        public bool Visible
        {
            get => Flags.HasFlag(PrimitiveFlags.Visible);
            set => Flags = Flags.ModifyFlags(value, PrimitiveFlags.Visible);
        }

        /// <summary>
        /// Gets or sets the primitive flags.
        /// </summary>
        public PrimitiveFlags Flags
        {
            get => Base.NetworkPrimitiveFlags;
            set => Base.NetworkPrimitiveFlags = value;
        }

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveType">The <see cref="PrimitiveType"/> of the <see cref="Primitive"/>.</param>
        /// <param name="flags">The <see cref="PrimitiveFlags"/> of the <see cref="Primitive"/>.</param>
        /// <param name="color">The color of the <see cref="Primitive"/>.</param>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="isStatic">Whether or not the <see cref="Primitive"/> is static.</param>
        /// <param name="spawn">Whether or not the <see cref="Primitive"/> should be initially spawned.</param>
        /// <returns>The newly created <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveType primitiveType, PrimitiveFlags flags = PrimitiveFlags.Visible | PrimitiveFlags.Collidable, Color? color = null, Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null, bool isStatic = false, bool spawn = true)
            => Create(new(primitiveType, flags, color, position, rotation, scale, isStatic, spawn));

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveSettings">The settings of the <see cref="Primitive"/>.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveSettings primitiveSettings)
        {
            Primitive primitive = new(Object.Instantiate(PrefabObject.GetComponent<PrimitiveObjectToy>()))
            {
                Type = primitiveSettings.PrimitiveType,
                Flags = primitiveSettings.Flags,
                Color = primitiveSettings.Color,
                Position = primitiveSettings.Position,
                Rotation = primitiveSettings.Rotation,
                Scale = primitiveSettings.Scale,
                IsStatic = primitiveSettings.IsStatic,
            };

            if (primitiveSettings.ShouldSpawn)
                primitive.Spawn();

            return primitive;
        }

        /// <summary>
        /// Gets the <see cref="Primitive"/> belonging to the <see cref="PrimitiveObjectToy"/>.
        /// </summary>
        /// <param name="primitiveObjectToy">The <see cref="PrimitiveObjectToy"/> instance.</param>
        /// <returns>The corresponding <see cref="Primitive"/> instance.</returns>
        public static Primitive Get(PrimitiveObjectToy primitiveObjectToy)
        {
            AdminToy adminToy = Map.Toys.FirstOrDefault(x => x.AdminToyBase == primitiveObjectToy);
            return adminToy is not null ? adminToy as Primitive : new Primitive(primitiveObjectToy);
        }
    }
}
