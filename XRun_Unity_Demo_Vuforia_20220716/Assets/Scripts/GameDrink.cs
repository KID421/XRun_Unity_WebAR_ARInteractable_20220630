using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 遊戲飲料杯
    /// </summary>
    public class GameDrink : MonoBehaviour
    {
        [SerializeField, Header("準心")]
        private RectTransform rectCrossHair;
        [SerializeField, Header("準心左右限制")]
        private float limit = 245;
        [SerializeField, Header("吸管")]
        private Transform traStraw;
        [SerializeField, Header("吸管左右限制")]
        private float limitStraw = 0.2f;
        [SerializeField, Header("成功範圍")]
        private Vector2 v2Success = new Vector2(-25, 25);
        [SerializeField, Header("挑戰失敗")]
        private Button btnFail;
        [SerializeField, Header("挑戰成功")]
        private Button btnSuccess;

        private bool click;
        private bool check;
        private bool start;

        private void Awake()
        {
            btnFail.onClick.AddListener(ClickFail);
        }

        private void OnEnable()
        {
            Invoke("DelayStart", 0.5f);
        }

        private void Update()
        {
            if (!start) return;

            MoveCrossHair();
            Click();
            CheckSuccessOrNot();
        }

        private void DelayStart()
        {
            start = true;
        }

        /// <summary>
        /// 點擊失敗按鈕
        /// </summary>
        private void ClickFail()
        {
            click = false;
            check = false;
            btnFail.gameObject.SetActive(false);
        }

        /// <summary>
        /// 移動準心
        /// </summary>
        private void MoveCrossHair()
        {
            if (click) return;

            Vector2 posMove = new Vector2(Mathf.Sin(Time.time) * limit, -415);
            rectCrossHair.anchoredPosition = posMove;

            traStraw.localPosition = new Vector2(Mathf.Sin(Time.time) * limitStraw, 0.3f);
        }

        /// <summary>
        /// 檢查成功與否
        /// </summary>
        private void CheckSuccessOrNot()
        {
            if (!click) return;
            if (check) return;

            float x = rectCrossHair.anchoredPosition.x;

            if (x > v2Success.x && x < v2Success.y)
            {
                //print("成功");
                Invoke("DelaySuccess", 1);
                StartCoroutine(StrawMoveDown());
            }
            else
            {
                //print("失敗");
                Invoke("DelayFail", 0.5f);
            }

            check = true;
        }

        /// <summary>
        /// 延遲成功
        /// </summary>
        private void DelaySuccess()
        {
            btnSuccess.gameObject.SetActive(true);
        }

        /// <summary>
        /// 延遲失敗
        /// </summary>
        private void DelayFail()
        {
            btnFail.gameObject.SetActive(true);
        }

        /// <summary>
        /// 吸管往下
        /// </summary>
        private IEnumerator StrawMoveDown()
        {
            for (int i = 0; i < 10; i++)
            {
                traStraw.position -= Vector3.up * 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
        }

        /// <summary>
        /// 點擊
        /// </summary>
        private void Click()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                click = true;
            }
        }
    }
}

