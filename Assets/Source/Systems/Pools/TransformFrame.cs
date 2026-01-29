using UnityEngine;

public struct TransformFrame
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public static TransformFrame Zero => new TransformFrame { position = Vector3.zero, rotation = Quaternion.identity, scale = Vector3.one };
    public static TransformFrame T(Vector3 position) => new TransformFrame { position = position, rotation = Quaternion.identity, scale = Vector3.one };
    public static TransformFrame R(Quaternion rotation) => new TransformFrame { position = Vector3.zero, rotation = rotation, scale = Vector3.one };
    public static TransformFrame S(Vector3 scale) => new TransformFrame { position = Vector3.zero, rotation = Quaternion.identity, scale = scale };
    public static TransformFrame TR(Vector3 position, Quaternion rotation) => new TransformFrame { position = position, rotation = rotation, scale = Vector3.one };
    public static TransformFrame TRS(Vector3 position, Quaternion rotation, Vector3 scale) => new TransformFrame { position = position, rotation = rotation, scale = scale };
}
