// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentEntity), DEFAULT_ORDER)]
	public sealed class PersistentEntity : MonoBehaviour, IPersistent
	{
		[field: SerializeField] public string Key { get; private set; }

		Type IPersistent.StateType => typeof(object);

		public object CaptureState()
		{
			var state = new Dictionary<string, object>();
			foreach (var persistent in GetComponents<IPersistent>())
			{
				state[persistent.GetType().ToString()] = persistent.CaptureState();
			}

			return state;
		}

		public void RestoreState(object state)
		{
			var stateDictionary = (Dictionary<string, object>) state;
			foreach (var persistent in GetComponents<IPersistent>())
			{
				var typeName = persistent.GetType().ToString();
				if (stateDictionary.TryGetValue(typeName, out var value))
				{
					persistent.RestoreState(value);
				}
			}
		}

		[ContextMenu(nameof(GenerateKey))]
		private void GenerateKey() => Key = Guid.NewGuid().ToString();
	}
}