using System;
using UnityEngine;
using static Depra.Persistent.Runtime.Common.Module;

namespace Depra.Persistent.Runtime.Transformation
{
	[AddComponentMenu(menuName: MENU_NAME, order: DEFAULT_ORDER)]
	public sealed class PersistentTransform : MonoBehaviour, IPersistent
	{
		[SerializeField] private string _key;

		private const string FILE_NAME = nameof(PersistentTransform);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

		string IPersistent.Key => _key;

		Type IPersistent.StateType => typeof(TransformState);

		public void RestoreState(TransformState state)
		{
			transform.SetPositionAndRotation(state.LocalPosition, state.LocalRotation);
			transform.localScale = state.LocalScale;
		}

		void IPersistent.RestoreState(object state) => RestoreState((TransformState) state);

		object IPersistent.CaptureState() => new TransformState(transform);
	}
}