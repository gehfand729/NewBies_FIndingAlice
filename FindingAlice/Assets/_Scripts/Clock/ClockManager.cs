using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public static ClockManager C;
    private void Awake()
    {
        if (C == null) C = this;
        else if (C != this) Destroy(gameObject);
    }

    [Header("Value")]
    [SerializeField] float clockSpeed = 200f;
    [SerializeField] float timeScaleValue = 0.05f;
    [SerializeField] float t;
    [SerializeField] Vector3 keepDir;

    //Scene - Clock 오브젝트(Clock SstActive가 False일 경우 스크립트 실행X 때문)
    public GameObject clock;
    //lever UI Transform
    public RectTransform rect;

    GameObject player;
    Rigidbody rb;
    float clockMaxTime = 0.1f;
    float clockStartTime;
    float clockEndTime;
    float clockCooldown = 1f;
    bool _isPressKeyClock = false;
    [SerializeField]
    bool checkClockUse = true;

    //lever의 transform 저장 벡터
    Vector3 dir;

    public bool isPressKeyClock
    {
        get { return _isPressKeyClock; }
        set { _isPressKeyClock = value; }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        t = Time.time - clockEndTime;
        //X를 눌렀을 때 시간 느리게 만들고 Clock 활성화
        if (Input.GetKeyDown(KeyCode.X) && checkClockUse && Time.time - clockEndTime > clockCooldown)
        {
            checkClockUse = false;
            clockStartTime = Time.time;
            Time.timeScale = timeScaleValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            clock.SetActive(true);
            _isPressKeyClock = true;
            GameManager.instance.clock = true;
        }

        //X를 누르고 있을 때 시계 사출
        if (_isPressKeyClock)
        {
            t = Time.time - clockStartTime;
            if (Time.time - clockStartTime < clockMaxTime)
            {
                dir = rect.anchoredPosition.normalized;
                if (dir == Vector3.zero)
                {
                    keepDir = new Vector3(clock.transform.position.x - player.transform.position.x,
                        clock.transform.position.y - player.transform.position.y, 0).normalized;
                    clock.transform.position += keepDir * clockSpeed * Time.deltaTime;
                }
                else
                    clock.transform.position += dir * clockSpeed * Time.deltaTime;
            }
            else
            {
                clockDefault();
                return;
            }
        }

        //X를 뗄 때 정상 시간 복귀, Clock으로 플레이어가 날아갈 준비
        if (Input.GetKeyUp(KeyCode.X) && _isPressKeyClock)
        {
            _isPressKeyClock = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            clock.transform.parent = null;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            //화면에 시계가 존재할 때 캐릭터의 속도를 0으로 초기화, 시계의 방향으로 캐릭터 이동
            rb.AddForce((clock.transform.position - player.transform.position).normalized * 
                (10 + Mathf.Pow(Vector3.Distance(clock.transform.position, player.transform.position) / 3, 2)), ForceMode.Impulse);
        }
    }

    void clockDefault()
    {
        _isPressKeyClock = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        clock.SetActive(false);
        GameManager.instance.clock = false;
        clock.transform.localPosition = Vector3.zero;
        checkClockUse = true;
    }

    void clockReset()
    {
        clock.SetActive(false);
        GameManager.instance.clock = false;
        clock.transform.SetParent(player.transform, true);
        clock.transform.localPosition = Vector3.zero;
        rb.useGravity = true;
        checkClockUse = true;
        clockEndTime = Time.time;
    }
}
