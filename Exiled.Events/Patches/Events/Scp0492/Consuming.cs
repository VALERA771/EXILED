// -----------------------------------------------------------------------
// <copyright file="Consuming.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events.Scp0492
{
    using System.Collections.Generic;
    using System.Reflection.Emit;

    using API.Features.Pools;
    using Exiled.Events.EventArgs.Scp0492;
    using HarmonyLib;
    using PlayerRoles.PlayableScps.Scp049;
    using PlayerRoles.PlayableScps.Scp049.Zombies;
    using PlayerRoles.PlayableScps.Subroutines;

    using static HarmonyLib.AccessTools;

    /// <summary>
    ///     Patches <see cref="RagdollAbilityBase{T}.ServerProcessCmd"/>
    ///     to add <see cref="Handlers.Scp0492.ConsumingCorpse" /> event.
    /// </summary>
    [HarmonyPatch(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.ServerProcessCmd))]
    public class Consuming
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

            int offset = 2;
            int index = newInstructions.FindIndex(instrc => instrc.Calls(Method(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.ServerValidateBegin)))) + offset;

            Label retLabel = generator.DefineLabel();
            LocalBuilder ev = generator.DeclareLocal(typeof(ConsumingCorpseEventArgs));

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new (OpCodes.Ldarg_0),
                new (OpCodes.Callvirt, PropertyGetter(typeof(ScpStandardSubroutine<ZombieRole>), nameof(ScpStandardSubroutine<ZombieRole>.Owner))),

                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.CurRagdoll))),

                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>._errorCode))),

                new(OpCodes.Ldc_I4_1),

                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(ConsumingCorpseEventArgs))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Dup),
                new(OpCodes.Stloc, ev.LocalIndex),

                new(OpCodes.Call, Method(typeof(Handlers.Scp0492), nameof(Handlers.Scp0492.OnConsuming))),

                new(OpCodes.Callvirt, PropertyGetter(typeof(ConsumingCorpseEventArgs), nameof(ConsumingCorpseEventArgs.IsAllowed))),
                new(OpCodes.Brtrue_S, retLabel),

                new(OpCodes.Ret),

                new CodeInstruction(OpCodes.Ldloc, ev.LocalIndex).WithLabels(retLabel),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(ConsumingCorpseEventArgs), nameof(ConsumingCorpseEventArgs.ErrorCode))),
                new(OpCodes.Stfld, Field(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>._errorCode))),
            });

            foreach (var instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Pool.Return(newInstructions);
        }
    }
}