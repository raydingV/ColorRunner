using UnityEngine;

namespace Commands
{
    public class IdleLevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int idleLevelId, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"LevelPrefabs/Idle/Level{idleLevelId}"), levelHolder);
        }
    }
}