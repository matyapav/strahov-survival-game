using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {

    public int maxNumberOfMessages = 3;
    public GameObject messagePrefab;

    private int actualNumberOfMessages = 0;

	public void AddMessage(string msg, float dissapearTime = 3f)
    {
        if(actualNumberOfMessages + 1 > maxNumberOfMessages || msg == "")
        {
            Debug.Log("Maximum number of messages reached.");
            return;
        }
        GameObject messageInstance = Instantiate(messagePrefab, transform);
        messageInstance.GetComponent<Text>().text = msg;
        StartCoroutine(DestroyMessage(messageInstance, dissapearTime));
        actualNumberOfMessages++;
    }

    private IEnumerator DestroyMessage(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(messageInstance);
        actualNumberOfMessages--;
    }
}
