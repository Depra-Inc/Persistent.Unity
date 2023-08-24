using System;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	[Serializable]
	public struct TransformState
	{
		[field: SerializeField] public Vector3 LocalPosition { get; private set; }
		[field: SerializeField] public Quaternion LocalRotation { get; private set; }
		[field: SerializeField] public Vector3 LocalScale { get; private set; }

		public TransformState(Transform transform) :
			this(transform.localPosition, transform.localRotation, transform.localScale) { }

		public TransformState(Vector3 position, Quaternion rotation, Vector3 scale)
		{
			LocalPosition = position;
			LocalRotation = rotation;
			LocalScale = scale;
		}

		public void ApplyTo(Transform transform)
		{
			transform.localPosition = LocalPosition;
			transform.localRotation = LocalRotation;
			transform.localScale = LocalScale;
		}
	}
}