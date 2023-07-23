using Mirror;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Saveable), typeof(NetworkIdentity))]
    public abstract class Manager : NetworkBehaviour
    {
        
    }
}