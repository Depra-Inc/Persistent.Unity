using UnityEngine;
using static Depra.Persistent.Runtime.Common.Module;

namespace Depra.Persistent.Runtime.Transformation
{
	[CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME, order = DEFAULT_ORDER)]
	public sealed class TransformStates : ScriptableObject
	{
		public TransformNode ParentNode;

		private const string FILE_NAME = nameof(TransformStates);
		private const string MENU_NAME = FRAMEWORK_NAME + SEPARATOR + MODULE_NAME + SEPARATOR + FILE_NAME;
	}
}