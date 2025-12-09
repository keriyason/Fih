using UnityEngine;

public class PauseManager: MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //when things happening in the game use Time.timeScale, we can set this value to 1 or 0, allowing us to freeze the game actions/elements that use it.
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; //since our game is first person, we are hiding and locking the mouse in the game window 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //checking every frame if the player presses esc
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined; //make the mouse visible, since we are pulling up a menu that needs to be navigated with the mouse
        Cursor.visible = true;
        Time.timeScale = 0; //setting time scale to 0, so every in progress action or motion gets frozen in place.
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; //resets time scale to 1 so everything resumes, and once again locks and hides the cursor since we don't need it in game anymore.
        Cursor.visible = false;
    }
}
