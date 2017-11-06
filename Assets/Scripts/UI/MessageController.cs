using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO make this somehow static?
public class MessageController : MonoBehaviourSingleton<MessageController> {

    public Transform messageList;
    public int maxNumberOfMessages = 3;
    public GameObject messagePrefab;
    private int actualNumberOfMessages = 0;

	public void AddMessage(string msg, float dissapearTime = 3f, Color? color = null)
    {
        if(actualNumberOfMessages + 1 > maxNumberOfMessages || msg == "")
        {
            Debug.Log("Maximum number of messages reached.");
            return;
        }
        GameObject messageInstance = Instantiate(messagePrefab, messageList);
        if(color != null) { 
            messageInstance.GetComponent<Text>().color = color.Value;
        }
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
