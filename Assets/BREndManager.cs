using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BREndManager : Singleton<BREndManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ALLDeadEnd()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You Died.");
        dialogs.Add("You All Died.");
        dialogs.Add("No one survive in this battle, which is quite a shock for those who held this 'game'.");
        dialogs.Add("After long time discussion, they decided to stop this game, until they find a better rule.");
        dialogs.Add("That is.. good. I guess.");
        dialogs.Add("But it is not related to you.");
        dialogs.Add("FINAL END");
        dialogs.Add("CLICK TO RESTART GAME");
        DialogManager.CreateViewController(dialogs, RestartGame);
        BGMManager.Instance.PlayBGM(BGMEnum.beBGM);
    }

    public void BadEnd()
    {
        BadEnd(null);
    }

    public void GoodEnd()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You survived. Alone.");
        dialogs.Add("You might have done something bad, but who cares, you are the one who wins.");
        dialogs.Add("You leave the island to face the congratulation and welcome of other adults.");
        dialogs.Add("Don't cry, you are an adult too.");
        dialogs.Add("DOn't cry, they are looking at you.");
        dialogs.Add("You can't show your weakness now. or ever.");
        dialogs.Add("GOOD END?");
        dialogs.Add("CLICK TO RESTART GAME");
        DialogManager.CreateViewController(dialogs, RestartGame);
        BGMManager.Instance.PlayBGM(BGMEnum.geBGM);
    }

    public void TrueEnd()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You survived. With your lover.");
        dialogs.Add("Normally they would not allow this but this time, they were moved by your care about each other.");
        dialogs.Add("You leave the island to face the congratulation and welcome of other adults.");
        dialogs.Add("Don't cry, you are adults now.");
        dialogs.Add("Think about all your classmates that don't have change to be adults.");
        dialogs.Add("You are lucky, at least you have each other.");
        dialogs.Add("You will be good adults and one day, you will come back and save other children");
        dialogs.Add("TRUE END");
        dialogs.Add("CLICK TO RESTART GAME");
        DialogManager.CreateViewController(dialogs, RestartGame);
        BGMManager.Instance.PlayBGM(BGMEnum.geBGM);
    }

    public void BadEnd(HumanInfo killerInfo)
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("You Died.");
        if (killerInfo == null)
        {
            dialogs.Add("You are killed by bomb around your neck, because you were in dead zone.");
            dialogs.Add("You were killed very fast so you didn't feel anything.");
        }
        else
        {
            switch (killerInfo.relationDescriptionEnum)
            {
                case RelationDescriptionEnum.dontKnow:
                case RelationDescriptionEnum.notFamiliar:

                    dialogs.Add("You are killed by "+killerInfo.Name+"whom you are not familiar with. It's good. At least you are not betrayed by those who you care.");
                    dialogs.Add("You are not sure what would happen to your friends or your lover. You hope they will survive.");
                    dialogs.Add("But you are dead now. You actually dont case, and there is nothing you can do now.");
                    break;
                case RelationDescriptionEnum.talkedSeveralTimes:
                case RelationDescriptionEnum.sitAround:
                case RelationDescriptionEnum.playedTogether:
                    dialogs.Add("You are killed by " + killerInfo.Name + " whom you have spent some time before. You thought you can make friends. You sat together, played together, chated with each other");
                    dialogs.Add("Those days were like a decade ago. Suddenly they killed you.");
                    dialogs.Add("You can't keep thinking. You are dead anyway.");

                    break;
                case RelationDescriptionEnum.friend:
                    dialogs.Add("You are killed by your friend " + killerInfo.Name+". How ironic?");
                    dialogs.Add("You shared so much time together before, but " + killerInfo.SubjectiveProunoun() + " choose to kill you.");
                    dialogs.Add("But who knows, maybe, if you have chance, you will be the one who kills " + killerInfo.ObjectiveProunoun());
                    break;
                case RelationDescriptionEnum.bestFriend:
                    dialogs.Add("You are killed by " + killerInfo.Name+", your.. best friend");
                    dialogs.Add("You are a little sad, and a little, happy.");
                    dialogs.Add("At least you don't need to kill " + killerInfo.ObjectiveProunoun());
                    break;
                case RelationDescriptionEnum.lover:
                    dialogs.Add("You are killed by " + killerInfo.Name + ", your.. lover.");
                    dialogs.Add("You can't feel anything.");
                    dialogs.Add("You are glad you died fast enough so you can't feel anything.");
                    break;
            }
        }
        List<HumanInfo> aliveHumans = OtherHumanManager.Instance.aliveHumans;
        HumanInfo winner = aliveHumans[Random.Range(0, aliveHumans.Count)];
        dialogs.Add(winner.Name+" is the winner and the only survivor among you.");
        switch (winner.relationDescriptionEnum)
        {
            case RelationDescriptionEnum.dontKnow:
            case RelationDescriptionEnum.notFamiliar:
            case RelationDescriptionEnum.talkedSeveralTimes:
            case RelationDescriptionEnum.sitAround:
            case RelationDescriptionEnum.playedTogether:
            case RelationDescriptionEnum.friend:
                dialogs.Add("The other "+(HumanManager.Instance.numberOfHuman-1)+" students sleep on this island forever.");
                break;
            case RelationDescriptionEnum.bestFriend:
            case RelationDescriptionEnum.lover:
                dialogs.Add(winner.SubjectiveProunoun(true)+" came back and take your body away, buried near the place you first met");
                if (winner == killerInfo)
                {
                    dialogs.Add(winner.SubjectiveProunoun(true) + " said " + winner.SubjectiveProunoun() + " felt sorry for what " + winner.SubjectiveProunoun() + " has done and ask for your forgive.");
                    dialogs.Add("But we all know you will not be able to reply.");
                } else
                {
                    dialogs.Add(winner.SubjectiveProunoun(true) + " said " + winner.SubjectiveProunoun() + " regret not find you earlier, and maybe both of you can survive.");
                    dialogs.Add("Though we doubt it.");
                    dialogs.Add("Or could this happen?");
                }
                break;
        }
        dialogs.Add("BAD END");
        dialogs.Add("CLICK TO RESTART GAME");
        DialogManager.CreateViewController(dialogs,RestartGame);
        BGMManager.Instance.PlayBGM(BGMEnum.beBGM);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void RestartGameAsk()
    {
        List<string> dialogs = new List<string>();
        dialogs.Add("CLICK TO RESTART GAME");
        DialogManager.CreateViewController(dialogs, RestartGame);
    }
}
