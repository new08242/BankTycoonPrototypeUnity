using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSecThenDestroy(2));
    }

    IEnumerator WaitForSecThenDestroy(int sec) {
        yield return new WaitForSeconds(sec);

        Destroy(this.gameObject);
    }
}
