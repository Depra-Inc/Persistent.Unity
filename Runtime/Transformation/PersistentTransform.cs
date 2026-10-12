// SPDX-License-Identifier: Apache-2.0
// © 2023-2026 Depra <n.melnikov@depra.org>

using System;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentTransform), DEFAULT_ORDER)]
	public sealed class PersistentTransform : MonoBehaviour, ISnapshotParticipant
	{
		[SerializeField] private string _key;

		SnapshotKey ISnapshotParticipant.Key => SnapshotKey.ValueOf(_key);
		Type ISnapshotParticipant.StateType => typeof(TransformState);

		public void RestoreState(TransformState state)
		{
			transform.SetPositionAndRotation(state.LocalPosition, state.LocalRotation);
			transform.localScale = state.LocalScale;
		}

		object ISnapshotParticipant.Capture() => new TransformState(transform);

		void ISnapshotParticipant.Restore(object state) => RestoreState((TransformState)state);
	}
}