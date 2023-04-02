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
        CutScene(); // 마우스 클릭 때마다 컷씬 변경

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
        // 마우스 클릭을 받기 위해 update로 넣음
        // 컷씬이 안 끝난 상태에서만, 스토리 모드에서만 동작하도록
        if (Managers.Cutscene.cutFinished == false && Managers.Game.Mode == Define.Mode.StoryMode)
        {
            // 마지막 컷씬이 끝나면 모든 컷씬 삭제
            if (Managers.Cutscene.SceneNumber == 14 && Input.GetMouseButtonDown(0))
            {
                Managers.Cutscene.DistroyCutscene(); // 삭제
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                Managers.UI.ShowSceneUI<UI_Game>();
            }
            // 클릭했을 때 다음 씬으로
            else if (Input.GetMouseButtonDown(0))
                Managers.Cutscene.PlayCutscene();
            // 게임 시작 시 컷씬 로드되도록; storymode 함수에 붙였을 땐 이상한 버그 발생해서 여기로 옮김
            //else if (Managers.Cutscene.SceneNumber == 0)
            //    Managers.Cutscene.PlayCutscene();
        }
    }
}
