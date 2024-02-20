using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunFire : MonoBehaviour
{

    public Gun CurrentGun;

    public List<Gun> GunInventory;

    private const int DefaultFOV = 60;
    private const int ZoomFOV = 20;

    private bool _isZoomMode = false; // 줌 했냐?

    public Image ZoomImage;

    public GameObject CrosshairUI;
    public GameObject CrosshairZoomUI;



    // 목표: 마우스 왼족 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다.
    // 필요 속성
    // - 총알 튀는 이펙트 프리팹
    public ParticleSystem HitEffect;

    public float Timer = 0;

 


    
    // 총알집
    public int BulletBox;
    // - 총알 개수 텍스트 UI
    public Text bulletUI;
    public Text ReloadUI;

    // 무기 이미지 UI
    public Image GunImageUI;

    private int _currentGunIndex = 0;

    public bool _isReloading = false; // 재장전 중이냐?

    private void Start()
    {
        RifreshGun();
        RefreshUI();
        ZoomImage.gameObject.SetActive(false);
    }

    
    private void Update()
    {
       Timer += Time.deltaTime;

        RefreshZoomMood();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentGun = GunInventory[0];
            RifreshGun();
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentGun = GunInventory[1];
            RifreshGun();
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentGun = GunInventory[2];
            RifreshGun();
            RefreshUI();
        }
        // 1. 만약에 마우스 왼쪽 버튼을 누른 상태 && 총알 쿨타임 && 총알이 0보다 클 때 발사
        if (Input.GetMouseButton(0) && Timer >= CurrentGun.FireCoolTime && CurrentGun.BulletRemainCount > 0)
        {
            // 재장전 중이라면 재장전 코루틴을 중지
            if (_isReloading)
            {
                StopAllCoroutines();
                _isReloading = false;
                ReloadUI.text = "";
            }
            CurrentGun.BulletRemainCount--;
            Timer = 0;

            // 2. 레이(광선)을 생성하고,위치와 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
            // 3. 레이를 발사한다.
            // 4. 레이가 부딛힌 대상의 정보를 받아온다.
            RaycastHit hitInfo;
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if (IsHit)
            {
                IHitable hitObject = hitInfo.collider.GetComponent<IHitable>();
                if (hitObject != null)
                {
                    hitObject.Hit(CurrentGun.damage);
                }

                // 실습 과제 18. 레이저를 몬스터에게 맞출 시 몬스터 체력 닳는 기능 구현
                Monster monster = GetComponent<Monster>();

                // 5. 부딛힌 위치에 (총알이 튀는) 이펙트를 위치한다.
                HitEffect.gameObject.transform.position = hitInfo.point;
                // 6. 이펙트가 쳐다보는 방향을 부딛힌 위치에 법선 벡터로 한다.반동
                HitEffect.gameObject.transform.forward = hitInfo.normal;
                HitEffect.Play();
            }
            RefreshUI();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && CurrentGun.BulletMaxCount > 0)   // R 키를 누르면 재장전을 시작
        {
            _isReloading = true;
            StartCoroutine(Reload_Coroutine(CurrentGun.Relode));
            ReloadingUI();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) // [ 키를 눌렀을 때
        {
            _currentGunIndex--;
            if (_currentGunIndex < 0)
                _currentGunIndex = GunInventory.Count - 1;
            ChangeGun();
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket)) // ] 키를 눌렀을 때
        {
            _currentGunIndex++;
            if (_currentGunIndex >= GunInventory.Count)
                _currentGunIndex = 0;
            ChangeGun();
        }
    }

    private IEnumerator Reload_Coroutine(float delayTime) // 재장전 코루틴
    {

        yield return new WaitForSeconds(delayTime);

        if (_isReloading)
        {
            CurrentGun.BulletRemainCount = CurrentGun.BulletMaxCount; // 총알 장전
            RefreshUI(); // 총알 장전 UI
            ReloadUI.text = ""; // 1.5초 후 공백으로 만듬
            _isReloading = false; // false는 재장전이 아닐 때를 의미함
        }
    }
    private void ChangeGun()
    {
        CurrentGun = GunInventory[_currentGunIndex];
        RifreshGun();
        RefreshUI();
    }
    private void RefreshUI()
    {
        GunImageUI.sprite = CurrentGun.ProfileImage;
        bulletUI.text = $"{CurrentGun.BulletRemainCount}/{CurrentGun.BulletMaxCount}";
        CrosshairUI.SetActive(!_isZoomMode);
        CrosshairZoomUI.SetActive(_isZoomMode);

    }
    private void ReloadingUI()
    {
        ReloadUI.text = $"{"재장전 중..."}";
    }

    public void RifreshGun()
    {
        foreach ( Gun gun in GunInventory)
        {
            if(gun == CurrentGun)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
            
        }
    }

    // 춤 모드에 따라 카메라 FOV 수정해주는 메서드
   private void RefreshZoomMood()
    {
        // 마우스 휠 버튼 눌렀을 때 && 현재 무기가 스나이퍼
        if (Input.GetMouseButtonDown(2) && CurrentGun.GType == GunType.Sniper)
        {
            if (_isZoomMode)
            {
                
                Camera.main.fieldOfView = DefaultFOV;
            }
            else
            {
                
                Camera.main.fieldOfView = ZoomFOV;
            }

            RefreshUI();
        }
    }

}
