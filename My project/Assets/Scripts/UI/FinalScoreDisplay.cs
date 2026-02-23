using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] private PlayerScoreDisplay playerScoreDisplayPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject contentContainer;
    [SerializeField] private float animationDuration = 2f;

    void OnEnable()
    {
        GameManager.Instance.LevelCompleted += OnLevelCompleted;
        panel.SetActive(false);
    }

    private void OnLevelCompleted(PlayerData[] obj)
    {
        panel.SetActive(true);
        StartCoroutine(PanelAnimation());

        foreach (PlayerData player in obj)
        {
            PlayerScoreDisplay display = Instantiate(playerScoreDisplayPrefab, contentContainer.transform);
            display.Label = $"{player.Name} : {player.Score}";

            StartCoroutine(PlayerScoreDisplayAnimation(display, 1f));
        }
    }
    private IEnumerator PanelAnimation()
    {
        float timer = 0;
        Image panelImage = panel.GetComponent<Image>();
        Color panelTargetColour = panelImage.color;

        Color panelStartColour = panelTargetColour;
        panelStartColour.a = 0f;

        panelImage.color = panelStartColour;

        while (timer < animationDuration)
        {
            float t = timer / animationDuration;

            panelImage.color = Color.Lerp(panelStartColour,panelTargetColour,t);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PlayerScoreDisplayAnimation(PlayerScoreDisplay display, float delay = 0)
    {
        float timer = 0f;

        // Store original colours
        Color targetLabelColour = display.LabelColour;
        Color targetImageColour = display.Background.color;

        // Start fully transparent
        Color startLabelColour = targetLabelColour;
        startLabelColour.a = 0f;

        Color startImageColour = targetImageColour;
        startImageColour.a = 0f;

        display.LabelColour = startLabelColour;
        display.Background.color = startImageColour;

        yield return new WaitForSeconds(delay);

        while (timer < animationDuration)
        {
            float t = timer / animationDuration;

            display.LabelColour = Color.Lerp(startLabelColour, targetLabelColour, t);
            display.Background.color = Color.Lerp(startImageColour, targetImageColour, t);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final colour is exact
        display.LabelColour = targetLabelColour;
        display.Background.color = targetImageColour;
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnNextLevelButtonClicked()
    {
        ApplicationController.Instance.LoadNextLevel();
    }

    void OnDisable()
    {
        GameManager.Instance.LevelCompleted -= OnLevelCompleted;
    }
}
