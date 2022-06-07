using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace flanne.UI
{
    public class ToggleGroupMenu<T> : MonoBehaviour where T : ScriptableObject
    {
        public event EventHandler<T> ConfirmEvent;

        public T toggledData
        {
            get
            {
                Toggle toggle = this.toggleGroup.ActiveToggles().FirstOrDefault<Toggle>();
                if (toggle == null)
                {
                    return default(T);
                }
                return toggle.GetComponent<DataUIBinding<T>>().data;
            }
        }

        private void OnPointerEnter(int index)
        {
            this.description.data = this.entries[index].data;
            SoundEffectSO soundEffectSO = this.hoverSFX;
            if (soundEffectSO == null)
            {
                return;
            }
            soundEffectSO.Play(null);
        }

        private void OnPointerExit()
        {
            this.description.data = this.toggledData;
        }

        private void OnToggleChanged(int index)
        {
            if (this.saveLastSelectionToPlayerPrefs)
            {
                PlayerPrefs.SetInt(this.playerPrefsKey, index);
            }
            this.description.data = this.toggledData;
            if (this.playerInput != null && this.playerInput.currentControlScheme == "Gamepad" && this.toggles[index].isOn && this._currIndex == index)
            {
                this.OnConfirmClicked();
            }
            if (this._currIndex != index)
            {
                this._currIndex = index;
                SoundEffectSO soundEffectSO = this.clickSFX;
                if (soundEffectSO == null)
                {
                    return;
                }
                soundEffectSO.Play(null);
            }
        }

        public void OnConfirmClicked()
        {
            EventHandler<T> confirmEvent = this.ConfirmEvent;
            if (confirmEvent == null)
            {
                return;
            }
            confirmEvent(this, this.toggledData);
        }

        private void Start()
        {
            this.description = this.descriptionObj.GetComponent<DataUIBinding<T>>();
            this.entries = new List<DataUIBinding<T>>();
            this.pointerDetectors = new List<PointerDetector>();
            for (int i = 0; i < this.toggles.Length; i++)
            {
                int index = i;
                PointerDetector component = this.toggles[i].GetComponent<PointerDetector>();
                component.onEnter.AddListener(delegate ()
                {
                    this.OnPointerEnter(index);
                });
                component.onExit.AddListener(new UnityAction(this.OnPointerExit));
                this.pointerDetectors.Add(component);
                DataUIBinding<T> component2 = this.toggles[i].GetComponent<DataUIBinding<T>>();
                this.entries.Add(component2);
            }
            int num;
            if (this.saveLastSelectionToPlayerPrefs)
            {
                num = PlayerPrefs.GetInt(this.playerPrefsKey, 0);
            }
            else
            {
                num = 0;
            }
            this.description.data = this.entries[num].data;
            this.toggles[num].isOn = true;
            this._currIndex = num;
            for (int j = 0; j < this.toggles.Length; j++)
            {
                int index = j;
                this.toggles[j].onValueChanged.AddListener(delegate (bool < p0 >)

                {
                    this.OnToggleChanged(index);
                });
        }
    }

    public void SetData(int index, T data)
    {
        this.entries[index].data = data;
        this.description.data = this.toggledData;
    }

    public void RefreshDescription()
    {
        this.description.data = this.toggledData;
    }

    [SerializeField]
    private Toggle[] toggles;

    [SerializeField]
    private ToggleGroup toggleGroup;

    [SerializeField]
    private GameObject descriptionObj;

    [SerializeField]
    private SoundEffectSO hoverSFX;

    [SerializeField]
    private SoundEffectSO clickSFX;

    [SerializeField]
    private bool saveLastSelectionToPlayerPrefs;

    [SerializeField]
    private string playerPrefsKey;

    [SerializeField]
    private PlayerInput playerInput;

    private const string gamepadScheme = "Gamepad";

    private const string mouseScheme = "Keyboard&Mouse";

    private DataUIBinding<T> description;

    private List<DataUIBinding<T>> entries;

    private List<PointerDetector> pointerDetectors;

    private int _currIndex;
}
}
