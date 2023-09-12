using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ArcadeController : MonoBehaviour
{
    public Material DefaultMaterial;
    public Material GameMaterial;
    public Renderer GameScreen;
    public string TargetArcadeGame;

    private void Start()
    {
        GameScreen.material = DefaultMaterial;
    }

    public void LoadArcadeGame()
    {
        SceneManager.LoadScene(TargetArcadeGame, LoadSceneMode.Additive);
        GameScreen.material = GameMaterial;
    }
    public void UnloadArcadeGame()
    {
        SceneManager.UnloadSceneAsync(TargetArcadeGame);
        GameScreen.material = DefaultMaterial;
    }
}
