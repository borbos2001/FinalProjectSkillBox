
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int _numArrayX;
    public int _numArrayY;

    private MazeCraete _mazeCraete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _mazeCraete = other.GetComponent<MazeCraete>();
            _mazeCraete.ChoseSideToSpawn(_numArrayX,_numArrayY);
        }
    }
    public void DeleteZone()
    {
        Collider[] _overlapBoxs = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2, Quaternion.identity);
        foreach (Collider _ovelapBox in _overlapBoxs)
        {

            Destroy(_ovelapBox.gameObject);

        }
    }
}
