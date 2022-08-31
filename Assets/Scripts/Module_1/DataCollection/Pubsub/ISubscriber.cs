using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataCollection 
{
    public interface ISubscriber
    {
        public abstract void Callback();
    }
}
