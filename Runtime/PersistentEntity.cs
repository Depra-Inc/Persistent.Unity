using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depra.Persistent.Runtime
{
	public sealed class PersistentEntity : MonoBehaviour, IPersistent
	{
		[field: SerializeField] public string Key { get; private set; }

		public Type StateType => typeof(object);

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
			var stateDictionary = (Dictionary<string, object>)state;
			foreach (var persistent in GetComponents<IPersistent>())
			{
				var typeName = persistent.GetType().ToString();
				if (stateDictionary.TryGetValue(typeName, out var value))
				{
					persistent.RestoreState(value);
				}
			}
		}

		[ContextMenu(nameof(GenerateId))]
		private void GenerateId() => Key = Guid.NewGuid().ToString();
	}
}