using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static Define;

public class CutSceneBgmManager : UI_Scene
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/BGM_CutScene", Sound.Bgm);
        Managers.Cutscene.cutFinished = false;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        CutScene(); // ���콺 Ŭ�� ������ �ƾ� ����

        if (GameObject.Find("CheckPoint1") != null)
        {
            Managers.Sound.GetCurrent().volume = 0.1f;
        }

        if (GameObject.Find("CheckPoint2") != null)
        {
            Managers.Sound.Clear();
        }

        if (GameObject.Find("CheckPoint3") != null)
        {
            Managers.Sound.Play("BGM/Sound_Beep", Sound.Bgm);

        }

        if (GameObject.Find("CheckPoint4") != null)
        {
            Managers.Sound.Clear();
            Managers.Sound.Play("Explosion 1", Sound.Effect, 0.5f);
        }
    }

    void CutScene()
    {
        // ���콺 Ŭ���� �ޱ� ���� update�� ����
        // �ƾ��� �� ���� ���¿�����, ���丮 ��忡���� �����ϵ���
        if (Managers.Cutscene.cutFinished == false && Managers.Game.Mode == Define.Mode.StoryMode)
        {
            // ������ �ƾ��� ������ ��� �ƾ� ����
            if (Managers.Cutscene.SceneNumber == 14 && Input.GetMouseButtonDown(0))
            {
                Managers.Cutscene.DistroyCutscene(); // ����
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                Managers.UI.ShowSceneUI<UI_Game>();
            }
            // Ŭ������ �� ���� ������
            else if (Input.GetMouseButtonDown(0))
                Managers.Cutscene.PlayCutscene();
            // ���� ���� �� �ƾ� �ε�ǵ���; storymode �Լ��� �ٿ��� �� �̻��� ���� �߻��ؼ� ����� �ű�
            //else if (Managers.Cutscene.SceneNumber == 0)
            //    Managers.Cutscene.PlayCutscene();
        }
    }
}
