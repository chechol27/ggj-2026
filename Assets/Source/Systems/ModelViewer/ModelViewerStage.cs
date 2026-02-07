using UnityEngine.SceneManagement;

namespace Source.Systems.ModelViewer
{
    public class ModelViewerStage : GameStage
    {
        public override void OnStateEnter()
        {
            SceneManager.LoadScene("ModelViewer", LoadSceneMode.Single);
        }
        public override void OnStateExit()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}