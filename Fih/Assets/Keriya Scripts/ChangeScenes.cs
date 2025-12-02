using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes: MonoBehaviour
{
    [SerializeField] string scene;
    
    [SerializeField] AudioSource sound;

    [SerializeField] Button button;

    private void Start()
    {
        //sound = gameObject.GetComponent<AudioSource>();
        //GetComponent<Button>().onClick.AddListener(PerformCoroutine);
        button.onClick.AddListener(PerformCoroutine);
    }
    /*
    public void ChangeScene()
    {
        sound.Play();
        SceneManager.LoadScene(scene);
    }
    */
    public void PerformCoroutine()
    {
        
        StartCoroutine(SoundLoadScene());
    }
    IEnumerator SoundLoadScene()
    {
        sound.PlayOneShot(sound.clip);
        yield return new WaitForSeconds(sound.clip.length);
        SceneManager.LoadScene(scene);
    }
}
