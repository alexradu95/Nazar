using SceneGraph.Interfaces;

namespace SceneGraph.Behaviours
{
    internal class NodeEvents : INodeEvents
    {
        /// <summary>
        /// Callback that triggers every time a node updates its matrix.
        /// </summary>
        public event NodeEventCallback OnTransformationsUpdate;

        /// <summary>
        /// Callback that triggers every time a node is rendered.
        /// Note: nodes that are culled out should not trigger this.
        /// </summary>
        public event NodeEventCallback OnDraw;
    }
}