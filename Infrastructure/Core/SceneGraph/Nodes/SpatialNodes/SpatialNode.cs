using BaseFramework.Core.SceneGraph.Nodes.BaseNode;
using BaseFramework.Core.SceneGraph.Nodes.BaseNode.Behaviours.Interfaces;
using BaseFramework.Core.SceneGraph.SpatialNodes.Behaviours;
using BaseFramework.Core.SceneGraph.SpatialNodes.Interfaces;

namespace BaseFramework.Core.SceneGraph.SpatialNodes
{
    public class SpatialNode : Node
    {

        public readonly INodeTransform Transform;

        public SpatialNode(INodeIdentity identity, INodeHierarchy hierarchy, INodeTransform transform) : base(identity, hierarchy)
        {
            Transform = transform ?? new NodeTransform();
        }

        public void UpdateTransforms()
        {
            if (Hierarchy.Parent is SpatialNode parentSpatialNode)
            {
                Transform.GlobalTransform = Transform.LocalTransform * parentSpatialNode.Transform.GlobalTransform;
            }
            else
            {
                Transform.GlobalTransform = Transform.LocalTransform;
            }

            foreach (SpatialNode child in Hierarchy.Children.OfType<SpatialNode>())
            {
                child.UpdateTransforms();
            }
        }
    }
}
