using StereoKit;

namespace SceneGraphExample
{
    public interface INode
    {
        string Name { get; }
        INode Parent { get; }
        List<INode> Children { get; }
        Matrix LocalTransform { get; set; }
        Matrix GlobalTransform { get; }
        bool Enabled { get; set; }

        void AddChild(INode child);
        void RemoveChild(INode child);
        void SetParent(INode parent);
        void UpdateTransforms();
        void Update();
        void Draw();
    }
}
