// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[AddComponentMenu(MENU_PATH + nameof(PersistentPath), DEFAULT_ORDER)]
	public sealed class PersistentPath : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;
		[Min(0)] [SerializeField] private float _minDistance = 1;
		[SerializeField] private List<TransformState> _savedStates;

		private TransformState _lastState;

		private void Start()
		{
			_savedStates = new List<TransformState>();
			CaptureIntermediateState();
		}

		private void LateUpdate()
		{
			var distance = Vector3.Distance(transform.localPosition, _lastState.LocalPosition);
			if (distance > _minDistance)
			{
				CaptureIntermediateState();
			}
		}

		string IPersistent.Key => _key;

		Type IPersistent.StateType => typeof(List<TransformState>);

		[ContextMenu(nameof(CaptureIntermediateState))]
		public void CaptureIntermediateState()
		{
			_lastState = new TransformState(transform);
			_savedStates.Add(_lastState);
		}

		public void RestoreState(List<TransformState> states)
		{
			_savedStates = states;
			if (_savedStates == null)
			{
				_lastState = default;
				return;
			}

			_lastState = _savedStates.LastOrDefault();
			_lastState.ApplyTo(transform);
		}

		object IPersistent.CaptureState() => _savedStates;

		void IPersistent.RestoreState(object state) => RestoreState((List<TransformState>) state);
	}
}