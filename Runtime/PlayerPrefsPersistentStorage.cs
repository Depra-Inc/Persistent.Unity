// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using UnityEngine;

namespace Depra.Persistent.Runtime
{
	public sealed class PlayerPrefsPersistentStorage : IPersistentStorage
	{
		bool IPersistentStorage.HasKey(string key) => PlayerPrefs.HasKey(key);

		void IPersistentStorage.Save(string key, IPersistent persistent)
		{
			var type = persistent.StateType;
			if (type == typeof(string))
			{
				PlayerPrefs.SetString(key, (string) persistent.CaptureState());
			}
			else if (type == typeof(int))
			{
				PlayerPrefs.SetInt(key, (int) persistent.CaptureState());
			}
			else if (type == typeof(float))
			{
				PlayerPrefs.SetFloat(key, (float) persistent.CaptureState());
			}
			else
			{
				PlayerPrefs.SetString(key, persistent.ToString());
			}

			PlayerPrefs.Save();
		}

		object IPersistentStorage.Load(string key, IPersistent persistent)
		{
			if (persistent.StateType == typeof(string))
			{
				return PlayerPrefs.GetString(key);
			}

			if (persistent.StateType == typeof(int))
			{
				return PlayerPrefs.GetInt(key);
			}

			if (persistent.StateType == typeof(float))
			{
				return PlayerPrefs.GetFloat(key);
			}

			return PlayerPrefs.GetString(key);
		}
	}
}