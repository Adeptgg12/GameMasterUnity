using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public GameObject activeMenuisland1;
    public GameObject activeMenuisland2;
    public GameObject userpass;
    public GameObject score;
    public GameObject islandonegamelist;
    public GameObject setting;

    public void Start()
    {
        userpass.SetActive(false);
        score.SetActive(false);
        islandonegamelist.SetActive(false);
        setting.SetActive(false);
    }

    public void ActiveIsland()
    {
        if (activeMenuisland1.activeSelf != true && activeMenuisland2.activeSelf != true)
        {
            activeMenuisland1.SetActive(true);
            activeMenuisland2.SetActive(true);
            userpass.SetActive(false);
            islandonegamelist.SetActive(false);
            score.SetActive(false);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }
    public void ActiveUserpass()
    {
        if (userpass.activeSelf != true && userpass.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(false);
            userpass.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void ActiveScore()
    {
        if (score.activeSelf != true && score.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            islandonegamelist.SetActive(false);
            score.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void Activeislandonegamelist()
    {
        if (islandonegamelist.activeSelf != true && islandonegamelist.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(true);
            setting.SetActive(false);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }

    public void ActiveSettings()
    {
        if (setting.activeSelf != true && setting.activeSelf != true)
        {
            activeMenuisland1.SetActive(false);
            activeMenuisland2.SetActive(false);
            userpass.SetActive(false);
            score.SetActive(false);
            islandonegamelist.SetActive(false);
            setting.SetActive(true);

            AudioSystem.Instance.PlaySFX("Sfx_click");
        }
    }
}
