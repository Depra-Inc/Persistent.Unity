using System;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	public sealed class PersistentTransformHierarchy : MonoBehaviour, IPersistent
	{
		[field: SerializeField] public string Key { get; private set; }
		[SerializeField] private TransformStates _states;

		public Type StateType => typeof(TransformNode);

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