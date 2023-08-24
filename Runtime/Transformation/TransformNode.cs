using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depra.Persistent.Runtime.Transformation
{
	[Serializable]
	public struct TransformNode
	{
		[field: SerializeField] public TransformState State { get; private set; }
		[field: SerializeField] public List<TransformNode> ChildrenStates { get; private set; }

		public TransformNode(TransformState state)
		{
			State = state;
			ChildrenStates = new List<TransformNode>();
		}
	}
}