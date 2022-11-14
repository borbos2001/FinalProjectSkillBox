using System.Collections;
using UnityEngine;

public class WeaponsShoot : MonoBehaviour
{
    [SerializeField] GameObject[] _weapon;
    [SerializeField] Rigidbody[] _bullet;
    [SerializeField] GameObject _shootPoint;
    [SerializeField] float _power;

    public static AudioClip _audioClip;

    static AudioSource _sound;

    private float _dilayToShoot = 3;

    private int _numWeapon = 0;

    private bool _canShoot = true;

    private int _countToRaload;

    private int _currentCountToReload = 0;

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
    }
    public void ChangeWeapon(int num)
    {
        _weapon[_numWeapon].SetActive(false);
        _numWeapon = num;
        _weapon[_numWeapon].SetActive(true);
    }

    public void PaternOfShoot(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, _shootPoint.transform.position.y, hitPoint.z + 1.4f);
        Vector3 _relativePos = hitPoint - _shootPoint.transform.position;
        if (Mathf.Abs(_relativePos.x) > 0.5f || Mathf.Abs(_relativePos.z) > 0.5f)
        {
            switch (_numWeapon)
            {
                case 0:
                    _dilayToShoot = 2;
                    _countToRaload = 2;

                    if (_canShoot)
                    {

                        Rigidbody _currentBullet = Instantiate(_bullet[_numWeapon], _shootPoint.transform.position, Quaternion.identity) as Rigidbody;
                        Rigidbody _currentBullet1 = Instantiate(_bullet[_numWeapon], _shootPoint.transform.position, Quaternion.identity) as Rigidbody;
                        Rigidbody _currentBullet2 = Instantiate(_bullet[_numWeapon], _shootPoint.transform.position, Quaternion.identity) as Rigidbody;
                        _currentBullet.AddForce(new Vector3(_relativePos.x + 1, _relativePos.y, _relativePos.z + 1).normalized * _power, ForceMode.Impulse);
                        _currentBullet1.AddForce(_relativePos.normalized * _power, ForceMode.Impulse);
                        _currentBullet2.AddForce(new Vector3(_relativePos.x - 1, _relativePos.y, _relativePos.z - 1).normalized * _power, ForceMode.Impulse);
                        SoundShoot(_numWeapon);
                        AmmoSound(_numWeapon);
                        _currentCountToReload++;
                    }
                    if (_currentCountToReload == _countToRaload && _canShoot)
                    {
                        StartCoroutine(TimeDilay());
                    }
                    break;
                case 1:
                    _dilayToShoot = 4;
                    _countToRaload = 20;

                    if (_canShoot)
                    {
                        
                        Rigidbody _currentBullet = Instantiate(_bullet[_numWeapon], _shootPoint.transform.position, Quaternion.identity) as Rigidbody;
                        _currentBullet.AddForce(_relativePos.normalized * _power, ForceMode.Impulse);
                        _currentCountToReload++;
                        SoundShoot(_numWeapon);
                    }
                    if (_currentCountToReload == _countToRaload && _canShoot)
                    {
                        StartCoroutine(TimeDilay());
                    }
                    break;
            }
        }
    }

    private void SoundShoot(int numOfGun)
    {
        _sound.Stop();
        _audioClip = Resources.Load<AudioClip>("Shoot" + numOfGun);
        _sound.PlayOneShot(_audioClip);
    }
    private void SoundReload(int numOfGun)
    {
            _audioClip = Resources.Load<AudioClip>("Reload" + numOfGun);
            _sound.PlayOneShot(_audioClip);
    }
    private void AmmoSound(int numOfGun)
    {
        _audioClip = Resources.Load<AudioClip>("Ammo" + numOfGun);
        _sound.PlayOneShot(_audioClip);
    }
    private IEnumerator TimeDilay()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_dilayToShoot - 0.5f);
        SoundReload(_numWeapon);
        yield return new WaitForSeconds(0.5f);
        _canShoot = true;
        _currentCountToReload = 0;
    }
}