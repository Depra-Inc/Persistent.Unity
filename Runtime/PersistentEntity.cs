// SPDX-License-Identifier: Apache-2.0
// © 2023-2026 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentEntity), DEFAULT_ORDER)]
	public sealed class PersistentEntity : MonoBehaviour, ISnapshotParticipant
	{
		[SerializeField] private string _key;

		SnapshotKey ISnapshotParticipant.Key => SnapshotKey.ValueOf(_key);
		Type ISnapshotParticipant.StateType => typeof(object);

		public object Capture()
		{
			var state = new Dictionary<string, object>();
			foreach (var persistent in GetComponents<ISnapshotParticipant>())
			{
				state[persistent.GetType().ToString()] = persistent.Capture();
			}

			return state;
		}

		public void Restore(object state)
		{
			var stateDictionary = (Dictionary<string, object>) state;
			foreach (var persistent in GetComponents<ISnapshotParticipant>())
			{
				var typeName = persistent.GetType().ToString();
				if (stateDictionary.TryGetValue(typeName, out var value))
				{
					persistent.Restore(value);
				}
			}
		}

		[ContextMenu(nameof(GenerateKey))]
		private void GenerateKey() => _key = Guid.NewGuid().ToString();
	}
}