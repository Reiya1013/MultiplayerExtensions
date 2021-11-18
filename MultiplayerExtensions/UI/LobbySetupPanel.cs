using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using MultiplayerExtensions.Extensions;
using Polyglot;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MultiplayerExtensions.UI
{
    class LobbySetupPanel : BSMLResourceViewController
    {
        public override string ResourceName => "MultiplayerExtensions.UI.LobbySetupPanel.bsml";

        private ExtendedSessionManager _sessionManager = null!;
        private LobbyPlayerPermissionsModel _permissionsModel = null!;

        CurvedTextMeshPro? modifierText;

        private GameObject ScreenSystem;

        [Inject]
        internal void Inject(IMultiplayerSessionManager sessionManager, LobbyPlayerPermissionsModel permissionsModel, LobbySetupViewController lobbyViewController)
        {
            this._sessionManager = (sessionManager as ExtendedSessionManager)!;
            this._permissionsModel = permissionsModel;
            base.DidActivate(true, false, true);

            lobbyViewController.didActivateEvent += OnActivate;
        }

        #region UIComponents

        [UIComponent("HostPickToggle")]
        public ToggleSetting hostPickToggle = null!;

        [UIComponent("DefaultHUDToggle")]
        public ToggleSetting defaultHUDToggle = null!;

        [UIComponent("HologramToggle")]
        public ToggleSetting hologramToggle = null!;

        [UIComponent("LagReducerToggle")]
        public ToggleSetting lagReducerToggle = null!;

        [UIComponent("MissLightingToggle")]
        public ToggleSetting missLightingToggle = null!;

        [UIComponent("MenuPositionUpToggle")]
        public ToggleSetting menuPositionUpToggle = null!;

        [UIComponent("MenuScaleUpToggle")]
        public ToggleSetting menuScaleUpToggle = null!;

        [UIComponent("DownloadProgressText")]
        public FormattableText downloadProgressText = null!;
        #endregion

        #region UIValues

        [UIValue("DefaultHUD")]
        public bool DefaultHUD
        {
            get => Plugin.Config.SingleplayerHUD;
            set { Plugin.Config.SingleplayerHUD = value; }
        }

        [UIValue("Hologram")]
        public bool Hologram
        {
            get => Plugin.Config.Hologram;
            set { Plugin.Config.Hologram = value; }
        }

        [UIValue("LagReducer")]
        public bool LagReducer
        {
            get => Plugin.Config.LagReducer;
            set { Plugin.Config.LagReducer = value; }
        }

        [UIValue("MissLighting")]
        public bool MissLighting
        {
            get => Plugin.Config.MissLighting;
            set { Plugin.Config.MissLighting = value; }
        }

        [UIValue("DownloadProgress")]
        public string DownloadProgress
        {
            get => downloadProgressText.text;
            set { downloadProgressText.text = value; }
        }

        private bool MenuPositionUpVal;
        [UIValue("MenuPositionUp")]
        public bool MenuPositionUp
        {
            get => MenuPositionUpVal;
            set { MenuPositionUpVal = value; }
        }

        private bool MenuScaleUpVal;
        [UIValue("MenuScaleUp")]
        public bool MenuScaleUp
        {
            get => MenuScaleUpVal;
            set { MenuScaleUpVal = value; }
        }
        #endregion

        #region UIActions

        [UIAction("SetDefaultHUD")]
        public void SetDefaultHUD(bool value)
        {
            DefaultHUD = value;
            defaultHUDToggle.Value = value;

            //VerticalHUD = VerticalHUD || value;
            //verticalHUDToggle.Value = VerticalHUD || value;
        }

        [UIAction("SetHologram")]
        public void SetHologram(bool value)
        {
            Hologram = value;
            hologramToggle.Value = value;
        }

        [UIAction("SetLagReducer")]
        public void SetLagReducer(bool value)
        {
            LagReducer = value;
            lagReducerToggle.Value = value;
        }

        [UIAction("SetMissLighting")]
        public void SetMissLighting(bool value)
        {
            MissLighting = value;
            missLightingToggle.Value = value;
        }

        [UIAction("SetMenuPositionUp")]
        public void SetMenuPositionUp(bool value)
        {
            MenuPositionUp = value;
            menuPositionUpToggle.Value = value;
            SetScreenSystem();
        }

        [UIAction("SetMenuScaleUp")]
        public void SetMenuScaleUp(bool value)
        {
            MenuScaleUp = value;
            menuScaleUpToggle.Value = value;
            SetScreenSystem();
        }
        #endregion

        private void OnActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                //Transform spectatorText = transform.Find("Wrapper").Find("SpectatorModeWarningText");
                //spectatorText.position = new Vector3(spectatorText.position.x, 0.25f, spectatorText.position.z);

                if (!ScreenSystem)
                {
                    ScreenSystem = GameObject.Find("MenuCore/UI/ScreenSystem");
                }

                SetScreenSystem();

            }
        }

        private void SetScreenSystem()
        {
            if (ScreenSystem)
            {
                ScreenSystem.transform.position = new Vector3(0.0f, MenuPositionUp ? 0.5f : 0.1f, 0.1f);
                if (MenuScaleUp)
                {
                    ScreenSystem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                else
                {
                    ScreenSystem.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                }
            }
        }
    }
}
