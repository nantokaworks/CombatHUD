using Oxide.Core;
using Oxide.Game.Rust.Cui;
using System;
using System.Collections.Generic;

namespace Oxide.Plugins {
  [Info("CombatHud", "snicyme", "0.1.0")]
  [Description("Combat Damage on Hud XD")]

  public class CombatHUD : RustPlugin {

    #region json

    private static string TEMPLATE_BASE = @"
 [
  {
    ""name"": ""CH-Hud"",
    ""parent"": ""Hud"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""0 0 0 0""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.344 0.114"",
        ""anchormax"": ""0.64 0.175""
      }
    ]
  },
  {
    ""name"": ""attack"",
    ""parent"": ""CH-Hud"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.2""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0 0.545"",
        ""anchormax"": ""1 1""
      }
    ]
  },
  {
    ""name"": ""icon_damage"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.8"",
        ""sprite"": ""{damage_icon}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.008 0.15"",
        ""anchormax"": ""0.045 0.85""
      }
    ]
  },
  {
    ""name"": ""label_damage"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Text"",
        ""color"": ""1 1 1 0.8"",
        ""fontSize"": 14,
        ""align"": ""MiddleCenter"",
        ""text"": ""{damage_text}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.066 -0.05"",
        ""anchormax"": ""0.317 1""
      }
    ]
  },
  {
    ""name"": ""icon_distance"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.8"",
        ""sprite"": ""assets/icons/target.png""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.675 0.15"",
        ""anchormax"": ""0.712 0.85""
      }
    ]
  },
  {
    ""name"": ""label_distance"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Text"",
        ""color"": ""1 1 1 0.8"",
        ""fontSize"": 14,
        ""align"": ""MiddleCenter"",
        ""text"": ""{distance_text}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.736 -0.05"",
        ""anchormax"": ""0.987 1""
      }
    ]
  },
  {
    ""name"": ""icon_info"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.8"",
        ""sprite"": ""assets/icons/check.png""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.34 0.15"",
        ""anchormax"": ""0.377 0.85""
      }
    ]
  },
  {
    ""name"": ""label_info"",
    ""parent"": ""attack"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Text"",
        ""color"": ""1 1 1 0.8"",
        ""fontSize"": 14,
        ""align"": ""MiddleCenter"",
        ""text"": ""{info_text}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.391 -0.05"",
        ""anchormax"": ""0.665 1""
      }
    ]
  },
  {
    ""name"": ""target"",
    ""parent"": ""CH-Hud"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.2""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0 0"",
        ""anchormax"": ""1 0.455""
      }
    ]
  },
  {
    ""name"": ""icon_health"",
    ""parent"": ""target"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.8"",
        ""sprite"": ""assets/icons/health.png""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.675 0.15"",
        ""anchormax"": ""0.712 0.85""
      }
    ]
  },
  {
    ""name"": ""guage_health"",
    ""parent"": ""target"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""0.64 0.89 0.2 {guage_alpha}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.736 0.15"",
        ""anchormax"": ""{health_guage} 0.85""
      }
    ]
  },
  {
    ""name"": ""label_health"",
    ""parent"": ""target"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Text"",
        ""color"": ""1 1 1 0.8"",
        ""fontSize"": 14,
        ""align"": ""MiddleCenter"",
        ""text"": ""{health_text}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.736 -0.05"",
        ""anchormax"": ""0.987 1""
      }
    ]
  },
  {
    ""name"": ""label_target"",
    ""parent"": ""target"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Text"",
        ""color"": ""1 1 1 0.8"",
        ""fontSize"": 14,
        ""align"": ""MiddleCenter"",
        ""text"": ""{target_text}""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.045 -0.05"",
        ""anchormax"": ""0.701 1""
      }
    ]
  },
  {
    ""name"": ""icon_target"",
    ""parent"": ""target"",
    ""components"": [
      {
        ""type"": ""UnityEngine.UI.Image"",
        ""color"": ""1 1 1 0.8"",
        ""sprite"": ""assets/icons/fun.png""
      },
      {
        ""type"": ""RectTransform"",
        ""anchormin"": ""0.008 0.15"",
        ""anchormax"": ""0.045 0.85""
      }
    ]
  }
]
    ";

    #endregion

    #region Field

    List<ulong> EnablePlayer { get; set; }

    Dictionary<ulong, long> CloseTimes = new Dictionary<ulong, long>();

    Dictionary<string, string> DamageTypeImages
      = new Dictionary<string, string>()
      {
        {"Generic", "assets/icons/weapon.png"},
        {"Hunger", "assets/icons/weapon.png"},
        {"Thirst", "assets/icons/weapon.png"},
        {"Cold", "assets/icons/freezing.png"},
        {"Drowned", "assets/icons/drowning.png"},
        {"Heat", "assets/icons/fire.png"},
        {"Bleeding", "assets/icons/bleeding.png"},
        {"Poison", "assets/icons/poison.png"},
        {"Suicide", "assets/icons/weapon.png"},
        {"Bullet", "assets/icons/bullet.png"},
        {"Slash", "assets/icons/slash.png"},
        {"Blunt", "assets/icons/blunt.png"},
        {"Fall", "assets/icons/fall.png"},
        {"Radiation", "assets/icons/radiation.png"},
        {"Bite", "assets/icons/bite.png"},
        {"Stab", "assets/icons/stab.png"},
        {"Explosion", "assets/icons/explosion.png"},
        {"Radiation Exposure", "assets/icons/radiation.png"},
        {"Cold Exposure", "assets/icons/freezing.png"},
        {"Decay", "assets/icons/weapon.png"},
        {"Electric Shock", "assets/icons/electric.png"},
        {"Arrow", "assets/icons/bullet.png"},
        {"Anti Vehicle", "assets/icons/weapon.png"},
        {"Collision", "assets/icons/weapon.png"},
        {"Fun_Water", "assets/icons/weapon.png"}
      };

    double minGuage = 0.736;
    double maxGuage = 0.987;
    double diffGuage = 0.987 - 0.736;
    double guageAlpha = 0.77;

    const string M_PREFIX = "Prefix";
    const string M_ENABLE = "HUD Enable";
    const string M_DISABLE = "hud Disable";
    const string M_HELP = "Help message";

    #endregion

    #region Localization
    protected override void LoadDefaultMessages() => lang.RegisterMessages(defmessages, this, "en");

    void SendMsg(BasePlayer player, string message, params string[] args) => player.ChatMessage($"{lang.GetMessage(M_PREFIX, this)} {string.Format(lang.GetMessage(message, this), args)}");

    Dictionary<string, string> defmessages = new Dictionary<string, string> {
      [M_PREFIX] = "<color=#66ff66>[</color>CombatHUD<color=#66ff66>]</color>",
      [M_ENABLE] = "<color=yellow>{0}</color> HUD is Enabled :)",
      [M_DISABLE] = "<color=yellow>{0}</color> HUD is Disabled XD",
      [M_HELP] = "\n<color=yellow>HUD</color>\n<color=#66ff66>{0}</color>\n\n<color=yellow>command usage</color>\n<color=#36a1d8>/ch on</color> enable HUD.\n<color=#36a1d8>/ch off</color> disable HUD.\n<color=#36a1d8>/ch</color> this help.",
    };
    #endregion

    #region Commands

    [ChatCommand("ch")]
    void CmdCH(BasePlayer player, string command, string[] args) {
      switch (args.Length) {
        case 1:
          Boolean handled = false;
          String subCmd = args[0];
          // safe mode
          if (subCmd == "on" || subCmd == "1") {
            if (!EnablePlayer.Contains(player.userID)) {
              EnablePlayer.Add(player.userID);
            }
            SendMsg(player, M_ENABLE, player.displayName);
            handled = true;
          } else if (subCmd == "off" || subCmd == "0") {
            if (EnablePlayer.Contains(player.userID)) {
              EnablePlayer.Remove(player.userID);
            }
            SendMsg(player, M_DISABLE, player.displayName);
            handled = true;
          }

          if (!handled) {
            SendMsg(player, M_HELP, EnablePlayer.Contains(player.userID).ToString());
          }
          break;
        default:
          SendMsg(player, M_HELP, EnablePlayer.Contains(player.userID).ToString());
          break;
      }

    }

    #endregion

    #region Utilities
    string FirstUpper(string s) {

      if (string.IsNullOrEmpty(s)) return string.Empty;

      string phrase = "";
      char[] delimiterChars = { ' ', '_', '.' };

      foreach (string word in s.Split(delimiterChars)) {
        if (word == string.Empty) continue;
        phrase = phrase + char.ToUpper(word[0]) + word.Substring(1) + " ";
      };

      if (phrase.EndsWith(" ")) phrase = phrase.Substring(0, phrase.Length - 1);

      return phrase;
    }

    string GetVictimName(BaseCombatEntity victim) {
      string victimName = "Unknown Target";
 
      if (victim == null) return victimName;

      if (victim.ToPlayer() != null && !victim.IsNpc) {

        victimName = victim.ToPlayer().displayName;
      } else {

        string temp = (victim.ShortPrefabName.Trim() ?? "Unknown Target").Replace(".prefab", "").Replace("npc", "").Trim('_').Trim('.');
        victimName = FirstUpper(temp);
      }


      return victimName;
    }

    #endregion

    #region Hooks

    void Init() {
      // make enable players
      if (Interface.Oxide.DataFileSystem.ExistsDatafile("CombatHUD")) {
        try {
          EnablePlayer = Interface.Oxide.DataFileSystem.GetFile("CombatHUD").ReadObject<List<ulong>>();
        } catch {
          EnablePlayer = new List<ulong>();
        }
      } else {
        EnablePlayer = new List<ulong>();
      }

      Puts("EnabledPlayer Count -> " + EnablePlayer.Count.ToString());

      // close timer
      int countdown = 10;
      timer.Every(1f, () => {
        OnCloseTimerEvent();
      });

    }

    void Unload() { Interface.Oxide.DataFileSystem.GetFile("CombatHUD").WriteObject(EnablePlayer); }

    void OnEntityTakeDamage(BaseCombatEntity victim, HitInfo info) {
      if (info == null || info.InitiatorPlayer == null || victim == null) return;

      BasePlayer player = info.InitiatorPlayer;
      if (!player.IsConnected || !player.userID.IsSteamId()) return;
      if (!EnablePlayer.Contains(player.userID)) return;
      // SendMsg(player, victim.ShortPrefabName);
      if (victim.ShortPrefabName.Contains("_corpse") || victim.ShortPrefabName.Contains(".corpse")) return;
      bool victimIsPlayer = (victim.ToPlayer() && !victim.IsNpc);;

      // get damage
      float damage = info.damageTypes.Total();
      string damageText = damage.ToString("F2").TrimEnd('0').TrimEnd('.');

      string damageIcon;
      if (!DamageTypeImages.TryGetValue(info.damageTypes.GetMajorityDamageType().ToString(), out damageIcon)) {
        damageIcon = "assets/icons/weapon.png"; ;
      }

      // get entity health
      float health = Math.Max(victim.health - damage, 0);
      string healthText = !victimIsPlayer ? health.ToString("F2").TrimEnd('0').TrimEnd('.') : "???";
      float maxHealth = victim.MaxHealth();
      //string maxHealthText = maxHealth.ToString("F2").TrimEnd('0').TrimEnd('.');

      float distance = info.ProjectileDistance;
      string distanceText = distance.ToString("F2").TrimEnd('0').TrimEnd('.');

      string hitName = info.boneName != "N/A" ? FirstUpper(info.boneName) : "Body";

      double appedGuage = !victimIsPlayer ? (diffGuage * (Math.Max(health, 0) / maxHealth)) : 0;
      double guage = minGuage + appedGuage;

      // make ui string
      var filledTemplate = TEMPLATE_BASE
        .Replace("{damage_text}", damageText)
        .Replace("{damage_icon}", damageIcon)
        .Replace("{distance_text}", distanceText)
        .Replace("{health_text}", healthText)
        .Replace("{info_text}", hitName)
        .Replace("{target_text}", GetVictimName(victim) + " - " + ((victim.net != null) ? victim.net.ID.ToString() : 0u.ToString()))
        .Replace("{health_guage}", guage.ToString("F3"))
        .Replace("{guage_alpha}", (appedGuage == 0 ? 0 : guageAlpha).ToString("F2"));

      // apply to ui
      CuiHelper.DestroyUi(player, "CH-Hud");
      CuiHelper.AddUi(player, filledTemplate);

      // append timer
      long currentTicks = Convert.ToInt64(Math.Floor(DateTime.Now.Ticks / 10000.0));
      CloseTimes[player.userID] = currentTicks;
    }

    #endregion

    #region timer

    void OnCloseTimerEvent() {
      List<ulong> keys = new List<ulong>(CloseTimes.Keys);
      long currentTicks = Convert.ToInt64(Math.Floor(DateTime.Now.Ticks / 10000.0));
      foreach (ulong userId in keys) {
        long ticks = CloseTimes[userId];

        if (currentTicks - ticks < 8000) continue;
        CloseTimes.Remove(userId);


        foreach (var activePlayer in BasePlayer.activePlayerList) {
          if ((activePlayer as BasePlayer).userID == userId) {
            CuiHelper.DestroyUi(activePlayer, "CH-Hud");
            break;
          }
        }
      }
    }

    #endregion
  }
}
