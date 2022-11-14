
using UnityEngine;
namespace InfiniteHell.Inputs
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMove _playerMove;
        private Vector3 _movement;
        private RaycastHit hit;
        private WeaponsShoot _weaponsShoot;
        private void Start()
        {
            _weaponsShoot = GetComponent<WeaponsShoot>();
            _playerMove = GetComponent<PlayerMove>();
            
        }
        private void Update()
        {
            float _horizontal = Input.GetAxis(GlobalStringVars.HorizontalAxis);
            float _vertiacal = Input.GetAxis(GlobalStringVars.VerticalAxis);
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if (Input.GetButtonDown(GlobalStringVars.Fire1))
            {
                _weaponsShoot.PaternOfShoot(hit.point);
            }
            Input.GetMouseButtonDown(0);
            Input.GetMouseButtonUp(0);


            _movement = new Vector3(_horizontal,0, _vertiacal);
            _playerMove.Move(_movement);

            _playerMove.Rotation(hit.point);
        }
        
        
    }
}
