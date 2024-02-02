// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[AddComponentMenu(menuName: MENU_NAME, order: DEFAULT_ORDER)]
	public sealed class PersistentTransform : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;

		private const string FILE_NAME = nameof(PersistentTransform);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

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