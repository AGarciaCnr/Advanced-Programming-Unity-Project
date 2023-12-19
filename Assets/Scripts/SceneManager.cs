using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static int _score;

    // Carga la escena del men� principal.
    public static void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameScenes.MAIN_MENU);
    }

    // Carga la escena de juego.
    public static void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameScenes.GAME);
    }

    // Carga la escena de Game Over y almacena la puntuaci�n.
    public static void LoadGameOver(int score)
    {
        _score = score;
        // Suscribe la funci�n OnSceneLoaded al evento SceneManager.sceneLoaded
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameScenes.GAME_OVER);
    }

    // Funci�n subscrita al evento que utilizamos para verificar que la escena ha sido completamente cargada para evitar errores de referencia nula.
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es la de Game Over.
        if (scene.name == GameScenes.GAME_OVER)
        {
            SetScoreOnGameOverCanvas(_score);
            // Desvincula el evento despu�s de realizar la acci�n para evitar m�ltiples suscripciones.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // Establece la puntuaci�n en el GameOverCanvasManager.
    private static void SetScoreOnGameOverCanvas(int score)
    {
        GameOverCanvasManager gameOverCanvasManager = GameOverCanvasManager.instance;
        if (gameOverCanvasManager != null)
        {
            gameOverCanvasManager.SetScore(score);
        }
    }
}
