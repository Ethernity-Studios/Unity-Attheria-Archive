﻿using System.Threading.Tasks;
using Mirror;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Saveable), typeof(NetworkIdentity))]
    public abstract class Manager : NetworkBehaviour, ISaveable
    {
        public abstract object SaveData();
        public abstract Task LoadData(object data);

    }
}