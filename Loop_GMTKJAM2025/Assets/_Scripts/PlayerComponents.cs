using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerComponents Instance;
    public Composer playerComposer;
    public GameObject wheel;

    void Awake()
    {
        Instance = this;
    }

}
