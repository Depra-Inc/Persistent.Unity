// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentTransformHierarchy), DEFAULT_ORDER)]
	public sealed class PersistentTransformHierarchy : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;
		[SerializeField] private TransformStates _states;

		string IPersistent.Key => _key;

		Type IPersistent.StateType => typeof(TransformNode);

		[ContextMenu(nameof(RecordTransforms))]
		public void RecordTransforms()
		{
			if (_states)
			{
				_states.ParentNode = transform.RecordTransformsRecursively();
			}
		}

		[ContextMenu(nameof(LoadTransforms))]
		public void LoadTransforms()
		{
			if (_states)
			{
				transform.LoadTransformsRecursively(_states.ParentNode);
			}
		}

		object IPersistent.CaptureState()
		{
			_states.ParentNode = transform.RecordTransformsRecursively();
			return _states.ParentNode;
		}

		void IPersistent.RestoreState(object state)
		{
			_states.ParentNode = (TransformNode) state;
			transform.LoadTransformsRecursively(_states.ParentNode);
		}
	}
}