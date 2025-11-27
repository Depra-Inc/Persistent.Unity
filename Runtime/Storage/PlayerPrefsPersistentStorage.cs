// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depra.Persistent.Storage
{
	public sealed class PlayerPrefsPersistentStorage : IPersistentStorage
	{
		bool IPersistentStorage.Contains(string key) => PlayerPrefs.HasKey(key);

		void IPersistentStorage.Save(string key, IPersistent persistent, bool overwrite)
		{
			var type = persistent.StateType;
			var state = persistent.CaptureState();

			if (type == typeof(string))
			{
				PlayerPrefs.SetString(key, (string)state);
			}
			else if (type == typeof(int))
			{
				PlayerPrefs.SetInt(key, (int)state);
			}
			else if (type == typeof(float))
			{
				PlayerPrefs.SetFloat(key, (float)state);
			}
			else if (type == typeof(bool))
			{
				PlayerPrefs.SetInt(key, (bool)state ? 1 : 0);
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

		void IPersistentStorage.Delete(string key) => PlayerPrefs.DeleteKey(key);

		void IPersistentStorage.DeleteAll() => PlayerPrefs.DeleteAll();

		IEnumerable<string> IPersistentStorage.EnumerateKeys() => Array.Empty<string>();
	}
}