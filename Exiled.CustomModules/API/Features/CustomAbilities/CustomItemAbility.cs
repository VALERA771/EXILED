// -----------------------------------------------------------------------
// <copyright file="CustomItemAbility.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.CustomModules.API.Features.CustomAbilities
{
    using System;

    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Features.Items;
    using Utils.NonAllocLINQ;

    /// <summary>
    /// Represents a base class for custom abilities associated with a specific <see cref="Item"/>.
    /// </summary>
    public abstract class CustomItemAbility : CustomAbility<Item>
    {
        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> containing all registered custom abilities.
        /// </summary>
        public static new IEnumerable<CustomItemAbility> List => Registered[typeof(Item)].Cast<CustomItemAbility>();

        /// <summary>
        /// Gets all owners and all their respective <see cref="CustomItemAbility"/>'s.
        /// </summary>
        public static new Dictionary<Item, HashSet<CustomItemAbility>> Manager =>
            CustomAbility<Item>.Manager.Where(kvp => kvp.Key is Item)
            .ToDictionary(kvp => (Item)kvp.Key, kvp => kvp.Value.Cast<CustomItemAbility>().ToHashSet());

        /// <summary>
        /// Gets all owners belonging to a <see cref="CustomItemAbility"/>.
        /// </summary>
        public static IEnumerable<Item> Owners => Manager.Keys.ToHashSet();

        /// <summary>
        /// Gets a <see cref="CustomItemAbility"/> given the specified <paramref name="customAbilityType"/>.
        /// </summary>
        /// <param name="customAbilityType">The specified ability type.</param>
        /// <returns>The <see cref="CustomItemAbility"/> matching the search or <see langword="null"/> if not registered.</returns>
        public static new CustomItemAbility Get(object customAbilityType) =>
            List.FirstOrDefault(customAbility => customAbility == customAbilityType && customAbility.IsEnabled);

        /// <summary>
        /// Gets a <see cref="CustomItemAbility"/> given the specified name.
        /// </summary>
        /// <param name="name">The specified name.</param>
        /// <returns>The <see cref="CustomItemAbility"/> matching the search or <see langword="null"/> if not registered.</returns>
        public static new CustomItemAbility Get(string name) => List.FirstOrDefault(customAbility => customAbility.Name == name);

        /// <summary>
        /// Gets a <see cref="CustomItemAbility"/> given the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>The <see cref="CustomItemAbility"/> matching the search or <see langword="null"/> if not found.</returns>
        public static new CustomItemAbility Get(Type type) =>
            (type.BaseType != typeof(IAbilityBehaviour) && !type.IsSubclassOf(typeof(IAbilityBehaviour))) ? null :
            List.FirstOrDefault(customAbility => customAbility.BehaviourComponent == type);

        /// <summary>
        /// Gets all <see cref="CustomItemAbility"/>'s from a <see cref="Item"/>.
        /// </summary>
        /// <param name="entity">The <see cref="CustomItemAbility"/>'s owner.</param>
        /// <returns>The <see cref="CustomItemAbility"/> matching the search or <see langword="null"/> if not registered.</returns>
        public static new IEnumerable<CustomItemAbility> Get(Item entity) => Manager.FirstOrDefault(kvp => kvp.Key == entity).Value;

        /// <summary>
        /// Tries to get a <see cref="CustomItemAbility"/> given the specified <paramref name="customAbility"/>.
        /// </summary>
        /// <param name="customAbilityType">The <see cref="object"/> to look for.</param>
        /// <param name="customAbility">The found <paramref name="customAbility"/>, <see langword="null"/> if not registered.</param>
        /// <returns><see langword="true"/> if a <paramref name="customAbility"/> was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGet(object customAbilityType, out CustomItemAbility customAbility) => customAbility = Get(customAbilityType);

        /// <summary>
        /// Tries to get a <paramref name="customAbility"/> given a specified name.
        /// </summary>
        /// <param name="name">The <see cref="CustomItemAbility"/> name to look for.</param>
        /// <param name="customAbility">The found <see cref="CustomItemAbility"/>, <see langword="null"/> if not registered.</param>
        /// <returns><see langword="true"/> if a <see cref="CustomItemAbility"/> was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGet(string name, out CustomItemAbility customAbility) => customAbility = List.FirstOrDefault(cAbility => cAbility.Name == name);

        /// <summary>
        /// Tries to get the item's current <see cref="CustomItemAbility"/>'s.
        /// </summary>
        /// <param name="entity">The entity to search on.</param>
        /// <param name="customAbility">The found <see cref="CustomItemAbility"/>'s, <see langword="null"/> if not registered.</param>
        /// <returns><see langword="true"/> if a <see cref="CustomItemAbility"/> was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGet(Item entity, out IEnumerable<CustomItemAbility> customAbility) => (customAbility = Get(entity)) is not null;

        /// <summary>
        /// Tries to get the item's current <see cref="CustomItemAbility"/>.
        /// </summary>
        /// <param name="abilityBehaviour">The <see cref="IAbilityBehaviour"/> to search for.</param>
        /// <param name="customAbility">The found <see cref="CustomItemAbility"/>, <see langword="null"/> if not registered.</param>
        /// <returns><see langword="true"/> if a <see cref="CustomItemAbility"/> was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGet(IAbilityBehaviour abilityBehaviour, out CustomItemAbility customAbility) => customAbility = Get(abilityBehaviour.GetType());

        /// <summary>
        /// Tries to get the item's current <see cref="CustomItemAbility"/>.
        /// </summary>
        /// <param name="type">The type to search for.</param>
        /// <param name="customAbility">The found <see cref="CustomItemAbility"/>, <see langword="null"/> if not registered.</param>
        /// <returns><see langword="true"/> if a <see cref="CustomItemAbility"/> was found; otherwise, <see langword="false"/>.</returns>
        public static bool TryGet(Type type, out CustomItemAbility customAbility) => customAbility = Get(type.GetType());

        /// <inheritdoc cref="CustomAbility{T}.Add{TAbility}(T, out TAbility)"/>
        public static new bool Add<TAbility>(Item entity, out TAbility param)
            where TAbility : CustomItemAbility => CustomAbility<Item>.Add(entity, out param);

        /// <inheritdoc cref="CustomAbility{T}.Add(T, Type)"/>
        public static new bool Add(Item entity, Type type) => CustomAbility<Item>.Add(entity, type);

        /// <inheritdoc cref="CustomAbility{T}.Add(T, string)"/>
        public static new bool Add(Item entity, string name) => CustomAbility<Item>.Add(entity, name);

        /// <inheritdoc cref="CustomAbility{T}.Add(T, uint)"/>
        public static new bool Add(Item entity, uint id) => CustomAbility<Item>.Add(entity, id);

        /// <inheritdoc cref="CustomAbility{T}.Add(T, IEnumerable{Type})"/>
        public static new void Add(Item entity, IEnumerable<Type> types) => CustomAbility<Item>.Add(entity, types);

        /// <inheritdoc cref="CustomAbility{T}.Add(T, IEnumerable{string})"/>
        public static new void Add(Item entity, IEnumerable<string> names) => CustomAbility<Item>.Add(entity, names);

        /// <inheritdoc cref="CustomAbility{T}.Remove{TAbility}(T)"/>
        public static new bool Remove<TAbility>(Item entity)
            where TAbility : CustomItemAbility => CustomAbility<Item>.Remove<TAbility>(entity);

        /// <inheritdoc cref="CustomAbility{T}.Remove(T, Type)"/>
        public static new bool Remove(Item entity, Type type) => CustomAbility<Item>.Remove(entity, type);

        /// <inheritdoc cref="CustomAbility{T}.Remove(T, string)"/>
        public static new bool Remove(Item entity, string name) => CustomAbility<Item>.Remove(entity, name);

        /// <inheritdoc cref="CustomAbility{T}.Remove(T, string)"/>
        public static new bool Remove(Item entity, uint id) => CustomAbility<Item>.Remove(entity, id);

        /// <inheritdoc cref="CustomAbility{T}.RemoveAll(T)"/>
        public static new void RemoveAll(Item entity) => CustomAbility<Item>.RemoveAll(entity);

        /// <inheritdoc cref="CustomAbility{T}.RemoveRange(T, IEnumerable{Type})"/>
        public static new void RemoveRange(Item entity, IEnumerable<Type> types) => CustomAbility<Item>.RemoveRange(entity, types);

        /// <inheritdoc cref="CustomAbility{T}.RemoveRange(T, IEnumerable{string})"/>
        public static new void RemoveRange(Item entity, IEnumerable<string> names) => CustomAbility<Item>.RemoveRange(entity, names);

        /// <inheritdoc cref="CustomAbility{T}.Add(T)"/>
        public new void Add(Item entity) => base.Add(entity);

        /// <inheritdoc cref="CustomAbility{T}.Remove(T)"/>
        public new bool Remove(Item entity) => base.Remove(entity);
    }
}