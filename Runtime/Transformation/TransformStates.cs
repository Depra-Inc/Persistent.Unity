// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME, order = DEFAULT_ORDER)]
	public sealed class TransformStates : ScriptableObject
	{
		public TransformNode ParentNode;

		private const string FILE_NAME = nameof(TransformStates);
		private const string MENU_NAME = FRAMEWORK_NAME + SEPARATOR + MODULE_NAME + SEPARATOR + FILE_NAME;
	}
}