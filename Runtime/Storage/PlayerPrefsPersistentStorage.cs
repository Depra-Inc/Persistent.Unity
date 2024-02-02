// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using UnityEngine;

namespace Depra.Persistent.Storage
{
	public sealed class PlayerPrefsPersistentStorage : IPersistentStorage
	{
		void IPersistentStorage.DeleteAll() => PlayerPrefs.DeleteAll();

		bool IPersistentStorage.Has(string key) => PlayerPrefs.HasKey(key);

		void IPersistentStorage.Delete(string key) => PlayerPrefs.DeleteKey(key);

		void IPersistentStorage.Save(string key, IPersistent persistent)
		{
			var type = persistent.StateType;
			var state = persistent.CaptureState();

			if (type == typeof(string))
			{
				PlayerPrefs.SetString(key, (string) state);
			}
			else if (type == typeof(int))
			{
				PlayerPrefs.SetInt(key, (int) state);
			}
			else if (type == typeof(float))
			{
				PlayerPrefs.SetFloat(key, (float) state);
			}
			else
			{
				PlayerPrefs.SetString(key, JsonUtility.ToJson(state));
			}

			PlayerPrefs.Save();
		}

		object IPersistentStorage.Load(string key, IPersistent persistent) => persistent.StateType switch
		{
			_ when persistent.StateType == typeof(int) => PlayerPrefs.GetInt(key),
			_ when persistent.StateType == typeof(float) => PlayerPrefs.GetFloat(key),
			_ when persistent.StateType == typeof(bool) => PlayerPrefs.GetInt(key) == 1,
			_ => JsonUtility.FromJson(PlayerPrefs.GetString(key), persistent.StateType)
		};
	}
}