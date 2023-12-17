using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class GetFirstCollectableCommand
    {
        private List<Transform> _collectable;

        public GetFirstCollectableCommand(ref List<Transform> collectable)
        {
            _collectable = collectable;
        }

        public Transform OnGetFirstCollectable()
        {
            if (_collectable == null) return null;
            return _collectable[0];
        }
    }
}