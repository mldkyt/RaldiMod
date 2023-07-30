using MelonLoader;
using System;
using UnityEngine;

namespace RaldiMod
{
    public class RaldiMod : MelonMod
    {
        private bool _show;
        
        private bool _baldiDoesntMove = false;
        private bool _baldiDoesntHear = false;
        private bool _cowDisabled = false;
        public static bool FurryDisabled = false;
        private bool _gunnerDisabled = false;
        private bool _monke = false;
        private bool _vanManDisabled = false;
        private bool _mrBreastDisabled = false;
        private bool _shakyGUI = false;
        private bool _goofyAhhTeleportingFemboy = false;
        private bool _unlimitedStamina = false;

        private float _shakyGUITimer = 0;
        private float _shakyGUITimerSet = 0.5f;

        private float _r = 1;
        private float _g;
        private float _b;
        private float _time;

        public override void OnInitializeMelon()
        {
            HarmonyInstance.PatchAll();
            base.OnInitializeMelon();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                _show = !_show;
            }
            
            if (_baldiDoesntMove)
            {
                GameControllerScript.current.baldi.timeToMove = 1;
            }

            if (_baldiDoesntHear)
            {
                GameControllerScript.current.baldi.antiHearing = true;
                GameControllerScript.current.baldi.antiHearingTime = 1;
            }

            if (_cowDisabled)
            {
                GameControllerScript.current.polishCow.coolDown = 1;
            }

            if (FurryDisabled)
            {
                GameControllerScript.current.chipfloke.seesRuleBreak = false;
                GameControllerScript.current.chipfloke.coolDown = 1;
            }

            if (_gunnerDisabled)
            {
                GameControllerScript.current.beans.cooldown = 1;
            }

            if (_monke && !GameControllerScript.current.player.isMonke)
            {
                GameControllerScript.current.player.BecomeMonke();
            }

            if (_vanManDisabled)
            {
                GameControllerScript.current.vanman.coolDown = 1;
                GameControllerScript.current.vanman.playCool = 1;
                GameControllerScript.current.vanman.kidnapTime = 0;
            }

            if (_mrBreastDisabled)
            {
                GameControllerScript.current.mrBeast.coolDown = 1;
                if (GameControllerScript.current.mrBeast.inChallenge)
                    GameControllerScript.current.mrBeast.challengeTime = 0;
            }

            if (_goofyAhhTeleportingFemboy)
            {
                GameControllerScript.current.crafters.cooldown = 1;
                GameControllerScript.current.crafters.gettingAngry = false;
                GameControllerScript.current.crafters.anger = 0;
            }

            if (_shakyGUI)
            {
                _shakyGUITimer += Time.deltaTime;
                if (_shakyGUITimer > _shakyGUITimerSet)
                {
                    GameControllerScript.current.hudAnimator.SetBool("Direction", !GameControllerScript.current.hudAnimator.GetBool("Direction"));
                    GameControllerScript.current.hudAnimator.SetTrigger("Hit");
                    _shakyGUITimer = 0;
                }
            }

            if (_unlimitedStamina)
            {
                GameControllerScript.current.player.stamina = 99;
            }

            _time += Time.deltaTime * 15;
            _r = (int)(Math.Sin(2 * Math.PI / 50 * _time) * 127 + 128);
            _g = (int)(Math.Sin(2 * Math.PI / 50 * (_time + 50 / 3)) * 127 + 128);
            _b = (int)(Math.Sin(2 * Math.PI / 50 * (_time + 2 * 50 / 3)) * 127 + 128);

            base.OnUpdate();
        }

        public override void OnGUI()
        {
            GUI.color = new Color(_r / 255, _g / 255, _b / 255);
            var originalFontSize = GUI.skin.label.fontSize;
            GUI.skin.label.fontSize = 28;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(25, 25, 1000, 250), "RaldiMod v1.1 by mldkyt for v1.6.2 - PRESS F2 TO OPEN!");
            GUI.skin.label.fontSize = originalFontSize;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            GUI.color = Color.white;
            if (_show)
            {
                float w = Screen.width;
                float h = Screen.height;
                GUI.color = new Color(_r / 255, _g / 255, _b / 255);
                GUI.Box(new Rect(w / 2 - 150, h / 2 - 400, 300, 800), "BaldiMod v1.1 by mldkyt");
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 380, 290, 20),
                        "X"))
                {
                    _show = false;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 360, 290, 20),
                        $"Raldi doesn't move: {_baldiDoesntMove}"))
                {
                    _baldiDoesntMove = !_baldiDoesntMove;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 340, 290, 20),
                        $"Raldi doesn't hear: {_baldiDoesntHear}"))
                {
                    _baldiDoesntHear = !_baldiDoesntHear;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 320, 290, 20),
                        "Kiss Raldi"))
                {
                    GameControllerScript.current.baldi.Kill(999999);
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 300, 290, 20),
                        $"Disable cow: {_cowDisabled}"))
                {
                    _cowDisabled = !_cowDisabled;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 280, 290, 20),
                        $"Disable ANNOYING FURRY: {FurryDisabled}"))
                {
                    FurryDisabled = !FurryDisabled;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 260, 290, 20),
                        $"Disable GOOFY GUNNER: {_gunnerDisabled}"))
                {
                    _gunnerDisabled = !_gunnerDisabled;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 240, 290, 20),
                        $"Disable VAN: {_vanManDisabled}"))
                {
                    _vanManDisabled = !_vanManDisabled;
                }
                
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 220, 290, 20),
                        $"Disable MISSED HER BREAST: {_mrBreastDisabled}"))
                {
                    _mrBreastDisabled = !_mrBreastDisabled;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 200, 290, 20),
                        $"Disable BOOTY AHH TELEPORTING FEMBOY: {_goofyAhhTeleportingFemboy}"))
                {
                    _goofyAhhTeleportingFemboy = !_goofyAhhTeleportingFemboy;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 180, 290, 20),
                        $"MISSED HER BREASTLOVANIA"))
                {
                    GameControllerScript.current.beastlovania.clip =
                        GameControllerScript.current.mrBeast.mrbeastMegaloLmao;
                    GameControllerScript.current.beastlovania.Play();
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 160, 290, 20),
                        $"Femboy Nervosity (Monke mode): {_monke}"))
                {
                    _monke = !_monke;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 140, 290, 20),
                        $"Twerking GUI: {_shakyGUI}"))
                {
                    _shakyGUI = !_shakyGUI;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 120, 290, 20), "Add 10% stamina"))
                {
                    GameControllerScript.current.player.stamina += 10;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 100, 290, 20), "Add 50% stamina"))
                {
                    GameControllerScript.current.player.stamina += 50;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 80, 290, 20), "Add 100% stamina"))
                {
                    GameControllerScript.current.player.stamina += 100;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 60, 290, 20), "Add 250% stamina"))
                {
                    GameControllerScript.current.player.stamina += 250;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 40, 290, 20), "Add 1000% stamina"))
                {
                    GameControllerScript.current.player.stamina += 1000;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2 - 20, 290, 20), $"Infinite Stamina: {_unlimitedStamina}"))
                {
                    _unlimitedStamina = !_unlimitedStamina;
                }

                if (GUI.Button(new Rect(w / 2 - 145, h / 2, 290, 20), "Add $1"))
                {
                    GameControllerScript.current.money.money += 1;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 20, 290, 20), "Add $10"))
                {
                    GameControllerScript.current.money.money += 10;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 40, 290, 20), "Add $100"))
                {
                    GameControllerScript.current.money.money += 100;
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 60, 290, 20), "Add Ender Pearl"))
                {
                    // 1: Zesty Bar
                    // 2: Yellow Door Lock
                    // 3: Principal Keys
                    // 4: BSODA
                    // 5: Quarter
                    // 6: Anti-Hearing Tape
                    // 7: Alarm Clock
                    // 8: WD-NoSquee
                    // 9: Glock
                    // 10: Big Ol' Boots
                    // 22: EPearl
                    // 17: 15 second energy
                    // 25: Polish Marker
                    GameControllerScript.current.CollectItem(22);
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 80, 290, 20), "Add Crackpipe"))
                {
                    GameControllerScript.current.CollectItem(18);
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 100, 290, 20), "Add BSODA"))
                {
                    GameControllerScript.current.CollectItem(4);
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 120, 290, 20), "Add Zesty Bar"))
                {
                    GameControllerScript.current.CollectItem(1);
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 140, 290, 20), "Add 15 second energy"))
                {
                    GameControllerScript.current.CollectItem(17);
                }
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 160, 290, 20), "Add iPhone"))
                {
                    GameControllerScript.current.CollectItem(14);
                }

                GUI.skin.label.fontStyle = FontStyle.Bold;
                if (GUI.Button(new Rect(w / 2 - 145, h / 2 + 180, 290, 40), "MLDKYTS WEBSITE"))
                {
                    Application.OpenURL("https://mldkyt.com");
                    Application.OpenURL("https://mldkyt.com/pron");
                    Application.OpenURL("https://mldkyt.com/social");
                    Application.OpenURL("https://mldkyt.com/projects");
                    Application.OpenURL("https://mldkyt.com/requisha");
                }

                GUI.skin.label.fontStyle = FontStyle.Normal;

                GUI.Label(new Rect(w / 2 - 145, h / 2 + 320, 140, 20),
                    "Twerking speed: ");
                _shakyGUITimerSet = GUI.HorizontalSlider(new Rect(w / 2 + 5, h / 2 + 320, 140, 20),
                    _shakyGUITimerSet, 0.1f, 2.0f);
                GUI.color = Color.white;
            }
            base.OnGUI();
        }
    }
}