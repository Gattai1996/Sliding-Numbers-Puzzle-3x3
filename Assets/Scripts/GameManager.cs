using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<Vector3> positionsList;
    private List<ImageController> imagesList;
    private Queue positionsQueue;
    [SerializeField] private Transform slot;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject victoryPanel;
    private Vector3 slotInitialPosition;
    private AudioManager audioManager;

    private void Start()
    {
        Instance = this;
        slotInitialPosition = slot.position;

        imagesList = new List<ImageController>();

        foreach (GameObject image in GameObject.FindGameObjectsWithTag("Image"))
        {
            imagesList.Add(image.GetComponent<ImageController>());
        }

        RandomizePositions();
        victoryPanel.SetActive(false);
        audioManager = GetComponent<AudioManager>();
        audioManager.Play("Music");
    }

    private void UpdatePositionsList()
    {
        positionsList = new List<Vector3>();

        foreach (GameObject referencePoint in GameObject.FindGameObjectsWithTag("Reference Point"))
        {
            positionsList.Add(referencePoint.transform.position);
        }
    }

    public void CheckVictory()
    {
        for (int i = 0; i < imagesList.Count; i++)
        {
            if (imagesList[i].transform.position != imagesList[i].CorrectPosition)
            {
                return;
            }
        }

        victoryPanel.SetActive(true);
        audioManager.Play("Win");
    }

    public void RandomizePositions()
    {
        UpdatePositionsList();
        UpdatePositionsQueue();

        for (int i = 0; i < imagesList.Count; i++)
        {
            if (positionsQueue.Count > 0)
                imagesList[i].SetPosition((Vector3)positionsQueue.Dequeue());
        }

        ResetSlotPosition();
    }

    private void UpdatePositionsQueue()
    {
        positionsQueue = new Queue();

        for (int i = 0; i < positionsList.Count; i++)
        {
            int j = Random.Range(i, positionsList.Count);
            Vector3 temp = positionsList[i];
            positionsList[i] = positionsList[j];
            positionsList[j] = temp;
            positionsQueue.Enqueue(positionsList[i]);
        }
    }

    public void SolvePuzzle()
    {
        for (int i = 0; i < imagesList.Count; i++)
        {
            imagesList[i].Solve();
        }

        ResetSlotPosition();
    }

    public void RestartGame()
    {
        RandomizePositions();
        victoryPanel.SetActive(false);
    }

    private void ResetSlotPosition()
    {
        slot.position = GameObject.FindWithTag("Slot Reference Point").transform.position;
    }
}
