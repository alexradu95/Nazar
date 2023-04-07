using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneGraph.Interfaces
{
    public interface ITransform
    {

        Matrix LocalTransformations { get; }
        Matrix WorldTransformations { get; }

        /// <summary>
        /// Turns true when the transformations of this node changes.
        /// </summary>
        bool IsDirty { get; set; }

        /// <summary>
        /// This number increment every time we update transformations.
        /// We use it to check if our parent's transformations had been changed since last
        /// time this node was rendered, and if so, we re-apply parent updated transformations.
        /// </summary>
        uint TransformVersion { get; set; }

        /// <summary>
        /// The last transformations version we got from our parent.
        /// </summary>
        uint ParentLastTransformVersion { get; set; }

        void UpdateTransformations(INode parentNode);

    }
}
