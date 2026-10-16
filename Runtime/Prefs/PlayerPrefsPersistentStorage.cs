// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Depra.Persistent.Prefs
{
	public sealed class PlayerPrefsPersistentStorage : ISnapshotStorage
	{
		public bool Exists(SnapshotKey key) => PlayerPrefs.HasKey(key.Key);

		public Task<bool> ExistsAsync(SnapshotKey key) => Task.FromResult(Exists(key));

		public void Save(ISnapshotParticipant persistent)
		{
			var key = persistent.Key.Key;
			var stateType = persistent.StateType;
			var state = persistent.Capture();

			if (stateType == typeof(string))
			{
				PlayerPrefs.SetString(key, (string)state);
			}
			else if (stateType == typeof(int))
			{
				PlayerPrefs.SetInt(key, (int)state);
			}
			else if (stateType == typeof(float))
			{
				PlayerPrefs.SetFloat(key, (float)state);
			}
			else if (stateType == typeof(bool))
			{
				PlayerPrefs.SetInt(key, (bool)state ? 1 : 0);
			}
			else
			{
				PlayerPrefs.SetString(key, JsonUtility.ToJson(state));
			}

			PlayerPrefs.Save();
		}

		public Task SaveAsync(ISnapshotParticipant persistent)
		{
			Save(persistent);
			return Task.CompletedTask;
		}

		public object Load(ISnapshotParticipant persistent)
		{
			var key = persistent.Key.Key;
			var stateType = persistent.StateType;
			return stateType switch
			{
				_ when stateType == typeof(int) => PlayerPrefs.GetInt(key),
				_ when stateType == typeof(float) => PlayerPrefs.GetFloat(key),
				_ when stateType == typeof(bool) => PlayerPrefs.GetInt(key) == 1,
				_ => JsonUtility.FromJson(PlayerPrefs.GetString(key), stateType)
			};
		}

		public Task<object> LoadAsync(ISnapshotParticipant persistent)
		{
			var state = Load(persistent);
			return Task.FromResult(state);
		}

		public void Delete(SnapshotKey key) => PlayerPrefs.DeleteKey(key.Key);

		public void Delete(IReadOnlyCollection<SnapshotKey> keys)
		{
			foreach (var key in keys)
			{
				Delete(key);
			}
		}

		public Task DeleteAsync(SnapshotKey key)
		{
			Delete(key);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(IReadOnlyCollection<SnapshotKey> keys)
		{
			Delete(keys);
			return Task.CompletedTask;
		}

		public void DeleteAll() => PlayerPrefs.DeleteAll();

		public Task DeleteAllAsync()
		{
			DeleteAll();
			return Task.CompletedTask;
		}
	}
}