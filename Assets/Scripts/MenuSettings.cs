using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public GameObject M;
    public SaveFile SF;
    public Languages LANG;
    private int lang;

    public Text labelVolume;
    public Text textVolume;
    private float volume;
    public Text labelLanguage;
    public Image imageLanguage;
    public Sprite[] flags;
    public Text textVariants;
    public GameObject popupVariants;
    public Text textPickVariants;
    public SwitcherBool[] variantsSwitchers;
    public GameObject goNewSession;
    public Text textNewSession;
    public Text textButtonClearData;
    public Text textWarningClearData;
    public Text textButtonGameData;
    public Text textButtonPlayerData;
    public Text textWarningGameData;
    public Text textWarningPlayerData;
    public Text textButtonGameDataYes;
    public Text textButtonGameDataNo;
    public Text textButtonPlayerDataYes;
    public Text textButtonPlayerDataNo;

    private void Awake()
    {
        SF = M.GetComponent<SaveFile>();
        LANG = M.GetComponent<Languages>();
    }

    public void OnEnable()
    {
        lang = LANG.GetLanguage();

        UpdateLanguage();
        volume = SF.getVolume();
        UpdateVolumeText();
        goNewSession.SetActive(true);
    }

    private void OnDisable()
    {
        SF.SetLanguage(lang);
        SF.SetVolume(volume);
        SF.SaveSettingsData();

        SF.bodyguardGuardTwice = variantsSwitchers[0].getState();
        SF.doctorCanSaveBodyguard = variantsSwitchers[1].getState();
        SF.doctorCanSelfHeal = variantsSwitchers[2].getState();
        SF.sheriffFindWerewolfFullMoon = variantsSwitchers[3].getState();
        SF.survivorLynchedDisableTownAbilities = variantsSwitchers[4].getState();
        SF.blackmailedCanVote = variantsSwitchers[5].getState();
        SF.godfatherAloneTwoKills = variantsSwitchers[6].getState();
        SF.executionerPickTarget = variantsSwitchers[7].getState();
        SF.executionerOnlyWinner = variantsSwitchers[8].getState();
        SF.jesterOnlyWinner = variantsSwitchers[9].getState();
        SF.werewolfImmuneFullMoon = variantsSwitchers[10].getState();
        SF.witchDieCursedDie = variantsSwitchers[11].getState();
        SF.blackmailerPlaysBeforeConsigliere = variantsSwitchers[12].getState();

        SF.SaveVariantsData();
    }

    public void ButtonVolumeLeft()
    {
        if (volume <= 0.1f)
            volume = 1;
        else
            volume -= 0.2f;

        UpdateVolumeText();
    }

    public void ButtonVolumeRight()
    {
        if (volume >= 0.9f)
            volume = 0;
        else
            volume += 0.2f;

        UpdateVolumeText();
    }

    private void UpdateVolumeText()
    {
        textVolume.text = (volume * 100).ToString("F0") + "%";
    }

    public void buttonLanguageLeft()
    {
        LANG.previousLanguage();
        lang = LANG.GetLanguage();
        UpdateLanguage();
    }

    public void buttonLanguageRight()
    {
        LANG.nextLanguage();
        lang = LANG.GetLanguage();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        labelVolume.text = LANG.volume[lang];
        labelLanguage.text = LANG.strLanguage[lang];
        imageLanguage.sprite = flags[lang];
        textVariants.text = LANG.variants[lang];
        textNewSession.text = LANG.newSession[lang];
        textButtonClearData.text = LANG.clearData[lang];
    }

    public void ButtonVariants()
    {
        // Language
        textPickVariants.text = LANG.pickVariants[lang];
        variantsSwitchers[0].setOptionTrue(LANG.bodyguardGuardTwiceTrue[lang]);
        variantsSwitchers[0].setOptionFalse(LANG.bodyguardGuardTwiceFalse[lang]);
        variantsSwitchers[1].setOptionTrue(LANG.doctorCanSaveBodyguardTrue[lang]);
        variantsSwitchers[1].setOptionFalse(LANG.doctorCanSaveBodyguardFalse[lang]);
        variantsSwitchers[2].setOptionTrue(LANG.doctorCanSelfHealTrue[lang]);
        variantsSwitchers[2].setOptionFalse(LANG.doctorCanSelfHealFalse[lang]);
        variantsSwitchers[3].setOptionTrue(LANG.sheriffFindWerewolfFullMoonTrue[lang]);
        variantsSwitchers[3].setOptionFalse(LANG.sheriffFindWerewolfFullMoonFalse[lang]);
        variantsSwitchers[4].setOptionTrue(LANG.survivorLynchedDisableTownAbilitiesTrue[lang]);
        variantsSwitchers[4].setOptionFalse(LANG.survivorLynchedDisableTownAbilitiesFalse[lang]);
        variantsSwitchers[5].setOptionTrue(LANG.blackmailedCanVoteTrue[lang]);
        variantsSwitchers[5].setOptionFalse(LANG.blackmailedCanVoteFalse[lang]);
        variantsSwitchers[6].setOptionTrue(LANG.godfatherAloneTwoKillsTrue[lang]);
        variantsSwitchers[6].setOptionFalse(LANG.godfatherAloneTwoKillsFalse[lang]);
        variantsSwitchers[7].setOptionTrue(LANG.executionerPickTargetTrue[lang]);
        variantsSwitchers[7].setOptionFalse(LANG.executionerPickTargetFalse[lang]);
        variantsSwitchers[8].setOptionTrue(LANG.executionerOnlyWinnerTrue[lang]);
        variantsSwitchers[8].setOptionFalse(LANG.executionerOnlyWinnerFalse[lang]);
        variantsSwitchers[9].setOptionTrue(LANG.jesterOnlyWinnerTrue[lang]);
        variantsSwitchers[9].setOptionFalse(LANG.jesterOnlyWinnerFalse[lang]);
        variantsSwitchers[10].setOptionTrue(LANG.werewolfImmuneFullMoonTrue[lang]);
        variantsSwitchers[10].setOptionFalse(LANG.werewolfImmuneFullMoonFalse[lang]);
        variantsSwitchers[11].setOptionTrue(LANG.witchDieCursedDieTrue[lang]);
        variantsSwitchers[11].setOptionFalse(LANG.witchDieCursedDieFalse[lang]);
        variantsSwitchers[12].setOptionTrue(LANG.blackmailerPlaysBeforeConsigliereTrue[lang]);
        variantsSwitchers[12].setOptionFalse(LANG.blackmailerPlaysBeforeConsigliereFalse[lang]);

        variantsSwitchers[0].setState(SF.bodyguardGuardTwice);
        variantsSwitchers[1].setState(SF.doctorCanSaveBodyguard);
        variantsSwitchers[2].setState(SF.doctorCanSelfHeal);
        variantsSwitchers[3].setState(SF.sheriffFindWerewolfFullMoon);
        variantsSwitchers[4].setState(SF.survivorLynchedDisableTownAbilities);
        variantsSwitchers[5].setState(SF.blackmailedCanVote);
        variantsSwitchers[6].setState(SF.godfatherAloneTwoKills);
        variantsSwitchers[7].setState(SF.executionerPickTarget);
        variantsSwitchers[8].setState(SF.executionerOnlyWinner);
        variantsSwitchers[9].setState(SF.jesterOnlyWinner);
        variantsSwitchers[10].setState(SF.werewolfImmuneFullMoon);
        variantsSwitchers[11].setState(SF.witchDieCursedDie);
        variantsSwitchers[12].setState(SF.blackmailerPlaysBeforeConsigliere);

        popupVariants.SetActive(true);
    }

    public void ButtonNewSession()
    {
        goNewSession.SetActive(false);
        SF.NewSession();
    }

    public void ButtonResetData()
    {
        //Language
        textWarningClearData.text = LANG.warningClearData[lang];
        textButtonGameData.text = LANG.gameData[lang];
        textButtonPlayerData.text = LANG.playerData[lang];
        textWarningGameData.text = LANG.areYouSure[lang];
        textWarningPlayerData.text = LANG.areYouSure[lang];
        textButtonGameDataYes.text = LANG.yes[lang];
        textButtonGameDataNo.text = LANG.no[lang];
        textButtonPlayerDataYes.text = LANG.yes[lang];
        textButtonPlayerDataNo.text = LANG.no[lang];
    }

    public void ConfirmResetPlayerData()
    {
        SF.ResetPlayerData();
    }

    public void ConfirmResetGameData()
    {
        SF.ResetGameData();
    }
}
