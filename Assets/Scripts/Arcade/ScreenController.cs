using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenController : MonoBehaviour
{
    public Material DefaultMaterial;
    public Material GameMaterial;
    public Renderer GameScreen;
    public string TargetScene;

    private void Start()
    {
        GameScreen.material = DefaultMaterial;
    }

    public void LoadArcadeGame()
    {
        SceneManager.LoadScene(TargetScene, LoadSceneMode.Additive);
        GameScreen.material = GameMaterial;
    }
    public void UnloadArcadeGame()
    {
        SceneManager.UnloadSceneAsync(TargetScene);
        GameScreen.material = DefaultMaterial;
    }
}
