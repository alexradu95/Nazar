using Framework.SceneGraph.Interfaces;
using StereoKit;

namespace Framework.SceneGraph;

public class Transform : ITransform
{
    private Matrix _localTransform = Matrix.Identity;

    /// <summary>
    ///     Get / Set node local position.
    /// </summary>
    public Vec3 Position
    {
        get => _localTransform.Translation;
        set
        {
            if (_localTransform.Translation.ToString() != value.ToString())
            {
                _localTransform.Translation = value;
                OnTransformationsSet();
            }
        }
    }

    public bool IsDirty { get; set; }
    public uint TransformVersion { get; set; }
    public uint ParentLastTransformVersion { get; set; }

    public Matrix LocalTransform
    {
        get => _localTransform;
        set
        {
            _localTransform = value;
            OnTransformationsSet();
        }
    }

    public Matrix WorldTransform { get; private set; } = Matrix.Identity;


    public void UpdateTransformations(INode parentNode)
    {
        if (IsDirty || NeedUpdateDueToParentChange(parentNode))
        {
            if (parentNode != null)
            {
                if (parentNode.Transform.IsDirty) parentNode.Transform.UpdateTransformations(parentNode);

                WorldTransform = _localTransform * parentNode.Transform.WorldTransform;
                ParentLastTransformVersion = parentNode.Transform.TransformVersion;
            }
            else
            {
                WorldTransform = _localTransform;
                ParentLastTransformVersion = 0;
            }

            OnWorldMatrixChange(parentNode);
        }

        IsDirty = false;
    }

    protected virtual void OnWorldMatrixChange(INode parentNode)
    {
        TransformVersion++;
    }

    private bool NeedUpdateDueToParentChange(INode parentNode)
    {
        if (parentNode == null) return ParentLastTransformVersion != 0;

        return parentNode.Transform.IsDirty || ParentLastTransformVersion != parentNode.Transform.TransformVersion;
    }

    /// <summary>
    ///     Called when local transformations are set, eg when Position, Rotation, Scale etc. is changed.
    ///     We use this to set this node as "dirty", eg that we need to update local transformations.
    /// </summary>
    protected virtual void OnTransformationsSet()
    {
        IsDirty = true;
    }

    #region Position

    /// <summary>
    ///     Alias to access position X directly.
    /// </summary>
    public float PositionX
    {
        get => _localTransform.Translation.x;
        set
        {
            if (_localTransform.Translation.x != value) OnTransformationsSet();
            // How to edit Translation X
        }
    }

    /// <summary>
    ///     Alias to access position Y directly.
    /// </summary>
    public float PositionY
    {
        get => _localTransform.Translation.y;
        set
        {
            if (_localTransform.Translation.y != value) OnTransformationsSet();
            // How to edit Translation Y
        }
    }


    /// <summary>
    ///     Alias to access position X directly.
    /// </summary>
    public float PositionZ
    {
        get => _localTransform.Translation.z;
        set
        {
            if (_localTransform.Translation.z != value) OnTransformationsSet();
            // How to edit Translation Z
        }
    }

    /// <summary>
    ///     Move position by vector.
    /// </summary>
    /// <param name="moveBy">Vector to translate by.</param>
    public void Translate(Vec3 moveBy)
    {
        _localTransform.Translation += moveBy;
        OnTransformationsSet();
    }

    #endregion

    #region Rotation

    /// <summary>
    ///     Get / Set node local rotation.
    /// </summary>
    public Quat Rotation => _localTransform.Rotation;

    /// <summary>
    ///     Alias to access rotation X directly.
    /// </summary>
    public float RotationX
    {
        get => _localTransform.Rotation.x;
        set
        {
            if (_localTransform.Rotation.x != value)
            {
                // How to change the rotation of X?
            }
        }
    }

    /// <summary>
    ///     Alias to access rotation Y directly.
    /// </summary>
    public float RotationY
    {
        get => _localTransform.Rotation.y;
        set
        {
            if (_localTransform.Rotation.y != value)
            {
                // How to change the rotation of Y?
            }
        }
    }

    /// <summary>
    ///     Alias to access rotation Z directly.
    /// </summary>
    public float RotationZ
    {
        get => _localTransform.Rotation.z;
        set
        {
            if (_localTransform.Rotation.z != value)
            {
                // How to change the rotation of Z?
            }
        }
    }

    #endregion

    #region Scale

    /// <summary>
    ///     Get / Set node local scale.
    /// </summary>
    public Vec3 Scale
    {
        get => _localTransform.Scale;
        set
        {
            if (_localTransform.Scale.ToString() != value.ToString())
            {
                _localTransform = _localTransform.Pose.ToMatrix(value);
                OnTransformationsSet();
            }
        }
    }

    /// <summary>
    ///     Alias to access scale X directly.
    /// </summary>
    public float ScaleX
    {
        get => _localTransform.Scale.x;
        set
        {
            if (_localTransform.Scale.x != value) OnTransformationsSet();
            // How to change scale on x?
        }
    }


    /// <summary>
    ///     Alias to access scale Y directly.
    /// </summary>
    public float ScaleY
    {
        get => _localTransform.Scale.y;
        set
        {
            if (_localTransform.Scale.y != value) OnTransformationsSet();
            // How to change scale on Y?
        }
    }


    /// <summary>
    ///     Alias to access scale Z directly.
    /// </summary>
    public float ScaleZ
    {
        get => _localTransform.Scale.z;
        set
        {
            if (_localTransform.Scale.z != value) OnTransformationsSet();
            // How to change scale on Z?
        }
    }

    #endregion
}