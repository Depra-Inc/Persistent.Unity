using System;
using System.Collections.Generic;
using UnityEngine;
using static Depra.Persistent.Runtime.Common.Module;

namespace Depra.Persistent.Runtime
{
	[AddComponentMenu(menuName: MENU_NAME, order: DEFAULT_ORDER)]
	public sealed class PersistentEntity : MonoBehaviour, IPersistent
	{
		[field: SerializeField] public string Key { get; private set; }

		private const string FILE_NAME = nameof(PersistentEntity);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

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