using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{
    public UnityEvent OnCorrectAwnser;
    public UnityEvent OnIncorrectAwnser;

    [SerializeField] private List<DialogueSettings> _dialogueSettings;
    [SerializeField] private TextMeshProUGUI _dialogueTxt;
    [SerializeField] private TextMeshProUGUI _reactionATxt;
    [SerializeField] private TextMeshProUGUI _reactionBTxt;
    [SerializeField] private Button _reactionABtn;
    [SerializeField] private Button _reactionBBtn;

    private bool isReactionACorrect;

    private void Awake()
    {
        _reactionABtn.onClick.AddListener(() =>
        {
            if (isReactionACorrect) OnCorrectAwnser.Invoke();
            else OnIncorrectAwnser.Invoke();
        });

        _reactionBBtn.onClick.AddListener(() =>
        {
            if (!isReactionACorrect) OnCorrectAwnser.Invoke();
            else OnIncorrectAwnser.Invoke();
        });
    }

    private void Start() => DisplayNewDialogue();

    public void DisplayNewDialogue()
    {
        DialogueSettings dialogueSettings = _dialogueSettings[0];
        _dialogueSettings.Remove(dialogueSettings);

        _dialogueTxt.text = dialogueSettings.Dialogue;

        int r = Random.Range(0, 1);

        if (r > .5) 
        {
            _reactionATxt.text = dialogueSettings.CorrectReaction;
            _reactionBTxt.text = dialogueSettings.IncorrectReaction;
            isReactionACorrect = true;
        }
        else
        {
            _reactionATxt.text = dialogueSettings.IncorrectReaction;
            _reactionBTxt.text = dialogueSettings.CorrectReaction;
            isReactionACorrect = false;
        }

    }
}
