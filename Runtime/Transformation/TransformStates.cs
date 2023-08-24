using Depra.Persistent.Runtime.Common;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	[CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME, order = Module.DEFAULT_ORDER)]
	public class TransformStates : ScriptableObject
	{
		public TransformNode ParentNode;

		private const string FILE_NAME = nameof(TransformStates);
		private const string MENU_NAME = Module.FRAMEWORK_NAME + Module.SEPARATOR + Module.MODULE_NAME + Module.SEPARATOR + FILE_NAME;
	}
}