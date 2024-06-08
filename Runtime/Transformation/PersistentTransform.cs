// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentTransform), DEFAULT_ORDER)]
	public sealed class PersistentTransform : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;

		string IPersistent.Key => _key;

		Type IPersistent.StateType => typeof(TransformState);

		public void RestoreState(TransformState state)
		{
			transform.SetPositionAndRotation(state.LocalPosition, state.LocalRotation);
			transform.localScale = state.LocalScale;
		}

		object IPersistent.CaptureState() => new TransformState(transform);

		void IPersistent.RestoreState(object state) => RestoreState((TransformState) state);
	}
}