using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	public sealed class PersistentPath : MonoBehaviour, IPersistent
	{
		[Min(0f)] [SerializeField] private float _minDistance = 1f;
		[SerializeField] private List<TransformState> _savedStates;
		[field: SerializeField] public string Key { get; private set; }

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

		public Type StateType => typeof(List<TransformState>);

		[ContextMenu(nameof(CaptureState))]
		public void CaptureIntermediateState()
		{
			_lastState = new TransformState(transform);
			_savedStates.Add(_lastState);
		}

		public object CaptureState()
		{
			return _savedStates;
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

		void IPersistent.RestoreState(object state) =>
			RestoreState((List<TransformState>)state);
	}
}