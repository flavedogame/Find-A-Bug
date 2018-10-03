[1mdiff --git a/Assets/StartupSteps.cs b/Assets/StartupSteps.cs[m
[1mindex 27b808f..49222b3 100644[m
[1m--- a/Assets/StartupSteps.cs[m
[1m+++ b/Assets/StartupSteps.cs[m
[36m@@ -16,8 +16,8 @@[m [mpublic class StartupSteps : MonoBehaviour {[m
         //CheatManager.Instance.Init();[m
         //CurrencyManager.Instance.Init();[m
         //AbilityManager.Instance.Init();[m
[31m-        //AchievementManager.Instance.Init();[m
[31m-        //NarrativeManager.Instance.Init();[m
[32m+[m[32m        AchievementManager.Instance.Init();[m[41m[m
[32m+[m[32m        NarrativeManager.Instance.Init();[m[41m[m
         //TutorialManager.Instance.Init();[m
         //MonsterManager.Instance.Init();[m
         //LevelManager.Instance.Init();[m
[1mdiff --git a/Assets/StreamingAssets/achievement.csv b/Assets/StreamingAssets/achievement.csv[m
[1mindex 9150eba..fa9f513 100644[m
[1m--- a/Assets/StreamingAssets/achievement.csv[m
[1m+++ b/Assets/StreamingAssets/achievement.csv[m
[36m@@ -1,4 +1,3 @@[m
 identifier,prerequisite,achievementStep,fallThrough,description,isTutorial,reward,comment[m
[31m-firstStartPlaying,,firstStartPlayingStep,,get Into Start Playing,TRUE,,tutorial0[m
[31m-changeColor,firstStartPlaying,changeColorStep,,change Color,TRUE,,tutorial1[m
[31m-passCorrectColor,changeColor,passCorrectColorStep,,pass Correct Color,TRUE,,tutorial2[m
[32m+[m[32mfinishIntroduction,,,,,1,,[m[41m[m
[32m+[m[32mfinishWallBug,,,,,1,,[m[41m[m
[1mdiff --git a/Assets/StreamingAssets/achievementStep.csv b/Assets/StreamingAssets/achievementStep.csv[m
[1mindex 13abdc2..6ddbef2 100644[m
[1m--- a/Assets/StreamingAssets/achievementStep.csv[m
[1m+++ b/Assets/StreamingAssets/achievementStep.csv[m
[36m@@ -1,4 +1 @@[m
 identifier,requirementClassString,requirementAmount,category[m
[31m-firstStartPlayingStep,PlayerReachedLocationRequirement,1,0[m
[31m-changeColorStep,PlayerReachedLocationRequirement,1,1.48[m
[31m-passCorrectColorStep,PlayerReachedLocationRequirement,1,3.57[m
