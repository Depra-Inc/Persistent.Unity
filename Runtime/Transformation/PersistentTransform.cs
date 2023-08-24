using System;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	public sealed class PersistentTransform : MonoBehaviour, IPersistent
	{
		[field: SerializeField] public string Key { get; private set; }

		public Type StateType => typeof(TransformState);

		public TransformState LastState { get; private set; }

		public object CaptureState() =>
			LastState = new TransformState(transform);

		public void RestoreState(TransformState state)
		{
			transform.SetPositionAndRotation(state.LocalPosition, state.LocalRotation);
			transform.localScale = state.LocalScale;
		}

		void IPersistent.RestoreState(object state) =>
			RestoreState(LastState = (TransformState) state);
	}
}