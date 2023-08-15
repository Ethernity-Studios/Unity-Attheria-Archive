using System;
using Mirror;
using UnityEngine;

[Serializable]
public class Spawnpoint
{
    public int Id;
    public Vector3 Position;
    public Vector3 Rotation;
}

public static class SpawnpointReaderWriter
{
    public static void WriteSpawnpoint(this NetworkWriter writer, Spawnpoint spawnpoint)
    {
        writer.WriteInt(spawnpoint.Id);
        writer.WriteVector3(spawnpoint.Position);
        writer.WriteVector3(spawnpoint.Rotation);
    }

    public static Spawnpoint ReadSpawnpoint(this NetworkReader reader)
    {
        return new Spawnpoint()
        {
            Id = reader.ReadInt(),
            Position = reader.ReadVector3(),
            Rotation = reader.ReadVector3()
        };
    }
}