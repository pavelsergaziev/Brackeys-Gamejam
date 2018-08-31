using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _notificationsPanel;

    public void ShowNotification(string text, float time)
    {
        _notificationsPanel.transform.GetComponentInChildren<Text>().text = text;
        WaitForSeconds notificationShowTime = new WaitForSeconds(time);
        StartCoroutine("ShowNotificationsPanel", notificationShowTime);
    }

    private IEnumerator ShowNotificationsPanel(WaitForSeconds notificationShowTime)
    {        
        _notificationsPanel.SetActive(true);
        yield return notificationShowTime;
        _notificationsPanel.SetActive(false);
    }

}
