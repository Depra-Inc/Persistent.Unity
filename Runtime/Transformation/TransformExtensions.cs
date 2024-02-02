// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using UnityEngine;

namespace Depra.Persistent.Transformation
{
	internal static class TransformExtensions
	{
		public static TransformNode RecordTransformsRecursively(this Transform self)
		{
			var directChildren = self.GetDirectChildrenOfTransform();
			var nodeData = new TransformNode(new TransformState(self));

			if (directChildren.Count <= 0)
			{
				return nodeData;
			}

			foreach (var children in directChildren)
			{
				nodeData.ChildrenStates.Add(RecordTransformsRecursively(children));
			}

			return nodeData;
		}

		public static void LoadTransformsRecursively(this Transform self, TransformNode node)
		{
			var directChildren = self.GetDirectChildrenOfTransform();
			if (directChildren.Count > 0)
			{
				for (var nodeIndex = 0; nodeIndex < node.ChildrenStates.Count; nodeIndex++)
				{
					LoadTransformsRecursively(directChildren[nodeIndex], node.ChildrenStates[nodeIndex]);
				}
			}

			node.State.ApplyTo(self);
		}

		private static List<Transform> GetDirectChildrenOfTransform(this Transform self)
		{
			var directChildren = new List<Transform>();
			for (var index = 0; index < self.childCount; index++)
			{
				directChildren.Add(self.GetChild(index));
			}

			return directChildren;
		}
	}
}