using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// �C�����ƪM
    /// </summary>
    public class GameDrink : MonoBehaviour
    {
        [SerializeField, Header("�Ǥ�")]
        private RectTransform rectCrossHair;
        [SerializeField, Header("�Ǥߥ��k����")]
        private float limit = 245;
        [SerializeField, Header("�l��")]
        private Transform traStraw;
        [SerializeField, Header("�l�ޥ��k����")]
        private float limitStraw = 0.2f;
        [SerializeField, Header("���\�d��")]
        private Vector2 v2Success = new Vector2(-25, 25);
        [SerializeField, Header("�D�ԥ���")]
        private Button btnFail;
        [SerializeField, Header("�D�Ԧ��\")]
        private Button btnSuccess;
        [SerializeField, Header("���ѭ���")]
        private AudioClip soundFail;
        [SerializeField, Header("���\����")]
        private AudioClip soundSuccess;
        [SerializeField, Header("��ܼ���")]
        private AudioClip soundShowTicket;
        [SerializeField, Header("��q������")]
        private AudioClip soundEnergy;

        private bool click;
        private bool check;
        private bool start;

        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

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
        /// �I�����ѫ��s
        /// </summary>
        private void ClickFail()
        {
            click = false;
            check = false;
            btnFail.gameObject.SetActive(false);
        }

        /// <summary>
        /// ���ʷǤ�
        /// </summary>
        private void MoveCrossHair()
        {
            if (click) return;

            if (!aud.isPlaying)
            {
                aud.clip = soundEnergy;
                aud.Play();
            }

            Vector2 posMove = new Vector2(Mathf.Sin(Time.time) * limit, -415);
            rectCrossHair.anchoredPosition = posMove;

            traStraw.localPosition = new Vector2(Mathf.Sin(Time.time) * limitStraw, 0.3f);
        }

        /// <summary>
        /// �ˬd���\�P�_
        /// </summary>
        private void CheckSuccessOrNot()
        {
            if (!click) return;
            if (check) return;

            float x = rectCrossHair.anchoredPosition.x;

            if (x > v2Success.x && x < v2Success.y)
            {
                //print("���\");
                aud.PlayOneShot(soundSuccess);
                Invoke("DelaySuccess", 1);
                StartCoroutine(StrawMoveDown());
            }
            else
            {
                //print("����");
                aud.PlayOneShot(soundFail);
                Invoke("DelayFail", 0.5f);
            }

            check = true;
            aud.clip = null;
        }

        /// <summary>
        /// ���𦨥\
        /// </summary>
        private void DelaySuccess()
        {
            aud.PlayOneShot(soundShowTicket);
            btnSuccess.gameObject.SetActive(true);
        }

        /// <summary>
        /// ���𥢱�
        /// </summary>
        private void DelayFail()
        {
            btnFail.gameObject.SetActive(true);
        }

        /// <summary>
        /// �l�ީ��U
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
        /// �I��
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

