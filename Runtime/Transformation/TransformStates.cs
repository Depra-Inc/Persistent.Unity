// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using UnityEngine;
using static Depra.Persistent.Module;

namespace Depra.Persistent.Transformation
{
	[CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_PATH + FILE_NAME, order = DEFAULT_ORDER)]
	public sealed class TransformStates : ScriptableObject
	{
		public TransformNode ParentNode;

		private const string FILE_NAME = nameof(TransformStates);
	}
}