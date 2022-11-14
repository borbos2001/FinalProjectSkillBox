
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject _deathUI;
    public void DeadPlayer()
    {
        _deathUI.SetActive(true);
        Destroy(gameObject);
    }
}
