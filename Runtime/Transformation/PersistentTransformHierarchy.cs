using System;
using UnityEngine;
using static Depra.Persistent.Runtime.Common.Module;

namespace Depra.Persistent.Runtime.Transformation
{
	[AddComponentMenu(menuName: MENU_NAME, order: DEFAULT_ORDER)]
	public sealed class PersistentTransformHierarchy : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;
		[SerializeField] private TransformStates _states;

		private const string FILE_NAME = nameof(PersistentTransformHierarchy);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

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