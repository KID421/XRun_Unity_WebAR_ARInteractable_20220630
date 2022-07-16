using UnityEngine;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// 主畫面管理器
    /// </summary>
    public class MainManager : MonoBehaviour
    {
        /// <summary>
        /// 載入場景
        /// </summary>
        /// <param name="nameScene">場景名稱</param>
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}

