using UnityEngine;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// �D�e���޲z��
    /// </summary>
    public class MainManager : MonoBehaviour
    {
        /// <summary>
        /// ���J����
        /// </summary>
        /// <param name="nameScene">�����W��</param>
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}

