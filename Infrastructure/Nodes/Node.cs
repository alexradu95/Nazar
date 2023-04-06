using StereoKit;

namespace SceneGraphExample
{
    public class Node : INode
    {
        public string Name { get; }
        public INode Parent { get; private set; }
        public List<INode> Children { get; } = new List<INode>();
        public Matrix LocalTransform { get; set; } = Matrix.Identity;
        public Matrix GlobalTransform { get; private set; } = Matrix.Identity;
        public bool Enabled { get; set; } = true;

        public Node()
        {

        }

        public Node(Matrix position, string name)
        {
            LocalTransform = position;
            Name = name;
        }

        public Node(string name)
        {
            Name = name;
        }

        public void AddChild(INode child)
        {
            Children.Add(child);
            child.SetParent(this);
        }

        public void RemoveChild(INode child)
        {
            Children.Remove(child);
            child.SetParent(null);
        }

        public void SetParent(INode parent)
        {
            Parent = parent;
        }

        public void UpdateTransforms()
        {
            if (Parent != null)
            {
                GlobalTransform = LocalTransform * Parent.GlobalTransform;
            }
            else
            {
                GlobalTransform = LocalTransform;
            }

            foreach (var child in Children)
            {
                child.UpdateTransforms();
            }
        }

        public virtual void Update()
        {
            if (!Enabled) return;

            foreach (var child in Children)
            {
                child.Update();
            }
        }

        public virtual void Draw()
        {
            if (!Enabled) return;

            foreach (var child in Children)
            {
                child.Draw();
            }
        }
    }
}
